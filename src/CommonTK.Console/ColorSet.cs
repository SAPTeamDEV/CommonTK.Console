﻿using System;

namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Represent a data type for toring the Console color pairs.
    /// </summary>
    public struct ColorSet
    {
        private static ColorSet colors = new ColorSet();

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
        /// </summary>
        public static ColorSet Current { get => colors; set { colors = value; Utils.ResetColor(); } }

        /// <summary>
        /// Initializes a new instance of <see cref="ColorSet"/>.
        /// </summary>
        /// <param name="back">
        /// The background color of console.
        /// </param>
        /// <param name="fore">
        /// The foreground color of console.
        /// </param>
        public ColorSet(ConsoleColor back = ConsoleColor.Black, ConsoleColor fore = ConsoleColor.Gray)
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