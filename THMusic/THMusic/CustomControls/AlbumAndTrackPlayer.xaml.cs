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
    /// This <c>AlbumAndTrackPlay</c> is a xaml based used control, that displays 
    /// the album information, its related track and wiki information.  Additionally
    /// it makes use of the MediaPlayer user control.
    /// </summary>
    public sealed partial class AlbumAndTrackPlayer : UserControl
    {
        public AlbumAndTrackPlayer()
        {
            this.InitializeComponent();
        }
    }
}
