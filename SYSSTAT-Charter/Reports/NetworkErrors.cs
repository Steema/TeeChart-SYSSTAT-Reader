using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class NetworkErrors : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public NetworkErrors()
        {
            _propertyMap.Add("IFACE", nameof(RxerrPerSec));
            _propertyMap.Add("rxerr/s", nameof(RxerrPerSec));
            _propertyMap.Add("txerr/s", nameof(TxerrPerSec));
            _propertyMap.Add("coll/s", nameof(CollPerSec));
            _propertyMap.Add("rxdrop/s", nameof(RxdropPerSec));
            _propertyMap.Add("txdrop/s", nameof(TxdropPerSec));
            _propertyMap.Add("txcarr/s", nameof(TxcarrPerSEc));
            _propertyMap.Add("rxfram/s", nameof(RxframPerSec));
            _propertyMap.Add("rxfifo/s", nameof(RxfifoPerSec));
            _propertyMap.Add("txfifo/s", nameof(TxfifoPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => RxerrPerSec == 0.0 && RxerrPerSec == 0.0 && TxerrPerSec == 0.0 && CollPerSec == 0.0 && RxdropPerSec == 0.0 && TxdropPerSec == 0.0 && TxcarrPerSEc == 0.0
                                        && RxframPerSec == 0.0 && RxfifoPerSec == 0.0 && TxfifoPerSec == 0.0;

        public override string FilterName => "IFACE";

        public override string FilterValue => IFACE;

        public string IFACE { get; private set; }

        /// <summary>
        /// Total number of bad packets received per second.
        /// </summary>
        public double RxerrPerSec { get; private set; }

        /// <summary>
        /// Total number of errors that happened per second while transmitting packets.
        /// </summary>
        public double TxerrPerSec { get; private set; }

        /// <summary>
        /// Number of collisions that happened per second while transmitting packets.
        /// </summary>
        public double CollPerSec { get; private set; }

        /// <summary>
        /// Number of received packets dropped per second because of a lack of space in linux buffers.
        /// </summary>
        public double RxdropPerSec { get; private set; }

        /// <summary>
        /// Number of transmitted packets dropped per second because of a lack of space in linux buffers.
        /// </summary>
        public double TxdropPerSec { get; private set; }

        /// <summary>
        /// Number of carrier-errors that happened per second while transmitting packets.
        /// </summary>
        public double TxcarrPerSEc { get; private set; }

        /// <summary>
        /// Number of frame alignment errors that happened per second on received packets.
        /// </summary>
        public double RxframPerSec { get; private set; }

        /// <summary>
        /// Number of FIFO overrun errors that happened per second on received packets.
        /// </summary>
        public double RxfifoPerSec { get; private set; }

        /// <summary>
        /// Number of FIFO overrun errors that happened per second on transmitted packets.
        /// </summary>
        public double TxfifoPerSec { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new NetworkErrors();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;
                        switch (tags[i])
                        {
                            case "IFACE":
                                newValues.IFACE = vals[index];
                                break;
                            case "rxerr/s":
                                newValues.RxerrPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "txerr/s":
                                newValues.TxerrPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "coll/s":
                                newValues.CollPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "rxdrop/s":
                                newValues.RxdropPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "txdrop/s":
                                newValues.TxdropPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "txcarr/s":
                                newValues.TxcarrPerSEc = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "rxfram/s":
                                newValues.RxframPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "rxfifo/s":
                                newValues.RxfifoPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "txfifo/s":
                                newValues.TxfifoPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "rxerr/s" => RxerrPerSec,
                "txerr/s" => TxerrPerSec,
                "coll/s" => CollPerSec,
                "rxdrop/s" => RxdropPerSec,
                "txdrop/s" => TxdropPerSec,
                "txcarr/s" => TxcarrPerSEc,
                "rxfram/s" => RxframPerSec,
                "rxfifo/s" => RxfifoPerSec,
                "txfifo/s" => TxfifoPerSec,
                _ => double.NaN,
            };
        }
    }
}
