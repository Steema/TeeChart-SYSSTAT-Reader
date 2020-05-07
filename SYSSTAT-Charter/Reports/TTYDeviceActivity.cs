using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class TTYDeviceActivity : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public TTYDeviceActivity()
        {
            _propertyMap.Add("TTY", nameof(TTY));
            _propertyMap.Add("rcvin/s", nameof(RcvinPerSec));
            _propertyMap.Add("txmtin/s", nameof(TxmtinPerSec));
            _propertyMap.Add("framerr/s", nameof(FramerrPerSec));
            _propertyMap.Add("prtyerr/s", nameof(PrtyerrPerSec));
            _propertyMap.Add("brk/s", nameof(BrkPerSec));
            _propertyMap.Add("ovrun/s", nameof(OvrunPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => RcvinPerSec == 0.0 && TxmtinPerSec == 0.0 && FramerrPerSec == 0.0 && PrtyerrPerSec == 0.0 && BrkPerSec == 0.0 && OvrunPerSec == 0.0;

        public override string FilterName => "TTY";

        public override string FilterValue => TTY;

        public string TTY { get; private set; }

        /// <summary>
        /// Number of receive interrupts per second for current serial line. Serial line number is given in the TTY column.
        /// </summary>
        public double RcvinPerSec { get; private set; }

        /// <summary>
        /// Number of transmit interrupts per second for current serial line.
        /// </summary>
        public double TxmtinPerSec { get; private set; }

        /// <summary>
        /// Number of frame errors per second for current serial line.
        /// </summary>
        public double FramerrPerSec { get; private set; }

        /// <summary>
        /// Number of parity errors per second for current serial line.
        /// </summary>
        public double PrtyerrPerSec { get; private set; }

        /// <summary>
        /// Number of breaks per second for current serial line.
        /// </summary>
        public double BrkPerSec { get; private set; }

        /// <summary>
        /// Number of overrun errors per second for current serial line.
        /// </summary>
        public double OvrunPerSec { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new TTYDeviceActivity();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;

                        switch (tags[i])
                        {
                            case "TTY":
                                newValues.TTY = vals[index];
                                break;
                            case "rcvin/s":
                                newValues.RcvinPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "txmtin/s":
                                newValues.TxmtinPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "framerr/s":
                                newValues.FramerrPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "prtyerr/s":
                                newValues.PrtyerrPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "brk/s":
                                newValues.BrkPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "ovrun/s":
                                newValues.OvrunPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "rcvin/s" => RcvinPerSec,
                "txmtin/s" => TxmtinPerSec,
                "framerr/s" => FramerrPerSec,
                "prtyerr/s" => PrtyerrPerSec,
                "brk/s" => BrkPerSec,
                "ovrun/s" => OvrunPerSec,
                _ => double.NaN,
            };
        }
    }
}
