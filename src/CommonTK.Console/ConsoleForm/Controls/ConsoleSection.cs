namespace SAPTeam.CommonTK.Console.ConsoleForm.Controls
{
    /// <summary>
    /// Represents the textual component that used for aligning the ConsoleOptions.
    /// </summary>
    public class ConsoleSection : Control, IControl
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConsoleSection"/>.
        /// </summary>
        /// <param name="parent">
        /// The parent <see cref="Interface"/>.
        /// </param>
        /// <param name="line">
        /// The vertical position of this control.
        /// </param>
        /// <param name="text">
        /// The text property of this control.
        /// </param>
        public ConsoleSection(Interface parent, int line, string text) : base(parent, line, text) { }

        /// <inheritdoc/>
        public override bool Selectable => false;
    }
}