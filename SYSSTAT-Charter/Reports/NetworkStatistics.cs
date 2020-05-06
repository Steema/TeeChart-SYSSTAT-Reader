using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SYSSTATS_Charter
{
    public class NetworkStatistics : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public NetworkStatistics()
        {
            _propertyMap.Add("IFACE", nameof(IFACE));
            _propertyMap.Add("rxpck/s", nameof(RxpckPerSec));
            _propertyMap.Add("txpck/s", nameof(TxpckPerSec));
            _propertyMap.Add("rxkB/s", nameof(RxkBPerSec));
            _propertyMap.Add("txkB/s", nameof(TxkBPerSec));
            _propertyMap.Add("rxcmp/s", nameof(RxcmpPerSec));
            _propertyMap.Add("txcmp/s", nameof(TxcmpPerSEc));
            _propertyMap.Add("rxmcst/s", nameof(RxcmpPerSec));
            _propertyMap.Add("%ifutil", nameof(Ifutil));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => RxpckPerSec == 0.0 && TxpckPerSec == 0.0 && RxkBPerSec == 0.0 && TxkBPerSec == 0.0 && RxcmpPerSec == 0.0 && TxcmpPerSEc == 0.0 && RxcmpPerSec == 0.0
                                        && Ifutil == 0.0;

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var newValues = new NetworkStatistics();

                var vals = item.Split(';');
                newValues.FillCommonTags(vals);

                for (var i = 0; i < tags.Length; i++)
                {
                    var index = i + 3;
                    switch (tags[i])
                    {
                        case "IFACE":
                            newValues.IFACE = vals[index];
                            break;
                        case "rxpck/s":
                            newValues.RxpckPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "txpck/s":
                            newValues.TxpckPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "rxkB/s":
                            newValues.RxkBPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "txkB/s":
                            newValues.TxkBPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "rxcmp/s":
                            newValues.RxcmpPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "txcmp/s":
                            newValues.TxcmpPerSEc = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "rxmcst/s":
                            newValues.RxmcstPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "%ifutil":
                            newValues.Ifutil = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "rxpck/s" => RxpckPerSec,
                "txpck/s" => TxpckPerSec,
                "rxkB/s" => RxkBPerSec,
                "txkB/s" => TxkBPerSec,
                "rxcmp/s" => RxcmpPerSec,
                "txcmp/s" => TxcmpPerSEc,
                "rxmcst/s" => RxmcstPerSec,
                "%ifutil" => Ifutil,
                _ => double.NaN,
            };
        }

        /// <summary>
        /// Name of the network interface for which statistics are reported.
        /// </summary>
        public string IFACE { get; private set; }

        /// <summary>
        /// Total number of packets received per second.
        /// </summary>
        public double RxpckPerSec { get; private set; }

        /// <summary>
        /// Total number of packets transmitted per second.
        /// </summary>
        public double TxpckPerSec { get; private set; }

        /// <summary>
        /// Total number of kilobytes received per second.
        /// </summary>
        public double RxkBPerSec { get; private set; }

        /// <summary>
        /// Total number of kilobytes transmitted per second.
        /// </summary>
        public double TxkBPerSec { get; private set; }

        /// <summary>
        /// Number of compressed packets received per second (for cslip etc.).
        /// </summary>
        public double RxcmpPerSec { get; private set; }

        /// <summary>
        /// Number of compressed packets transmitted per second.
        /// </summary>
        public double TxcmpPerSEc { get; private set; }

        /// <summary>
        /// Number of multicast packets received per second.
        /// </summary>
        public double RxmcstPerSec { get; private set; }

        /// <summary>
        /// Utilization percentage of the network interface. For half-duplex interfaces, utilization is calculated using the sum of rxkB/s and txkB/s as a percentage of the interface speed. 
        /// For full-duplex, this is the greater of rxkB/S or txkB/s.
        /// </summary>
        public double Ifutil { get; private set; }

        public override string FilterName => "IFACE";

        public override string FilterValue => IFACE;
    }
}
