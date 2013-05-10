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

using Windows.Storage;
using System.Threading.Tasks;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace THMusic.CustomControls
{
    public sealed partial class MusicPlayer : UserControl
    {

        private static string _prevMediaFile = string.Empty;

        public MusicPlayer()
        {
            this.InitializeComponent();
        }

        async void PlayButton_Click(object sender, object e)
        {
            if (MedialFilePath.Text != String.Empty &&  MedialFilePath.Text != _prevMediaFile)
            {
                //  Load the file and set source
                await SetMediaSource(MedialFilePath.Text);
                //  Set 
                _prevMediaFile = MedialFilePath.Text;
            }
            if (MediaPlayer.CurrentState == MediaElementState.Closed || MediaPlayer.CurrentState == MediaElementState.Stopped)
            {
            }
            MediaPlayer.Play();
        }

        private async Task SetMediaSource(string newMediaFilePath)
        {
            //  Get the storagefile of the path
            var path = MedialFilePath.Text;
            StorageFile mediaStorageFile = await StorageFile.GetFileFromPathAsync(path);
            if (mediaStorageFile != null)
            {
                var contentType = mediaStorageFile.ContentType;
                //  Set up a stream for the mediaElement: MediaPlayer
                var stream = await mediaStorageFile.OpenAsync(FileAccessMode.Read);
                //  Set the source of the mediaElement so it can play the file
                MediaPlayer.SetSource(stream, mediaStorageFile.ContentType);
            }
        }

        void PauseButton_Click(object sender, object e)
        {
            MediaPlayer.Pause();
        }

        void StopButton_Click(object sender, object e)
        {
            MediaPlayer.Stop();
        }

        void RewindButton_Click(object sender, object e)
        {
            MediaPlayer.PlaybackRate = -1.0;
        }

        void ForwardButton_Click(object sender, object e)
        {
            MediaPlayer.PlaybackRate = 2.0;
        }


    }
}
