using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leaf.xNet;
using System.IO;

namespace ProxyChecker
{
    public partial class Form1 : Form
    {
        List<string> Proxy1 { get; set; } = new List<string>();

        string path1 = "C:\\Users\\misha\\Desktop\\Proxy.txt";
        public Form1()
        {
            InitializeComponent();
        }

        #region Open proxy
        List<string> OpenProxy(string Path)
        {
            List<string> Proxy = File.ReadAllLines(Path).ToList();
            return Proxy;
        }

         
        private void button1_Click(object sender, EventArgs e)//open
        {
            Proxy1 = OpenProxy(path1);
            label3.Text = $"Proxy [{Proxy1.Count}]";
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            string n = "\n";
            for (int i = 0; i < richTextBox1.Lines.Count(); i++)
            {
                string link = richTextBox1.Lines[i];
                richTextBox2.AppendText(link + n + Request.Get(link, Proxy1[0].Replace(" ", "")) + n);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }

    public class Request
    {
        public static string Get(string Link, string Proxy) //Leaf.xNet.HttpException
        {
            HttpRequest request = new HttpRequest();
            request.KeepAlive = true;
            request.UserAgentRandomize();
            try
            {
                request.Proxy = ProxyClient.Parse(ProxyType.Socks5, Proxy);
                request.Get(Link).ToString();

            }
            catch
            {
                request.Proxy = ProxyClient.Parse(ProxyType.Socks4, Proxy);
            }

            //Httpresponse и проверять значение, вместо параши снизу
            string response = "";
            try
            {
                request.Get(Link).ToString();
                response = "ok";
            }
            catch (HttpException err)
            {
                response = err.Message;
            }
            return response;
        }
    }
}
