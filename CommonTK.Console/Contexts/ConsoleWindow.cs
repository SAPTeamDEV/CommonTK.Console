using SAPTeam.CommonTK.Console;

namespace SAPTeam.CommonTK.Contexts
{
    /// <summary>
    /// Represents the <see cref="Context"/> that used for creating and releasing the console windows for the Desktop Applications.
    /// </summary>
    public class ConsoleWindow : Context
    {
        ConsoleLaunchMode mode;
        bool canClose;
        bool release;

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
        public ConsoleWindow(ConsoleLaunchMode mode = ConsoleLaunchMode.Allocation, bool canClose = false, bool release = true) : base(mode, canClose, release)
        {

        }

        /// <inheritdoc/>
        protected override void ArgsHandler(dynamic[] args)
        {
            mode = args[0];
            canClose = args[1];
            release = args[2];
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