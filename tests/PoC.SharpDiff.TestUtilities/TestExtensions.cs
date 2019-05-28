using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PoC.SharpDiff.TestUtilities
{
    public static class TestExtensions
    {
        public static StringContent ToStringContent(this object obj)
        {
            var jsonData = JsonConvert.SerializeObject(obj);
            var bodyData = new StringContent(jsonData)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return bodyData;
        }

        public static string ConvertToBase64FromUTF8String(this string str)
        {
            string base64EncodedData = null;
            if (str != null)
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                base64EncodedData = Convert.ToBase64String(bytes);
            }

            return base64EncodedData;
        }
    }
}
