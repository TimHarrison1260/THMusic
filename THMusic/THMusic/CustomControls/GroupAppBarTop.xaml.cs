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
    /// This <c>GroupBarBottom</c> class is the application AppBar displayed at the 
    /// top of the main page.  it provides functionality to change the groupging 
    /// selected from Artist, Albums and Playlist.  It also displayes the "Refresh"
    /// data button, that becomes visible when a change is made to the grouping.
    /// </summary>
    public sealed partial class GroupAppBarTop : UserControl
    {
        public GroupAppBarTop()
        {
            this.InitializeComponent();
        }
    }
}
