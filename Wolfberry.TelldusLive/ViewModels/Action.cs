namespace Wolfberry.TelldusLive.ViewModels
{
    public class Action
    {
        public string Id { get; set; }
        /// <summary>
        /// E.g. "push"
        /// </summary>
        public string Type { get; set; }
        public string Delay { get; set; }
        public string DelayPolicy { get; set; }
    }
}
