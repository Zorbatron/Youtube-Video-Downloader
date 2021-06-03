using System;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;

namespace YoutubeVideoDownloader
{
    public partial class QualitySelector : Window
    {
        public QualitySelector()
        {
            InitializeComponent();
        }

        private void QualSelectorLoaded(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "youtube-dl.exe";
            p.StartInfo.Arguments = "-F https://www.youtube.com/watch?v=LXb3EKWsInQ";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            string newLine = Environment.NewLine;

            List<string> output = new List<string>();
            List<string> qualNum = new List<string>();
            List<string> qualType = new List<string>();
            List<string> qualResolution = new List<string>();

            while (!p.StandardOutput.EndOfStream)
            {
                output.Add(p.StandardOutput.ReadLine());
            }

            output.RemoveRange(0, 3);

            foreach (string text in output)
            {
                qualNum.Add(text.Remove(3));
            }

            foreach (string text in output)
            {
                text.Remove(0, 13);
                qualType.Add(text.Remove(3));
            }

            foreach (string text in output)
            {
                text.Remove(0, 24);
                qualResolution.Add(text.Remove(10));
            }

            for (int x = 0; x < output.Count; x++)
            {
                OutputBox.Text += qualNum[x] + " ";
                OutputBox.Text += qualType[x] + " ";
                OutputBox.Text += qualResolution[x] + newLine;
            }
        }
    }
}
