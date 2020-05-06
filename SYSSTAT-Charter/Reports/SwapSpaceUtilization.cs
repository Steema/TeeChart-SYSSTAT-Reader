using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SYSSTATS_Charter
{
    public class SwapSpaceUtilization : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public SwapSpaceUtilization()
        {
            _propertyMap.Add("kbswpfree", nameof(Kbswpfree));
            _propertyMap.Add("kbswpused", nameof(Kbswpused));
            _propertyMap.Add("%swpused", nameof(Swpused));
            _propertyMap.Add("kbswpcad", nameof(Kbswpcad));
            _propertyMap.Add("%swpcad", nameof(Swpcad));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Kbswpfree == 0.0 && Kbswpused == 0.0 && Swpused == 0.0 && Kbswpcad == 0.0 && Swpcad == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Amount of free swap space in kilobytes.
        /// </summary>
        public double Kbswpfree { get; private set; }

        /// <summary>
        /// Amount of used swap space in kilobytes.
        /// </summary>
        public double Kbswpused { get; private set; }

        /// <summary>
        /// Percentage of used swap space.
        /// </summary>
        public double Swpused { get; private set; }

        /// <summary>
        /// Amount of cached swap memory in kilobytes.  This is memory that once was swapped out, is swapped back in but still also is in the swap area 
        /// (if memory is needed it doesn't need to be swapped out again because it is already in the swap area. This saves I/O).
        /// </summary>
        public double Kbswpcad { get; private set; }

        /// <summary>
        /// Percentage of cached swap memory in relation to the amount of used swap space.
        /// </summary>
        public double Swpcad { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var newValues = new SwapSpaceUtilization();

                var vals = item.Split(';');
                newValues.FillCommonTags(vals);

                for (var i = 0; i < tags.Length; i++)
                {
                    var index = i + 3;

                    switch (tags[i])
                    {
                        case "kbswpfree":
                            newValues.Kbswpfree = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "kbswpused":
                            newValues.Kbswpused = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "%swpused":
                            newValues.Swpused = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "kbswpcad":
                            newValues.Kbswpcad = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "%swpcad":
                            newValues.Swpcad = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "kbswpfree" => Kbswpfree,
                "kbswpused" => Kbswpused,
                "%swpused" => Swpused,
                "kbswpcad" => Kbswpcad,
                "%swpcad" => Swpcad,
                _ => double.NaN,
            };
        }
    }
}
