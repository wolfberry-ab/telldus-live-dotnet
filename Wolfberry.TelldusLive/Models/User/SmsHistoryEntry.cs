namespace Wolfberry.TelldusLive.Models.User
{
    public class SmsHistoryEntry
    {
        public string Id { get; set; }
        public int Date { get; set; }

        /// <summary>
        /// To phone number. E.g. "46709123456"
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 0 = pending, 1 = delivered, 2 = failed
        /// </summary>
        public int Status { get; set; }
    }
}
