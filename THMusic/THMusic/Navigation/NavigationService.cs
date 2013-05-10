//***************************************************************************************************
//Name of File:     NavigationService.cs
//Description:      The Navigation service the controls page navigation
//Author:           Tim Harrison
//Date of Creation: Apr/May 2013.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace THMusic.Navigation
{
    /// <summary>
    /// The <c>NavigationSerivce</c> class provides the means of navigating pages.
    /// It implements the <c>INavigationService</c> interface.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Navigate to page without any parameters
        /// </summary>
        /// <param name="sourcePageType">The typeOf page being navigated to.</param>
        public void Navigate(Type sourcePageType)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType);
        }

        /// <summary>
        /// Navigate to page passing parameter object
        /// </summary>
        /// <param name="sourcePageType">The parameter object</param>
        /// <param name="parameter">The typeOf page being navigated to.</param>
        public void Navigate(Type sourcePageType, object parameter)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType, parameter);
        }

        /// <summary>
        /// Performs a goBack
        /// </summary>
        public void GoBack()
        {
            ((Frame)Window.Current.Content).GoBack();
        }
    }
}
