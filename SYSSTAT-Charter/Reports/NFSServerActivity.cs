using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SYSSTATS_Charter
{
    public class NFSServerActivity : BaseReport
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public NFSServerActivity()
        {
            _propertyMap.Add("scall/s", nameof(ScallPerSec));
            _propertyMap.Add("badcall/s", nameof(BadcallPerSec));
            _propertyMap.Add("packet/s", nameof(PacketPerSec));
            _propertyMap.Add("udp/s", nameof(UdpPerSec));
            _propertyMap.Add("tcp/s", nameof(TcpPerSec));
            _propertyMap.Add("hit/s", nameof(HitPerSEc));
            _propertyMap.Add("miss/s", nameof(MissPerSec));
            _propertyMap.Add("sread/s", nameof(SreadPerSec));
            _propertyMap.Add("swrite/s", nameof(SwritePerSec));
            _propertyMap.Add("saccess/s", nameof(SaccessPerSec));
            _propertyMap.Add("sgetatt/s", nameof(SgetattPerSec));
        }

        public override Dictionary<string, string> PropertyMap => _propertyMap;

        public override bool AllZero => ScallPerSec == 0.0 && BadcallPerSec == 0.0 && PacketPerSec == 0.0 && UdpPerSec == 0.0 && TcpPerSec == 0.0 && HitPerSEc == 0.0 && MissPerSec == 0.0
                                        && SreadPerSec == 0.0 && SwritePerSec == 0.0 && SaccessPerSec == 0.0 && SgetattPerSec == 0.0;

        public override string FilterName => null;

        public override string FilterValue => null;

        /// <summary>
        /// Number of RPC requests received per second.
        /// </summary>
        public double ScallPerSec { get; private set; }

        /// <summary>
        /// Number of bad RPC requests received per second, those whose processing generated an error.
        /// </summary>
        public double BadcallPerSec { get; private set; }

        /// <summary>
        /// Number of network packets received per second.
        /// </summary>
        public double PacketPerSec { get; private set; }

        /// <summary>
        /// Number of UDP packets received per second.
        /// </summary>
        public double UdpPerSec { get; private set; }

        /// <summary>
        /// Number of TCP packets received per second.
        /// </summary>
        public double TcpPerSec { get; private set; }

        /// <summary>
        /// Number of reply cache hits per second.
        /// </summary>
        public double HitPerSEc { get; private set; }

        /// <summary>
        /// Number of reply cache misses per second.
        /// </summary>
        public double MissPerSec { get; private set; }

        /// <summary>
        /// Number of 'read' RPC calls received per second.
        /// </summary>
        public double SreadPerSec { get; private set; }

        /// <summary>
        /// Number of 'write' RPC calls received per second.
        /// </summary>
        public double SwritePerSec { get; private set; }

        /// <summary>
        /// Number of 'access' RPC calls received per second.
        /// </summary>
        public double SaccessPerSec { get; private set; }

        /// <summary>
        /// Number of 'getattr' RPC calls received per second.
        /// </summary>
        public double SgetattPerSec { get; private set; }

        public override List<BaseReport> GetReports(string[] tags, List<string> values)
        {
            var result = new List<BaseReport>();

            foreach (var item in values)
            {
                var newValues = new NFSServerActivity();

                var vals = item.Split(';');
                newValues.FillCommonTags(vals);

                for (var i = 0; i < tags.Length; i++)
                {
                    var index = i + 3;
                    switch (tags[i])
                    {
                        case "scall/s":
                            newValues.ScallPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "badcall/s":
                            newValues.BadcallPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "packet/s":
                            newValues.PacketPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "udp/s":
                            newValues.UdpPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "tcp/s":
                            newValues.TcpPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "hit/s":
                            newValues.HitPerSEc = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "miss/s":
                            newValues.MissPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "sread/s":
                            newValues.SreadPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "swrite/s":
                            newValues.SwritePerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "saccess/s":
                            newValues.SaccessPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
                            break;
                        case "sgetatt/s":
                            newValues.SgetattPerSec = double.Parse(vals[index], CultureInfo.InvariantCulture);
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
                "scall/s" => ScallPerSec,
                "badcall/s" => BadcallPerSec,
                "packet/s" => PacketPerSec,
                "udp/s" => UdpPerSec,
                "tcp/s" => TcpPerSec,
                "hit/s" => HitPerSEc,
                "miss/s" => MissPerSec,
                "sread/s" => SreadPerSec,
                "swrite/s" => SwritePerSec,
                "saccess/s" => SaccessPerSec,
                "sgetatt/s" => SgetattPerSec,
                _ => double.NaN,
            };
        }
    }
}
