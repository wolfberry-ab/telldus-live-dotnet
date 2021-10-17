using System;
using System.Runtime.Serialization;

namespace Wolfberry.TelldusLive.Repositories
{
    /// <summary>
    /// This exception is thrown when errors occurs in repositories
    /// </summary>
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException()
        { }
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception innerException) 
            : base(message, innerException) { }
        protected RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
