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

namespace KitapOgr.UI.Forms.KitapFormlar
{
    public partial class frmYazar : Form
    {
        public frmYazar()
        {
            InitializeComponent();
        }
        Yazarlar yazar = new Yazarlar();
        RepositoryBase<Yazarlar> yRepo = new RepositoryBase<Yazarlar>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != " " || textBox2.Text != " ")
                {
                    yazar.YazarAd = textBox1.Text;
                    yazar.YazarSoyad = textBox2.Text;
                    yRepo.insert(yazar);
                    MessageBox.Show("Kayıt Başarı ile Eklendi.Değişikliklerin aktif olması için kitap formu yeniden başlatılmalıdır");
                }
                else
                {
                    MessageBox.Show("Lütfen alanları eksiksiz doldurunuz");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("İşlem Başarısız");
            }
           
            
        }
    }
}
