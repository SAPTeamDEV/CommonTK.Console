namespace SAPTeam.CommonTK.Console
{
    /// <summary>
    /// Represents the Console launching mechanisms.
    /// </summary>
    public enum ConsoleLaunchMode
    {
        /// <summary>
        /// Allocates a Console for Executable using AllocConsole Win32 api P/Invoke call.
        /// </summary>
        Allocation,

        /// <summary>
        /// Attaches to Application Caller Process.
        /// </summary>
        AttachParent,

        /// <summary>
        /// Creates a Dedicated cmd Process, then kill it and uses it's Process Window.
        /// </summary>
        AttachProcess,

        /// <summary>
        /// Creates a pipe console client using ConClient.
        /// </summary>
        CreateClient,

        /// <summary>
        /// Creates a new blank console application and attaches to it.
        /// </summary>
        AttachClient
    }
}
