using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using KeyGenerator1.Data;
using KeyGenerator1.Model;

namespace KeyGenerator1
{
    public partial class ViewData : Form
    {
        public ViewData()
        {
            Database.SetInitializer<DemoContext2>(new CreateDatabaseIfNotExists<DemoContext2>());
            InitializeComponent();
            showDataGrid();
        }
        void showDataGrid()
        {
            var dbContext = new DemoContext2();
            System.Collections.ObjectModel.ObservableCollection<DataReg> valuesBd = new System.Collections.ObjectModel.ObservableCollection<DataReg>();
            var listBd = from p in dbContext.DataKeys select p;
            foreach (var p in listBd)
            {
                valuesBd.Add(new DataReg() { Id = p.Id, TargetKey = p.TargetKey, key = p.key });
            }
            dataGridView1.DataSource = valuesBd;
        }
    }
}
