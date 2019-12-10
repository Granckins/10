using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TelerikWpfApp1.Model
{
    public abstract class PV_Manager
    {
        static string StripComments(string code)
        {
            var re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            return Regex.Replace(code, re, "$1");
        }
        static string StripOneLine(string code)
        {
            code = Regex.Replace(code, @"[ \r\n\t]", " ");
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            code = regex.Replace(code, " ");
            return code;
        }
        static string StripNoDelimetr(string code)
        {
            return code.Replace(";", "");
        }
        public static Tuple<Dictionary<string,string>, Dictionary<string,string>> SearchTaskInPV(string Directory_name, string SPS_name, string Bibl_name)
        {
            Dictionary<string, string> tasks = new Dictionary<string, string>();
            Dictionary<string, string> procs = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(Directory_name + @"\" + SPS_name, Encoding.GetEncoding(866)))
            {
                // Read the stream to a string, and write the string to the console.

                string line;

                line = sr.ReadLine();
                var code = sr.ReadToEnd();
                code = StripComments(code);
                code = StripOneLine(code);
                code = StripNoDelimetr(code);
                var arr = code.Split(' ').ToList();
                arr.RemoveAll(u => u=="");
                if (arr.Count >= 2)
                {
                    arr.RemoveAt(0);
                    arr.RemoveAt(0);
                }
                //
                int count = arr.Count;
                int i = 0;
                for(i=0;i<count;i++)
                {
                    if (!arr[i].Contains(".ip"))
                    {
                        var path = "";
                        if (arr[i + 1].ToLower().Contains("библ"))
                        {
                            path = Bibl_name + System.IO.Path.GetFileName(arr[i + 1]);
                        }
                         string newPath = Path.Combine(Directory_name, arr[i+1]);
                        path = Path.GetFullPath(newPath);
                        tasks.Add(arr[i],path);
                        i++;
                    }
                    else
                    {
                        var path = "";
                        if (arr[i].ToLower().Contains("библ"))
                        {
                            path = Bibl_name + System.IO.Path.GetFileName(arr[i]);
                            procs.Add(System.IO.Path.GetFileName(arr[i]), path);
                        }
                        else
                        {
                            string newPath = Path.Combine(Directory_name, arr[i]);
                            path = Path.GetFullPath(newPath);
                            procs.Add(System.IO.Path.GetFileName(arr[i]), path);
                        }
                    }
                }


            }
            return new Tuple<Dictionary<string, string>, Dictionary<string, string>>(tasks, procs);
        }
        public static Dictionary<string, string> SearchProcInPV(Dictionary<string, string> D_in, string pathdirectory, string name)
        {
            int i = 0;
            var P = new Dictionary<string, string>();
            if (File.Exists(pathdirectory + @"\" + name))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(pathdirectory + @"\" + name, Encoding.GetEncoding(866));
                var input = file.ReadToEnd();
                string noComments = "";
                if (input != null)
                {
                    noComments = StripComments(input);
                }
                using (StringReader reader = new StringReader(noComments))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {



                        var arr = line.Split(new Char[] { ' ', '\t' }).ToArray();
                        if ((arr.First().ToString().Contains("ПРОЦ") || arr.First().ToString().Contains("ПРОЦЕДУРА")) && (Array.Exists(arr, x => x.Contains("ОБЩ")) || Array.Exists(arr, x => x.Contains("ОБЩАЯ"))))
                        {
                            i++;
                            if (arr.Count() == 1)
                                arr = line.Split('\t').ToArray();
                            try
                            {


                                if (D_in.ContainsKey(arr[1]))
                                    continue;
                                P.Add(arr[1], pathdirectory + @"\" + name);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                }
            }
            return P;
        }
    }
}
