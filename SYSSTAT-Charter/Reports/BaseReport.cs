using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public abstract class BaseReport
    {
        public string HostName { get; set; }

        public int Interval { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public void FillCommonTags(string[] values)
        {
            HostName = values[0];
            Interval = int.Parse(values[1]);
            var length = values[2].Length;
            TimeStamp = DateTimeOffset.Parse(values[2].Remove(length - 4, 4), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }

        public abstract Dictionary<string, string> PropertyMap { get; }

        public abstract double GetValue(string tag);

        public abstract string FilterName { get; }

        public abstract string FilterValue { get; }

        public abstract bool AllZero { get; }

        public bool HasFilter()
        {
            return (!string.IsNullOrEmpty(FilterName) && !string.IsNullOrEmpty(FilterValue));
        }

        public abstract List<BaseReport> GetReports(string[] tags, List<string> values);

        public static bool TryGetReports<T>(string[] tags, List<string> values, out List<BaseReport> reports) where T : BaseReport, new()
        {
            try
            {
                var report = new T();

                if (tags.All(x => report.PropertyMap.Keys.Any(y => y == x)))
                {
                    reports = report.GetReports(tags, values);
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            reports = null;
            return false;
        }
    }
}
