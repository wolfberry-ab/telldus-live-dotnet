namespace Wolfberry.TelldusLive.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Is not always set.
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

        /// <summary>
        /// Is not always set
        /// </summary>

        public string SubCode { get; set; }
    }
}
