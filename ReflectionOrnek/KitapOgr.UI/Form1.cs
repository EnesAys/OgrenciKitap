using System;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using KitapOgr.UI.Forms;
using KitapOgr.BLL;
using KitapOgr.DAL;


namespace KitapOgr.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    
        RepositoryBase<Islemler> islemRepo = new RepositoryBase<Islemler>();
        private void getir()
        {
            dataGridView1.DataSource = islemRepo.SelectAll().Select(x=>new {
                Öğrenci=x.Ogrenciler.Ad,
                Kitap=x.Kitaplar.KitabAdi,
                AlınanTarih=x.AlinanTarih,
                VerilenTarih=x.VerilenTarih
            }).ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmİslem formİslem = new frmİslem();
            formİslem.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmOgrenci formOgr = new frmOgrenci();
            formOgr.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmKitap formKitap = new frmKitap();
            formKitap.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
        }

       
    }
}
