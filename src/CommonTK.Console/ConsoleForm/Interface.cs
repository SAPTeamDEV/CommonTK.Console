using System;
using System.Runtime.InteropServices;

using SAPTeam.CommonTK.Console.ConsoleForm.Controls;
using SAPTeam.CommonTK.Contexts;

namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    /// <summary>
    /// Generic class for creating selectable elements through the Console Interface.
    /// </summary>
    public partial class Interface
    {
        private readonly int titleLine;
        private int bufferWidth;
        private DisposableWriter disposableWriter;
        private readonly Form rootForm;
        private Form activeForm;

        private int Index
        {
            get => activeForm.Current;
            set
            {
                int newIndex = Math.Max(Math.Min(value, activeForm.Last), activeForm.First);
                if (newIndex == activeForm.Current) return;
                bool isSum = newIndex > activeForm.Current;
                activeForm.Container[activeForm.Current].Update();
                activeForm.Current = newIndex;
                if (activeForm.Container[newIndex] is ISelectableControl opClass)
                {
                    opClass.Select();
                }
                else
                {
                    if (isSum) Index++;
                    else Index--;
                }
            }
        }

        /// <summary>
        /// Gets the corresponding console element of the active form.
        /// </summary>
        /// <param name="index">
        /// The vertical coordinate of the component.
        /// </param>
        /// <returns>
        /// The corresponding <see cref="IControl"/> component.
        /// </returns>
        public IControl this[int index] => activeForm.Container[index];

        /// <summary>
        /// Gets the space between cursor, top and bottom of console window.
        /// </summary>
        public int Spacing => rootForm.First - titleLine;

        /// <summary>
        /// Gets or Sets the color schema that used by components.
        /// </summary>
        public ColorSet ColorSchema { get; set; } = new ColorSet(ConsoleColor.White, ConsoleColor.Black);

        private bool hasExited;

        /// <summary>
        /// Initializes the Console User Interface.
        /// </summary>
        /// <param name="form">
        /// The root console form.
        /// </param>
        public Interface(Form form)
        {
            rootForm = activeForm = form;
            if (rootForm.UseDisposableWriter)
            {
                disposableWriter = new DisposableWriter(true);
            }
            else
            {
                disposableWriter = null;
            }
            bufferWidth = System.Console.BufferWidth;
            titleLine = Utils.GetLine();
        }

        /// <summary>
        /// Rebuilds the current form elements.
        /// </summary>
        public void Refresh()
        {
            if (disposableWriter != null)
            {
                disposableWriter.Clear();
            }
            else
            {
                System.Console.Clear();
            }
            RaiseTitle();
            foreach (var item in activeForm.Container.Values)
            {
                item.Write();
                Utils.Echo();
            }
            if (activeForm.Container.Count > 0 && activeForm.Container[Index] is ConsoleOption opClass) opClass.Select();
        }

        /// <summary>
        /// Starts the console ui.
        /// <para>
        /// The caller thread blocked until the <see cref="Close()"/> is called by pressing ESC, the <see cref="Form"/> or another thread.
        /// </para>
        /// </summary>
        public void Start()
        {
            Refresh();
            if (activeForm.FocusToTop && disposableWriter == null && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                System.Console.WindowTop = titleLine;
            }
            System.Console.CursorVisible = false;
            RaiseStart();
            while (!hasExited)
            {
                ConsoleKeyInfo keyInfo = System.Console.ReadKey(true);
                if (bufferWidth != System.Console.BufferWidth || keyInfo.Key == ConsoleKey.F5)
                {
                    Refresh();
                    bufferWidth = System.Console.BufferWidth;
                }
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    if (!activeForm.IsClosable)
                    {
                        ScreenMessage("You can't leave this page.");
                        continue;
                    }
                    if (activeForm == rootForm)
                    {
                        Close();
                        break;
                    }
                    else
                    {
                        CloseSubForm();
                        continue;
                    }
                }
                if (rootForm.Container.Count > 0)
                {
                    if (keyInfo.Key == ConsoleKey.Enter) RaiseEnter((ConsoleOption)rootForm.Container[Index]);
                    else if (keyInfo.Key == ConsoleKey.UpArrow) Index--;
                    else if (keyInfo.Key == ConsoleKey.DownArrow) Index++;
                }
                RaiseKeyPressed(keyInfo);
            }
        }

        /// <summary>
        /// Closes the <see cref="Form"/> and then closes the Console ui and releases the thread.
        /// </summary>
        public void Close()
        {
            this[Index].Update();
            RaiseClose();
            if (disposableWriter != null)
            {
                disposableWriter.Dispose();
                System.Console.CursorTop = titleLine;
            }
            else
            {
                System.Console.CursorTop = activeForm.Last + 1;
            }
            hasExited = true;
            System.Console.CursorVisible = true;
        }
    }
}