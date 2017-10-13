using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KitapOgr.BLL;
using KitapOgr.DAL;

namespace KitapOgr.UI.Forms
{
    public partial class frmOgrenci : Form
    {
        public frmOgrenci()
        {
            InitializeComponent();
        }
        Ogrenciler ogr = new Ogrenciler();
        RepositoryBase<Ogrenciler> ogrRepo = new RepositoryBase<Ogrenciler>();
        private void temizle() {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    txt.Clear();
                }
                if (c is ComboBox)
                {
                    ComboBox cb=(ComboBox)c;
                    cb.SelectedIndex = 0;
                }
                if (c is DateTimePicker)
                {
                    DateTimePicker dp = (DateTimePicker)c;
                    dp.Value = DateTime.Now; ;
                }
                if (c is NumericUpDown)
                {
                    NumericUpDown num=(NumericUpDown)c;
                    num.Value = 0;
                }
            }
        }
        private void getir()
        {
            dataGridView1.DataSource = ogrRepo.SelectAll();
        }

        private void frmOgrenci_Load(object sender, EventArgs e)
        {
            getir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ogr.Ad = textBox1.Text;
                ogr.Soyad = textBox2.Text;
                ogr.Cinsiyet = comboBox1.Text;
                ogr.DTarih = dateTimePicker1.Value;
                ogr.Sınıf = comboBox2.Text;
                ogr.Puan = (int)numericUpDown1.Value;
                ogrRepo.insert(ogr);
                temizle();
                MessageBox.Show("Öğrenci Başarıyla Eklendi");
                getir();
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt Eklenemedi");
            }
          
        }
        int seciliid;
        Ogrenciler guncellenecek;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                seciliid = (int)dataGridView1.CurrentRow.Cells[0].Value;
                ogrRepo.Delete(seciliid);
                MessageBox.Show("Öğrenci Silindi");
                getir();
            }
            catch (Exception)
            {

                MessageBox.Show("Kayıt Silinemedi");
            }
        
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                seciliid = (int)dataGridView1.CurrentRow.Cells[0].Value;
                guncellenecek = ogrRepo.SelectById(seciliid);
                textBox1.Text = guncellenecek.Ad;
                textBox2.Text = guncellenecek.Soyad;
                comboBox1.Text = guncellenecek.Cinsiyet;
                dateTimePicker1.Value = Convert.ToDateTime(guncellenecek.DTarih);
                comboBox2.Text = guncellenecek.Sınıf;
                numericUpDown1.Value = (int)guncellenecek.Puan;
            }
            catch (Exception)
            {

                MessageBox.Show("Seçim işlemi başarısız");
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                guncellenecek.Ad = textBox1.Text;
                guncellenecek.Soyad = textBox2.Text;
                guncellenecek.Cinsiyet = comboBox1.Text;
                guncellenecek.DTarih = dateTimePicker1.Value;
                guncellenecek.Sınıf = comboBox2.Text;
                guncellenecek.Puan = (int)numericUpDown1.Value;
                ogrRepo.Update(guncellenecek);
                MessageBox.Show("Öğrenci kaydı güncellendi");
                getir();
            }
            catch (Exception)
            {

                MessageBox.Show("Güncelleme başarısız");
            }
           
        }
    }
}
