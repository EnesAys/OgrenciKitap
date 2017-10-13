using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KitapOgr.UI.Forms.KitapFormlar;
using KitapOgr.BLL;
using KitapOgr.DAL;

namespace KitapOgr.UI.Forms
{
    public partial class frmKitap : Form
    {
        public frmKitap()
        {
            InitializeComponent();
        }
        Kitaplar kit = new Kitaplar();
        RepositoryBase<Kitaplar> kRepo = new RepositoryBase<Kitaplar>();
        RepositoryBase<Turler> tRepo = new RepositoryBase<Turler>();
        RepositoryBase<Yazarlar> yRepo = new RepositoryBase<Yazarlar>();
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbYazarDoldur() {
            comboBox1.DataSource = yRepo.SelectAll();
            comboBox1.DisplayMember = "YazarAd";
            comboBox1.ValueMember = "YazarID";
        }
        private void cbTurDoldur() {
            comboBox2.DataSource = tRepo.SelectAll();
            comboBox2.DisplayMember = "TurAdi";
            comboBox2.ValueMember = "TurID";
        }
        private void Temizle() {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt=(TextBox)c;
                    txt.Clear();
                }
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.SelectedIndex = 0;
                }
                if (c is NumericUpDown)
                {
                    NumericUpDown num = (NumericUpDown)c;
                    num.Value = 0;
                }
            }
        }
        private void getir() {
            dataGridView1.DataSource = kRepo.SelectAll();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmYazar formYazar = new frmYazar();
            formYazar.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmTur formTur = new frmTur();
            formTur.ShowDialog();
        }

        private void frmKitap_Load(object sender, EventArgs e)
        {
            cbTurDoldur();
            cbYazarDoldur();
            getir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == " " || textBox2.Text == " ")
                {
                    MessageBox.Show("ISBN No ve Kitap Adi girmek zorunludur");
                }
                else
                {
                    kit.ISBN_No = Convert.ToInt32(textBox1.Text);
                    kit.KitabAdi = textBox2.Text;
                    kit.YazarID = (int)comboBox1.SelectedValue;
                    kit.TurID = (int)comboBox2.SelectedValue;
                    kit.SayfaSayisi = (int)numericUpDown1.Value;
                    kit.Puan = (int)numericUpDown2.Value;
                    kRepo.insert(kit);
                    Temizle();
                    MessageBox.Show("Kitap Başarıyla Eklendi");
                    getir();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt Ekleme Başarısız");      
            }
           
         
        }
        int seciliid;
        Kitaplar guncellenecek;
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                seciliid = (int)dataGridView1.CurrentRow.Cells[0].Value;
                kRepo.Delete(seciliid);
                MessageBox.Show("Kitap kaydı silindi");
                Temizle();
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
                guncellenecek = kRepo.SelectById(seciliid);
                textBox1.Text = Convert.ToString(guncellenecek.ISBN_No);
                textBox2.Text = guncellenecek.KitabAdi;
                comboBox1.SelectedValue = guncellenecek.YazarID;
                comboBox2.SelectedValue = guncellenecek.TurID;
                numericUpDown1.Value =(int) guncellenecek.SayfaSayisi;
                numericUpDown2.Value = (int)guncellenecek.Puan;
               
            }
            catch (Exception)
            {

                MessageBox.Show("Seçim Yapılamadı");
            }
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                guncellenecek.ISBN_No = Convert.ToInt32(textBox1.Text);
                guncellenecek.KitabAdi = textBox2.Text;
                guncellenecek.YazarID =(int) comboBox1.SelectedValue;
                guncellenecek.TurID = (int)comboBox2.SelectedValue;
                guncellenecek.SayfaSayisi = (int)numericUpDown1.Value;
                guncellenecek.Puan = (int)numericUpDown2.Value;
                kRepo.Update(guncellenecek);
                MessageBox.Show("Kayıt Başarıyla Güncellendi");
                Temizle();
                getir();
            }
            catch (Exception)
            {

                MessageBox.Show("Güncelleme İşlemi Başarısız");
            }
        }
    }
}
