using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class SoftwareNetwork : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public SoftwareNetwork()
        {
            _propertyMap.Add("CPU", nameof(CPU));
            _propertyMap.Add("total/s", nameof(TotalPerSec));
            _propertyMap.Add("dropd/s", nameof(DropdPerSec));
            _propertyMap.Add("squeezd/s", nameof(SqueezdPerSec));
            _propertyMap.Add("rx_rps/s", nameof(Rx_rpsPerSec));
            _propertyMap.Add("flw_lim/s", nameof(Flw_limPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => TotalPerSec == 0.0 && DropdPerSec == 0.0 && SqueezdPerSec == 0.0 && Rx_rpsPerSec == 0.0 && Flw_limPerSec == 0.0;

        public override string FilterName => "CPU";

        public override string FilterValue => CPU;

        public string CPU { get; private set; }

        /// <summary>
        /// The total number of network frames processed per second.
        /// </summary>
        public double TotalPerSec { get; private set; }

        /// <summary>
        /// The total number of network frames dropped per second because there was no room on the processing queue.
        /// </summary>
        public double DropdPerSec { get; private set; }

        /// <summary>
        /// The number of times the softirq handler function terminated per second because its budget was consumed or the time limit was reached, but more work could have been done.
        /// </summary>
        public double SqueezdPerSec { get; private set; }

        /// <summary>
        /// The number of times the CPU has been woken up per second to process packets via an inter-processor interrupt.
        /// </summary>
        public double Rx_rpsPerSec { get; private set; }

        /// <summary>
        /// The number of times the flow limit has been reached per second.  Flow limiting is an optional RPS feature that can be used to limit the number of packets queued to the backlog for each flow to a certain amount.  
        /// This can help ensure that smaller flows are processed even though much larger flows are pushing packets in.
        /// </summary>
        public double Flw_limPerSec { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if(!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new SoftwareNetwork();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;
                        switch (tags[i])
                        {
                            case "CPU":
                                newValues.CPU = vals[index];
                                break;
                            case "total/s":
                                newValues.TotalPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "dropd/s":
                                newValues.DropdPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "squeezd/s":
                                newValues.SqueezdPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "rx_rps/s":
                                newValues.Rx_rpsPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "flw_lim/s":
                                newValues.Flw_limPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "total/s" => TotalPerSec,
                "dropd/s" => DropdPerSec,
                "squeezd/s" => SqueezdPerSec,
                "rx_rps/s" => Rx_rpsPerSec,
                "flw_lim/s" => Flw_limPerSec,
                _ => double.NaN,
            };
        }
    }
}
