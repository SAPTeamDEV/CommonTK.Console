namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Represents a data type for storing and computing the Coordinates of objects in console.
    /// </summary>
    public struct ConsoleCoords
    {
        /// <summary>
        /// Gets the default coordinates of ScreenMessage.
        /// </summary>
        public static ConsoleCoords ScreenMessage => new ConsoleCoords()
        {
            X = 3,
            Position = ConsolePosition.Bottom,
            IsStatic = false,
            Center = true
        };

        /// <summary>
        /// Gets or Sets the vertical coordinate of an object.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or Sets the horizontal coordinate of an object.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or Sets the horizontal length of an object.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets the relative vertical position of an object
        /// </summary>
        public ConsolePosition Position { get; set; }

        internal bool IsStatic { get; set; }
        internal bool Center { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ConsoleCoords"/>
        /// </summary>
        /// <param name="x">
        /// The vertical position.
        /// </param>
        /// <param name="y">
        /// The horizontal position.
        /// </param>
        public ConsoleCoords(int x, int y)
        {
            X = x;
            Y = y;

            Length = default;
            Position = default;
            IsStatic = true;
            Center = false;
        }

        /// <summary>
        /// Changes the console cursor position to the <see cref="X"/> and <see cref="Y"/> coordinates.
        /// </summary>
        public void Focus()
        {
            System.Console.CursorTop = ResolveX();
            if (Y != default)
            {
                System.Console.CursorLeft = Y;
            }
        }

        /// <summary>
        /// Computes the vertical coordinate according to <see cref="X"/>, <see cref="IsStatic"/> and <see cref="Position"/>.
        /// </summary>
        /// <returns>
        /// An <see langword="int"/> that contains the vertical coordinate.
        /// </returns>
        public int ResolveX()
        {
            if (IsStatic && Position == ConsolePosition.None)
            {
                return X;
            }
            else if (Position == ConsolePosition.None || Position == ConsolePosition.Top)
            {
                return System.Console.WindowTop + X;
            }
            else
            {
                return System.Console.WindowHeight - X + System.Console.WindowTop;
            }
        }
    }
}