﻿//***************************************************************************************************
//Name of File:     LocalisationiHelper.cs
//Description:      The LocalisationHelper provides common routines to localise the variouis resources.
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Resources;
using Windows.Globalization.DateTimeFormatting;


namespace THMusic.Helpers
{
    /// <summary>
    /// This <c>LocalsationHelp</c> class provides methods to format
    /// various Dates,  timespans etc to a localised format
    /// </summary>
    public static class LocalisationHelper
    {
        /// <summary>
        /// Provides access to the resouce files, Localisation, used
        /// when describing the Group Totals.
        /// </summary>
        private static ResourceLoader _loader = new ResourceLoader();

        /// <summary>
        /// Formats the supplied date as a ShortDate for the Culture
        /// </summary>
        /// <param name="inDateTime">The date to be formatted</param>
        /// <returns>The localised string representation of the date.</returns>
        public static string LocalisedDate(DateTime inDateTime)
        {
            //  TODO: Refactor the Datetimeformatter to one line of code;
            //  Get the DateTimeFormatter
            var formatter = DateTimeFormatter.ShortDate;
            var localisedDate = formatter.Format(inDateTime);
            return localisedDate;
        }

        /// <summary>
        /// Formates the Timespan to a culture specific format.
        /// </summary>
        /// <param name="inDuration">The Timespan to Format</param>
        /// <returns>Localised timespan.</returns>
        public static string LocaliseDuration(TimeSpan inDuration)
        {
            var formatter = new DateTimeFormatter("hour minute second");
            var unformattedDuration = DateTime.MinValue.Add(inDuration);
            var localisedDuration = formatter.Format(unformattedDuration);

            return localisedDuration;
        }

        /// <summary>
        /// Formats the Description for the Group.  It is different for each of
        /// the Group categories: Artist, Genre, Playlist, Album.
        /// The string is localised to tha language and culture.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="duration"></param>
        /// <returns>The formatted string.</returns>
        public static string LocaliseDescription(int number, TimeSpan duration)
        {
            StringBuilder bldr = new StringBuilder();
            bldr.AppendFormat("{0:d}", number);

            var counterStr = _loader.GetString("GroupCount");
            bldr.AppendFormat(" {0} ", counterStr);

            TimeSpan totalDuration = duration;
            bldr.AppendFormat("{0:t}", duration);

            var durationStr = _loader.GetString("GroupDurationMins");
            bldr.AppendFormat(" {0}", durationStr);

            return bldr.ToString();
        }

    }
}
