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
using System.Data.SqlTypes;
using System.Data.Common;

namespace VTYS_PROJE1
{
    public partial class FrmUrünler : Form
    {
        public FrmUrünler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True");

        void Listele()
        {
            SqlCommand komut = new SqlCommand("select *  from TBLURUNLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void KategoriListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from KATEGORILER", baglanti);//VIEW KULLANILDI.
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKategori.ValueMember = "KATEGORIID";
            CmbKategori.DisplayMember = "KATEGORIAD";
            CmbKategori.DataSource = dt;
        }
        private void FrmUrünler_Load(object sender, EventArgs e)
        {
            Listele();
            //KategoriListesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtMarka.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtUrunAlisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtUrunSatisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtUrunStok.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtUrunDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }
        private void BtnListele_Click_1(object sender, EventArgs e)
        {
            Listele();
        }

        private void BtnKaydet_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtAd.Text))
            {
                MessageBox.Show("ÜRÜN ADI BOŞ GEÇİLEMEZ.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into TBLURUNLER(URUNAD,URUNMARKA,KATEGORI,URUNALISFIYAT,URUNSATISFIYAT,URUNSTOK,URUNDURUM) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7)", baglanti);
                    komut.Parameters.AddWithValue("@P1", TxtAd.Text);
                    komut.Parameters.AddWithValue("@P2", TxtMarka.Text);
                    komut.Parameters.AddWithValue("@P3", CmbKategori.SelectedValue);
                     komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtUrunAlisFiyat.Text));
                     komut.Parameters.AddWithValue("@P5", decimal.Parse(TxtUrunSatisFiyat.Text));
                     komut.Parameters.AddWithValue("@P6", TxtUrunStok.Text);
                    komut.Parameters.AddWithValue("@P7", TxtUrunDurum.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Ürün Sisteme Kaydedildi.");
                    Listele();
            } 
        }

        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From TBLurunler WHERE URUNID=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Silindi");
            Listele();
        }

        private void BtnGüncelle_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLURUNLER SET URUNAD=@P1,URUNMARKA=@P2,KATEGORI=@P3,URUNALISFIYAT=@P4,URUNSATISFIYAT=@P5,URUNSTOK=@P6,URUNDURUM=@P7 WHERE URUNID=@P8", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@P3", CmbKategori.SelectedValue);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtUrunAlisFiyat.Text));
            komut.Parameters.AddWithValue("@P5", decimal.Parse(TxtUrunSatisFiyat.Text));
            komut.Parameters.AddWithValue("@P6", TxtUrunStok.Text);
            komut.Parameters.AddWithValue("@P7", TxtUrunDurum.Text);
            komut.Parameters.AddWithValue("@P8", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Güncellendi");
            Listele();
        }
        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Form1 newform = new Form1();
            MessageBox.Show("Çıkış Yapılıyor...");
            this.Hide();
        }
        private void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "select * from TBLURUNLER WHERE URUNAD LIKE @P1";
            SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
            //sadece e veya başka bir harf için arama yapmak istersen lıke komutunu kullan.
            da.SelectCommand.Parameters.AddWithValue("@P1", "%" + txtUrunAra.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void CmbKategori_Click(object sender, EventArgs e)
        {
            KategoriListesi();
        }
    }
}
