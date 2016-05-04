using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace esperancephone.Extensions
{
    public static class MobileServiceClientExtensions
    {
        public static string GetJWT(this string mobileServicesAuthenticationToken)
        {
            // Get just the JWT part of the token.
            var jwt = mobileServicesAuthenticationToken
                .Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+');
            jwt = jwt.Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new System.Exception(
               "The base64url string is not valid.");
            }

            // Decode the bytes from base64 and write to a JSON string.
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            return jsonString;
        }

        public static bool IsTokenExpired(this string mobileServiceAuthenticationToken)
        {
            // Get just the JWT part of the token.
            var jwt = mobileServiceAuthenticationToken
                .Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+');
            jwt = jwt.Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new System.Exception(
               "The base64url string is not valid.");
            }

            // Decode the bytes from base64 and write to a JSON string.
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value, 
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the 
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);

            Debug.WriteLine($"INFORMATION: UTC.NOW: {DateTime.UtcNow}");
            Debug.WriteLine($"INFORMATION: MobileServiceAuthenticationToken EXPIRY: {expire} [DATE: {expire.Date.ToString()}] [TIME: {expire.Hour}h:{expire.Minute}m:{expire.Second}s:{expire.Millisecond}ms");
            var isExpired = expire < DateTime.UtcNow ? true : false;
            Debug.WriteLine($"INFORMATION: IsExpired: {isExpired}");
            // If the expiration date is less than now, the token is expired and we return true.
            return expire < DateTime.UtcNow ? true : false;

        }
    }
}
