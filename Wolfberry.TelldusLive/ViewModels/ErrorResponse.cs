namespace Wolfberry.TelldusLive.ViewModels
{
    public class ErrorResponse
    {
        public string Error { get; set; }

        /// <summary>
        /// Not found	1
        /// Access denied	2
        /// Offline	3
        /// Invalid argument	4
        /// Length	5
        /// Out of range	6
        /// Unsupported	7
        /// Unknown	8
        /// </summary>
        public int Code { get; set; }

        public string SubCode { get; set; }
    }
}
