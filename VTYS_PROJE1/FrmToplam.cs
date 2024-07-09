using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace VTYS_PROJE1
{
    public partial class FrmToplam : Form
    {
        public FrmToplam()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True");
        private void BtnKategoriSayisi_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select *  from TBLTOPLAMKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void BtnUrünSayisi_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select *  from TBLSTOK", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Form1 newform = new Form1();
            MessageBox.Show("Çıkış Yapılıyor...");
            this.Hide();
        }

        private void BtnMusteriSayisi_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select *  from TBLTOPLAMMUSTERI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
    }
 }

