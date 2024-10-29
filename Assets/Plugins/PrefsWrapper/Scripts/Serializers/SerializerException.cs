using System;

namespace PrefsWrapper.Serializers
{
    public class SerializerException : Exception
    {
        public SerializerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SerializerException(string message) : base(message)
        {
        }
    }
}
