using System;
using System.Collections.Generic;
using System.Text;

namespace KillerAppClassLibrary.Classes
{
    public class Track
    {
        public int Id { get; }
        public string ArtistName { get; }
        public string TrackName { get; }
        public string Label { get; }
        public int Price { get; }

        //Used when creating a new track.
        public Track(string artistName, string trackName, string label, int price)
        {
            ArtistName = artistName;
            TrackName = trackName;
            Label = label;
            Price = price;
        }

        //Used when getting a track from the database.
        public Track(int id, string artistName, string trackName, string label, int price)
        {
            Id = id;
            ArtistName = artistName;
            TrackName = trackName;
            Label = label;
            Price = price;
        }
    }
}
