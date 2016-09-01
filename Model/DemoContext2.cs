using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KeyGenerator1.Data;

namespace KeyGenerator1.Model
{
    public class DemoContext2 : DbContext
    {
        public DbSet<DataReg> DataKeys { get; set; } 
    }
}