using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ubidots
{
    public class Value : ApiObject
    {
        public Value(Dictionary<string, object> Raw, ApiClient Api) : base(Raw, Api) {}

        /// <summary>
        /// Gets the timestamp of the value.
        /// </summary>
        /// <returns>The timestamp of the value. Formatted in Unix Time</returns>
        public long GetTimestamp()
        {
            return (long)GetAttributeDouble("timestamp");
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        // <returns>The value</returns>
        public double GetValue() 
        {
            return GetAttributeDouble("value");
        }
    }
}
