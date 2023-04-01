using System;
#if !NETSTANDARD && !NETCOREAPP
using System.Windows.Forms;
#endif

using SAPTeam.CommonTK.Contexts;

namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Provides various functions for controlling and writing data to Console.
    /// </summary>
    public static partial class Utils
    {
        /// <summary>
        /// Writes <paramref name="text"/> Message to the Console
        /// or shows MessageBox depending on the <see cref="Context.Interface"/>.
        /// </summary>
        /// <param name="text">
        /// The message text.
        /// </param>
        /// <param name="newLine">
        /// Determines that this method will add a new line character in the end of <paramref name="text"/>.
        /// </param>
        public static void Echo(string text = "", bool newLine = true)
        {
            if (text == null)
            {
                text = "";
            }

            switch (Context.Interface)
            {
                case InteractInterface.Console:
                    {
                        if (text.Length > 0 && Context.Current.HasContext<DisposableWriter>() && !Context.Current.HasContext<RedirectConsole>())
                        {
                            Context.Current.GetContext<DisposableWriter>().AddCoords(System.Console.CursorTop, System.Console.CursorLeft + text.Length);
                        }

                        if (newLine)
                        {
                            System.Console.WriteLine(text);
                            if (Context.Current.HasContext<RedirectConsole>())
                            {
                                Context.Current.GetContext<RedirectConsole>().Line++;
                            }
                        }
                        else
                        {
                            System.Console.Write(text);
                        }
                    }
                    break;

#if !NETSTANDARD && !NETCOREAPP
                case InteractInterface.UI:
                        MessageBox.Show(text, AppDomain.CurrentDomain.FriendlyName);
                        break;
#endif

                default:
                        throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Writes <paramref name="colorizedString"/> to the Console. if the <see cref="Context.Interface"/> is set to ui, shows a simple MessageBox.
        /// </summary>
        /// <param name="colorizedString">
        /// The colorized string.
        /// </param>
        /// <param name="newLine">
        /// Determines that this method will add a new line character in the end of <paramref name="colorizedString"/>.
        /// </param>
        public static void Echo(Colorize colorizedString, bool newLine = true)
        {
            if (Context.Interface == InteractInterface.Console)
            {
                foreach (var (text, color) in colorizedString.ColorizedString)
                {
                    if (color != null)
                    {
                        System.Console.ForegroundColor = (ConsoleColor)color;
                    }
                    Echo(text, false);
                    ResetColor();
                }
                Echo(newLine: newLine);
            }
            else
            {
                Echo(colorizedString.Text);
            }
        }

        /// <summary>
        /// Gets the cursor vertical coordinate when using <see cref="RedirectConsole"/>.
        /// </summary>
        /// <returns></returns>
        internal static int GetLine()
        {
            return Context.Current.HasContext<RedirectConsole>() ? Context.Current.GetContext<RedirectConsole>().Line + System.Console.CursorTop : System.Console.CursorTop;
        }

        /// <summary>
        /// Clears Previous or Current line contents and set cursor to Beginning of that line.
        /// </summary>
        /// <param name="inLineClear">
        /// Determines that which line is cleared. if <see langword="true"/> current line is cleared, otherwise The previous line is cleared.
        /// </param>
        /// <param name="length">
        /// Length of characters to be cleared, if <see langword="null"/> the entire line is cleared.
        /// </param>
        public static void ClearLine(bool inLineClear = false, int? length = null)
        {
            System.Console.SetCursorPosition(0, System.Console.CursorTop - (inLineClear ? 0 : 1));
            System.Console.Write(new string(' ', (int)(length == null ? System.Console.BufferWidth : length)));
            System.Console.SetCursorPosition(0, Math.Max(0, System.Console.CursorTop - (ConsoleManager.Type == ConsoleType.Native ? 0 : 1)));
        }

        internal static void ClearInLine()
        {
            ClearLine(true);
        }
    }
}