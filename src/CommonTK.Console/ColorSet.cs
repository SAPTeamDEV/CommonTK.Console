using System;

using static SAPTeam.CommonTK.Context;

namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Represent a data type for toring the Console color pairs.
    /// </summary>
    public struct ColorSet
    {
        private static ColorSet colors = Default;

        /// <summary>
        /// Gets the console background color.
        /// </summary>
        public ConsoleColor Back { get; }

        /// <summary>
        /// Gets the console foreground color.
        /// </summary>
        public ConsoleColor Fore { get; }

        /// <summary>
        /// Gets the default color set for ScreenMessage.
        /// </summary>
        public static ColorSet ScreenMesage => new ColorSet(ConsoleColor.White, ConsoleColor.Black);

        /// <summary>
        /// Gets the inverted color set for ScreenMessage that suitable for white backgrounds.
        /// </summary>
        public static ColorSet InvertedScreenMesage => new ColorSet(ConsoleColor.Black, ConsoleColor.White);

        /// <summary>
        /// Gets or Sets the Global Color Set.
        /// <para>
        /// Property setter Action Group: global.color
        /// </para>
        /// </summary>
        public static ColorSet Current
        {
            get => colors;
            set
            {
                QueryGroup(ActionGroup(ActionScope.Global, "color"));
                colors = value;
                Utils.ResetColor();
            }
        }

        /// <summary>
        /// Gets the default Color Set.
        /// </summary>
        public static ColorSet Default => new ColorSet(ConsoleColor.Black, ConsoleColor.Gray);

        /// <summary>
        /// Initializes a new instance of <see cref="ColorSet"/>.
        /// </summary>
        /// <param name="back">
        /// The background color of console.
        /// </param>
        /// <param name="fore">
        /// The foreground color of console.
        /// </param>
        public ColorSet(ConsoleColor back, ConsoleColor fore)
        {
            Back = back;
            Fore = fore;
        }

        /// <summary>
        /// Changes the current console color to the values of this instance.
        /// </summary>
        public void ChangeColor()
        {
            System.Console.BackgroundColor = Back;
            System.Console.ForegroundColor = Fore;
        }
    }
}