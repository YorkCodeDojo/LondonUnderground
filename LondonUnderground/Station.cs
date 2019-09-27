using System.Collections.Generic;

namespace LondonUnderground
{
    class Station
    {
        public string StationName { get; set; }

        public List<Link> Links { get; set; }

        public string BestLine { get; set; }
        public int Cost { get; set; }

    }
}
