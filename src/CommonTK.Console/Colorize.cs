using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
#if NET461
using IEnumerable.Append;
#endif

namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Stores the formatted multi color string.
    /// </summary>
    public struct Colorize
    {
        /// <summary>
        /// Gets the untouched string.
        /// </summary>
        public string Text { get; }

        private readonly ConsoleColor[] colors;
        private readonly string clearText;

        /// <summary>
        /// Gets the divided tuple pair of each piece of text and it's color.
        /// </summary>
        public IEnumerable<(string text, ConsoleColor? color)> ColorizedString { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Colorize"/>.
        /// </summary>
        /// <param name="text">
        /// The well-formatted string.
        /// <para>
        /// See github page for more details about text formatting.
        /// </para>
        /// </param>
        /// <param name="colors">
        /// The console colors that will be respectively applied to the formatted parts of <paramref name="text"/>.
        /// </param>
        public Colorize(string text, params ConsoleColor[] colors)
        {
            Text = text;
            this.colors = colors;
            clearText = null;

            var pieces = Regex.Split(text, @"(\[[^\]]*\])");
            ColorizedString = new (string text, ConsoleColor? color)[pieces.Length];

            int ci = 0;

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];
                bool formatted = false;

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    formatted = true;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                if (formatted && colors.Length > 0)
                {
                    ColorizedString = ColorizedString.Append((piece, colors[Math.Min(ci, colors.Length - 1)])) as IEnumerable<(string, ConsoleColor?)>;
                    ci++;
                }
                else
                {
                    ColorizedString = (IEnumerable<(string, ConsoleColor?)>)ColorizedString.Append((piece, null));
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this;
        }

        /// <summary>
        /// Appends <paramref name="y"/> string to <paramref name="x"/>.
        /// </summary>
        /// <param name="x">
        /// An instance of <see cref="Colorize"/>.
        /// </param>
        /// <param name="y">
        /// A <see cref="string"/>.
        /// </param>
        /// <returns></returns>
        public static Colorize operator +(Colorize x, string y)
        {
            return new Colorize(x.Text + y, x.colors);
        }

        /// <summary>
        /// Combines two <see cref="Colorize"/> objects.
        /// </summary>
        /// <param name="x">
        /// An instance of <see cref="Colorize"/>.
        /// </param>
        /// <param name="y">
        /// An instance of <see cref="Colorize"/>.
        /// </param>
        /// <returns></returns>
        public static Colorize operator +(Colorize x, Colorize y)
        {
            ConsoleColor[] colors = new ConsoleColor[x.colors.Length + y.colors.Length];
            int i = 0;
            foreach (var color in x.colors.Concat(y.colors))
            {
                colors[i] = color;
                i++;
            }
            return new Colorize(x.Text + y.Text, colors);
        }

        /// <summary>
        /// Implicitly casts an instance of <see cref="Colorize"/> to the <see cref="string"/>.
        /// </summary>
        /// <param name="x">
        /// An instance of <see cref="Colorize"/>.
        /// </param>
        public static implicit operator string(Colorize x)
        {
            string clearedText = "";

            if (x.clearText == null)
            {
                foreach (var (text, color) in x.ColorizedString)
                {
                    clearedText += text;
                }
            }
            return clearedText;
        }
    }
}