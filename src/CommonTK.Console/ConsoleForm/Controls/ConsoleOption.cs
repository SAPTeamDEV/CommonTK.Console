namespace SAPTeam.CommonTK.Console.ConsoleForm.Controls
{
    /// <summary>
    /// Represents the textual component that can be selected by user.
    /// </summary>
    public class ConsoleOption : Control, ISelectableControl
    {
        /// <inheritdoc/>
        public override bool Selectable => true;

        /// <summary>
        /// Gets the identifier this control.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets the section to which this control belongs.
        /// </summary>
        public ConsoleSection Section;

        /// <summary>
        /// Initializes a new instance of <see cref="ConsoleOption"/>.
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
        /// <param name="section">
        /// The section to which this control belongs.
        /// </param>
        public ConsoleOption(Interface parent, int line, string text, ConsoleSection section) : base(parent, line, text)
        {
            Section = section;
        }

        /// <inheritdoc/>
        public void Select()
        {
            Focus();
            Clear();
            Parent.ColorSchema.ChangeColor();
            Update();
            Utils.ResetColor();
        }

        /// <inheritdoc/>
        public override void Write()
        {
            string text = Text;
            if (Section != null) text = "   " + text;
            Write(text);
        }
    }
}