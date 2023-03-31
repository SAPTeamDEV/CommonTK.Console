namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    /// <summary>
    /// Provides interface for implementing the selectable console ui components.
    /// </summary>
    public interface ISelectableControl : IControl
    {
        /// <summary>
        /// Applies the visual changes to this control when the user navigates to this control.
        /// </summary>
        void Select();
    }
}