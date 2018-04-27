using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.DTO.Reports
{
    public class ChartData
    {
        public string Title { get; set; }
        public string YLabel { get; set; }
        public string XLabel { get; set; }
        public List<ChartSeries> SeriesData { get; set; }
    }
}
