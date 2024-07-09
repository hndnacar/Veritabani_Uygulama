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

namespace VTYS_PROJE1
{
    public partial class FrmKategoriler : Form
    {
        public FrmKategoriler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True");
        private void BtnListele_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLKATEGORI", baglanti);
            SqlDataAdapter da= new SqlDataAdapter(komut);
            DataTable dt=new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("KategoriEkle", baglanti);
            komut2.CommandType = System.Data.CommandType.StoredProcedure;
            komut2.Parameters.AddWithValue("@KATEGORIAD", TxtKategoriAd.Text);
            baglanti.Open();
            komut2.ExecuteNonQuery();//sorguyu çalıştırıp sorgudaki değişiklikleri tabloya yansıt.
            baglanti.Close();
            MessageBox.Show("Kategori Kaydetme İşlemi Başarıyla Gerçekleşti.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKategoriID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKategoriAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("KategoriSil", baglanti);
            komut3.CommandType = System.Data.CommandType.StoredProcedure;
            komut3.Parameters.AddWithValue("@KATEGORIID", TxtKategoriID.Text);
            baglanti.Open();
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Silme İşlemi Gerçekleşti.");
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("KategoriGüncelle", baglanti);
            komut4.CommandType = System.Data.CommandType.StoredProcedure;
            komut4.Parameters.AddWithValue("@KATEGORIAD", TxtKategoriAd.Text);
            komut4.Parameters.AddWithValue("@KATEGORIID", TxtKategoriID.Text);
            baglanti.Open();
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Güncelleme İşlemi Gerçekleşti.");
        }

        private void FrmKategoriler_Load(object sender, EventArgs e)
        {

        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Form1 newform = new Form1();
            MessageBox.Show("Çıkış Yapılıyor...");
            this.Hide();
        }
    }                                                                                   
}
//Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True
