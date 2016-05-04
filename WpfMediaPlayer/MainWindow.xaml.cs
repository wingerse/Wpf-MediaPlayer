namespace WpfMediaPlayer
{
    using System;
    using System.Windows;

    using Microsoft.Win32;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlaying = false;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ChangeIsPlaying(false);
        }

        private void PlayPause_OnClick(object sender, RoutedEventArgs e)
        {
            this.ChangeIsPlaying(!this.isPlaying);
        }

        private void SetMediaSource(string file)
        {
            this.mediaElement.Source = new Uri(file);
        }

        private void Stop_OnClick(object sender, RoutedEventArgs e)
        {
            this.OnMediaEnded();
        }

        private void MediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            this.OnMediaEnded();
        }

        private void MediaElement_OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            this.OnMediaEnded();
        }

        private void OnMediaEnded()
        {
            this.mediaElement.Stop();
            this.ChangeIsPlaying(false);
        }

        private void ChangeIsPlaying(bool isPlaying)
        {
            this.isPlaying = isPlaying;

            if (this.isPlaying)
            {
                this.playPause.ChangeToPlayingState();
                this.mediaElement.Play();
            }
            else
            {
                this.playPause.ChangeToPauseState();
                this.mediaElement.Pause();
            }
        }

        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.mediaElement.Volume = this.volumeSlider.Value;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            string file = this.OpenFile();
            if (file == null)
            {
                return;
            }

            this.SetMediaSource(file);
        }

        private string OpenFile()
        {
            string filter = "Video (*.avi, *.mkv, *.mp4, *.flv)|*.avi;*.mkv;*.mp4;*.flv|Audio(*.ogg, *.mp3, *.wav)|*.ogg;*.mp3;*.wav;|All Files(*.*)|*.*";
            var openDialog = new OpenFileDialog {Multiselect = false, CheckFileExists = true, CheckPathExists = true, Title = "Select video file", AddExtension = true, Filter = filter};
            if (openDialog.ShowDialog(this) == true)
            {
                return openDialog.FileName;
            }
            return null;
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Aiman copyright 2016, All rights deserved!");
        }
    }
}
