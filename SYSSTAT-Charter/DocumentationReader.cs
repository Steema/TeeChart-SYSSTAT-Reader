using Steema.TeeChart.Languages;
using SYSSTATS_Charter.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SYSSTATS_Charter
{
    public static class DocumentationReader
    {
        private static XElement _s_xElement;

        static DocumentationReader()
        {
            _s_xElement = XElement.Parse(Resources.SYSSTAT_Charter);
        }

        public static string GetSummary(string className, string tag, Dictionary<string, List<BaseReport>> reports)
        {
            if(reports.ContainsKey(className))
            {
                var firstPair = reports.FirstOrDefault(x => x.Key == className);

                var firstValue = firstPair.Value.FirstOrDefault();

                if(firstValue.PropertyMap.ContainsKey(tag))
                {
                    var propertyName = firstValue.PropertyMap[tag];

                    var elements = _s_xElement.Descendants("member").FirstOrDefault(x => x.Attribute("name").Value.Contains(className) && x.Attribute("name").Value.Contains(propertyName));

                    var doc = elements.Element("summary").Value.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < doc.Length; i++)
                    {
                        doc[i] = doc[i].Trim();
                    }

                    return tag + Environment.NewLine + string.Join(Environment.NewLine, doc);
                }
            }

            return null;
        }
    }
}
