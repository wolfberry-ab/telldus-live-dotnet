using System.Collections.Generic;

namespace Wolfberry.TelldusLive.Models.Device
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

        /// <summary>
        /// Unknown/other	00000000-0001-1000-2005-ACCA54000000
        /// Alarm Sensor	00000001-0001-1000-2005-ACCA54000000
        /// Container	00000002-0001-1000-2005-ACCA54000000
        /// Controller	00000003-0001-1000-2005-ACCA54000000
        /// Door/Window	00000004-0001-1000-2005-ACCA54000000
        /// Light	00000005-0001-1000-2005-ACCA54000000
        /// Lock	00000006-0001-1000-2005-ACCA54000000
        /// Media	00000007-0001-1000-2005-ACCA54000000
        /// Meter	00000008-0001-1000-2005-ACCA54000000
        /// Motion	00000009-0001-1000-2005-ACCA54000000
        /// On/Off sensor	0000000A-0001-1000-2005-ACCA54000000
        /// Person	0000000B-0001-1000-2005-ACCA54000000
        /// Remote control	0000000C-0001-1000-2005-ACCA54000000
        /// Sensor	0000000D-0001-1000-2005-ACCA54000000
        /// Smoke sensor	0000000E-0001-1000-2005-ACCA54000000
        /// Speaker	0000000F-0001-1000-2005-ACCA54000000
        /// Switch/Outlet	00000010-0001-1000-2005-ACCA54000000
        /// Thermostat	00000011-0001-1000-2005-ACCA54000000
        /// Virtual	00000012-0001-1000-2005-ACCA54000000
        /// Window covering	00000013-0001-1000-2005-ACCA54000000
        /// Projector screen	00000014-0001-1000-2005-ACCA54000000
        /// </summary>
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
        public List<Parameter> Parameter { get; set; }
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
