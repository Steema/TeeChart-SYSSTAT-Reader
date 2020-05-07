using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class HugePagesUtilization : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public HugePagesUtilization()
        {
            _propertyMap.Add("kbhugfree", nameof(Kbhugfree));
            _propertyMap.Add("kbhugused", nameof(Kbhugused));
            _propertyMap.Add("%hugused", nameof(Hugused));
            _propertyMap.Add("kbhugrsvd", nameof(Kbhugrsvd));
            _propertyMap.Add("kbhugsurp", nameof(Kbhugsurp));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Kbhugfree == 0.0 && Kbhugused == 0.0 && Hugused == 0.0 && Kbhugrsvd == 0.0 && Kbhugsurp == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Amount of hugepages memory in kilobytes that is not yet allocated.
        /// </summary>
        public double Kbhugfree { get; private set; }

        /// <summary>
        /// Amount of hugepages memory in kilobytes that has been allocated.
        /// </summary>
        public double Kbhugused { get; private set; }

        /// <summary>
        /// Percentage of total hugepages memory that has been allocated.
        /// </summary>
        public double Hugused { get; private set; }

        /// <summary>
        /// Amount of reserved hugepages memory in kilobytes.
        /// </summary>
        public double Kbhugrsvd { get; private set; }

        /// <summary>
        /// Amount of surplus hugepages memory in kilobytes.
        /// </summary>
        public double Kbhugsurp { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new HugePagesUtilization();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;

                        switch (tags[i])
                        {
                            case "kbhugfree":
                                newValues.Kbhugfree = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbhugused":
                                newValues.Kbhugused = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%hugused":
                                newValues.Hugused = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbhugrsvd":
                                newValues.Kbhugrsvd = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbhugsurp":
                                newValues.Kbhugsurp = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "kbhugfree" => Kbhugfree,
                "kbhugused" => Kbhugused,
                "%hugused" => Hugused,
                "kbhugrsvd" => Kbhugrsvd,
                "kbhugsurp" => Kbhugsurp,
                _ => double.NaN,
            };
        }
    }
}
