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
    /// <summary>
    /// This <c>MusicPlayer</c> class contains the functionliaty required to be able to Play,
    /// Pause, Stop, Rewind and Fast forward an audio file.  This functionality is coded within
    /// the code-behond for this user control.
    /// <para>
    /// The control's UI has a hidden field that contains the path to the audio file being played.
    /// Whe the play button is click a check is made to see if the current audio file is the same
    /// as the one currently palying.  If it is, then it just continues playing it.  However, if 
    /// it is not, then the new file is loaded into the player and play of the new audio file 
    /// commences.
    /// </para>
    /// </summary>
    public sealed partial class MusicPlayer : UserControl
    {

        private static string _prevMediaFile = string.Empty;

        /// <summary>
        /// Initialise the MusicPlayer
        /// </summary>
        public MusicPlayer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Hendles the Play button click evenr
        /// </summary>
        /// <param name="sender">The play button</param>
        /// <param name="e">The parameters</param>
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

        /// <summary>
        /// Uses the mediaElement SetSource method to set the sourse
        /// as the audio file.  The file is opened as a stream and 
        /// MUST be located within on the of the users' known folders
        /// otherwise it sill not play.
        /// </summary>
        /// <param name="newMediaFilePath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Handles the Pause button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PauseButton_Click(object sender, object e)
        {
            MediaPlayer.Pause();
        }

        /// <summary>
        /// Handles the Stop button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StopButton_Click(object sender, object e)
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Handles the Rewind button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RewindButton_Click(object sender, object e)
        {
            MediaPlayer.PlaybackRate = -1.0;
        }

        /// <summary>
        /// Handles the forward button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ForwardButton_Click(object sender, object e)
        {
            MediaPlayer.PlaybackRate = 2.0;
        }


    }
}
