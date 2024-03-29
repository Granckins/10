﻿using System;
using System.Collections.Generic;
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
using TelerikWpfApp1.Model;

namespace TelerikWpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Instruction_data ID = new Instruction_data();
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
        }

         

        private void RadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RadOpenFolderDialog openFolderDialog = new RadOpenFolderDialog();
            openFolderDialog.Owner = Window.GetWindow(this);
            openFolderDialog.ShowDialog(); 
            if (openFolderDialog.DialogResult == true)
            {
                string folderName = openFolderDialog.FileName;
                ID.Directory_name = folderName;
                var fg = Directory.GetFiles(folderName, "*.sps", SearchOption.AllDirectories);
                foreach (var f in fg)
                {
                    RLB_sps.Items.Add(System.IO.Path.GetFileName(f));
                }
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
                                ID.IP_names.Add(m.Replace("'",""));
                               line= line.Replace(m, "");
                            }
                            matches = line.Split(' ').ToList(); 
                            foreach (var m in matches)
                            {
                              if(m.Contains(".ip"))
                                    ID.IP_names.Add(m.Replace(";",""));

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
            List<string> lines_ = new List<string>();
            if (File.Exists(ID.Directory_name + @"\" + ID.IP_names.First()))
            {
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(ID.Directory_name + @"\" + ID.IP_names.First(), Encoding.GetEncoding(866));
                while ((line1 = file.ReadLine()) != null)
                {
                    lines_.Add(line1);
                }
                file.Close();
            }
        }

        private void Example_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
