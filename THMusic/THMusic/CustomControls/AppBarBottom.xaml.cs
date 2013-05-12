using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace THMusic.CustomControls
{
    /// <summary>
    /// This <c>appBarBottom</c> class is the application AppBar displayed at the 
    /// bottom of the main page.  It provides access to the LastFM and MP3 import
    /// facilities
    /// </summary>
    public sealed partial class AppBarBottom : UserControl
    {
        public AppBarBottom()
        {
            this.InitializeComponent();
        }
    }
}
