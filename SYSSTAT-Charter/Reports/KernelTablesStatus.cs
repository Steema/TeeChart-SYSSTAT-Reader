using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SYSSTATS_Charter
{
    public class KernelTablesStatus : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public KernelTablesStatus()
        {
            _propertyMap.Add("dentunusd", nameof(Dentunusd));
            _propertyMap.Add("file-nr", nameof(FileNum));
            _propertyMap.Add("inode-nr", nameof(InodeNum));
            _propertyMap.Add("pty-nr", nameof(PtyNum));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Dentunusd == 0.0 && FileNum == 0.0 && InodeNum == 0.0 && PtyNum == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Number of unused cache entries in the directory cache.
        /// </summary>
        public double Dentunusd { get; private set; }

        /// <summary>
        /// Number of file handles used by the system.
        /// </summary>
        public double FileNum { get; private set; }

        /// <summary>
        /// Number of inode handlers used by the system.
        /// </summary>
        public double InodeNum { get; private set; }

        /// <summary>
        /// Number of pseudo-terminals used by the system.
        /// </summary>
        public double PtyNum { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var newValues = new KernelTablesStatus();

                var vals = item.Split(';');
                newValues.FillCommonTags(vals);

                for (var i = 0; i < tags.Length; i++)
                {
                    var index = i + 3;

                    switch (tags[i])
                    {
                        case "dentunusd":
                            newValues.Dentunusd = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "file-nr":
                            newValues.FileNum = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "inode-nr":
                            newValues.InodeNum = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "pty-nr":
                            newValues.PtyNum = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "dentunusd" => Dentunusd,
                "file-nr" => FileNum,
                "inode-nr" => InodeNum,
                "pty-nr" => PtyNum,
                _ => double.NaN,
            };
        }
    }
}
