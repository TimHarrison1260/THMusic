using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using THMusic.Data;
using THMusic.DataModel;

namespace THMusic.Design
{
    /// <summary>
    /// This <c>DesignAlbumModelDataService</c> class supports the design time data for the album Details page
    /// </summary>
    public class DesignAlbumModelDataService :IAlbumModelDataService
    {
        /// <summary>
        /// It gets the Artist name, either "Pink Floyd" or "Opeth"
        /// </summary>
        /// <param name="Id">The id</param>
        /// <param name="Type">The type</param>
        /// <returns>the group name</returns>
        public async Task<string> LoadGroupNameAsync(int Id, DataModel.GroupTypeEnum Type)
        {
            string groupName = string.Empty;
            if (Id == 1)
                groupName = "Pink Floyd";
            else
                groupName =  "Opeth";
            return groupName;
        }

        /// <summary>
        /// Gets the albums for the design time data.
        /// </summary>
        /// <param name="Id">The id of the artist</param>
        /// <param name="Type">The type: artist</param>
        /// <returns></returns>
        public async Task<List<DataModel.AlbumModel>> LoadAlbumsAsync(int Id, DataModel.GroupTypeEnum Type)
        {
            var albumModels = new List<AlbumModel>();

            albumModels.Add(CreateDarkSideOfTheMoon());

            albumModels.Add(CreateWishYouWereHere());

            return albumModels;
        }


        private static AlbumModel CreateDarkSideOfTheMoon()
        {
            var DarkSideOfTheMoon = new AlbumModel();

            DarkSideOfTheMoon.Id = "1";
            DarkSideOfTheMoon.SetImageLarge(@"Assets\DarkSideOfTheMoonLarge.png");    //  String
            DarkSideOfTheMoon.SetImageMedium(@"Assets\DarkSideOfTheMoonMedium.png");   //  String
            DarkSideOfTheMoon.Title = "DarkSide of the Moon";
            DarkSideOfTheMoon.RawReleased = new DateTime(1973, 3, 1);
            DarkSideOfTheMoon.LastFMUrl = string.Empty;
            DarkSideOfTheMoon.LastFMMbid = string.Empty;
            DarkSideOfTheMoon.ArtistName = "Pink floyd";
            DarkSideOfTheMoon.ArtistMbid = string.Empty;
            DarkSideOfTheMoon.ArtistUrl = string.Empty;

            DarkSideOfTheMoon.Wiki = CreateDarkSideOfTheMoonWiki();
            DarkSideOfTheMoon.Tracks = CreateDarkSideOfTheMoonTracks();
            DarkSideOfTheMoon.Genres = CreateDarkSideOfTheMoonGenres();

            return DarkSideOfTheMoon;
        }

        private static AlbumModel CreateWishYouWereHere()
        {
            var wywh = new AlbumModel();
            wywh.Id = "2";
            wywh.SetImageLarge(@"Assets\wywhLarge.png");    //  String
            wywh.SetImageMedium(@"Assets\wywhMedium.png");   //  String
            wywh.Title = "Wish You Were Here";
            wywh.RawReleased = new DateTime(1975, 9, 12);
            wywh.LastFMUrl = string.Empty;
            wywh.LastFMMbid = string.Empty;
            wywh.ArtistName = "Pink floyd";
            wywh.ArtistMbid = string.Empty;
            wywh.ArtistUrl = string.Empty;

            wywh.Wiki = new WikiModel();
            wywh.Tracks = new List<TrackModel>();
            wywh.Genres = new List<GenreModel>();

            return wywh;
        }



        private static WikiModel CreateDarkSideOfTheMoonWiki()
        {
            var wiki = new WikiModel();
            wiki.Summary = @"The Dark Side of the Moon (titled Dark Side of the Moon in the 1993 CD edition) is a 
                            concept album by the British progressive rock band Pink Floyd. It was released on 
                            March 17, 1973 in the U.S. and March 24, 1973 in the UK. The Dark Side of the Moon builds 
                            upon previous experimentation Pink Floyd had done, especially on their album. Its themes 
                            include old age, conflict and insanity; the latter possibly inspired by the deteriorating 
                            mental state of their former band leader Syd Barrett.";
            wiki.Content = @"The Dark Side of the Moon (titled Dark Side of the Moon in the 1993 CD edition) is a 
                            concept album by the British progressive rock band Pink Floyd. It was released on 
                            March 17, 1973 in the U.S. and March 24, 1973 in the UK. The Dark Side of the Moon builds 
                            upon previous experimentation Pink Floyd had done, especially on their album. Its themes 
                            include old age, conflict and insanity; the latter possibly inspired by the deteriorating 
                            mental state of their former band leader Syd Barrett. The album is notable for its use of 
                            musique concrète and conceptual, philosophical lyrics, as found in much of Pink Floyd's work.\r\n" +
                           @"The Dark Side of the Moon spent 741 consecutive weeks (14 years) on the USA-based 
                            Billboard 200 album chart, the longest duration of any album in history. It is one of three
                            albums tied for the claim of second highest selling album globally of all time, selling 
                            forty million or more units. In addition to its commercial success, The Dark Side of the 
                            Moon is often considered to be the group's defining work, and is still frequently ranked 
                            by music critics as one of the greatest and most influential albums of all time.\r\n" +
                           @"In 2006 it was voted &quot;My Favourite Album&quot; by viewers and listeners to the 
                            Australian Broadcasting Corporation, and in 2003 Rolling Stone listed The Dark Side of 
                            the Moon 43rd on its list of the 500 greatest albums of all time. It is also #2 on the 
                            Definitive 200 Albums of All Time, a list made by the National Association of Recording 
                            Merchandisers &quot;in celebration of the art form of the record album&quot;. 
                            User-contributed text is available under the Creative Commons By-SA License and may also 
                            be available under the GNU FDL.";
            wiki.RawPublished = new DateTime(2009, 9, 4);
            return wiki;
        }

