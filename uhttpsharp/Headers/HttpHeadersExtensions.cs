using System;
using System.Text;

namespace uhttpsharp.Headers
{
    public static class HttpHeadersExtensions
    {
        public static bool KeepAliveConnection(this IHttpHeaders headers)
        {
            string value;
            return headers.TryGetByName("Connection", out value) && value.Equals("Keep-Alive", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool TryGetByName<T>(this IHttpHeaders headers, string name, out T value)
        {
            string stringValue;
            
            if (headers.TryGetByName(name, out stringValue))
            {
                value = (T) Convert.ChangeType(stringValue, typeof(T));
                return true;
            }

            value = default(T);
            return false;
        }

        public static T GetByName<T>(this IHttpHeaders headers, string name)
        {
            T value;
            headers.TryGetByName(name, out value);
            return value;
        }

        public static string ToUriData(this IHttpHeaders headers)
        {
            var builder = new StringBuilder();

            foreach (var header in headers)
            {
                builder.AppendFormat("{0}={1}&", Uri.EscapeDataString(header.Key), Uri.EscapeDataString(header.Value));
            }

            return builder.ToString(0, builder.Length - 1);
        }
    }
}