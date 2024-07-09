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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnKategori_Click(object sender, EventArgs e)
        {
            FrmKategoriler fr=new FrmKategoriler();
            fr.Show();
        }

        private void BtnMusteri_Click(object sender, EventArgs e)
        {
            FrmMusteriler fr2= new FrmMusteriler();
            fr2.Show();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True");
        //Adres olduğunu belirtmek için @ işareti koyduk.
        private void Form1_Load(object sender, EventArgs e)
        {
            //Ürünleri Durum Seviyesine Göre Çalıştırdığım Prosedür
            SqlCommand komut = new SqlCommand("EXECUTE URUNDURUMFİLTRELE", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);//sqlden verileri çekmek için kullandığımız sınıfımız
            DataTable dt = new DataTable();//BU VE AŞAĞIDAKİ KOD SORGUDAN GELECEK OLAN DEĞERLERİ TABLODA DOLDUR DEMEKTİR.
            da.Fill(dt);
            dataGridView1.DataSource = dt;
         
            //Grafiğe Veri Çekme
            //manuel olarak chartta veri eklemek için series özelliğine git.

            //chart1.Series["Akdeniz"].Points.AddXY("Adana", 24);
            //chart1.Series["Akdeniz"].Points.AddXY("Isparta", 27);

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("CHARTKATEGORI",baglanti);//PROSEDÜR(SAKLI YORDAMLAR) KULLANILDI.
            SqlDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();
        }

        private void BtnHareketler_Click(object sender, EventArgs e)
        {
            FrmHareket fr3= new FrmHareket();
            fr3.Show();
        }

        private void BtnToplam_Click(object sender, EventArgs e)
        {
            FrmToplam fr4= new FrmToplam();
            fr4.Show();
        }
        private void BtnUrünler_Click_1(object sender, EventArgs e)
        {
            FrmUrünler fr5 = new FrmUrünler();
            fr5.Show();
        }
        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Çıkış Yapılıyor...");
            Application.Exit();
        }

        private void BtnKasa_Click(object sender, EventArgs e)
        {
            FrmKasa fr6=new FrmKasa();
            fr6.Show();
        }
        //FİLTRELEMELER
        private void BtnYasFiltrele_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Execute YASFİLTRELE", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        private void BtnCalıstıgıYilFiltrele_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Execute KIDEMFİLTRELE", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void btnPersoneller_Click(object sender, EventArgs e)
        {
            frmPersoneller frm=new frmPersoneller();
            frm.Show();
        }

        private void btnERDiyagram_Click(object sender, EventArgs e)
        {
            frmERDiyagram frm=new frmERDiyagram();
            frm.Show();
        }
    }
}
