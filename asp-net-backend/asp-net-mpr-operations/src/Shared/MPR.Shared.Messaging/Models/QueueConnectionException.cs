using System.Runtime.Serialization;

namespace MPR.Shared.Messaging.Models
{
    [Serializable]
    public sealed class QueueConnectionException : Exception
    {
        public QueueConnectionException(string message) : base(message)
        {
        }

        private QueueConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
