using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SYSSTATS_Charter
{
    public class NFSClientActivity : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public NFSClientActivity()
        {
            _propertyMap.Add("call/s", nameof(CallPerSec));
            _propertyMap.Add("retrans/s", nameof(RetransPerSec));
            _propertyMap.Add("read/s", nameof(ReadPerSec));
            _propertyMap.Add("write/s", nameof(WritePerSec));
            _propertyMap.Add("access/s", nameof(AccessPerSec));
            _propertyMap.Add("getatt/s", nameof(GetattPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => CallPerSec == 0.0 && RetransPerSec == 0.0 && ReadPerSec == 0.0 && WritePerSec == 0.0 && AccessPerSec == 0.0 && GetattPerSec == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Number of RPC requests made per second.
        /// </summary>
        public double CallPerSec { get; private set; }

        /// <summary>
        /// Number of RPC requests per second, those which needed to be retransmitted (for example because of a server timeout).
        /// </summary>
        public double RetransPerSec { get; private set; }

        /// <summary>
        /// Number of 'read' RPC calls made per second.
        /// </summary>
        public double ReadPerSec { get; private set; }

        /// <summary>
        /// Number of 'write' RPC calls made per second.
        /// </summary>
        public double WritePerSec { get; private set; }

        /// <summary>
        /// Number of 'access' RPC calls made per second.
        /// </summary>
        public double AccessPerSec { get; private set; }

        /// <summary>
        /// Number of 'getattr' RPC calls made per second.
        /// </summary>
        public double GetattPerSec { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var newValues = new NFSClientActivity();

                var vals = item.Split(';');
                newValues.FillCommonTags(vals);

                for (var i = 0; i < tags.Length; i++)
                {
                    var index = i + 3;
                    switch (tags[i])
                    {
                        case "call/s":
                            newValues.CallPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "retrans/s":
                            newValues.RetransPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "read/s":
                            newValues.ReadPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "write/s":
                            newValues.WritePerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "access/s":
                            newValues.AccessPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "getatt/s":
                            newValues.GetattPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                    }
                }
                result.Add(newValues);
            }
            return result;
        }

        public override double GetValue(string tag)
        {
            return tag switch
            {
                "call/s" => CallPerSec,
                "retrans/s" => RetransPerSec,
                "read/s" => ReadPerSec,
                "write/s" => WritePerSec,
                "access/s" => AccessPerSec,
                "getatt/s" => GetattPerSec,
                _ => double.NaN,
            };
        }
    }
}
