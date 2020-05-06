using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class TaskCreationSwitching : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public TaskCreationSwitching()
        {
            _propertyMap.Add("proc/s", nameof(ProcPerSec));
            _propertyMap.Add("cswch/s", nameof(CSwchPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => ProcPerSec == 0.0 && CSwchPerSec == 0.0;

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var newValues = new TaskCreationSwitching();

                var vals = item.Split(';');
                newValues.FillCommonTags(vals);

                for (var i = 0; i < tags.Length; i++)
                {
                    var index = i + 3;
                    switch (tags[i])
                    {
                        case "proc/s":
                            newValues.ProcPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "cswch/s":
                            newValues.CSwchPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "proc/s" => ProcPerSec,
                "cswch/s" => CSwchPerSec,
                _ => double.NaN,
            };
        }

        /// <summary>
        /// Total number of tasks created per second.
        /// </summary>
        public double ProcPerSec { get; private set; }

        /// <summary>
        /// Total number of context switches per second.
        /// </summary>
        public double CSwchPerSec { get; private set; }

        public override string FilterName => null;

        public override string FilterValue => null;
    }
}
