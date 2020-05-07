using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SYSSTATS_Charter
{
    public class CPUUtilization : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public CPUUtilization()
        {
            _propertyMap.Add("CPU", nameof(CPU));
            _propertyMap.Add("%usr", nameof(Usr));
            _propertyMap.Add("%nice", nameof(Nice));
            _propertyMap.Add("%system", nameof(System));
            _propertyMap.Add("%sys", nameof(Sys));
            _propertyMap.Add("%iowait", nameof(IOWait));
            _propertyMap.Add("%steal", nameof(Steal));
            _propertyMap.Add("%irq", nameof(IRQ));
            _propertyMap.Add("%soft", nameof(Soft));
            _propertyMap.Add("%guest", nameof(Guest));
            _propertyMap.Add("%gnice", nameof(Gnice));
            _propertyMap.Add("%idle", nameof(Idle));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => Usr == 0.0 && Nice == 0.0 && System == 0.0 && Sys == 0.0 && IOWait == 0.0 && Steal == 0.0 && IRQ == 0.0
                                        && Soft == 0.0 && Guest == 0.0 && Gnice == 0.0 && Idle == 0.0;

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var vals = item.Split(';');

                if (!vals.Any(x => x.StartsWith("LINUX-RESTART")))
                {
                    var newValues = new CPUUtilization();

                    newValues.FillCommonTags(vals);

                    for (var i = 0; i < tags.Length; i++)
                    {
                        var index = i + 3;

                        switch (tags[i])
                        {
                            case "CPU":
                                newValues.CPU = vals[index];
                                break;
                            case "%usr":
                                newValues.Usr = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%nice":
                                newValues.Nice = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%system":
                                newValues.System = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%sys":
                                newValues.Sys = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%iowait":
                                newValues.IOWait = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%steal":
                                newValues.Steal = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%irq":
                                newValues.IRQ = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%soft":
                                newValues.Soft = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%guest":
                                newValues.Guest = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%gnice":
                                newValues.Gnice = double.Parse(vals[index], CultureInfo.InvariantCulture);
                                break;
                            case "%idle":
                                newValues.Idle = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "%usr" => Usr,
                "%nice" => Nice,
                "%system" => System,
                "%sys" => Sys,
                "%iowait" => IOWait,
                "%steal" => Steal,
                "%irq" => IRQ,
                "%soft" => Soft,
                "%guest" => Guest,
                "%gnice" => Gnice,
                "%idle" => Idle,
                _ => double.NaN,
            };
        }

        public string CPU { get; private set; }

        /// <summary>
        /// Percentage of CPU utilization that occurred while executing at the user level (application). Note that this field does NOT include time spent running virtual processors.
        /// </summary>
        public double Usr { get; private set; }

        /// <summary>
        /// Percentage of CPU utilization that occurred while executing at the user level with nice priority.
        /// </summary>
        public double Nice { get; private set; }

        /// <summary>
        /// Percentage of CPU utilization that occurred while executing at the system level (kernel). Note that this field includes time spent servicing hardware and software interrupts.
        /// </summary>
        public double System { get; private set; }

        /// <summary>
        /// Percentage of CPU utilization that occurred while executing at the system level (kernel). Note that this field does NOT include time spent servicing hardware or software interrupts.
        /// </summary>
        public double Sys { get; private set; }

        /// <summary>
        /// Percentage of time that the CPU or CPUs were idle during which the system had an outstanding disk I/O request.
        /// </summary>
        public double IOWait { get; private set; }

        /// <summary>
        /// Percentage of time spent in involuntary wait by the virtual CPU or CPUs while the hypervisor was servicing another virtual processor.
        /// </summary>
        public double Steal { get; private set; }

        /// <summary>
        /// Percentage of time spent by the CPU or CPUs to service hardware interrupts.
        /// </summary>
        public double IRQ { get; private set; }

        /// <summary>
        /// Percentage of time spent by the CPU or CPUs to service software interrupts.
        /// </summary>
        public double Soft { get; private set; }

        /// <summary>
        /// Percentage of time spent by the CPU or CPUs to run a virtual processor.
        /// </summary>
        public double Guest { get; private set; }

        /// <summary>
        /// Percentage of time spent by the CPU or CPUs to run a niced guest.
        /// </summary>
        public double Gnice { get; private set; }

        /// <summary>
        /// Percentage of time that the CPU or CPUs were idle and the system did not have an outstanding disk I/O request.
        /// </summary>
        public double Idle { get; private set; }

        public override string FilterName => "CPU";

        public override string FilterValue => CPU;
    }
}
