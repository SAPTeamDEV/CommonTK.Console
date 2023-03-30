namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Represents the mechanisms that the console window processes the texts.
    /// </summary>
    public enum ConsoleType
    {
        /// <summary>
        /// Unsupported or Unknown console type.
        /// </summary>
        None,

        /// <summary>
        /// Represents the state that the console launched in native forms such as Console Application
        /// or a console that launched with <see cref="ConsoleLaunchMode.AttachProcess"/> method.
        /// </summary>
        Native,

        /// <summary>
        /// Represents the state that the console launched in external forms such as <see cref="ConsoleLaunchMode.Allocation"/>.
        /// </summary>
        External
    }
}