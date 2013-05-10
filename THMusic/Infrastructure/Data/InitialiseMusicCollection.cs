//***************************************************************************************************
//Name of File:     InitiliaseMusicCollection.cs
//Description:      It seeds the In-Memory MusicCollection context with data.  Only for the first time.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;

using Core.Model;
using Core.Interfaces;
using Core.Factories;

namespace Infrastructure.Data
{
    /// <summary>
    /// This <c>InitialiseMusicCollection</c> class has the responsibility of seeding
    /// the application with sample / initial data.  Once the app has been run for the
    /// first time, the data is persisted within the underlying Xml file, and the
    /// seeding stops.
    /// </summary>
    /// <remarks>
    /// This has deliberately not been made an async method, as it wil execute extremely
    /// quickly, and is called from the constructor of the MusicCollection context, to 
    /// initialise the collections with a minimal amount of sample data.  it should only
    /// ever be called once, unlike the load method which is called on every application
    /// start up.
    /// </remarks>
    public static class InitialiseMusicCollection
    {
        /// <summary>
        /// Initialises the In-Memory context with initial data.
        /// </summary>
        /// <param name="UnitOfWork">Instance of the in-memory context.</param>
        /// <param name="ArtistFactory">Instance of the factory to create Artist classes.</param>
        /// <param name="AlbumFactory">Instance of the factory to create Album classes.</param>
        /// <param name="TrackFactory">Instance of the factory to create Track classes.</param>
        /// <param name="ImageFactory">Instance of the factory to create Image classes.</param>
        /// <param name="GenreFactory">Instance of the factory to create Genre classes.</param>
        /// <param name="PlaylistFactory">Instance of the factory to create Playlist classes.</param>
        /// <param name="WikiFactory">Instance of the factory to create Wiki classes.</param>
        public static void  SeedData(IUnitOfWork UnitOfWork,
            AbstractFactory<Artist> ArtistFactory,
            AbstractFactory<Album> AlbumFactory,
            AbstractFactory<Track> TrackFactory,
            AbstractFactory<Image> ImageFactory,
            AbstractFactory<Genre> GenreFactory,
            AbstractFactory<PlayList> PlaylistFactory,
            AbstractFactory<Wiki> WikiFactory)
        {
            //  ******************* Create the Artists ***************************************
            var pinkFloyd = ArtistFactory.Create();     //  Create a new artist.
            //var pinkFloyd = new Artist();     //  Create a new artist.
            pinkFloyd.Id = 1;
            pinkFloyd.Name = "Pink Floyd";
            pinkFloyd.Albums = new List<Album>();
            //pinkFloyd.Albums = new List<int>();
            pinkFloyd.AlbumIds = new List<int>();

            var Opeth = ArtistFactory.Create();
            //var Opeth = new Artist();
            Opeth.Id = 2;
            Opeth.Name = "Opeth";
            Opeth.Albums = new List<Album>();
            //Opeth.Albums = new List<int>();
            Opeth.AlbumIds = new List<int>();

            //  ******************* Create the Playlists  ************************************
            var FloydFavourites = PlaylistFactory.Create();
            //var FloydFavourites = new PlayList();
            FloydFavourites.Id = 1;
            FloydFavourites.Name = "Pink Floyd Favourites";
            FloydFavourites.Tracks = new List<Track>();
            //FloydFavourites.Tracks = new List<int>();
            FloydFavourites.TrackIds = new List<int>();

            var OpethFavourites = PlaylistFactory.Create();
            //var OpethFavourites = new PlayList();
            OpethFavourites.Id = 2;
            OpethFavourites.Name = "Opeth Favourites";
            OpethFavourites.Tracks = new List<Track>();
            //OpethFavourites.Tracks = new List<int>();
            OpethFavourites.TrackIds = new List<int>();


            //  ******************* Create the Albums  ***************************************
            var DarkSideOfTheMoon = AlbumFactory.Create();
            //var DarkSideOfTheMoon = new Album();
            DarkSideOfTheMoon.Id = 1;
            DarkSideOfTheMoon.Title = "Dark Side of the Moon";
            DarkSideOfTheMoon.Artist = pinkFloyd;
            DarkSideOfTheMoon.Released = new DateTime(1973, 06, 01);

            DarkSideOfTheMoon.Tracks = new List<Track>();
            DarkSideOfTheMoon.Images = new List<Image>();
            DarkSideOfTheMoon.Genres = new List<Genre>();
            DarkSideOfTheMoon.Wiki = WikiFactory.Create();
            //DarkSideOfTheMoon.Wiki = new Wiki();

            var Damnation = AlbumFactory.Create();
            //var Damnation = new Album();
            Damnation.Id = 2;
            Damnation.Title = "Damnation";
            Damnation.Artist = Opeth;
            Damnation.Released = new DateTime(2003, 01, 01);

            Damnation.Tracks = new List<Track>();
            Damnation.Images = new List<Image>();
            Damnation.Genres = new List<Genre>();
            Damnation.Wiki = WikiFactory.Create();
            //Damnation.Wiki = new Wiki();


            var WishYouWereHere = AlbumFactory.Create();
            //var WishYouWereHere = new Album();
            WishYouWereHere.Id = 3;
            WishYouWereHere.Title = "Wish You Were Here";
            WishYouWereHere.Artist = pinkFloyd;
            WishYouWereHere.Released = new DateTime(1975, 06, 30);

            WishYouWereHere.Tracks = new List<Track>();
            WishYouWereHere.Images = new List<Image>();
            WishYouWereHere.Genres = new List<Genre>();
            WishYouWereHere.Wiki = WikiFactory.Create();
            //WishYouWereHere.Wiki = new Wiki();


            //  ******************* Create the Wikis  ***************************************
            var dsotmWiki = WikiFactory.Create();
            //var dsotmWiki = new Wiki();
            dsotmWiki.Id = 1;
            dsotmWiki.Summary = "Dark Side Wiki Summary";
            dsotmWiki.Content = "Dark Side Wiki Content";
            dsotmWiki.Published = new DateTime(1990, 01, 01);

            var damWiki = WikiFactory.Create();
            //var damWiki = new Wiki();
            damWiki.Id = 2;
            damWiki.Summary = "Damnation Wiki Summary";
            damWiki.Content = "Damnation Wiki Content";
            damWiki.Published = new DateTime(2004, 01, 01);


            var wywhWiki = WikiFactory.Create();
            //var wywhWiki = new Wiki();
            wywhWiki.Id = 3;
            wywhWiki.Summary = "Wish You Were Here Wiki Summary";
            wywhWiki.Content = "Wish You Were Here Wiki Content";
            wywhWiki.Published = new DateTime(1990, 06, 01);


            //  Add wiki to albums
            DarkSideOfTheMoon.Wiki = dsotmWiki;
            Damnation.Wiki = damWiki;
            WishYouWereHere.Wiki = wywhWiki;

            //  ******************* Create the Genres  ***************************************
            var pop = GenreFactory.Create();
            //var pop = new Genre();
            pop.Id = 1;
            pop.Name = "Pop";
            pop.Url = string.Empty;
            pop.Albums = new List<Album>();
            //pop.Albums = new List<int>();
            pop.AlbumIds = new List<int>();

            var rock = GenreFactory.Create();
            //var rock = new Genre();
            rock.Id = 2;
            rock.Name = "Rock";
            rock.Url = string.Empty;
            rock.Albums = new List<Album>();
            //rock.Albums = new List<int>();
            rock.AlbumIds = new List<int>();

            var seventies = GenreFactory.Create();
            //var seventies = new Genre();
            seventies.Id = 3;
            seventies.Name = "70's";
            seventies.Url = string.Empty;
            seventies.Albums = new List<Album>();
            //seventies.Albums = new List<int>();
            seventies.AlbumIds = new List<int>();


            //  ******************* Create the Tracks  ***************************************
            var dsotmTrack1 = TrackFactory.Create();
            //var dsotmTrack1 = new Track();
            dsotmTrack1.Id = 1;
            dsotmTrack1.Title = "Speak to Me/Breathe";
            dsotmTrack1.Number = 1;
            dsotmTrack1.Duration = TimeSpan.Parse("00:03:57");
            dsotmTrack1.Album = DarkSideOfTheMoon;
            dsotmTrack1.Artist = pinkFloyd;
            dsotmTrack1.Playlists = new List<PlayList>();
            dsotmTrack1.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack2 = TrackFactory.Create();
            //var dsotmTrack2 = new Track();
            dsotmTrack2.Id = 2;
            dsotmTrack2.Title = "On the Run";
            dsotmTrack2.Number = 2;
            dsotmTrack2.Duration = TimeSpan.Parse("00:03:35");
            dsotmTrack2.Album = DarkSideOfTheMoon;
            dsotmTrack2.Artist = pinkFloyd;
            dsotmTrack2.Playlists = new List<PlayList>();
            dsotmTrack2.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack3 = TrackFactory.Create();
            //var dsotmTrack3 = new Track();
            dsotmTrack3.Id = 3;
            dsotmTrack3.Title = "Time";
            dsotmTrack3.Number = 3;
            dsotmTrack3.Duration = TimeSpan.Parse("00:07:04");
            dsotmTrack3.Album = DarkSideOfTheMoon;
            dsotmTrack3.Artist = pinkFloyd;
            dsotmTrack3.Playlists = new List<PlayList>();
            dsotmTrack3.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack4 = TrackFactory.Create();
            //var dsotmTrack4 = new Track();
            dsotmTrack4.Id = 4;
            dsotmTrack4.Title = "The Great Gig in the Sky";
            dsotmTrack4.Number = 4;
            dsotmTrack4.Duration = TimeSpan.Parse("00:04:47");
            dsotmTrack4.Album = DarkSideOfTheMoon;
            dsotmTrack4.Artist = pinkFloyd;
            dsotmTrack4.Playlists = new List<PlayList>();
            dsotmTrack4.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack5 = TrackFactory.Create();
            //var dsotmTrack5 = new Track();
            dsotmTrack5.Id = 5;
            dsotmTrack5.Title = "Money";
            dsotmTrack5.Number = 5;
            dsotmTrack5.Duration = TimeSpan.Parse("00:06:22");
            dsotmTrack5.Album = DarkSideOfTheMoon;
            dsotmTrack5.Artist = pinkFloyd;
            dsotmTrack5.Playlists = new List<PlayList>();
            dsotmTrack5.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack6 = TrackFactory.Create();
            //var dsotmTrack6 = new Track();
            dsotmTrack6.Id = 6;
            dsotmTrack6.Title = "Us and Them";
            dsotmTrack6.Number = 6;
            dsotmTrack6.Duration = TimeSpan.Parse("00:07:50");
            dsotmTrack6.Album = DarkSideOfTheMoon;
            dsotmTrack6.Artist = pinkFloyd;
            dsotmTrack6.Playlists = new List<PlayList>();
            dsotmTrack6.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack7 = TrackFactory.Create();
            //var dsotmTrack7 = new Track();
            dsotmTrack7.Id = 7;
            dsotmTrack7.Title = "Any Colour You Like";
            dsotmTrack7.Number = 7;
            dsotmTrack7.Duration = TimeSpan.Parse("00:03:25");
            dsotmTrack7.Album = DarkSideOfTheMoon;
            dsotmTrack7.Artist = pinkFloyd;
            dsotmTrack7.Playlists = new List<PlayList>();
            dsotmTrack7.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack8 = TrackFactory.Create();
            //var dsotmTrack8 = new Track();
            dsotmTrack8.Id = 8;
            dsotmTrack8.Title = "Brain Damage";
            dsotmTrack8.Number = 8;
            dsotmTrack8.Duration = TimeSpan.Parse("00:03:50");
            dsotmTrack8.Album = DarkSideOfTheMoon;
            dsotmTrack8.Artist = pinkFloyd;
            dsotmTrack8.Playlists = new List<PlayList>();
            dsotmTrack8.AlbumId = DarkSideOfTheMoon.Id;

            var dsotmTrack9 = TrackFactory.Create();
            //var dsotmTrack9 = new Track();
            dsotmTrack9.Id = 9;
            dsotmTrack9.Title = "Eclipse";
            dsotmTrack9.Number = 9;
            dsotmTrack9.Duration = TimeSpan.Parse("00:02:01");
            dsotmTrack9.Album = DarkSideOfTheMoon;
            dsotmTrack9.Artist = pinkFloyd;
            dsotmTrack9.Playlists = new List<PlayList>();
            dsotmTrack9.AlbumId = DarkSideOfTheMoon.Id;


            var DamTrack1 = TrackFactory.Create();
            //var DamTrack1 = new Track();
            DamTrack1.Id = 10;
            DamTrack1.Title = "Windowpane";
            DamTrack1.Number = 1;
            DamTrack1.Duration = TimeSpan.Parse("00:07:44");
            DamTrack1.Album = Damnation;
            DamTrack1.Artist = Opeth;
            DamTrack1.Playlists = new List<PlayList>();
            DamTrack1.AlbumId = Damnation.Id;

            var DamTrack2 = TrackFactory.Create();
            //var DamTrack2 = new Track();
            DamTrack2.Id = 11;
            DamTrack2.Title = "In My Time Of Need";
            DamTrack2.Number = 2;
            DamTrack2.Duration = TimeSpan.Parse("00:05:49");
            DamTrack2.Album = Damnation;
            DamTrack2.Artist = Opeth;
            DamTrack2.Playlists = new List<PlayList>();
            DamTrack2.AlbumId = Damnation.Id;

            var DamTrack3 = TrackFactory.Create();
            //var DamTrack3 = new Track();
            DamTrack3.Id = 12;
            DamTrack3.Title = "Death Whispered A Lullaby";
            DamTrack3.Number = 3;
            DamTrack3.Duration = TimeSpan.Parse("00:05:49");
            DamTrack3.Album = Damnation;
            DamTrack3.Artist = Opeth;
            DamTrack3.Playlists = new List<PlayList>();
            DamTrack3.AlbumId = Damnation.Id;

            var DamTrack4 = TrackFactory.Create();
            //var DamTrack4 = new Track();
            DamTrack4.Id = 13;
            DamTrack4.Title = "Closure";
            DamTrack4.Number = 4;
            DamTrack4.Duration = TimeSpan.Parse("00:05:15");
            DamTrack4.Album = Damnation;
            DamTrack4.Artist = Opeth;
            DamTrack4.Playlists = new List<PlayList>();
            DamTrack4.AlbumId = Damnation.Id;

            var DamTrack5 = TrackFactory.Create();
            //var DamTrack5 = new Track();
            DamTrack5.Id = 14;
            DamTrack5.Title = "Hope Leaves";
            DamTrack5.Number = 5;
            DamTrack5.Duration = TimeSpan.Parse("00:04:30");
            DamTrack5.Album = Damnation;
            DamTrack5.Artist = Opeth;
            DamTrack5.Playlists = new List<PlayList>();
            DamTrack5.AlbumId = Damnation.Id;

            var DamTrack6 = TrackFactory.Create();
            //var DamTrack6 = new Track();
            DamTrack6.Id = 15;
            DamTrack6.Title = "To Rid The Disease";
            DamTrack6.Number = 6;
            DamTrack6.Duration = TimeSpan.Parse("00:06:21");
            DamTrack6.Album = Damnation;
            DamTrack6.Artist = Opeth;
            DamTrack6.Playlists = new List<PlayList>();
            DamTrack6.AlbumId = Damnation.Id;

            var DamTrack7 = TrackFactory.Create();
            //var DamTrack7 = new Track();
            DamTrack7.Id = 16;
            DamTrack7.Title = "Ending Credits";
            DamTrack7.Number = 7;
            DamTrack7.Duration = TimeSpan.Parse("00:03:39");
            DamTrack7.Album = Damnation;
            DamTrack7.Artist = Opeth;
            DamTrack7.Playlists = new List<PlayList>();
            DamTrack7.AlbumId = Damnation.Id;

            var DamTrack8 = TrackFactory.Create();
            //var DamTrack8 = new Track();
            DamTrack8.Id = 17;
            DamTrack8.Title = "Weakness";
            DamTrack8.Number = 8;
            DamTrack8.Duration = TimeSpan.Parse("00:04:08");
            DamTrack8.Album = Damnation;
            DamTrack8.Artist = Opeth;
            DamTrack8.Playlists = new List<PlayList>();
            DamTrack8.AlbumId = Damnation.Id;


            //  Add tracks to the playlist
            FloydFavourites.Tracks.Add(dsotmTrack4);
            FloydFavourites.TrackIds.Add(dsotmTrack4.Id);
            FloydFavourites.Tracks.Add(dsotmTrack6);
            FloydFavourites.TrackIds.Add(dsotmTrack6.Id);
            OpethFavourites.Tracks.Add(DamTrack7);
            OpethFavourites.TrackIds.Add(DamTrack7.Id);


            //  Add the playlist to the track
            dsotmTrack4.Playlists.Add(FloydFavourites);
            dsotmTrack6.Playlists.Add(FloydFavourites);
            DamTrack7.Playlists.Add(OpethFavourites);

            //  Add the tracks to the Albums
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack1);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack2);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack3);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack4);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack5);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack6);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack7);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack8);
            DarkSideOfTheMoon.Tracks.Add(dsotmTrack9);

            Damnation.Tracks.Add(DamTrack1);
            Damnation.Tracks.Add(DamTrack2);
            Damnation.Tracks.Add(DamTrack3);
            Damnation.Tracks.Add(DamTrack4);
            Damnation.Tracks.Add(DamTrack5);
            Damnation.Tracks.Add(DamTrack6);
            Damnation.Tracks.Add(DamTrack7);
            Damnation.Tracks.Add(DamTrack8);


            //  Create Images
            var dsotmImageSmall = ImageFactory.Create();
            //var dsotmImageSmall = new Image();
            dsotmImageSmall.Id = 1;
            dsotmImageSmall.Size = ImageSizeEnum.small;
            dsotmImageSmall.Url = string.Empty;

            var dsotmImageMedium = ImageFactory.Create();
            //var dsotmImageMedium = new Image();
            dsotmImageMedium.Id = 2;
            dsotmImageMedium.Size = ImageSizeEnum.medium;
            dsotmImageMedium.Url = @"http://userserve-ak.last.fm/serve/64s/69704236.png";
            //dsotmImageMedium.Url = @"DarkSideOfTheMoonMedium.png";

            var dsotmImageLarge = ImageFactory.Create();
            //var dsotmImageLarge = new Image();
            dsotmImageLarge.Id = 3;
            dsotmImageLarge.Size = ImageSizeEnum.large;
            dsotmImageLarge.Url = @"http://userserve-ak.last.fm/serve/174s/69704236.png";
            //dsotmImageLarge.Url = @"DarkSideOfTheMoonLarge.png";

            var dsotmImageExtraLarge = ImageFactory.Create();
            //var dsotmImageExtraLarge = new Image();
            dsotmImageExtraLarge.Id = 4;
            dsotmImageExtraLarge.Size = ImageSizeEnum.extralarge;
            dsotmImageExtraLarge.Url = string.Empty;

            var dsotmImageMega = ImageFactory.Create();
            //var dsotmImageMega = new Image();
            dsotmImageMega.Id = 5;
            dsotmImageMega.Size = ImageSizeEnum.mega;
            dsotmImageMega.Url = string.Empty;


            var damImageSmall = ImageFactory.Create();
            //var damImageSmall = new Image();
            damImageSmall.Id = 6;
            damImageSmall.Size = ImageSizeEnum.small;
            damImageSmall.Url = string.Empty;

            var damImageMedium = ImageFactory.Create();
            //var damImageMedium = new Image();
            damImageMedium.Id = 7;
            damImageMedium.Size = ImageSizeEnum.medium;
            damImageMedium.Url = @"http://userserve-ak.last.fm/serve/64s/86402755.png";
            //damImageMedium.Url = @"DamnationMedium.png";

            var damImageLarge = ImageFactory.Create();
            //var damImageLarge = new Image();
            damImageLarge.Id = 8;
            damImageLarge.Size = ImageSizeEnum.large;
            damImageLarge.Url = @"http://userserve-ak.last.fm/serve/174s/86402755.png";
            //damImageLarge.Url = @"DamnationLarge.png";

            var damImageExtraLarge = ImageFactory.Create();
            //var damImageExtraLarge = new Image();
            damImageExtraLarge.Id = 9;
            damImageExtraLarge.Size = ImageSizeEnum.extralarge;
            damImageExtraLarge.Url = string.Empty;

            var damImageMega = ImageFactory.Create();
            //var damImageMega = new Image();
            damImageMega.Id = 10;
            damImageMega.Size = ImageSizeEnum.mega;
            damImageMega.Url = string.Empty;


            var wywhImageMedium = ImageFactory.Create();
            //var damImageMedium = new Image();
            wywhImageMedium.Id = 11;
            wywhImageMedium.Size = ImageSizeEnum.medium;
            wywhImageMedium.Url = @"http://userserve-ak.last.fm/serve/64s/40625357.png";

            var wywhImageLarge = ImageFactory.Create();
            //var damImageLarge = new Image();
            wywhImageLarge.Id = 12;
            wywhImageLarge.Size = ImageSizeEnum.large;
            wywhImageLarge.Url = @"http://userserve-ak.last.fm/serve/174s/40625357.png";



            //  Add the images tot the albums
            DarkSideOfTheMoon.Images.Add(dsotmImageSmall);
            DarkSideOfTheMoon.Images.Add(dsotmImageMedium);
            DarkSideOfTheMoon.Images.Add(dsotmImageLarge);
            DarkSideOfTheMoon.Images.Add(dsotmImageExtraLarge);
            DarkSideOfTheMoon.Images.Add(dsotmImageMega);

            Damnation.Images.Add(damImageSmall);
            Damnation.Images.Add(damImageMedium);
            Damnation.Images.Add(damImageLarge);
            Damnation.Images.Add(damImageExtraLarge);
            Damnation.Images.Add(damImageMega);

            WishYouWereHere.Images.Add(wywhImageMedium);
            WishYouWereHere.Images.Add(wywhImageLarge);



            //  Add the Genres to the Albums
            DarkSideOfTheMoon.Genres.Add(rock);
            DarkSideOfTheMoon.Genres.Add(seventies);
            Damnation.Genres.Add(rock);
            WishYouWereHere.Genres.Add(seventies);
            WishYouWereHere.Genres.Add(pop);

            //  Add albums to the Genres
            rock.Albums.Add(DarkSideOfTheMoon);
            rock.AlbumIds.Add(DarkSideOfTheMoon.Id);
            rock.Albums.Add(Damnation);
            rock.AlbumIds.Add(Damnation.Id);
            seventies.Albums.Add(DarkSideOfTheMoon);
            seventies.AlbumIds.Add(DarkSideOfTheMoon.Id);
            seventies.Albums.Add(WishYouWereHere);
            seventies.AlbumIds.Add(WishYouWereHere.Id);
            pop.Albums.Add(WishYouWereHere);
            pop.AlbumIds.Add(WishYouWereHere.Id);


            //  Add the albums to the Artists
            //pinkFloyd.Albums.Add(DarkSideOfTheMoon);
            //Opeth.Albums.Add(Damnation);
            pinkFloyd.Albums.Add(DarkSideOfTheMoon);
            pinkFloyd.AlbumIds.Add(DarkSideOfTheMoon.Id);
            Opeth.Albums.Add(Damnation);
            Opeth.AlbumIds.Add(Damnation.Id);
            pinkFloyd.Albums.Add(WishYouWereHere);
            pinkFloyd.AlbumIds.Add(WishYouWereHere.Id);

            //  Now add them to the collections
            //  Track
            UnitOfWork.Tracks.Add(dsotmTrack1);
            UnitOfWork.Tracks.Add(dsotmTrack2);
            UnitOfWork.Tracks.Add(dsotmTrack3);
            UnitOfWork.Tracks.Add(dsotmTrack4);
            UnitOfWork.Tracks.Add(dsotmTrack5);
            UnitOfWork.Tracks.Add(dsotmTrack6);
            UnitOfWork.Tracks.Add(dsotmTrack7);
            UnitOfWork.Tracks.Add(dsotmTrack8);
            UnitOfWork.Tracks.Add(dsotmTrack9);

            UnitOfWork.Tracks.Add(DamTrack1);
            UnitOfWork.Tracks.Add(DamTrack2);
            UnitOfWork.Tracks.Add(DamTrack3);
            UnitOfWork.Tracks.Add(DamTrack4);
            UnitOfWork.Tracks.Add(DamTrack5);
            UnitOfWork.Tracks.Add(DamTrack6);
            UnitOfWork.Tracks.Add(DamTrack7);
            UnitOfWork.Tracks.Add(DamTrack8);

            //  Genres
            UnitOfWork.Genres.Add(rock);
            UnitOfWork.Genres.Add(pop);
            UnitOfWork.Genres.Add(seventies);

            //  Album
            UnitOfWork.Albums.Add(DarkSideOfTheMoon);
            UnitOfWork.Albums.Add(Damnation);

            UnitOfWork.Albums.Add(WishYouWereHere);

            //  Artist
            UnitOfWork.Artists.Add(pinkFloyd);
            UnitOfWork.Artists.Add(Opeth);

            //  Playlists
            UnitOfWork.PlayLists.Add(FloydFavourites);
            UnitOfWork.PlayLists.Add(OpethFavourites);

        }
    }
}
