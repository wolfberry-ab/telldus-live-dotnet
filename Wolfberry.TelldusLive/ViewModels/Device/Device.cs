using System.Collections.Generic;

namespace Wolfberry.TelldusLive.ViewModels.Device
{
    public class Device
    {
        public string Id { get; set; }
        public string ClientDeviceId { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public string StateValue { get; set; }
        public List<StateValue> StateValues { get; set; }
        public int Methods { get; set; }
        public string MetadataHash { get; set; }
        public string ParametersHash { get; set; }
        public string Type { get; set; }
        public string Client { get; set; }
        public string ClientName { get; set; }
        public string Online { get; set; }
        public int Editable { get; set; }
        public int Ignored { get; set; }
        /// <summary>
        /// Extras parameter timezone
        /// </summary>
        public string Timezone { get; set; }
        /// <summary>
        /// Extras parameter coordinate
        /// </summary>
        public double Longitute { get; set; }
        /// <summary>
        /// Extras parameter coordinate
        /// </summary>
        public double Latitute { get; set; }
        /// <summary>
        /// Extras parameter devicetype
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// Untested parameter. Extras parameter parameters
        /// </summary>
        public List<object> Parameter { get; set; }
        /// <summary>
        /// Extras parameter transport. E.g. "zwave" or "433".
        /// </summary>
        public string Transport { get; set; }
        /// <summary>
        /// Extras parameter tzoffset
        /// </summary>
        public int TzOffset { get; set; }
        /// <summary>
        /// Extras parameter uuid
        /// </summary>
        public string Uuid { get; set; }
    }
}
