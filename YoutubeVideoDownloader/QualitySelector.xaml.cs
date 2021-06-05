using System;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;

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
            List<int> qualNum = new List<int>();
            List<string> qualType = new List<string>();
            List<string> qualResolution = new List<string>();

            while (!p.StandardOutput.EndOfStream)
            {
                output.Add(p.StandardOutput.ReadLine());
            }
            
            output.RemoveRange(0, 3);

            foreach (string text in output)
            {
                qualNum.Add(Convert.ToInt32(text.Remove(3)));
            }

            foreach (string text in output)
            {
                string temp = text.Remove(0, 13);
                temp = temp.Remove(3);
                qualType.Add(temp);
            }

            foreach (string text in output)
            {
                string temp = text.Remove(0, 24);
                temp = temp.Remove(10);
                qualResolution.Add(temp);
            }

            for (int x = 0; x < output.Count; x++)
            { 
                OutputBox.Text += qualNum[x] + " ";
                OutputBox.Text += qualType[x] + " ";
                OutputBox.Text += qualResolution[x] + newLine;
            }

            for (int x = 0; x < output.Count; x++)
            {
                Button button = new Button();
                button.Name = $"BT{qualNum[x]}";
                button.Content = qualResolution[x];
                button.VerticalAlignment = VerticalAlignment.Center;
                button.Width = 100;
                button.Click += new RoutedEventHandler(QualButtonPressed);
                StackPanel.Children.Add(button);
            }
        }

        private void QualButtonPressed(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show($"Resolution: {button.Content}", $"Button pressed: {button.Name}");
        }
    }
}
