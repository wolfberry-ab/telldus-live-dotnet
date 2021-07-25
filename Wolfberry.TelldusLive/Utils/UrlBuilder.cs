﻿using System;
using System.Collections.Specialized;
using System.Web;

namespace Wolfberry.TelldusLive.Utils
{
    /// <summary>
    /// Simplifies the creation of URLs.
    /// Note that :433 will be added to the host address if https:// is used
    /// </summary>
    public class UrlBuilder
    {
        private readonly UriBuilder _uriBuilder;
        private readonly NameValueCollection _query;
        private readonly string _url;

        public UrlBuilder(string url)
        {
            _url = url;
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
        /// Add query parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddQuery(string name, int value)
        {
            _query[name] = value.ToString();
        }

        public void AddOptionalQuery(string name, string value)
        {
            if (value != null)
            {
                AddQuery(name, value);
            }
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

        // TODO: Fix so that escaped strings are not double esecaped
        // Messages like "New alarm!" is now received as "New%20alarm%21"
        public string UriBuild()
        {
            _uriBuilder.Query = _query.ToString();
            return _uriBuilder.ToString();
        }

        public void AddQuery(string name, bool value)
        {
            var intValue = value ? 1 : 0;
            AddQuery(name, intValue.ToString());
        }

        public string Build()
        {
            var url = _url;
            var i = 0;
            foreach (string key in _query)
            {
                url += i == 0
                    ? "?" 
                    : "&";
                
                url += $"{key}={Uri.EscapeDataString(_query[key])}";
                i++;
            }

            return url;
        }

        public void AddOptionalQuery(string name, bool? value)
        {
            if (value != null)
            {
                AddQuery(name, (bool) value);
            }
        }

        public void AddOptionalQuery(string name, int? value)
        {
            if (value != null)
            {
                AddQuery(name, (int) value);
            }
        }
    }
}
