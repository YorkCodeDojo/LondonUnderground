using System;

namespace LondonUnderground
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();

            graph.LoadFromFile("TestStationsAndLines.txt");

            graph.DisplayLinksFrom(Console.Out, "Waterloo");

            graph.WorkOutCosts(Console.Out, "Waterloo", "Pimlico");
        }
    }
}
