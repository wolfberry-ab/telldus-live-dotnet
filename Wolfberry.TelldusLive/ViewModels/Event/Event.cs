using System.Collections.Generic;

namespace Wolfberry.TelldusLive.ViewModels.Event
{
    public class Event
    {
        public string Id { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public int MinRepeatInterval { get; set; }
        public int Active { get; set; }
        public List<Trigger> Trigger { get; set; }
        public List<Condition> Condition { get; set; }
        public List<Action> Action { get; set; }
    }
}
