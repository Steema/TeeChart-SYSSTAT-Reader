using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class SwappingStatistics : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public SwappingStatistics()
        {
            _propertyMap.Add("pswpin/s", nameof(PSwpinPerSec));
            _propertyMap.Add("pswpout/s", nameof(PSwpoutPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => PSwpinPerSec == 0.0 && PSwpoutPerSec == 0.0;

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new SwappingStatistics();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;
                        switch (tags[i])
                        {
                            case "pswpin/s":
                                newValues.PSwpinPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "pswpout/s":
                                newValues.PSwpoutPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "pswpin/s" => PSwpinPerSec,
                "pswpout/s" => PSwpoutPerSec,
                _ => double.NaN,
            };
        }

        /// <summary>
        /// Total number of swap pages the system brought in per second.
        /// </summary>
        public double PSwpinPerSec { get; private set; }

        /// <summary>
        /// Total number of swap pages the system brought out per second.
        /// </summary>
        public double PSwpoutPerSec { get; private set; }

        public override string FilterName => null;

        public override string FilterValue => null;
    }
}
