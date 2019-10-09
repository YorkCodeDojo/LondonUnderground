# York Code Dojo - London Underground Map

Tonights meeting is based around the London Undergroup Map.  It's your groups choice what you do,  but some suggested exercises are as follows

1. Given a station,  display the names (and lines) of all stations which are one stop away.

2. Given 2 stations find a route between them.

3. Now find the shortest route between them,  assuming that all stations are 1 minute apart and there is no overhead in changing lines.

4. Again find the shortest route, this time taking into the account that it takes different times to travel between different stations.

5. Finally,  also take into account that changing lines takes on average 5 minutes.

A list of lines and stations is available in `StationsAndLines.txt` (Be careful of the Northern Line!)

## Possible solution

* Parse the `StationsAndLines.txt` file to create a graph of Stations and Links.

* The properties of a `Station` would be

  * Station Name
  * Array/list of `Links`

* The properties of a `Link` would be

  * Line Name
  * Station 1
  * Station 2

* This will allow you to solve Exercise 1

* Exercises 2,3,4  can all be solved using Dijkstra's shortest path algorithm

* Exercise 5 is a bit more challenging.  

## Alternatives

Register on https://api-portal.tfl.gov.uk/login and create a service using the data from their APIs.

They have a dataset provides a 5% sample of all Oyster card journeys performed in a week during November 2009 on bus, Tube, DLR and London Overground. http://tfl.gov.uk/tfl/syndication/feeds/oystercardjourneyinformation.zip
