using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KitapOgr.DAL;
using KitapOgr.BLL;

namespace KitapOgr.UI.Forms
{
    public partial class frmİslem : Form
    {
        public frmİslem()
        {
            InitializeComponent();
        }
        RepositoryBase<Ogrenciler> ogrRepo = new RepositoryBase<Ogrenciler>();
        RepositoryBase<Kitaplar> kitapRepo = new RepositoryBase<Kitaplar>();
        RepositoryBase<Islemler> islemRepo = new RepositoryBase<Islemler>();

        private void cbogrDoldur() {
            comboBox1.DataSource = ogrRepo.SelectAll();
            comboBox1.DisplayMember = "Ad";
            comboBox1.ValueMember = "OgrenciID";
        }
        private void cbKitapDoldur() {
            comboBox2.DataSource = kitapRepo.SelectAll();
            comboBox2.DisplayMember = "KitabAdi";
            comboBox2.ValueMember = "KitapID";
        }
        private void getir()
        {
            dataGridView1.DataSource = islemRepo.SelectAll();
        }
        private void Temizle()
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        private void frmİslem_Load(object sender, EventArgs e)
        {
            cbogrDoldur();
            cbKitapDoldur();
            getir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Islemler i = new Islemler();
                i.OgrenciID = (int)comboBox1.SelectedValue;
                i.KitapID = (int)comboBox2.SelectedValue;
                i.AlinanTarih = dateTimePicker1.Value;
                i.VerilenTarih = dateTimePicker2.Value;
                islemRepo.insert(i);
                MessageBox.Show("İşlem Başarı ile oluşturuldu");
                Temizle();
                getir();
            }
            catch (Exception)
            {

                MessageBox.Show("Kayıt oluşturulamadı");
            }
          
        }
        int seciliid;
        Islemler guncellenecek;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                seciliid = (int)dataGridView1.CurrentRow.Cells[0].Value;
                islemRepo.Delete(seciliid);
                MessageBox.Show("İşlem başarıyla silindi");
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
                guncellenecek = islemRepo.SelectById(seciliid);
                comboBox1.SelectedValue = guncellenecek.OgrenciID;
                comboBox2.SelectedValue = guncellenecek.KitapID;
                dateTimePicker1.Value = Convert.ToDateTime(guncellenecek.AlinanTarih);
                dateTimePicker2.Value = Convert.ToDateTime(guncellenecek.VerilenTarih);
            }
            catch (Exception)
            {

                MessageBox.Show("Seçim Doğru Bir Şekilde Yapılamadı");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                guncellenecek.OgrenciID = (int)comboBox1.SelectedValue;
                guncellenecek.KitapID = (int)comboBox2.SelectedValue;
                guncellenecek.AlinanTarih = dateTimePicker1.Value;
                guncellenecek.VerilenTarih = dateTimePicker2.Value;
                islemRepo.Update(guncellenecek);
                MessageBox.Show("Kayıt Başarıyla Güncellendi");
                getir();
            }
            catch (Exception)
            {

                MessageBox.Show("Güncelleme Başarısız");
            }
          
        }
    }
}
