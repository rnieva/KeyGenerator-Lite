using System;
using System.Collections.Generic;
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
using System.Data.Entity;
using System.Security.Cryptography;
using KeyGenerator1.Model;
using KeyGenerator1.Data;

namespace KeyGenerator1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Database.SetInitializer<DemoContext2>(new CreateDatabaseIfNotExists<DemoContext2>());
            InitializeComponent();
            comboBox1.Items.Add("Numbers");
            comboBox1.Items.Add("Letters");
            comboBox1.Items.Add("Letters Uppercase");
            comboBox1.Items.Add("AlphaNumeric");
            comboBox1.Items.Add("AlphaNumeric Uppercase");
            comboBox1.Items.Add("Symbols");
            comboBox1.Items.Add("MixTotal");
            comboBox1.SelectedItem = "Numbers";
            comboBox2.Items.Add("8 bits");
            comboBox2.Items.Add("16 bits");
            comboBox2.Items.Add("32 bits");
            comboBox2.SelectedItem = "8 bits";
        }
        private void buttonGenerator_Click(object sender, RoutedEventArgs e)
        {
            int maxSize = MaxSize();
            string keyTypeStr = KeyTypeStr();
            labelKeyGenerated.Content = Generator(keyTypeStr, maxSize);
        }
        private string Generator(string keyTypeStr, int maxSize)
        {
            int tamA = keyTypeStr.Length;
            char[] chars = new char[tamA];
            string a = keyTypeStr;
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }
        private string KeyTypeStr()
        {
            string typeStr = "";
            var item = comboBox1.Text;
            switch (item)
            {
                case "Numbers":
                    typeStr = "1234567890";
                    break;
                case "Letters":
                    typeStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case "Letters Uppercase":
                    typeStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case "AlphaNumeric":
                    typeStr = ("abcdefghijklmnopqrstuvwxyz1234567890");
                    break;
                case "AlphaNumeric Uppercase":
                    typeStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    break;
                case "Symbols":
                    typeStr = "@#€%&=Ç+*-.:!¡¿?()";
                    break;
                case "MixTotal":
                    typeStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@#€%&=Ç+*-.:!¡¿?()";
                    break;
                default:
                    typeStr = "1234567890";
                    break;
            }
            return typeStr;
        }
        public int MaxSize()
        {
            int index = comboBox2.SelectedIndex;
            int maxSize = 8;
            switch (index)
            {
                case 0:
                    maxSize = 8;
                    break;
                case 1:
                    maxSize = 16;
                    break;
                case 2:
                    maxSize = 32;
                    break;
                default:
                    maxSize = 8;
                    break;
            }
            return maxSize;
        }
        private void buttonSaveKey_Click(object sender, RoutedEventArgs e)
        {
               if ((textBoxSiteKey.Text.Length != 0) && (textBoxSiteKey.Text != "Site Key") && (textBoxSiteKey.Text.Length != 0) && (textBoxSiteKey.Text != "Write your own Key"))
                 {
                    var dbContext = new DemoContext2();
                    var info = new DataReg() { TargetKey = textBoxSiteKey.Text, key = labelKeyGenerated.Content.ToString() };
                    dbContext.DataKeys.Add(info);
                    dbContext.SaveChanges();
                    MessageBox.Show("Key User Save");
                 }
                  else
                 {
                    MessageBox.Show("Site Key or Key into null");
                 }
        }
        private void buttonStoreKeys_Click(object sender, RoutedEventArgs e)
        {
            ViewData formData = new ViewData();
            formData.Show();
        }

        private void button_CopyToClipboard(object sender, RoutedEventArgs e)
        {
            if ((string)labelKeyGenerated.Content != "")
                System.Windows.Forms.Clipboard.SetText((string)labelKeyGenerated.Content);
        }
    }
}
