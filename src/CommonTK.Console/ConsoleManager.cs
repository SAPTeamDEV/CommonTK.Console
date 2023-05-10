using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

using static PInvoke.Kernel32;
using static PInvoke.User32;
using static SAPTeam.CommonTK.Context;

using SAPTeam.Zily;
using Serilog;

namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Represent methods for control Application consoles.
    /// </summary>
    public static class ConsoleManager
    {
        [DllImport("user32.dll")] private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32.dll")] private static extern bool SetFocus(IntPtr hWnd);

        /// <summary>
        /// Gets the name of the pipe server.
        /// </summary>
        public const string PipeServerName = "pipe.console";

        private static void DisableCloseButton()
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), (int)SysCommands.SC_CLOSE, (int)MenuItemFlags.MF_BYCOMMAND);
        }

        /// <summary>
        /// Gets or Sets the mechanism that the console window processes the texts.
        /// </summary>
        public static ConsoleType Type { get; set; }

        /// <summary>
        /// Gets console Launching method.
        /// </summary>
        public static ConsoleLaunchMode Mode { get; private set; }

        /// <summary>
        /// Gets the named pipe that used for communicating with the console client.
        /// </summary>
        public static ZilyPipeServerStream Pipe { get; private set; }

        /// <summary>
        /// Checks if The Application has Console.
        /// </summary>
        public static bool HasConsole => GetConsoleWindow() != IntPtr.Zero;

        /// <summary>
        /// Shows up existing Console Window, if Console Window not found then creates a new Console.
        /// <para>
        /// Method Action Group: process.console
        /// </para>
        /// </summary>
        /// <param name="mode">
        /// Determines the method that used for launching a new Console. if there is an existing Console, this value is ignored.
        /// </param>
        /// <param name="canClose">
        /// Determines that the console window can be closed without handling or Needs to be closed in an expected way.
        /// </param>
        public static void ShowConsole(ConsoleLaunchMode mode, bool canClose = false)
        {
            QueryGroup(ActionGroup(ActionScope.Process, "console"));
            if (!HasConsole)
            {
                switch (mode)
                {
                    case ConsoleLaunchMode.Allocation:
                        AllocateConsole();
                        break;
                    case ConsoleLaunchMode.AttachParent:
                        AttachToParent();
                        break;
                    case ConsoleLaunchMode.AttachProcess:
                        AttachProcess(CreateConsole());
                        break;
                    case ConsoleLaunchMode.CreateClient:
                        CreateClient();
                        break;
                    case ConsoleLaunchMode.AttachClient:
                        AttachClient();
                        break;
                }

                Mode = mode;
                if (mode != ConsoleLaunchMode.AttachClient && mode != ConsoleLaunchMode.CreateClient)
                {
                    System.Console.Title = AppDomain.CurrentDomain.FriendlyName;
                }

                if (!canClose)
                {
                    DisableCloseButton();
                }
            }
            else
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, WindowShowStyle.SW_SHOW);
                SetForegroundWindow(handle);
                SetFocus(handle);
            }
        }

        private static void AttachClient()
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "ConClient.exe",
                Arguments = "-s",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            });
            Thread.Sleep(1000);
            Type = ConsoleType.Native;
            System.Console.SetIn(process.StandardOutput);
            System.Console.SetOut(process.StandardInput);
            // System.Console.Clear();
        }

        /// <summary>
        /// Hides or Releases Application Console.
        /// <para>
        /// Method Action Group: process.console
        /// </para>
        /// </summary>
        /// <param name="release">
        /// Determines that current Console should be closed or just hides it.
        /// </param>
        public static void HideConsole(bool release = true)
        {
            QueryGroup(ActionGroup(ActionScope.Process, "console"));
            if (release)
            {
                ReleaseConsole();
            }
            else
            {
                ShowWindow(GetConsoleWindow(), WindowShowStyle.SW_HIDE);
            }
        }

        /// <summary>
        /// Creates a new console instance if the process is not attached to a console already.
        /// </summary>
        private static void AllocateConsole()
        {
            if (!HasConsole)
            {
                Type = ConsoleType.External;
                AllocConsole();
                InvalidateOutAndError();
            }
        }

        /// <summary> 
        /// If the process has a console attached to it, it will be detached and no longer visible. Writing to the System.Console is still possible, but no output will be shown.
        /// </summary>
        private static void ReleaseConsole()
        {
            if (HasConsole)
            {
                SetOutAndErrorNull();
                FreeConsole();
            }
        }

        /// <summary>
        /// Takes control of the Caller's Console.
        /// </summary>
        private static void AttachToParent()
        {
            if (!HasConsole)
            {
                AttachConsole(-1);
            }
        }

        /// <summary>
        /// Creates a new console process.
        /// </summary>
        /// <param name="name">
        /// The name of the process.
        /// </param>
        /// <param name="args">
        /// The arguments that will be passed to the process.
        /// </param>
        /// <returns>A <see cref="Process"/> object of the new console.</returns>
        private static Process CreateConsole(string name = "cmd.exe", string args = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = name,
                Arguments = args
            };
            Process con = Process.Start(startInfo);
            return con;
        }

        /// <summary>
        /// Attaches to given <see cref="Process"/> Console and kills that process.
        /// </summary>
        /// <param name="process">A Process that have Console.</param>
        private static void AttachProcess(Process process)
        {
            Thread.Sleep(1000);
            Type = ConsoleType.Native;
            AttachConsole(process.Id);
            process.Kill();
            System.Console.Clear();
        }

        private static void CreateClient()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#endif
                .WriteTo.File("Zily.log")
                .CreateLogger();

            var server = new NamedPipeServerStream(PipeServerName, PipeDirection.InOut);
            Pipe = new ZilyPipeServerStream(server);

            string args = $"-p {PipeServerName} --zily";
#if DEBUG
            args += " -v";
#else
            args += " -q";
#endif

            var process = CreateConsole("ConClient.exe", args);

            Pipe.Accept();

            var tw = new ZilyTextWriter(Pipe);
            System.Console.SetOut(tw);
        }

        private static void ForceSet(ConsoleField field, object obj)
        {
            Type type = typeof(System.Console);
            string pField = "";
            switch (field)
            {
                case ConsoleField.In:
                    pField = "s_in";
                    break;
                case ConsoleField.Out:
                    pField = "s_out";
                    break;
                case ConsoleField.Error:
                    pField = "s_error";
                    break;
            }
            FieldInfo fControl = type.GetField(pField, BindingFlags.Static | BindingFlags.NonPublic);
            Debug.Assert(fControl != null);
            fControl.SetValue(obj, obj);
        }

        private static void InvalidateOutAndError()
        {
            ForceSet(ConsoleField.Out, null);
            ForceSet(ConsoleField.Error, null);
        }

        private static void SetOutAndErrorNull()
        {
            System.Console.SetOut(TextWriter.Null);
            System.Console.SetError(TextWriter.Null);
        }
    }
}