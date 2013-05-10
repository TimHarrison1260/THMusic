//***************************************************************************************************
//Name of File:     LastFMProxy.cs
//Description:      The provides the LastFM functionality from the serivce, abstracting it away from the 
//                  resst of the applicatinon.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Threading.Tasks;

using System.Xml.Linq;              //  Linq to XML
using System.Net;                   //  Url Encoding stuff

using Core.Services;

namespace Infrastructure.Services
{
    /// <summary>
    /// This <c>LastFMProxy</c> class acts as a proxy to the LastFM http service, that
    /// supplies information about music albums, artists and just about everything 
    /// else associated with music.
    /// </summary>
    public class LastFmProxy : ILastFMService
    {
        /// <summary>
        /// This <c>GetAlbumInfoAsync</c> method is responsible for calling the 
        /// LastFM album.getInfo method for the supplied search criteria.  It then 
        /// maps the results to an instance of the <see cref="Core.Services.LastFMAlbumInfo"/>
        /// class.
        /// <para>
        /// It uses the XElement.Load method of Linq to XML to call the service, and then
        /// uses Linq-To-Xml to interrogate the result.  Xml is used in preference to 
        /// JSon, as the JSon didn't deserialise properly with NewtonSoft.Json.  Rather
        /// than attempt to write a JSon deserialised, Linq-To-Xml was used instead.
        /// This had been used in a previous project so was a quicker option in this case.
        /// </para>
        /// </summary>
        /// <param name="albumName">The album to search for.</param>
        /// <param name="artistName">The artist to search for.</param>
        /// <returns>A populated instance of LastFMAlbumInfo class.</returns>
        /// <remarks>
        /// This methods contains no asynchronous call, and is therefore the bottleneck
        /// in the asynchronous nature of the application.  This is due to XElement not
        /// providing an asynchronous method to call it.
        /// However, by making this method itself callabe in an asynchronous way, allows
        /// the UI to remain unblocked.
        /// Using XElement to call the LastFM XML restful service rather than the Json version
        /// because the Json deserialiser in currently not providing a fully working model,
        /// it currently contain some invalid names properties.  Tried using NewtonSoft.Json
        /// which is a .Net implementation of a Json deserialiser, but it does not fully
        /// retrieve the model as yet.
        /// Don't want to spend too long learning how to configure this deserialiser just
        /// now as there's not much time for this project.  However, if it can be configured 
        /// to retrieve the full model, then it would be possible to make the call to the
        /// LastFM service asynchronously, which ultimately would be the best way.
        /// </remarks>
        public async Task<LastFMAlbumInfo> GetAlbumInfoAsync(string albumName, string artistName)
        {
            var info = new LastFMAlbumInfo();           //  This should created using a Factory!!!

            //  Encode the parameters before including in the url
            //  Do NOT encode the full url as this causes problems and corrupts the url.
            string encodedAlbumName = WebUtility.UrlEncode(albumName);
            string encodedArtistName = WebUtility.UrlEncode(artistName);

            //  create the Url for the Google Elevation Api
            string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}", encodedArtistName, encodedAlbumName);
            //string url = string.Format("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=883f78cd9f3317abb2e2a88008dc0e99&artist={0}&album={1}&format=json");

            //  Create the result by streaming the return from the load(url).  This has
            //  the effect of submitting an HttpWebRequest as the "file" is a url which 
            //  returns XML serialised data.
            //  We could also have the results returned in JSON format, but we would need
            //  a different parsers.  C# is easiest with XML at this level.

            try
            {
                var result = XElement.Load(url);

                //  Get the statues returned and check it was successful
                var status = (string)result.Attribute("status");
                if (status == "ok")
                {
                    //  Deserialize the result
                    var album = LastFMHelper.Deserialise(result);
                    //  et the Status
                    album.Status.code = status;

                    return album;
                }
                else
                {
                    //  Get the message from the returned string
                    var error = new LastFMAlbumInfo();
                    var errorElement = result.Element("error");
                    error.Status.code = (string)errorElement.Attribute("code");
                    error.Status.message = (string)errorElement;
                    return error;
                }
            }
            catch (Exception ex)
            {
                //  Error encountered from http server
                var error = new LastFMAlbumInfo();
                error.Status.code = "Unknown error";
                error.Status.message = ex.Message;
                return error;
            }
        }
    }
}
