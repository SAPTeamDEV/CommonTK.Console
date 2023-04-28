using System;

using SAPTeam.CommonTK.Console;

namespace SAPTeam.CommonTK.Contexts
{
    /// <summary>
    /// Represents the <see cref="Context"/> that used for creating and releasing the console windows for the Desktop Applications.
    /// <para>
    /// Context locked Action Groups: global.interface, process.console
    /// </para>
    /// </summary>
    public class ConsoleWindow : Context
    {
        ConsoleLaunchMode mode;
        bool canClose;
        bool release;

        /// <inheritdoc/>
        public override string[] Groups => new string[]
        {
            ActionGroup(ActionScope.Global, "interface"),
            ActionGroup(ActionScope.Process, "console")
        };

        /// <summary>
        /// Initializes a new instance of <see cref="ConsoleWindow"/>.
        /// </summary>
        /// <param name="mode">
        /// Determines the console creating mode.
        /// </param>
        /// <param name="canClose">
        /// Determines that the console window can be closed unexpectedly or Must be closed automatically when object is disposed.
        /// </param>
        /// <param name="release">
        /// Determines that current Console should be closed or just hides it.
        /// </param>
        public ConsoleWindow(ConsoleLaunchMode mode = ConsoleLaunchMode.Allocation, bool canClose = false, bool release = true)
        {
            this.mode = mode;
            this.canClose = canClose;
            this.release = release;

            if (ConsoleManager.HasConsole && Interface == InteractInterface.UI)
            {
                throw new InvalidOperationException("This process already has a console.");
            }

            Initialize(true);
        }

        /// <inheritdoc/>
        protected override void CreateContext()
        {
            Interface = InteractInterface.Console;
            ConsoleManager.ShowConsole(mode, canClose);
        }

        /// <inheritdoc/>
        protected override void DisposeContext()
        {
            ConsoleManager.HideConsole(release);
            Interface = InteractInterface.UI;
        }
    }
}