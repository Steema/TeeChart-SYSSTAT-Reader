using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;
using SYSSTATS_Charter.Properties;

namespace SYSSTATS_Charter
{
    public partial class SYSSTAT_Form : Form
    {

        private readonly TChart _tChart1 = new TChart();
        private readonly ToolTip _toolTip = new ToolTip();
        private int _oldIndex = -1;

        public SYSSTAT_Form()
        {
            InitializeComponent();

            _tChart1.Dock = DockStyle.Fill;
            _tChart1.Axes.Bottom.FixedLabelSize = false;
            _tChart1.Axes.Bottom.Labels.Angle = 90;
            _tChart1.Axes.Bottom.Labels.DateTimeFormat = "g";
            _tChart1.Axes.Right.Grid.Transparency = 50;
            _tChart1.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            _tChart1.Axes.Right.Title.Font = _tChart1.Axes.Bottom.Title.Font;
            _tChart1.Axes.Right.Title.Text = "% Percentage";
            _tChart1.Axes.Right.Title.Angle = 90;
            _tChart1.Zoom.Direction = ZoomDirections.Horizontal;
            _tChart1.Panning.Allow = ScrollModes.Horizontal;
            _tChart1.Legend.CheckBoxes = true;
            _tChart1.Legend.Symbol.WidthUnits = LegendSymbolSize.Pixels;
            _tChart1.Legend.FontSeriesColor = true;
            _tChart1.MouseMove += _tChart1_MouseMove;
            _tChart1.AfterDraw += _tChart1_AfterDraw;
            _tChart1.Visible = false;

            splitContainer1.Panel2.Controls.Add(_tChart1);
        }

        private void _tChart1_AfterDraw(object sender, Graphics3D g)
        {
            if(_tChart1.Series.Count > 0)
            {
                g.Font = _tChart1.Legend.Font;
                g.Font.Color = _tChart1.Header.Color;
                g.TextOut(_tChart1.Legend.Left + _tChart1.Legend.Symbol.Width * 2, _tChart1.Legend.Top - 20, "(min; max values)");
            }
        }

        private void _tChart1_MouseMove(object sender, MouseEventArgs e)
        {
            if(cbMouseOver.Checked)
            {
                var index = _tChart1.Legend.Clicked(e.X, e.Y);
                var p = new Point(_tChart1.Legend.Left, e.Y);
                p.X += _tChart1.Parent.Left;
                p.Y += _tChart1.Parent.Top;

                if (index > -1 && index != _oldIndex)
                {
                    var title = _tChart1[index].Title;
                    title = title.Substring(0, title.IndexOf("(") - 1);

                    _toolTip.Show(DocumentationReader.GetSummary(cbReportTypes.SelectedItem.ToString(), title, _reports), this, p, 3000);
                    _oldIndex = index;
                }
                else if (index == -1)
                {
                    _oldIndex = -1;
                }
            }
        }

