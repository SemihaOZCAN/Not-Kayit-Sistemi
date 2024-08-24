using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Sistemi
{
    public partial class OgretmenDetay : Form
    {
        public OgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True");


        private void OgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TblDers' table. You can move, or remove it, as needed.
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);
            int gecenSayisi = 0;
            int kalanSayisi = 0;

            foreach (DataRow row in this.dbNotKayitDataSet.TblDers.Rows)
            {
                if (row["DURUM"].ToString() == "True")
                {
                    gecenSayisi++;
                }
                else
                {
                    kalanSayisi++;
                }
            }

            lblGecenSayisi.Text = "Geçen Sayısı: " + gecenSayisi.ToString();
            lblKalanSayisi.Text = "Kalan Sayısı: " + kalanSayisi.ToString();
        }

    

        private void btnOgrKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TblDers (OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", mskNumara.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);

            // Geçen ve kalan öğrenci sayısını güncelle
            GecenKalanSayisiGuncelle();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();   
            txtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            lblOrtalama.Text=dataGridView1.Rows[secilen].Cells[7].Value.ToString();

        }

        private void btnNotGuncelle_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;

            s1 = Convert.ToDouble(txtSinav1.Text);
            s2 = Convert.ToDouble(txtSinav2.Text);
            s3 = Convert.ToDouble(txtSinav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblOrtalama.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TblDers set OGRS1=@p1,OGRS2=@p2,OGRS3=@p3,ORTALAMA=@p4,DURUM=@p5 WHERE OGRNUMARA=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1", txtSinav1.Text);
            komut.Parameters.AddWithValue("@p2", txtSinav2.Text);
            komut.Parameters.AddWithValue("@p3", txtSinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(lblOrtalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", mskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Başarılı Bir Şekilde Güncellendi.");
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.TblDers);

            // Geçen ve kalan öğrenci sayısını güncelle
            GecenKalanSayisiGuncelle();

        }
        private void GecenKalanSayisiGuncelle()
        {
            int gecenSayisi = 0;
            int kalanSayisi = 0;

            foreach (DataRow row in this.dbNotKayitDataSet.TblDers.Rows)
            {
                if (row["DURUM"].ToString() == "True")
                {
                    gecenSayisi++;
                }
                else
                {
                    kalanSayisi++;
                }
            }

            lblGecenSayisi.Text = "Geçen Sayısı: " + gecenSayisi.ToString();
            lblKalanSayisi.Text = "Kalan Sayısı: " + kalanSayisi.ToString();
        }
    }
}
