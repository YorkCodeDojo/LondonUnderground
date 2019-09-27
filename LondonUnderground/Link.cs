using System;

namespace LondonUnderground
{
    class Link
    {
        public string LineName { get; set; }
        public Station Station1 { get; set; }
        public Station Station2 { get; set; }
        public int DurationInMinutes { get; set; }

        internal Station OtherEnd(Station station) => station == Station1 ? Station2 : Station1;

    }
}
