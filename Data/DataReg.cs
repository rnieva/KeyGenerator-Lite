using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KeyGenerator1.Data
{
    public class DataReg
    {
        public int Id { get; set; }
        public string TargetKey { get; set; } 
        public string key { get; set; }
    }
}