using System;

namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    /// <summary>
    /// Represents the console ui components.
    /// </summary>
    public abstract class Control : IControl
    {
        /// <summary>
        /// Gets the parent <see cref="Interface"/>.
        /// </summary>
        protected Interface Parent { get; }

        /// <inheritdoc/>
        public int Line { get; }

        /// <inheritdoc/>
        public string Text { get; }

        /// <inheritdoc/>
        public abstract bool Selectable { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Control"/>.
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
        public Control(Interface parent, int line, string text)
        {
            Line = line;
            Text = text;
            Parent = parent;
        }

        /// <inheritdoc/>
        public void Update()
        {
            Focus(Clear, Write);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Focus(Utils.ClearInLine);
        }

        /// <inheritdoc/>
        public void Focus()
        {
            Utils.SetCursor(0, Line, Parent.Spacing);
        }

        /// <summary>
        /// Changes the cursor position and then calls the parameterless methods or lambda expressions.
        /// </summary>
        /// <param name="methods"></param>
        protected void Focus(params Action[] methods)
        {
            Focus();
            foreach (Action method in methods)
            {
                method();
            }
        }

        /// <summary>
        /// Writes the control text to the console ui.
        /// </summary>
        /// <param name="text">
        /// The text of the control.
        /// </param>
        /// <param name="centerize">
        /// Determines whether the wrote text must be centered the the console ui.
        /// </param>
        protected static void Write(string text, bool centerize = false)
        {
            if (centerize)
            {
                text = new string(' ', Utils.GetCenterPosition(text.Length)) + text;
            }
            Utils.Echo(text, false);
        }

        /// <inheritdoc/>
        public virtual void Write()
        {
            Write(Text);
        }
    }
}