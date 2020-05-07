using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class TransferRateStatistics : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public TransferRateStatistics()
        {
            _propertyMap.Add("tps", nameof(Tps));
            _propertyMap.Add("rtps", nameof(Rtps));
            _propertyMap.Add("wtps", nameof(Wtps));
            _propertyMap.Add("dtps", nameof(Dtps));
            _propertyMap.Add("bread/s", nameof(BreadPerSec));
            _propertyMap.Add("bwrtn/s", nameof(BwrtnPerSec));
            _propertyMap.Add("bdscd/s", nameof(BdscdPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Tps == 0.0 && Rtps == 0.0 && Wtps == 0.0 && Dtps == 0.0 && BreadPerSec == 0.0 && BwrtnPerSec == 0.0 && BdscdPerSec == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Total number of transfers per second that were issued to physical devices.  A transfer is an I/O request to a physical device. 
        /// Multiple logical requests can be combined into a single I/O request to the device.  A transfer is of indeterminate size.
        /// </summary>
        public double Tps { get; private set; }

        /// <summary>
        /// Total number of read requests per second issued to physical devices.
        /// </summary>
        public double Rtps { get; private set; }

        /// <summary>
        /// Total number of write requests per second issued to physical devices.
        /// </summary>
        public double Wtps { get; private set; }

        /// <summary>
        /// Total number of discard requests per second issued to physical devices.
        /// </summary>
        public double Dtps { get; private set; }

        /// <summary>
        /// Total amount of data read from the devices in blocks per second.  Blocks are equivalent to sectors and therefore have a size of 512 bytes.
        /// </summary>
        public double BreadPerSec { get; private set; }

        /// <summary>
        /// Total amount of data written to devices in blocks per second.
        /// </summary>
        public double BwrtnPerSec { get; private set; }

        /// <summary>
        /// Total amount of data discarded for devices in blocks per second.
        /// </summary>
        public double BdscdPerSec { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new TransferRateStatistics();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;

                        switch (tags[i])
                        {
                            case "tps":
                                newValues.Tps = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "rtps":
                                newValues.Rtps = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "wtps":
                                newValues.Wtps = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "dtps":
                                newValues.Dtps = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "bread/s":
                                newValues.BreadPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "bwrtn/s":
                                newValues.BwrtnPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "bdscd/s":
                                newValues.BdscdPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "tps" => Tps,
                "rtps" => Rtps,
                "wtps" => Wtps,
                "dtps" => Dtps,
                "bread/s" => BreadPerSec,
                "bwrtn/s" => BwrtnPerSec,
                "bdscd/s" => BdscdPerSec,
                _ => double.NaN,
            };
        }
    }
}
