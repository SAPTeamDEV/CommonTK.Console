using System;

using SAPTeam.CommonTK.Console.ConsoleForm.Controls;

namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    public partial class Interface
    {
        /// <summary>
        /// Occurs when the console ui creates or refreshes the form elements.
        /// </summary>
        public event Action Title;

        /// <summary>
        /// Occurs when the user selects a selectable element.
        /// </summary>
        public event Action<ConsoleOption> OnEnter;

        /// <summary>
        /// Occurs when the console ui is about to start.
        /// </summary>
        public event Action OnStart;

        /// <summary>
        /// Occurs when the form is closing.
        /// </summary>
        public event Action OnClose;

        /// <summary>
        /// Occurs when the user pressed a key.
        /// </summary>
        public event Action<ConsoleKeyInfo> KeyPressed;

        /// <summary>
        /// Clears all event subscribers.
        /// </summary>
        public void ClearEvents()
        {
            Title = null;
            OnEnter = null;
            OnStart = null;
            OnClose = null;
            KeyPressed = null;
        }

        private void RaiseStart()
        {
            Action start = OnStart;
            start?.Invoke();
        }

        private void RaiseClose()
        {
            Action close = OnClose;
            close?.Invoke();
        }

        private void RaiseTitle()
        {
            Action title = Title;
            title?.Invoke();
        }

        private void RaiseKeyPressed(ConsoleKeyInfo key)
        {
            Action<ConsoleKeyInfo> keyPressed = KeyPressed;
            keyPressed?.Invoke(key);
        }

        private void RaiseEnter(ConsoleOption option)
        {
            Action<ConsoleOption> enter = OnEnter;
            enter?.Invoke(option);
        }
    }
}