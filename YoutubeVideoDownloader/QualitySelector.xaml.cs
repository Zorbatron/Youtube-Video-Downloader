using System;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.IO;

namespace YoutubeVideoDownloader
{
    public partial class QualitySelector : Window
    {
        public string webLink;

        public QualitySelector()
        {
            InitializeComponent();
        }

        private void QualSelectorLoaded(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "yt-dlp.exe";
            p.StartInfo.Arguments = $"-F {webLink}";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();






            //Old parsing code, not deleting just in case.
            
            /* string newLine = Environment.NewLine;

            List<string> output = new List<string>();
            List<int> qualNum = new List<int>();
            List<string> qual = new List<string>();

            while (!p.StandardOutput.EndOfStream)
            {
                output.Add(p.StandardOutput.ReadLine());
            }

            try

                output.RemoveRange(0, 3);

                foreach (string text in output)
                {
                    qualNum.Add(Convert.ToInt32(text.Remove(3)));
                }

                foreach (string text in output)
                {
                    string temp = text.Remove(0, 13);
                    //temp = temp.Remove(30);
                    qual.Add(temp);
                }

                for (int x = 0; x < output.Count; x++)
                {
                    Button button = new Button 
                    {
                        Name = $"BT{qualNum[x]}",
                        Content = qual[x],
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 600 
                    };
                    button.Click += new RoutedEventHandler(QualButtonPressed);
                    StackPanel.Children.Add(button);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            } */
        }

        private void QualButtonPressed(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string directory = Directory.GetCurrentDirectory();
            string fileName = Path.Combine(directory, "youtube-dl.exe");
            string ffmpeg = Path.Combine("ffmpeg", "ffmpeg.exe");
            string ffmpegLocation = Path.Combine(directory, ffmpeg);

            Process p = new Process();
            p.StartInfo.FileName = "youtube-dl.exe";
            p.StartInfo.Arguments = $"-f {button.Name.Remove(0, 2)} {webLink}";
            p.Start();
        }
    }
}
