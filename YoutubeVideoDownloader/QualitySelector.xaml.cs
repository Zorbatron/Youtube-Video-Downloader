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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "youtube-dl.exe";
            p.StartInfo.Arguments = "-F https://www.youtube.com/watch?v=4sRDppM6cj4";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            List<string> output = new List<string>();

            while (!p.StandardOutput.EndOfStream)
            {
                output.Add(p.StandardOutput.ReadLine());
            }

            output.RemoveRange(0, 3);

            for (int x = 0; x < output.Count; x++)
            {
                Console.WriteLine(output[x]);
            }

            Console.ReadLine();
        }
    }
}
