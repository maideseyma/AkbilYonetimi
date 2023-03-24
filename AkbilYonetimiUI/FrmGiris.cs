namespace AkbilYonetimiUI
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {

        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            // Bu formu gizleyeceğiz.
            // Kayıt ol formunu açacağız.
            this.Hide();
            FrmKayitOl frm = new FrmKayitOl();
            frm.Show();
        }
    }
}