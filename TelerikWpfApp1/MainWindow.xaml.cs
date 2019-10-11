using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using TelerikWpfApp1.Helpers;
using TelerikWpfApp1.Model;

namespace TelerikWpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string Joke
        {
            get { return (string)GetValue(JokeProperty); }
            set { SetValue(JokeProperty, value); }
        }
        public bool Vis
        {
            get { return (bool)GetValue(VisProperty); }
            set { SetValue(VisProperty, value); }
        }
        public static readonly DependencyProperty JokeProperty =
            DependencyProperty.Register("Joke", typeof(string), typeof(MainWindow), new PropertyMetadata(null));
        public static readonly DependencyProperty VisProperty =
                DependencyProperty.Register("Vis", typeof(bool), typeof(MainWindow), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;
        Instruction_data ID = new Instruction_data();
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
            this.DataContext = this;
           Joke = "Путь к БИБЛ не задан";
            Alert_icon.Visibility = Visibility.Visible;
        }
        public StatusUpdate Status { get; set; } = new StatusUpdate();
        private void RadMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BIBL_Click(object sender, RoutedEventArgs e)
        {
            RadOpenFolderDialog openFolderDialog = new RadOpenFolderDialog();
            openFolderDialog.Owner = Window.GetWindow(this);
            openFolderDialog.ShowDialog(); 
            if (openFolderDialog.DialogResult == true)
            {
                string folderName = openFolderDialog.FileName;
                ID.Directory_name = folderName;
                Joke = "Путь к БИБЛ: "+ ID.Directory_name;
            }
        }



        private void Btn_sps_Click_1(object sender, RoutedEventArgs e)
        {
            ID.SPS_name = RLB_sps.SelectedItem.ToString();
            ID.IP_names = new List<string>();
            var lines = new List<string>();


            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(ID.Directory_name + @"\" + ID.SPS_name, Encoding.GetEncoding(866)))
                {
                    // Read the stream to a string, and write the string to the console.

                    string line;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line.StartsWith("\\"))
                            continue;
                        else
                        {
                            MatchCollection matchList = Regex.Matches(line, "\'[^\']*\'");
                            var matches = matchList.Cast<Match>().Select(match => match.Value).ToList();
                            foreach (var m in matches)
                            {
                                ID.IP_names.Add(m.Replace("'", ""));
                                line = line.Replace(m, "");
                            }
                            matches = line.Split(' ').ToList();
                            foreach (var m in matches)
                            {
                                if (m.Contains(".ip"))
                                    ID.IP_names.Add(m.Replace(";", ""));

                            }
                        }
                    }



                }
            }
            catch (IOException ef)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ef.Message);
            }
            string line1 = string.Empty;
            int start = 0;
            int step = 20;
            string[] stringArray = { "text1", "text2", "text3", "text4" };
            string value = "text3";
            List<string> lines_ = new List<string>();
            tabControl1.GetData();
            foreach (var f in ID.IP_names)
            {
                if (File.Exists(ID.Directory_name + @"\" + f))
                {
                    // Read the file and display it line by line.
                    System.IO.StreamReader file = new System.IO.StreamReader(ID.Directory_name + @"\" + f, Encoding.GetEncoding(866));
                    var ddd =
                   ID.Proc_ip_path = ID.Proc_ip_path.Concat(PV_Manager.SearchProcInPV(ID.Proc_ip_path, ID.Directory_name, f)).ToDictionary(x=>x.Key, x=>x.Value);
                    while ((line1 = file.ReadLine()) != null)
                    {
                     //   lines_.Add(line1);
                        start += step;
                   //     int pos = Array.IndexOf(stringArray, value);
                     //   if (pos > -1)
                     //if(line1)
                     //   {
                     //       // the array contains the string and the pos variable
                     //       // will have its position in the array
                     //   }
                    }
                    file.Close();
                }
            }
        }
    }
}
