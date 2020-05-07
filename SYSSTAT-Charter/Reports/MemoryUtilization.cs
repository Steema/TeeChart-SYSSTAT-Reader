using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class MemoryUtilization : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public MemoryUtilization()
        {
            _propertyMap.Add("kbmemfree", nameof(Kbmemfree));
            _propertyMap.Add("kbavail", nameof(Kbavail));
            _propertyMap.Add("kbmemused", nameof(Kbmemused));
            _propertyMap.Add("%memused", nameof(Memused));
            _propertyMap.Add("kbbuffers", nameof(Kbbuffers));
            _propertyMap.Add("kbcached", nameof(Kbcached));
            _propertyMap.Add("kbcommit", nameof(Kbcommit));
            _propertyMap.Add("%commit", nameof(Commit));
            _propertyMap.Add("kbactive", nameof(Kbactive));
            _propertyMap.Add("kbinact", nameof(Kbinact));
            _propertyMap.Add("kbdirty", nameof(Kbdirty));
            _propertyMap.Add("kbanonpg", nameof(Kbanonpg));
            _propertyMap.Add("kbslab", nameof(Kbslab));
            _propertyMap.Add("kbkstack", nameof(Kbkstack));
            _propertyMap.Add("kbpgtbl", nameof(Kbpgtbl));
            _propertyMap.Add("kbvmused", nameof(Kbvmused));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Kbmemfree == 0.0 && Kbavail == 0.0 && Kbmemused == 0.0 && Memused == 0.0 && Kbbuffers == 0.0 && Kbcached == 0.0
                                        && Kbcommit == 0.0 && Commit == 0.0 && Kbactive == 0.0 && Kbinact == 0.0 && Kbdirty == 0.0 && Kbanonpg == 0.0 && Kbslab == 0.0
                                        && Kbkstack == 0.0 && Kbpgtbl == 0.0 && Kbvmused == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Amount of free memory available in kilobytes.
        /// </summary>
        public double Kbmemfree { get; private set; }

        /// <summary>
        /// Estimate of how much memory in kilobytes is available for starting new applications, without swapping.  
        /// The estimate takes into account that the system needs some page cache to function well, and that not all reclaimable slab will be reclaimable, due to items being in use. 
        /// The impact of those factors will vary from system to system.
        /// </summary>
        public double Kbavail { get; private set; }

        /// <summary>
        /// Amount of used memory in kilobytes (calculated as total installed memory - kbmemfree - kbbuffers - kbcached - kbslab).
        /// </summary>
        public double Kbmemused { get; private set; }

        /// <summary>
        /// Percentage of used memory.
        /// </summary>
        public double Memused { get; private set; }

        /// <summary>
        /// Amount of memory used as buffers by the kernel in kilobytes.
        /// </summary>
        public double Kbbuffers { get; private set; }

        /// <summary>
        /// Amount of memory used to cache data by the kernel in kilobytes.
        /// </summary>
        public double Kbcached { get; private set; }

        /// <summary>
        /// Amount of memory in kilobytes needed for current workload. This is an estimate of how much RAM/swap is needed to guarantee that there never is out of memory.
        /// </summary>
        public double Kbcommit { get; private set; }

        /// <summary>
        /// Percentage of memory needed for current workload in relation to the total amount of memory (RAM+swap). This number may be greater than 100% because the kernel usually overcommits memory.
        /// </summary>
        public double Commit { get; private set; }

        /// <summary>
        /// Amount of active memory in kilobytes (memory that has been used more recently and usually not reclaimed unless absolutely necessary).
        /// </summary>
        public double Kbactive { get; private set; }

        /// <summary>
        /// Amount of inactive memory in kilobytes (memory which has been less recently used. It is more eligible to be reclaimed for other purposes).
        /// </summary>
        public double Kbinact { get; private set; }

        /// <summary>
        /// Amount of memory in kilobytes waiting to get written back to the disk.
        /// </summary>
        public double Kbdirty { get; private set; }

        /// <summary>
        /// Amount of non-file backed pages in kilobytes mapped into userspace page tables.
        /// </summary>
        public double Kbanonpg { get; private set; }

        /// <summary>
        /// Amount of memory in kilobytes used by the kernel to cache data structures for its own use.
        /// </summary>
        public double Kbslab { get; private set; }

        /// <summary>
        /// Amount of memory in kilobytes used for kernel stack space.
        /// </summary>
        public double Kbkstack { get; private set; }

        /// <summary>
        /// Amount of memory in kilobytes dedicated to the lowest level of page tables.
        /// </summary>
        public double Kbpgtbl { get; private set; }

        /// <summary>
        /// Amount of memory in kilobytes of used virtual address space.
        /// </summary>
        public double Kbvmused { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new MemoryUtilization();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;

                        switch (tags[i])
                        {
                            case "kbmemfree":
                                newValues.Kbmemfree = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbavail":
                                newValues.Kbavail = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbmemused":
                                newValues.Kbmemused = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%memused":
                                newValues.Memused = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbbuffers":
                                newValues.Kbbuffers = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbcached":
                                newValues.Kbcached = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbcommit":
                                newValues.Kbcommit = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%commit":
                                newValues.Commit = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbactive":
                                newValues.Kbactive = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbinact":
                                newValues.Kbinact = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbdirty":
                                newValues.Kbdirty = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbanonpg":
                                newValues.Kbanonpg = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbslab":
                                newValues.Kbslab = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbkstack":
                                newValues.Kbkstack = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbpgtbl":
                                newValues.Kbpgtbl = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "kbvmused":
                                newValues.Kbvmused = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "kbmemfree" => Kbmemfree,
                "kbavail" => Kbavail,
                "kbmemused" => Kbmemused,
                "%memused" => Memused,
                "kbbuffers" => Kbbuffers,
                "kbcached" => Kbcached,
                "kbcommit" => Kbcommit,
                "%commit" => Commit,
                "kbactive" => Kbactive,
                "kbinact" => Kbinact,
                "kbdirty" => Kbdirty,
                "kbanonpg" => Kbanonpg,
                "kbslab" => Kbslab,
                "kbkstack" => Kbkstack,
                "kbpgtbl" => Kbpgtbl,
                "kbvmused" => Kbvmused,
                _ => double.NaN,
            };
        }
    }
}
