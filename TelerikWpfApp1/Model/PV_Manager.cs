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
        public string BIBL_path = "";
      public static  Dictionary<string, string> SearchProcInPV(Dictionary<string, string> D, string pathdirectory, string name)
        {
            string line = string.Empty;
            var Proc = new Dictionary<string, string>();
            if (File.Exists(pathdirectory + @"\" + name))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(pathdirectory + @"\" + name, Encoding.GetEncoding(866));
                while ((line = file.ReadLine()) != null)
                {
                   if( line.StartsWith("ПРОЦ"))
                    {
                        try
                        {
                            var arr = line.Split(' ').ToArray();
                            if (arr.Contains("ВЫБ_БОИ"))
                            {
                             var   line1 = "fsfdsfs 'ВНЕШ' ";
                                var result = from Match match in Regex.Matches(line1, "'[^']*'")
                                             select match.ToString();
                                continue;
                            }
                                
                            if (D.ContainsKey(arr[1]))
                                continue;
                            Proc.Add(arr[1], pathdirectory + @"\" + name);
                        }
                        catch (Exception e) { }                        
                    }
                }

                }
            return Proc;
        }
    }
}
