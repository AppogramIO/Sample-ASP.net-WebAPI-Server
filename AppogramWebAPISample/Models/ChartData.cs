using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppogramWebAPISample.Models
{
    public class BarChart
    {
        public string name { get; set; }
        public IEnumerable<string> setColor { get; set; }
        public IEnumerable<Point> points { get; set; }
    }

    public class PieChart
    {
        public string name { get; set; }
        public IEnumerable<string> setColor { get; set; }
        public IEnumerable<Point> points { get; set; }
    }

    public class RadarChart
    {
        public string name { get; set; }
        public string fillColor { get; set; }
        public string borderColor { get; set; }
        public IEnumerable<Point> points { get; set; }
    }

    public class LinearChart
    {
        public string name { get; set; }
        public string startColor { get; set; }
        public string endColor { get; set; }
        public string circleColor { get; set; }
        public string borderColor { get; set; }
        public IEnumerable<Point> points { get; set; }
    }

    public class Point
    {
        public string x { get; set; }
        public string y { get; set; }
    }

    public class BarChartData
    {
        public IEnumerable<BarChart> datasets { get; set; }
    }

    public class PieChartData
    {
        public IEnumerable<PieChart> datasets { get; set; }
    }
    public class RadarChartData
    {
        public IEnumerable<RadarChart> datasets { get; set; }
    }
    public class LinearChartData
    {
        public IEnumerable<LinearChart> datasets { get; set; }
    }

    public class GridviewData
    {
        public IEnumerable<IEnumerable<string>> data { get; set; }
    }

    public class AppoDatalist
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string right { get; set; }
        public string left { get; set; }
        public string imageIcon { get; set; }
    }
    public class ItemListData
    {
        public int page { get; set; }
        public IEnumerable<AppoDatalist> items { get; set; }
        public int totalPages { get; set; }
        public string toast { get; set; }
    }

    public class AppoImagelist
    {
        public string id { get; set; }
        public string caption { get; set; }
        public string subtitle { get; set; }
        public string imageSource { get; set; }
    }
    public class ImageListData
    {
        public int page { get; set; }
        public IEnumerable<AppoImagelist> items { get; set; }
        public int totalPages { get; set; }
        public string toast { get; set; }
    }
}