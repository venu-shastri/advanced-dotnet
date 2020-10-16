using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SynchronizationContext _context;
        public Form1()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() => {
            Thread.Sleep(3000);
            // this.textBox1 .Text=
            var currentTime = System.DateTime.Now.ToLongTimeString();
                _context.Post(new SendOrPostCallback((obj) => {

                    this.textBox1.Text = obj.ToString();
                }), currentTime);
            
            }).Start();
        }

        
    }
}
