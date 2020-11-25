using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplik_Proje
{
    public partial class kitap_kayit : Form
    {
        public kitap_kayit()
        {
            InitializeComponent();

        }
        string durum = "";

        OleDbConnection baglanti= new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\MUSOG_BA\Desktop\C#\access\kitaplik.mdb");

        void listele()
        {

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        void temizle()
        {
            TxtKitapadi.Text = "";
            Txtyazar.Text = "";
            TxtSayfa.Text = "";
            CmbTur.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listele();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into kitaplar (Kitapadi,Yazar,Tur,Sayfa,Durum) values(@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1",TxtKitapadi.Text);
            komut.Parameters.AddWithValue("@p2",Txtyazar.Text);
            komut.Parameters.AddWithValue("@p3", CmbTur.Text);
            komut.Parameters.AddWithValue("@p4", TxtSayfa.Text);
            komut.Parameters.AddWithValue("@p5", durum);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KİTAP KAYDINIZ YAPILMIŞTIR.");
            listele();
            temizle();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            TxtKitapadi.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            Txtyazar.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            CmbTur.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secim].Cells[5].Value.ToString()=="True")
            {
                radioButton1.Checked=true;
            

            }
            else
            {
                radioButton2.Checked = true;
     
            }
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("delete from kitaplar where Kİtapid=@d1",baglanti);
            komut1.Parameters.AddWithValue("@d1",Txtid.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Başarılı","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut2 = new OleDbCommand("update kitaplar set Kitapadi=@k1,Yazar=@k2,Sayfa=@k3,Tur=@k4,Durum=@k5 where Kitapid=@k6", baglanti);
            komut2.Parameters.AddWithValue("@k1",TxtKitapadi.Text);
            komut2.Parameters.AddWithValue("@k2", Txtyazar.Text);
            komut2.Parameters.AddWithValue("@k3", TxtSayfa.Text);
            komut2.Parameters.AddWithValue("@k4", CmbTur.Text);
            if (radioButton1.Checked==true)
            {
                komut2.Parameters.AddWithValue("@k5",true);
            }
            if (radioButton2.Checked==true)
            {
                komut2.Parameters.AddWithValue("@k5",false);
            }
            komut2.Parameters.AddWithValue("@k6", Txtid.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşleminiz Başarılı");
            listele();
            temizle();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komutbul = new OleDbCommand("Select * from kitaplar where Kitapadi=@p1",baglanti);
            komutbul.Parameters.AddWithValue("@p1",TxtBul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komutbul);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komutbul = new OleDbCommand("Select * from kitaplar where Kitapadi like '%"+TxtBul.Text+"%'",baglanti);
           
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komutbul);
            da.Fill(dt);
            dataGridView1.DataSource = dt; 
            baglanti.Close();
        }
    }
}
