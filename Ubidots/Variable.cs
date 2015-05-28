using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ubidots
{
    public class Variable : ApiObject
    {
        public Variable(Dictionary<string, object> Raw, ApiClient Api) 
            : base(Raw, Api) { }

        /// <summary>
        /// Get the name of the variable.
        /// </summary>
        /// <returns>The name of the variable</returns>
        public string GetName()
        {
            return GetAttributeString("name");
        }

        /// <summary>
        /// Get the unit of the variable.
        /// </summary>
        // <returns>The unit of the value.</returns>
        public string GetUnit()
        {
            return GetAttributeString("unit");
        }

        /// <summary>
        /// Deletes the variable and all its contents.
        /// </summary>
        public void Remove()
        {
            Bridge.Delete("variables/" + GetId());
        }

        /// <summary>
        /// Get all the values of the variable.
        /// </summary>
        /// <returns>The list of all the values of the variable</returns>
        public Value[] GetValues()
        {
            string Json = Bridge.Get("variable/" + GetId() + "/values");

            List<Dictionary<string, object>> RawValues = 
                JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(Json);

            Value[] Values = new Value[RawValues.Count];

            for (var i = 0; i < RawValues.Count; i++)
            {
                Values[i] = new Value(RawValues[i], Api);
            }

            return Values;
        }

        /// <summary>
        /// Send a value to Ubidots API and save it.
        /// </summary>
        /// <param name="Value">The value to be saved</param>
        public void SaveValue(int Value)
        {
            SaveValue((double)Value);
        }

        /// <summary>
        /// Send a value to Ubidots API and save it.
        /// </summary>
        /// <param name="Value">The value to be saved</param>
        public void SaveValue(double Value)
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("value", Value);
            Data.Add("timestamp", GetTimestamp());

            string Json = JsonConvert.SerializeObject(Data);

            Bridge.Post("variables/" + GetId() + "/values", Json);
        }

        /// <summary>
        /// Send a value and a context to Ubidots API and save it.
        /// </summary>
        /// <param name="Value">The value to be saved</param>
        /// <param name="Context">The context to be saved</param>
        public void SaveValue(int Value, Dictionary<string, object> Context)
        {
            SaveValue((double)Value, Context);
        }

        /// <summary>
        /// Send a value and a context to Ubidots API and save it.
        /// </summary>
        /// <param name="Value">The value to be saved</param>
        /// <param name="Context">The context to be saved</param>
        public void SaveValue(double Value, Dictionary<string, object> Context)
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("value", Value);
            Data.Add("context", Context);
            Data.Add("timestamp", GetTimestamp());

            string Json = JsonConvert.SerializeObject(Data);

            Bridge.Post("variables/" + GetId() + "/values", Json);
        }

        /// <summary>
        /// Send a bulk of values to  Ubidots API and save them
        /// </summary>
        /// <remarks>Values and Timestamps arrays must have the same length</remarks>
        /// <param name="Values">The values to be sent</param>
        /// <param name="Timestamps">The timestamps of the values to be sent</param>
        public void SaveValues(int[] Values, long[] Timestamps)
        {
            double[] Data = new double[Values.Length];

            for (var i = 0; i < Values.Length; i++)
            {
                Data[i] = (double)Values[i];
            }

            SaveValues(Data, Timestamps);
        }

        /// <summary>
        /// Send a bulk of values to  Ubidots API and save them
        /// </summary>
        /// <remarks>Values and Timestamps arrays must have the same length</remarks>
        /// <param name="Values">The values to be sent</param>
        /// <param name="Timestamps">The timestamps of the values to be sent</param>
        public void SaveValues(double[] Values, long[] Timestamps)
        {
            if (Values == null || Timestamps == null)
            {
                throw new ArgumentNullException();
            }
            else if (Values.Length != Timestamps.Length)
            {
                throw new IndexOutOfRangeException("Values and Timestamps must"
                + " have the same values");
            }

            List<Dictionary<string, object>> ValuesList = 
                new List<Dictionary<string, object>>();
            for (var i = 0; i < Values.Length; i++)
            {
                Dictionary<string, object> Data = 
                    new Dictionary<string, object>();
                Data.Add("value", Values[i]);
                Data.Add("timestamp", Timestamps[i]);
                ValuesList.Add(Data);
            }

            string Json = JsonConvert.SerializeObject(ValuesList);

            Bridge.Post("variables/" + GetId() + "/values", Json);
        }

        /// <summary>
        /// Get the timestamp in Unix time
        /// </summary>
        /// <returns>The current time taken from January 1 1970</returns>
        private long GetTimestamp()
        {
            DateTime Jan1st1970 = 
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
