using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TelerikWpfApp1.Model
{
   public abstract  class PV_Manager
    {
        static string StripComments(string code)
        {
            var re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            return Regex.Replace(code, re, "$1");
        } 
      public static Dictionary<string, string> SearchProcInPV(Dictionary<string, string> D_in,string pathdirectory, string name)
        {
            int i = 0;
            var P=new Dictionary<string, string>();
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
                        if ((arr.First().ToString().Contains("ПРОЦ")  || arr.First().ToString().Contains("ПРОЦЕДУРА")   ) && (Array.Exists(arr, x => x.Contains("ОБЩ")) || Array.Exists(arr, x => x.Contains("ОБЩАЯ"))))
                        {
                            i++;
                            if(arr.Count()==1)
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
