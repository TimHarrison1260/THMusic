//***************************************************************************************************
//Name of File:     INavigationService.cs
//Description:      The interface that provides access to the NavigationService used to navigate pages.
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

namespace THMusic.Navigation
{
    /// <summary>
    /// The <c>INavigationService</c> interface provides access to the
    /// necessary supporting navigation methods, to navigate pages.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigate to page without any parameters
        /// </summary>
        /// <param name="sourcePageType">The typeOf page being navigated to.</param>
        void Navigate(Type sourcePageType);
        /// <summary>
        /// Navigate to page passing parameter object
        /// </summary>
        /// <param name="sourcePageType">The parameter object</param>
        /// <param name="parameter">The typeOf page being navigated to.</param>
        void Navigate(Type sourcePageType, object parameter);
        /// <summary>
        /// Performs a goBack
        /// </summary>
        void GoBack();
    }
}
