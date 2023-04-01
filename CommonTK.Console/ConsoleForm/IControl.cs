namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    /// <summary>
    /// Provides interface for implementing the console ui components.
    /// </summary>
    public interface IControl
    {
        /// <summary>
        /// Gets the vertical coordinate of this component.
        /// </summary>
        int Line { get; }

        /// <summary>
        /// Gets the component text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets a value indicating whether the user can give the focus to this control using arrow keys.
        /// </summary>
        bool Selectable { get; }

        /// <summary>
        /// Writes the visual elements of this control to the console ui.
        /// </summary>
        void Write();

        /// <summary>
        /// Clears the visual elements of this control from the console ui.
        /// </summary>
        void Clear();

        /// <summary>
        /// Changes the cursor position according to the <see cref="Line"/> and Console ui properties.
        /// </summary>
        void Focus();

        /// <summary>
        /// Clears and rewrites the visual elements of this control.
        /// </summary>
        void Update();
    }
}