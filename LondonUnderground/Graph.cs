using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LondonUnderground
{
    class Graph
    {
        Dictionary<string, Station> stations = new Dictionary<string, Station>();

        public void WorkOutCosts(TextWriter textWriter, string startStationName, string endStationName)
        {
            ResetCosts();

            var startStation = GetStation(startStationName);
            var endStation = GetStation(endStationName);
            endStation.Cost = 0;

            WorkOutCosts(startStation, endStation);

            DisplayRoute(textWriter, startStation, endStation,0);
        }

        private void DisplayRoute(TextWriter textWriter, Station startStation, Station endStation, int cost)
        {
            var bestLink = default(Link);
            var bestScore = int.MaxValue;
            foreach (var link in startStation.Links)
            {
                var otherEnd = link.OtherEnd(startStation);
                if (otherEnd.Cost < bestScore)
                {
                    bestScore = otherEnd.Cost;
                    bestLink = link;
                }
            }

            var goingTo = bestLink.OtherEnd(startStation);
            textWriter.WriteLine($"{startStation.StationName} to {goingTo.StationName} on the {bestLink.LineName} line.  Cost was {cost}");

            if (goingTo != endStation)
            {
                DisplayRoute(textWriter, goingTo, endStation, bestScore);
            }

        }

        private void ResetCosts()
        {
            foreach (var station in stations)
            {
                station.Value.Cost = int.MaxValue;
                station.Value.BestLine = string.Empty;
            }
        }

        private void WorkOutCosts(Station startStation, Station endStation)
        {
            if (startStation.StationName != endStation.StationName)
            {
                foreach (var link in endStation.Links)
                {
                    var cost = endStation.Cost + link.DurationInMinutes;
                    if (link.LineName != endStation.BestLine)
                        cost += 5;

                    var otherStation = link.OtherEnd(endStation);
                    if (otherStation.Cost > cost)
                    {
                        otherStation.Cost = cost;
                        otherStation.BestLine = link.LineName;
                        WorkOutCosts(startStation, otherStation);
                    }
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            var entries = File.ReadAllLines(filename);
            var index = 0;

            while (index < entries.Length)
            {
                var lineName = entries[index].Replace("Line: ", "", StringComparison.InvariantCulture);
                index++;

                var stations = new List<string>();
                while (index < entries.Length && !entries[index].StartsWith("Line: ", StringComparison.InvariantCulture))
                {
                    stations.Add(entries[index]);
                    index++;
                }

                AddLine(lineName, stations);
            }
        }

        private void AddLine(string lineName, List<string> stations)
        {
            var previousStation = default(Station);
            for (int i = 0; i < stations.Count(); i++)
            {
                var newStation = GetStation(stations[i]);

                if (previousStation != null)
                {
                    AddLink(previousStation, newStation, lineName);
                }

                previousStation = newStation;
            }

        }

        internal void DisplayLinksFrom(TextWriter textWriter, string stationName)
        {
            var station = GetStation(stationName);

            textWriter.WriteLine($"From {stationName} you can get to:");
            foreach (var link in station.Links)
            {
                var otherStation = link.OtherEnd(station);
                textWriter.WriteLine($"     {otherStation.StationName} on the {link.LineName} line.");
            }
        }

        private static void AddLink(Station nextStation, Station previousStation, string lineName)
        {
            var link = new Link() { LineName = lineName, Station1 = nextStation, Station2 = previousStation, DurationInMinutes = 2 };
            nextStation.Links.Add(link);
            previousStation.Links.Add(link);
        }

        private Station GetStation(string stationName)
        {
            if (!stations.TryGetValue(stationName, out var station))
            {
                station = new Station { StationName = stationName, Links = new List<Link>() };
                stations.Add(stationName, station);
            }

            return station;
        }
    }
}
