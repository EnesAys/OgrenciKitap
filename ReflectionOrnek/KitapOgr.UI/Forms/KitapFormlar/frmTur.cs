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
    public partial class frmTur : Form
    {
        public frmTur()
        {
            InitializeComponent();
        }
        RepositoryBase<Turler> tRepo = new RepositoryBase<Turler>();
        Turler tur = new Turler();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != " ")
                {
                    tur.TurAdi = textBox1.Text;
                    tRepo.insert(tur);
                    MessageBox.Show("Kayıt Başarı ile Eklendi.Değişikliklerin aktif olması için kitap formu yeniden başlatılmalıdır");
                }
                else
                {
                    MessageBox.Show("Lütfen Alanı Doldurunuz");
                }
               
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem Başarısz");
            }
            
        }
    }
}
