using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace SYSSTATS_Charter
{
    public class SocketStatistics : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public SocketStatistics()
        {
            _propertyMap.Add("totsck", nameof(Totsck));
            _propertyMap.Add("tcpsck", nameof(Tcpsck));
            _propertyMap.Add("udpsck", nameof(Udpsck));
            _propertyMap.Add("rawsck", nameof(Rawsck));
            _propertyMap.Add("ip-frag", nameof(Ipfrag));
            _propertyMap.Add("tcp-tw", nameof(Tcptw));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Totsck == 0.0 && Tcpsck == 0.0 && Udpsck == 0.0 && Rawsck == 0.0 && Ipfrag == 0.0 && Tcptw == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Total number of sockets used by the system.
        /// </summary>
        public double Totsck { get; private set; }

        /// <summary>
        /// Number of TCP sockets currently in use.
        /// </summary>
        public double Tcpsck { get; private set; }

        /// <summary>
        /// Number of UDP sockets currently in use.
        /// </summary>
        public double Udpsck { get; private set; }

        /// <summary>
        /// Number of RAW sockets currently in use.
        /// </summary>
        public double Rawsck { get; private set; }

        /// <summary>
        /// Number of IP fragments currently in queue.
        /// </summary>
        public double Ipfrag { get; private set; }

        /// <summary>
        /// Number of TCP sockets in TIME_WAIT state.
        /// </summary>
        public double Tcptw { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new SocketStatistics();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;
                        switch (tags[i])
                        {
                            case "totsck":
                                newValues.Totsck = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "tcpsck":
                                newValues.Tcpsck = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "udpsck":
                                newValues.Udpsck = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "rawsck":
                                newValues.Rawsck = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "ip-frag":
                                newValues.Ipfrag = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "tcp-tw":
                                newValues.Tcptw = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                        }
                    }
                    result.Add(newValues);
                }
            }
            return result;
        }

        public override double GetValue(string tag)
        {
            return tag switch
            {
                "totsck" => Totsck,
                "tcpsck" => Tcpsck,
                "udpsck" => Udpsck,
                "rawsck" => Rawsck,
                "ip-frag" => Ipfrag,
                "tcp-tw" => Tcptw,
                _ => double.NaN,
            };
        }
    }
}
