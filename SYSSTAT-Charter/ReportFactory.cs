using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


//http://man7.org/linux/man-pages/man1/sar.1.html

namespace SYSSTATS_Charter
{
    public class ReportFactory
    {
        public static Tuple<string, List<BaseReport>> GetReport(string tag, List<string> values)
        {
            var tags = tag.Split(";").Skip(3).ToArray();

            if (BaseReport.TryGetReports<SwappingStatistics>(tags, values, out var swappingStatistics))
            {
                return new Tuple<string, List<BaseReport>>(nameof(SwappingStatistics), swappingStatistics);
            }
            else if (BaseReport.TryGetReports<TaskCreationSwitching>(tags, values, out var taskCreationSwitching))
            {
                return new Tuple<string, List<BaseReport>>(nameof(TaskCreationSwitching), taskCreationSwitching);
            }
            else if (BaseReport.TryGetReports<CPUUtilization>(tags, values, out var cpuUtilization))
            {
                return new Tuple<string, List<BaseReport>>(nameof(CPUUtilization), cpuUtilization);
            }
            else if (BaseReport.TryGetReports<NetworkStatistics>(tags, values, out var networkStatistics))
            {
                return new Tuple<string, List<BaseReport>>(nameof(NetworkStatistics), networkStatistics);
            }
            else if (BaseReport.TryGetReports<PagingStatistics>(tags, values, out var pagingStatistics))
            {
                return new Tuple<string, List<BaseReport>>(nameof(PagingStatistics), pagingStatistics);
            }
            else if (BaseReport.TryGetReports<TransferRateStatistics>(tags, values, out var transferRateStatistics))
            {
                return new Tuple<string, List<BaseReport>>(nameof(TransferRateStatistics), transferRateStatistics);
            }
            else if (BaseReport.TryGetReports<MemoryUtilization>(tags, values, out var memoryUtilization))
            {
                return new Tuple<string, List<BaseReport>>(nameof(MemoryUtilization), memoryUtilization);
            }
            else if (BaseReport.TryGetReports<SwapSpaceUtilization>(tags, values, out var swapSpaceUtlization))
            {
                return new Tuple<string, List<BaseReport>>(nameof(SwapSpaceUtilization), swapSpaceUtlization);
            }
            else if (BaseReport.TryGetReports<HugePagesUtilization>(tags, values, out var hugePagesUtilization))
            {
                return new Tuple<string, List<BaseReport>>(nameof(HugePagesUtilization), hugePagesUtilization);
            }
            else if (BaseReport.TryGetReports<KernelTablesStatus>(tags, values, out var kernelTablesStatus))
            {
                return new Tuple<string, List<BaseReport>>(nameof(KernelTablesStatus), kernelTablesStatus);
            }
            else if (BaseReport.TryGetReports<QueueLoadStatistics>(tags, values, out var queueLoadStatistics))
            {
                return new Tuple<string, List<BaseReport>>(nameof(QueueLoadStatistics), queueLoadStatistics);
            }
            else if (BaseReport.TryGetReports<TTYDeviceActivity>(tags, values, out var ttyDeviceActivity))
            {
                return new Tuple<string, List<BaseReport>>(nameof(TTYDeviceActivity), ttyDeviceActivity);
            }
            else if (BaseReport.TryGetReports<BlockDeviceActivity>(tags, values, out var blockDeviceActivity))
            {
                return new Tuple<string, List<BaseReport>>(nameof(BlockDeviceActivity), blockDeviceActivity);
            }
            else if (BaseReport.TryGetReports<NetworkErrors>(tags, values, out var networkErrors))
            {
                return new Tuple<string, List<BaseReport>>(nameof(NetworkErrors), networkErrors);
            }
            else if (BaseReport.TryGetReports<NFSClientActivity>(tags, values, out var nfsClientActivity))
            {
                return new Tuple<string, List<BaseReport>>(nameof(NFSClientActivity), nfsClientActivity);
            }
            else if (BaseReport.TryGetReports<NFSServerActivity>(tags, values, out var nfsServerActivity))
            {
                return new Tuple<string, List<BaseReport>>(nameof(NFSServerActivity), nfsServerActivity);
            }
            else if (BaseReport.TryGetReports<SocketStatistics>(tags, values, out var socketStatistics))
            {
                return new Tuple<string, List<BaseReport>>(nameof(SocketStatistics), socketStatistics);
            }
            else if (BaseReport.TryGetReports<SoftwareNetwork>(tags, values, out var softwareNetwork))
            {
                return new Tuple<string, List<BaseReport>>(nameof(SoftwareNetwork), softwareNetwork);
            }

            return null;
        }
    }
}
