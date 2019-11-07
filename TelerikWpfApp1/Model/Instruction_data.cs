﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikWpfApp1.Model
{
   public class Instruction_data
    {
        public string SPS_name { get; set; }
        public List<string> IP_names { get; set; }
        public Dictionary<string, string> Proc_ip_path { get; set; }
        public string Directory_name { get; set; }
        public string Bibl_name
        {
            get; set;
        }
       public Instruction_data()
        {
            IP_names = new List<string>();
            Proc_ip_path = new Dictionary<string, string>();
        }
    }
}
