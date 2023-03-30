using System;
using System.Runtime.InteropServices;

namespace SAPTeam.CommonTK.Console
{
    public static partial class Utils
    {
        /// <summary>
        /// Sets the position of the cursor, regarding to the <paramref name="spacing"/> it will adjust the <see cref="System.Console.WindowTop"/>.
        /// </summary>
        /// <param name="left">
        /// X coordinate of new cursor position
        /// </param>
        /// <param name="top">
        /// Y coordinate of new cursor position
        /// </param>
        /// <param name="spacing">
        /// The space between <paramref name="top"/> and <see cref="System.Console.WindowTop"/>.
        /// </param>
        public static void SetCursor(int left, int top, int spacing)
        {
            if (top - spacing < System.Console.WindowTop && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                System.Console.WindowTop = Math.Max(0, System.Console.WindowTop - 1);
            }
            System.Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Temporarily changes the console color set.
        /// </summary>
        /// <param name="colors">
        /// The new console color set.
        /// </param>
        public static void SetColor(ColorSet colors)
        {
            System.Console.BackgroundColor = colors.Back;
            System.Console.ForegroundColor = colors.Fore;
        }

        /// <summary>
        /// Resets the console color to the <see cref="ColorSet.Current"/>.
        /// </summary>
        public static void ResetColor()
        {
            System.Console.BackgroundColor = ColorSet.Current.Back;
            System.Console.ForegroundColor = ColorSet.Current.Fore;
        }

        /// <summary>
        /// Gets the central X position of console for the <paramref name="textLength"/>.
        /// </summary>
        /// <param name="textLength">
        /// The length of text.
        /// </param>
        /// <returns>
        /// X coordinate of text.
        /// </returns>
        public static int GetCenterPosition(int textLength)
        {
            return System.Console.BufferWidth / 2 - textLength / 2;
        }
    }
}