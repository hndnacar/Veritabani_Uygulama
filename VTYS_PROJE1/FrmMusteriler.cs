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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True");
        //Neden global alana tanımladım?
        //Çünkü her defasında her bir buton için de yeniden yeniden bunu yazmamam için.

        //FONKSİYON TANIMLAYACAĞIM.
        void Listele()
        {
            SqlCommand komut = new SqlCommand("select *  from TBLMUSTERI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("SELECT * FROM TBLSEHIRLER", baglanti);
            SqlDataReader dr= komut1.ExecuteReader();
            while(dr.Read())
            {
                CmbSehir.Items.Add(dr["SEHIRAD"]);
            }
            baglanti.Close();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Tablodaki herhangi bir yere tıkladığında o satırdaki değerleri yandaki yerde göstermiş oluyor.
            TxtID.Text =dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();                                                     
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("MusteriEkle",baglanti);
            komut.CommandType = System.Data.CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@MUSTERIAD", TxtAd.Text);
            komut.Parameters.AddWithValue("@MUSTERISOYAD", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@MUSTERISEHIR", CmbSehir.Text);
            komut.Parameters.AddWithValue("@MUSTERIBAKIYE", decimal.Parse(TxtBakiye.Text));
            
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Sisteme Kaydedildi.");
            Listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("MusteriSil", baglanti);
            komut.CommandType = System.Data.CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@MUSTERIID", TxtID.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Silindi");
            Listele();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("MusteriGüncelle", baglanti);
            komut.CommandType = System.Data.CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@MUSTERIAD", TxtAd.Text);
            komut.Parameters.AddWithValue("@MUSTERISOYAD", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@MUSTERISEHIR", CmbSehir.Text);
            komut.Parameters.AddWithValue("@MUSTERIBAKIYE", decimal.Parse(TxtBakiye.Text));
            komut.Parameters.AddWithValue("@MUSTERIID", TxtID.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Güncellendi");
            Listele();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("MüşteriAra", baglanti);
            komut.CommandType = System.Data.CommandType.StoredProcedure;
            //sadece e veya başka bir harf için arama yapmak istersen lıke komutunu kullan.
            komut.Parameters.AddWithValue("@MUSTERIAD", TxtAd.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Form1 newform = new Form1();
            MessageBox.Show("Çıkış Yapılıyor...");
            this.Hide();
        }

        private void txtMusteriAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "select * from TBLMUSTERI WHERE MUSTERIAD LIKE @P1";
            SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
            //sadece e veya başka bir harf için arama yapmak istersen lıke komutunu kullan.
            da.SelectCommand.Parameters.AddWithValue("@P1", "%" + txtMusteriAra.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
