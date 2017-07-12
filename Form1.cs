using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ConStr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox1.Text = fbd.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(textBox1.Text);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("web.config*", System.IO.SearchOption.AllDirectories); //Getting Text files
    
            foreach (FileInfo file in Files)
            {
               // XmlTextReader reader = new XmlTextReader(file);
               
                XmlDocument doc = new XmlDocument();
                doc.Load(file.FullName);
                XmlNodeList nodelist = doc.DocumentElement.GetElementsByTagName("connectionStrings");
                foreach(XmlNode node in nodelist)
                {
                    richTextBox1.AppendText(file.FullName);
                    richTextBox1.AppendText(Environment.NewLine);
                    XmlNodeList subnodelist=node.ChildNodes;
                    foreach(XmlNode addNode in subnodelist)
                    {
                        richTextBox1.AppendText(addNode.Attributes["connectionString"].Value);
                        richTextBox1.AppendText(Environment.NewLine);
                        richTextBox1.AppendText("-------------------------------");
                        richTextBox1.AppendText(Environment.NewLine);
                    }

                   
                    richTextBox1.AppendText("--------------New File -----------------");
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText(Environment.NewLine);

                }
            }
            MessageBox.Show("Done!");
        }
    }
}
