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
    public partial class frmPersoneller : Form
    {
        public frmPersoneller()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True");

        void Listele()
        {
            SqlCommand komut = new SqlCommand("select *  from TBLPERSONEL", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmPersoneller_Load(object sender, EventArgs e)
        {
            Listele();
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPersonelYas.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPPersonelÇlştığıYil.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("PersonelEkle", baglanti);
            komut.CommandType = System.Data.CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@PersonelAD", TxtAd.Text);
            komut.Parameters.AddWithValue("@PersonelSOYAD", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@PersonelYAS", int.Parse(txtPersonelYas.Text));
            komut.Parameters.AddWithValue("@PersonelCalistigiTarih", int.Parse(txtPPersonelÇlştığıYil.Text));

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Sisteme Kaydedildi.");
            Listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("PersonelSil", baglanti);
            komut.CommandType = System.Data.CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@PersonelD", TxtID.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Silindi");
            Listele();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("PersonelGüncelle", baglanti);
            komut.CommandType = System.Data.CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@PersonelD", TxtID.Text);
            komut.Parameters.AddWithValue("@PersonelAD", TxtAd.Text);
            komut.Parameters.AddWithValue("@PersonelSOYAD", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@PersonelYAS", int.Parse(txtPersonelYas.Text));
            komut.Parameters.AddWithValue("@PersonelCalistigiTarih", int.Parse(txtPPersonelÇlştığıYil.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Güncellendi");
            Listele();
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Form1 newform = new Form1();
            MessageBox.Show("Çıkış Yapılıyor...");
            this.Hide();
        }
    }
}
