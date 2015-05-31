using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ubidots
{
    public class Value : ApiObject
    {
        public interface StatisticFigures
        {
            public static const string MEAN = "mean";
            public static const string VARIANCE = "variance";
            public static const string MIN = "min";
            public static const string MAX = "max";
            public static const string COUNT = "count";
            public static const string SUM = "sum";
        }

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
