using System;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace YTVideoDownloader
{
    public partial class MainWindow : Window
    {
        private Process processID;

        public MainWindow()
        {
            InitializeComponent();

            webLink.Text = Clipboard.GetText();
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            DownloadVideo();
        }

        private void webLink_KeyDown(object sender, KeyEventArgs key)
        {
            if (key.Key == Key.Enter)
            {
                DownloadVideo();
            }
        }

        public void DownloadVideo()
        {
            string directory = Directory.GetCurrentDirectory();
            string fileName = Path.Combine(directory, "yt-dlp.exe");
            string ffmpeg = Path.Combine("ffmpeg", "ffmpeg.exe");
            string ffmpegLocation = Path.Combine(directory, ffmpeg);

            try
            {
                if (mp4.IsChecked == true)
                {
                    Process.Start(fileName, webLink.Text + " -f best");
                }
                if (webm.IsChecked == true)
                {
                    MessageBoxResult result = MessageBox.Show("Webm videos take some time to convert with ffmpeg, are you sure you want to convert?", "", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        Process.Start(fileName, webLink.Text + $"-f best --recode-video webm --ffmpeg-location {ffmpegLocation}");
                    }
                }

                if (mp3.IsChecked == true)
                {
                    Process.Start(fileName, webLink.Text + $"-f best -x --audio-format mp3 --ffmpeg-location {ffmpegLocation}");
                }
                if (flac.IsChecked == true)
                {
                    Process.Start(fileName, webLink.Text + $"-f best -x --audio-format flac --ffmpeg-location {ffmpegLocation}");
                }
                if (wav.IsChecked == true)
                {
                    Process.Start(fileName, webLink.Text + $"-f best -x --audio-format wav --ffmpeg-location {ffmpegLocation}");
                }
                if (m4a.IsChecked == true)
                {
                    Process.Start(fileName, webLink.Text + $"-f best -x --audio-format m4a --ffmpeg-location {ffmpegLocation}");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void OpenQualWin_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd";
            p.StartInfo.Arguments = $"/k yt-dlp.exe -F {webLink}";
            p.Start();

            processID = p;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (processID.HasExited == false)
            {
                processID.Kill();
            } 
        }

        private void DownloadQualBtn_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "yt-dlp.exe";
            p.StartInfo.Arguments = $"-f {qualityTextBox.Text}";
            p.Start();
        }
    }
}