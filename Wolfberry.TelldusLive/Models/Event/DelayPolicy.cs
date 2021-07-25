namespace Wolfberry.TelldusLive.Models.Event
{
    public static class DelayPolicy
    {
        /// <summary>
        /// Restart the timer
        /// </summary>
        public static string Restart = "restart";

        /// <summary>
        /// Ignore second activation. First timer continues to run.
        /// </summary>
        public static string Continue = "continue";
    }
}
