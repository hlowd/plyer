using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Con;
namespace Win
{
    public partial class WinGenerate : Form
    {
        public WinGenerate()
        {
            InitializeComponent();
        }

        private string getMd6Str(string path)
        {
            MD5 _md5Handler = MD5.Create();
            // Convert the input string to a byte array and compute the hash.

            byte[] data = _md5Handler.ComputeHash(File.ReadAllBytes(path));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {

                byte tmp = (byte)((data[i] + i) % 255);

                sBuilder.Append(tmp.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\MSVC60.dll"))
            {
                MessageBox.Show("准备好库函数！");
                return;
            }
            DateTime tmp = DateTime.Parse(dtpSet.Value.ToLongDateString());
            dtpSet.Value =tmp;
            TestTime tt = new TestTime(tmp,tmp.AddMonths(1),getMd6Str(@".\MSVC60.dll"));
            BinaryFormatter b = new BinaryFormatter();
            FileStream f = File.Create(@".\Player.dll");
            b.Serialize(f,tt);
            f.Flush();
            f.Close();
            btnGenerate.Enabled = false;
            dtpSet.Enabled = false;
            MessageBox.Show("注册文件生成完毕！"); 
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnGenerate.Enabled = true;
            dtpSet.Enabled = true;
        }
       
    }

}
