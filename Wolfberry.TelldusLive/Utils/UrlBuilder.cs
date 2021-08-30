using System;
using System.Collections.Specialized;
using System.Web;

namespace Wolfberry.TelldusLive.Utils
{
    /// <summary>
    /// Simplifies the creation of URLs
    /// </summary>
    public class UrlBuilder
    {
        private readonly UriBuilder _uriBuilder;
        private readonly NameValueCollection _query;

        public UrlBuilder(string url)
        {
            _uriBuilder = new UriBuilder(url);
            _query = HttpUtility.ParseQueryString(_uriBuilder.Query);
        }

        /// <summary>
        /// Add query parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddQuery(string name, string value)
        {
            _query[name] = value;
        }

        /// <summary>
        /// Add query parameter and automatically Uri escape the value
        /// </summary>
        /// <param name="name">Name of query parameter</param>
        /// <param name="value">Raw/un-escaped string</param>
        public void AddAsEscapedQuery(string name, string value)
        {
            _query[name] = Uri.EscapeDataString(value);
        }

        public string Build()
        {
            _uriBuilder.Query = _query.ToString();
            return _uriBuilder.ToString();
        }
    }
}
