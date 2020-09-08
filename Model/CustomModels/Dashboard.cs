using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Dashboard
    {
        public int Technicians { get; set; }
        public int Services { get; set; }
        public int sales { get; set; }
        public int NewMembers { get; set; }
        public List<TechnicalsVM> TechnicalsVM { get; set; }
        public List<CountryVM> CountryVM { get; set; }
        public List<ItemsVM> ItemsVM { get; set; }
        public List<ServeicesVM> ServeicesVM { get; set; }
        public ServeicesChart ServeicesChart { get; set; }
        public ItemesChart ItemesChart { get; set; }

    }

    public class ItemesChart
    {
        public List<double> ThisWeek { get; set; }
        public List<double> PastWeek { get; set; }

    }
    public class ServeicesChart
    {
        public List<double> ThisWeek { get; set; }
        public List<double> PastWeek { get; set; }

    }
    public class CountryVM
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public int Total { get; set; }

    }

    public class ItemsVM
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ImagePath { get; set; }

        public double Price { get; set; }
        public double Sales { get; set; }
    }

    public class ServeicesVM
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public double Price { get; set; }
        public double Sales { get; set; }
    }
}
