using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorDetectionUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> ColorName = new List<string>();
        List<string> ColorHex = new List<string>();
        List<int> R = new List<int>();
        List<int> G = new List<int>();
        List<int> B = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            using (var reader = new StreamReader("Colors.csv"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    ColorName.Add(values[0]);
                    ColorHex.Add(values[2]);
                    R.Add(Convert.ToInt32(values[3]));
                    G.Add(Convert.ToInt32(values[4]));
                    B.Add(Convert.ToInt32(values[5]));
                }
            }
        }

        public void DetectColors(string FileName,int Position)
        {
            Bitmap bm = ConvertToBitmap(FileName);
            string selectedFileName = FileName;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFileName);
            bitmap.EndInit();
            if(Position == 1)
                ImageControl.Source = bitmap;
            else
                ImageControl2.Source = bitmap;

            Dictionary<string, int> histo = new Dictionary<string, int>();
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    System.Drawing.Color c = bm.GetPixel(x, y);

                    Double Min_Dist = 10000.0;
                    string close_hex = "";

                    for (int i = 0; i < ColorHex.Count; i++)
                    {
                        Double Dist = Math.Sqrt(Math.Pow((c.R - R[i]), 2.0) + Math.Pow((c.G - G[i]), 2.0) + Math.Pow((c.B - B[i]), 2.0));
                        if (Dist < Min_Dist)
                        {
                            close_hex = ColorHex[i];
                            Min_Dist = Dist;
                        }
                    }

                    if (histo.ContainsKey(close_hex))
                        histo[close_hex] = histo[close_hex] + 1;
                    else
                        histo.Add(close_hex, 1);
                }
            }

            int? max = histo.Values.Max();
            var color = histo.FirstOrDefault(x => x.Value == max).Key;
            if (Position == 1)
            {
                color_plot1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot1_hex.Content = color;
                color_plot1_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            else
            {
                color_plot6.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot6_hex.Content = color;
                color_plot6_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            histo.Remove(color);

            max = histo.Values.Max();
            color = histo.FirstOrDefault(x => x.Value == max).Key;
            if (Position == 1)
            {
                color_plot2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot2_hex.Content = color;
                color_plot2_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            else
            {
                color_plot7.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot7_hex.Content = color;
                color_plot7_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            histo.Remove(color);

            max = histo.Values.Max();
            color = histo.FirstOrDefault(x => x.Value == max).Key;
            if (Position == 1)
            {
                color_plot3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot3_hex.Content = color;
                color_plot3_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            else
            {
                color_plot8.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot8_hex.Content = color;
                color_plot8_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            histo.Remove(color);

            max = histo.Values.Max();
            color = histo.FirstOrDefault(x => x.Value == max).Key;
            if (Position == 1)
            {
                color_plot4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot4_hex.Content = color;
                color_plot4_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            else
            {
                color_plot9.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot9_hex.Content = color;
                color_plot9_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            histo.Remove(color);

            max = histo.Values.Max();
            color = histo.FirstOrDefault(x => x.Value == max).Key;
            if (Position == 1)
            {
                color_plot5.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot5_hex.Content = color;
                color_plot5_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
            else
            {
                color_plot10.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
                color_plot10_hex.Content = color;
                color_plot10_percent.Content = ((max * 100) / (bm.Width * bm.Height)).ToString() + "%";
            }
        }

        public static Bitmap ConvertToBitmap(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            return bitmap;
        }

        private void btn_select_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "C:\\Users\\h.hokmabadi\\Desktop\\test_preview";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                DetectColors(dlg.FileName, 1);
            }

        }

        private void btn_select_file2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "C:\\Users\\h.hokmabadi\\Desktop\\test_preview";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                DetectColors(dlg.FileName, 2);
            }
        }
    }
}
