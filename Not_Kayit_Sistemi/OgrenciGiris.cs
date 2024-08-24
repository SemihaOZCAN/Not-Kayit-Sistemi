using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Not_Kayit_Sistemi
{
    public partial class OgrGiris : Form
    {
        public OgrGiris()
        {
            InitializeComponent();
        }
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //OgrenciDetay formuna yonlendırme...
           OgrenciDetay frm = new OgrenciDetay();
            frm.numara = maskedTextBox1.Text;
            frm.ShowDialog();
          
        }      

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            //OgretmenDetay formuna yonlendırme..
            if (maskedTextBox1.Text == "1111")
            {
                OgretmenDetay fr = new OgretmenDetay();
                fr.Show();

            }
        }
    }
}
