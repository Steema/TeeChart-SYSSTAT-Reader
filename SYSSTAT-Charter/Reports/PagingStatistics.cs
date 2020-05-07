using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class PagingStatistics : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public PagingStatistics()
        {
            _propertyMap.Add("pgpgin/s", nameof(PgpginPerSec));
            _propertyMap.Add("pgpgout/s", nameof(PgpgoutPerSec));
            _propertyMap.Add("fault/s", nameof(FaultPerSec));
            _propertyMap.Add("majflt/s", nameof(MajfltPerSec));
            _propertyMap.Add("pgfree/s", nameof(PgfreePerSec));
            _propertyMap.Add("pgscank/s", nameof(PgscankPerSec));
            _propertyMap.Add("pgscand/s", nameof(PgscandPerSec));
            _propertyMap.Add("pgsteal/s", nameof(PgstealPerSec));
            _propertyMap.Add("%vmeff", nameof(Vmeff));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => PgpginPerSec == 0.0 && PgpgoutPerSec == 0.0 && FaultPerSec == 0.0 && MajfltPerSec == 0.0 && PgfreePerSec == 0.0 && PgscankPerSec == 0.0 && PgscandPerSec == 0.0
                                        && PgstealPerSec == 0.0 && Vmeff == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Total number of kilobytes the system paged in from disk per second.
        /// </summary>
        public double PgpginPerSec { get; private set; }

        /// <summary>
        /// Total number of kilobytes the system paged out to disk per second.
        /// </summary>
        public double PgpgoutPerSec { get; private set; }

        /// <summary>
        /// Number of page faults (major + minor) made by the system per second.  This is not a count of page faults that generate I/O, because some page faults can be resolved without I/O.
        /// </summary>
        public double FaultPerSec { get; private set; }

        /// <summary>
        /// Number of major faults the system has made per second, those which have required loading a memory page from disk.
        /// </summary>
        public double MajfltPerSec { get; private set; }

        /// <summary>
        /// Number of pages placed on the free list by the system per second.
        /// </summary>
        public double PgfreePerSec { get; private set; }

        /// <summary>
        /// Number of pages scanned by the kswapd daemon per second.
        /// </summary>
        public double PgscankPerSec { get; private set; }

        /// <summary>
        /// Number of pages scanned directly per second.
        /// </summary>
        public double PgscandPerSec { get; private set; }

        /// <summary>
        /// Number of pages the system has reclaimed from cache (pagecache and swapcache) per second to satisfy its memory demands.
        /// </summary>
        public double PgstealPerSec { get; private set; }

        /// <summary>
        /// Calculated as pgsteal / pgscan, this is a metric of the efficiency of page reclaim. If it is near 100% then almost every page coming off the tail of the inactive list is being reaped. 
        /// If it gets too low (e.g. less than 30%) then the virtual memory is having some difficulty.  This field is displayed as zero if no pages have been scanned during the interval of time.
        /// </summary>
        public double Vmeff { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new PagingStatistics();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;

                        switch (tags[i])
                        {
                            case "pgpgin/s":
                                newValues.PgpginPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "pgpgout/s":
                                newValues.PgpgoutPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "fault/s":
                                newValues.FaultPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "majflt/s":
                                newValues.MajfltPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "pgfree/s":
                                newValues.PgfreePerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "pgscank/s":
                                newValues.PgscankPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "pgscand/s":
                                newValues.PgscandPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "pgsteal/s":
                                newValues.PgstealPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%vmeff":
                                newValues.Vmeff = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "pgpgin/s" => PgpginPerSec,
                "pgpgout/s" => PgpgoutPerSec,
                "fault/s" => FaultPerSec,
                "majflt/s" => MajfltPerSec,
                "pgfree/s" => PgfreePerSec,
                "pgscank/s" => PgscankPerSec,
                "pgscand/s" => PgscandPerSec,
                "pgsteal/s" => PgstealPerSec,
                "%vmeff" => Vmeff,
                _ => double.NaN,
            };
        }
    }
}
