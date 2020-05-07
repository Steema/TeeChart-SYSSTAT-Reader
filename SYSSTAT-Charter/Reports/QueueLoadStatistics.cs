using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class QueueLoadStatistics : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public QueueLoadStatistics()
        {
            _propertyMap.Add("runq-sz", nameof(Runqsz));
            _propertyMap.Add("plist-sz", nameof(Plistsz));
            _propertyMap.Add("ldavg-1", nameof(Ldavg1));
            _propertyMap.Add("ldavg-5", nameof(Ldavg5));
            _propertyMap.Add("ldavg-15", nameof(Ldavg15));
            _propertyMap.Add("blocked", nameof(Blocked));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Runqsz == 0.0 && Plistsz == 0.0 && Ldavg1 == 0.0 && Ldavg5 == 0.0 && Ldavg15 == 0.0 && Blocked == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Run queue length (number of tasks waiting for run time).
        /// </summary>
        public double Runqsz { get; private set; }

        /// <summary>
        /// Number of tasks in the task list.
        /// </summary>
        public double Plistsz { get; private set; }

        /// <summary>
        /// System load average for the last minute.  The load average is calculated as the average number of runnable or running tasks (R state), 
        /// and the number of tasks in uninterruptible sleep (D state) over the specified interval.
        /// </summary>
        public double Ldavg1 { get; private set; }

        /// <summary>
        /// System load average for the past 5 minutes.
        /// </summary>
        public double Ldavg5 { get; private set; }

        /// <summary>
        /// System load average for the past 15 minutes.
        /// </summary>
        public double Ldavg15 { get; private set; }

        /// <summary>
        /// Number of tasks currently blocked, waiting for I/O to complete.
        /// </summary>
        public double Blocked { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new QueueLoadStatistics();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;

                        switch (tags[i])
                        {
                            case "runq-sz":
                                newValues.Runqsz = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "plist-sz":
                                newValues.Plistsz = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "ldavg-1":
                                newValues.Ldavg1 = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "ldavg-5":
                                newValues.Ldavg5 = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "ldavg-15":
                                newValues.Ldavg15 = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "blocked":
                                newValues.Blocked = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "runq-sz" => Runqsz,
                "plist-sz" => Plistsz,
                "ldavg-1" => Ldavg1,
                "ldavg-5" => Ldavg5,
                "ldavg-15" => Ldavg15,
                "blocked" => Blocked,
                _ => double.NaN,
            };
        }
    }
}
