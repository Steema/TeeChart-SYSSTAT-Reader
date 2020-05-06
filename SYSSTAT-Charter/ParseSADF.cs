using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SYSSTATS_Charter
{
    public class ParseSADF
    {
        public static async Task<Dictionary<string, List<BaseReport>>> GetSADFReports(string[] files)
        {
            var sadf = new ParseSADF(files);
            return sadf.TransformDictionary(await sadf.CreateDictionary());
        }

        private readonly string[] _files;
        public ParseSADF(string[] files)
        {
            _files = files;
        }

        public Dictionary<string, List<BaseReport>> TransformDictionary(Dictionary<string, List<string>> keyValuePairs)
        {
            var result = new Dictionary<string, List<BaseReport>>();

            foreach (var item in keyValuePairs)
            {
                var report = ReportFactory.GetReport(item.Key, item.Value);

                if (report != null)
                {
                    result.Add(report.Item1, report.Item2);
                }
            }

            return result;
        }


        public async Task<Dictionary<string, List<string>>> CreateDictionary()
        {
            var result = new Dictionary<string, List<string>>();
            foreach (var file in _files)
            {
                var lines = await File.ReadAllLinesAsync(file);

                var categories = lines.Select((y, i) => new { Line = y, Index = i }).Where(x => x.Line.StartsWith("#"));

                var lastIndex = lines.Count();

                for (var i = categories.Count() - 1; i >= 0; i--)
                {
                    var cat = categories.ElementAt(i);

                    var series = lines.Where((x, j) => (j < lastIndex && j > cat.Index));

                    if (result.ContainsKey(cat.Line))
                    {
                        var list = result[cat.Line];
                        list.AddRange(series.ToList());
                    }
                    else
                    {
                        result.Add(cat.Line, series.ToList());
                    }

                    lastIndex = cat.Index;
                }
            }
            return result;
        }
    }
}