        private static List<TrackModel> CreateDarkSideOfTheMoonTracks()
        {
            var darkSideTracks = new List<TrackModel>();

            var Breathe = new TrackModel();
            Breathe.Id = "1";
            Breathe.Number = "1";
            Breathe.Title = "Speak To Me/Breathe";
            Breathe.RawDuration = new TimeSpan(00, 03, 57);
            Breathe.LastFMUrl = string.Empty;
            Breathe.LastFMMbid = string.Empty;
            Breathe.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(Breathe);

            var OnTheRun = new TrackModel();
            OnTheRun.Id = "2";
            OnTheRun.Number = "2";
            OnTheRun.Title = "On the Run";
            OnTheRun.RawDuration = new TimeSpan(00, 03, 55);
            OnTheRun.LastFMUrl = string.Empty;
            OnTheRun.LastFMMbid = string.Empty;
            OnTheRun.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(OnTheRun);

            var Time = new TrackModel();
            Time.Id = "3";
            Time.Number = "3";
            Time.Title = "Time";
            Time.RawDuration = new TimeSpan(00, 07, 04);
            Time.LastFMUrl = string.Empty;
            Time.LastFMMbid = string.Empty;
            Time.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(Time);

            var Gig = new TrackModel();
            Gig.Id = "4";
            Gig.Number = "4";
            Gig.Title = "The Great Gig in the Sky";
            Gig.RawDuration = new TimeSpan(00, 04, 47);
            Gig.LastFMUrl = string.Empty;
            Gig.LastFMMbid = string.Empty;
            Gig.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(Gig);

            var Money = new TrackModel();
            Money.Id = "5";
            Money.Number = "5";
            Money.Title = "Money";
            Money.RawDuration = new TimeSpan(00, 06, 22);
            Money.LastFMUrl = string.Empty;
            Money.LastFMMbid = string.Empty;
            Money.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(Money);

            var UsAndThem = new TrackModel();
            UsAndThem.Id = "6";
            UsAndThem.Number = "6";
            UsAndThem.Title = "Us and Them";
            UsAndThem.RawDuration = new TimeSpan(00, 07, 50);
            UsAndThem.LastFMUrl = string.Empty;
            UsAndThem.LastFMMbid = string.Empty;
            UsAndThem.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(UsAndThem);

            var AnyColour = new TrackModel();
            AnyColour.Id = "7";
            AnyColour.Number = "7";
            AnyColour.Title = "Any Colour You Like";
            AnyColour.RawDuration = new TimeSpan(00, 03, 25);
            AnyColour.LastFMUrl = string.Empty;
            AnyColour.LastFMMbid = string.Empty;
            AnyColour.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(AnyColour);

            var Brain = new TrackModel();
            Brain.Id = "8";
            Brain.Number = "8";
            Brain.Title = "Brain Damage";
            Brain.RawDuration = new TimeSpan(00, 03, 50);
            Brain.LastFMUrl = string.Empty;
            Brain.LastFMMbid = string.Empty;
            Brain.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(Brain);

            var Eclipse = new TrackModel();
            Eclipse.Id = "9";
            Eclipse.Number = "9";
            Eclipse.Title = "Eclipse";
            Eclipse.RawDuration = new TimeSpan(00, 02, 01);
            Eclipse.LastFMUrl = string.Empty;
            Eclipse.LastFMMbid = string.Empty;
            Eclipse.MediaFilePath = @"Assets\SpeakToMe.wma";
            darkSideTracks.Add(Eclipse);

            return darkSideTracks;
        }


        private static List<GenreModel> CreateDarkSideOfTheMoonGenres()
        {
            var darkSideGenres = new List<GenreModel>();

            var rock = new GenreModel();
            rock.Id = "1";
            rock.Name = "Rock";
            rock.LastFMUrl = string.Empty;
            darkSideGenres.Add(rock);

            var seventies = new GenreModel();
            seventies.Id = "2";
            seventies.Name = "70's";
            seventies.LastFMUrl = string.Empty;
            darkSideGenres.Add(seventies);

            var concept = new GenreModel();
            concept.Id = "3";
            concept.Name = "Concept";
            concept.LastFMUrl = string.Empty;
            darkSideGenres.Add(concept);

            return darkSideGenres;
        }




    }
}