        private async void BOpenDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                CreateUI(await Task.Run(async () => await ParseSADF.GetSADFReports(Directory.GetFiles(folderBrowserDialog1.SelectedPath))));
            }
        }

        private Dictionary<string, List<BaseReport>> _reports;

        private void CreateUI(Dictionary<string, List<BaseReport>> reports)
        {
            _reports = reports.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

            FillReportTypes();

            gbReports.Enabled = true;
            gbChart.Enabled = true;
            _tChart1.Visible = true;
        }

        private void FillReportTypes()
        {
            var selected = cbNonZero.Checked ? _reports.Where(x => !x.Value.All(y => y.AllZero)).ToDictionary(x => x.Key, y => y.Value) : _reports;

            cbReportTypes.Items.Clear();
            cbReportTypes.Items.AddRange(selected.Keys.Select(x => x).ToArray());
            cbReportTypes.SelectedIndex = 0;
        }

        private void CbReportTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSelection(true);
        }

        private void DoSelection(bool setFilterCombo)
        {
            void SetFilter(List<BaseReport> lists)
            {
                var first = lists.First();

                if (first.HasFilter())
                {
                    lFilter.Enabled = true;
                    cbFilter.Enabled = true;
                    lFilter.Text = first.FilterName + ":";
                    cbFilter.Items.Clear();

                    if(cbNonZero.Checked)
                    {
                        cbFilter.Items.AddRange(lists.Where(x => !x.AllZero).Select(x => x.FilterValue).Distinct().ToArray());
                    }
                    else
                    {
                        cbFilter.Items.AddRange(lists.Select(x => x.FilterValue).Distinct().ToArray());
                    }
                    cbFilter.SelectedIndex = 0;
                }
                else
                {
                    cbFilter.Items.Clear();
                    cbFilter.Text = "";
                    lFilter.Text = "filter:";
                    lFilter.Enabled = false;
                    cbFilter.Enabled = false;
                }
            }
            if (_reports != null)
            {
                var selected = cbNonZero.Checked ? _reports.Where(x => !x.Value.All(y => y.AllZero)).ToDictionary(x => x.Key, y => y.Value) : _reports;

                var key = cbReportTypes.SelectedItem.ToString();
                if (selected.ContainsKey(key))
                {
                    if (setFilterCombo)
                    {
                        SetFilter(selected[key]);
                    }

                    CreateChart(key, selected[key]);
                }
            }
        }

        private void CreateChart(string key, List<BaseReport> values)
        {
            var first = values.First();
            var filterName = first.FilterName;
            var filterValue = first.FilterValue;
            var wantedFilter = "";

            if (first.HasFilter())
            {
                wantedFilter = cbFilter.SelectedItem.ToString();
            }

            void AddSeries(string tag)
            {
                var actualValues = first.HasFilter() ? values.Where(x => x.FilterValue == wantedFilter) : values;
                IEnumerable<DateTime> dates;

                if(cbUTC.Checked)
                {
                    dates = actualValues.Select(x => x.TimeStamp).Select(y => y.UtcDateTime);
                    _tChart1.Axes.Bottom.Title.Text = "UTC DateTime";
                }
                else
                {
                    dates = actualValues.Select(x => x.TimeStamp).Select(y => y.LocalDateTime);
                    _tChart1.Axes.Bottom.Title.Text = "Local DateTime";
                }

                var valors = actualValues.Select(x => x.GetValue(tag));

                if (!cbNonZero.Checked || !valors.All(x => x == 0.0))
                {
                    var minMax = $" ({valors.Min()}; {valors.Max()})";

                    if (cbUseBarSeries.Checked)
                    {
                        var bar = new Bar(_tChart1.Chart)
                        {
                            Title = tag + minMax
                        };

                        bar.XValues.DateTime = true;
                        bar.Add(dates.ToArray(), valors.ToArray());
                        bar.Add(dates.ToArray(), valors.ToArray());
                        bar.Marks.Visible = false;

                        if (tag.StartsWith("%"))
                        {
                            bar.VertAxis = VerticalAxis.Right;
                        }
                    }
                    else
                    {
                        var line = new Line(_tChart1.Chart)
                        {
                            Title = tag + minMax
                        };

                        line.LinePen.Style = (System.Drawing.Drawing2D.DashStyle)(_tChart1.Series.Count % 4);
                        line.LinePen.Width = 2;
                        line.XValues.DateTime = true;
                        line.Add(dates.ToArray(), valors.ToArray());

                        if (tag.StartsWith("%"))
                        {
                            line.VertAxis = VerticalAxis.Right;
                        }
                    }
                }
            }

            _tChart1.Series.Clear();
            _tChart1.Header.Text = key;
            _tChart1.SubHeader.Visible = false;

            if (first.HasFilter())
            {
                _tChart1.SubHeader.Text = filterName + ": " + wantedFilter;
                _tChart1.SubHeader.Visible = true;
            }


            foreach (var tag in first.PropertyMap.Keys)
            {
                if (tag != filterName)
                {
                    AddSeries(tag);
                }
            }
        }

        private void CbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoSelection(false);
        }

        private void CbNonZero_CheckedChanged(object sender, EventArgs e)
        {
            cbReportTypes.BeginUpdate();
            cbFilter.BeginUpdate();

            var reportIndex = cbReportTypes.SelectedItem;
            var filterIndex = cbFilter.SelectedItem; 

            FillReportTypes();
            DoSelection(true);

            if(reportIndex == null)
            {
                if (cbReportTypes.Items.Count > 0) cbReportTypes.SelectedIndex = 0;
            }
            else
            {
                cbReportTypes.SelectedItem = reportIndex;
            }

            if(filterIndex == null)
            {
                if (cbFilter.Items.Count > 0) cbFilter.SelectedIndex = 0;
            }
            else
            {
                cbFilter.SelectedItem = filterIndex;
            }

            cbReportTypes.EndUpdate();
            cbFilter.EndUpdate();
        }

        private void CbUseBarSeries_CheckedChanged(object sender, EventArgs e)
        {
            DoSelection(false);
        }

        private void BExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = cbReportTypes.SelectedItem.ToString() + ".jpg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _tChart1.Export.Image.JPEG.Save(saveFileDialog1.FileName);
            }
        }

        private void CbUTC_CheckedChanged(object sender, EventArgs e)
        {
            DoSelection(false);
        }
    }
}
