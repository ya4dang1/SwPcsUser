using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helper
{
    public class HMACHelper
    {
        const string VERSION = "1.1";
        private readonly string method;

        public Guid MobileUUID { get; private set; }
        public DateTime TranDateTime { get; private set; }

        public Dictionary<string,object> Properties { get; private set; }

        public HMACHelper(string method)
        {
            Properties = new Dictionary<string, object>();
            this.method = method;
            Reset();
        }

        public void AddProperty(string key, object value)
        {
            Properties.Add(key, value);
        }

        public void Reset()
        {
            MobileUUID = Guid.NewGuid();
            TranDateTime = DateTime.UtcNow;
        }

        public string GetVersion()
        {
            return VERSION;
        }
        public string GetMethod()
        {
            return method.ToLower();
        }
        public string GetTranDateTime()
        {
            return GetDateTimeString(TranDateTime);
        }

        public string GetMobileUUID()
        {
            return MobileUUID.ToString();
        }      

        public string GetDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("MMddyyyyHHmmss");
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"version={VERSION}");
            result.Append($"&method={GetMethod()}");
            result.Append($"&uuid={GetMobileUUID()}");
            result.Append($"&datetime={GetTranDateTime()}");

            foreach (var property in Properties)
            {
                if (property.Value.GetType() == typeof(System.DateTime))
                {
                    result.Append($"&{property.Key}={GetDateTimeString((DateTime)property.Value)}");
                }
                else
                {
                    result.Append($"&{property.Key}={property.Value.ToString()}");
                }
            }

            return result.ToString();
        }
    }
}
