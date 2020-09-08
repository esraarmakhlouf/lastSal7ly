using System;
using System.Collections.Generic;
using System.Text;

namespace Model.CustomModels
{
    public class RegionViewModel
    {

        public List<Point> LstOfPoints { set; get; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public bool IsCycle { get; set; }

        public double Radius { get; set; }
        public double CenterLat { get; set; }
        public double CenterLong { get; set; }

    }
    public class Point
    {
        public double lat { get; set; }
        public double lng { get; set; }

    }
}
