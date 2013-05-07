using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using GalaSoft.MvvmLight.Ioc;
using THMusic.ViewModel;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace THMusic
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class ItemsPage : THMusic.Common.LayoutAwarePage
    {
        //  A bit of a hack to control loading the first time, so I don't have to
        //  have any async calls to load methods from within the ViewModel ctor.
        private static bool IsViewModelInitialised = false;

        public ItemsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            //  If the navigation parameter is "AlbumImportedOK" Then we must refresh the data in the viewmodel.
            if (!IsViewModelInitialised || (string)navigationParameter == "AlbumImportedOK")   // Not that keen on magic Strings, but MS template does.
            {
                var mainVM = SimpleIoc.Default.GetInstance<MainViewModel>();
                await mainVM.RefreshData();
                IsViewModelInitialised = true;      // Ensure this doesnt run again, unless specifically requested.
            }

        }

        /// <summary>
        /// Invoked when an item is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter

            ////var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            //var groupId = "1";
            //this.Frame.Navigate(typeof(SplitPage), groupId);
        }

    }
}
