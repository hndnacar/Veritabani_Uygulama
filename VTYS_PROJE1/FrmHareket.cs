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
    public partial class FrmHareket : Form
    {
        public FrmHareket()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=PC-ENG-HANDAN\SQLEXPRESS;Initial Catalog=SATİSVT;Integrated Security=True");
        private void FrmHareket_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da= new SqlDataAdapter("SELECT * FROM Siparisler",baglanti); //VİEW İÇİNDE KENDİM OLUŞTURDUĞUM FORNKSİYONU KULLANDIM.
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
    }
}
