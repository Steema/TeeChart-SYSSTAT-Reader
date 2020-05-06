using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SYSSTATS_Charter
{
    public class BlockDeviceActivity : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public BlockDeviceActivity()
        {
            _propertyMap.Add("DEV", nameof(DEV));
            _propertyMap.Add("tps", nameof(Tps));
            _propertyMap.Add("rkB/s", nameof(RkBPerSec));
            _propertyMap.Add("wkB/s", nameof(WkBPerSec));
            _propertyMap.Add("dkB/s", nameof(DkBPerSec));
            _propertyMap.Add("areq-sz", nameof(Areqsz));
            _propertyMap.Add("aqu-sz", nameof(Aqusz));
            _propertyMap.Add("await", nameof(Await));
            _propertyMap.Add("%util", nameof(Util));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Tps == 0.0 && RkBPerSec == 0.0 && WkBPerSec == 0.0 && DkBPerSec == 0.0 && Areqsz == 0.0 && Aqusz == 0.0 && Aqusz == 0.0
                                        && Await == 0.0 && Util == 0.0;

        public override string FilterName => "DEV";

        public override string FilterValue => DEV;

        public string DEV { get; private set; }

        /// <summary>
        /// Total number of transfers per second that were issued to physical devices.  A transfer is an I/O request to a physical device. 
        /// Multiple logical requests can be combined into a single I/O request to the device.  A transfer is of indeterminate size.
        /// </summary>
        public double Tps { get; private set; }

        /// <summary>
        /// Number of kilobytes read from the device per second.
        /// </summary>
        public double RkBPerSec { get; private set; }

        /// <summary>
        /// Number of kilobytes written to the device per second.
        /// </summary>
        public double WkBPerSec { get; private set; }

        /// <summary>
        /// Number of kilobytes discarded for the device per second.
        /// </summary>
        public double DkBPerSec { get; private set; }

        /// <summary>
        /// The average size (in kilobytes) of the I/O requests that were issued to the device. Note: In previous versions, this field was known as avgrq-sz and was expressed in sectors.
        /// </summary>
        public double Areqsz { get; private set; }

        /// <summary>
        /// The average queue length of the requests that were issued to the device. Note: In previous versions, this field was known as avgqu-sz.
        /// </summary>
        public double Aqusz { get; private set; }

        /// <summary>
        /// The average time (in milliseconds) for I/O requests issued to the device to be served. This includes the time spent by the requests in queue and the time spent servicing them.
        /// </summary>
        public double Await { get; private set; }

        /// <summary>
        /// Percentage of elapsed time during which I/O requests were issued to the device (bandwidth utilization for the device). 
        /// Device saturation occurs when this value is close to 100% for devices serving requests serially. 
        /// But for devices serving requests in parallel, such as RAID arrays and modern SSDs, this number does not reflect their performance limits.
        /// </summary>
        public double Util { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var newValues = new BlockDeviceActivity();

                var vals = item.Split(';');
                newValues.FillCommonTags(vals);

                for (var i = 0; i < tags.Length; i++)
                {
                    var index = i + 3;

                    switch (tags[i])
                    {
                        case "DEV":
                            newValues.DEV = vals[index];
                            break;
                        case "tps":
                            newValues.Tps = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "rkB/s":
                            newValues.RkBPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "wkB/s":
                            newValues.WkBPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "dkB/s":
                            newValues.DkBPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "areq-sz":
                            newValues.Areqsz = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "aqu-sz":
                            newValues.Aqusz = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "await":
                            newValues.Await = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "%util":
                            newValues.Util = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "tps" => Tps,
                "rkB/s" => RkBPerSec,
                "wkB/s" => WkBPerSec,
                "dkB/s" => DkBPerSec,
                "areq-sz" => Areqsz,
                "aqu-sz" => Aqusz,
                "await" => Await,
                "%util" => Util,
                _ => double.NaN,
            };
        }
    }
}
