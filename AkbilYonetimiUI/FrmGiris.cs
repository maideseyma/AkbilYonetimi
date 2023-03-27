using System.Data.SqlClient;

namespace AkbilYonetimiUI
{
    public partial class FrmGiris : Form
    {
        public string Email { get; set; } // Kayıt ol formunda kayır olan kullanıcının emaili buraya gelsin
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            if (Email != null)
            {
                txtEmail.Text = Email;

            }
            txtEmail.TabIndex = 1;
            txtSifre.TabIndex = 2;
            checkBoxHatirla.TabIndex = 3;
            btnGirisYap.TabIndex = 4;
            btnKayitOl.TabIndex = 5;

            if (Properties.Settings1.Default.BeniHatirla)
            {
                txtEmail.Text = Properties.Settings1.Default.KullaniciEmail;
                txtSifre.Text = Properties.Settings1.Default.KullaniciSifre;
                BeniHatirla();
            }

        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            // Bu formu gizleyeceğiz.
            // Kayıt ol formunu açacağız.
            this.Hide();
            FrmKayitOl frm = new FrmKayitOl();
            frm.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            GirisYap();
        }

        private void GirisYap()
        {
            try

            {
                // 1) Email ve şifre textboxları dolu mu?
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSifre.Text))
                {
                    MessageBox.Show("Bilgileri eksiksiz giriniz!",
                        "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                // 2) Girdiği email ve şifre veritabanında mevcur mu?
                // select * from Kullanicilar where Email= '' and Sifre=''
                string baglantiCumlesi = @"Server=DESKTOP-E30TBPJ\MSSQLSERVER01;Database=AKBILDB;Trusted_Connection=True;";
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                string sorgu = $"select * from Kullanicilar where Email='{txtEmail.Text.Trim()}' and Parola='{txtSifre.Text.Trim()}'";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                if (!okuyucu.HasRows)
                {
                    MessageBox.Show("Email ya da şifrenizi doğru girdiğinize emin olunuz!",
                        "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    while (okuyucu.Read())
                    {
                        MessageBox.Show($"HOŞGELDİNİZ {okuyucu["Ad"]} {okuyucu["Soyad"]}");
                        Properties.Settings1.Default.KullaniciId = (int)okuyucu["Id"];
                    }
                    baglanti.Close();
                }
                if (checkBoxHatirla.Checked)
                {
                    Properties.Settings1.Default.BeniHatirla = true;
                    Properties.Settings1.Default.KullaniciEmail = txtEmail.Text.Trim();
                    Properties.Settings1.Default.KullaniciSifre = txtSifre.Text.Trim();
                    Properties.Settings1.Default.Save();
                }
                this.Hide();
                FrmAnasayfa frmA = new FrmAnasayfa();
                frmA.Show();




                // Eğer email ve şifre doğruysa
                // Eğer beni hatırlayı tıkladıysa?? bilgileri hatırlanacak
                // hoşgeldiniz yazacak ve anasayfa formuna yönlendirecek değilse yanlış mesajı verecek.
            }
            catch (Exception hata)
            {
                // DipNot exceptionlar asla kullanıcıya gösterilmez
                // Exceptionlar loglanır. Biz şu an öğrenme / geliştirme aşamasında olduğumuz için yazdık.
                MessageBox.Show("Beklenmedik bir sorun oluştu!" + hata.Message);
            }
        }

        private void checkBoxHatirla_CheckedChanged(object sender, EventArgs e)
        {
            BeniHatirla();
        }

        private void BeniHatirla()
        {
            if (checkBoxHatirla.Checked)
            {
                Properties.Settings1.Default.BeniHatirla = true;
            }
            else
            {
                Properties.Settings1.Default.BeniHatirla = false;
            }
        }

        private void txtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) // basılan tuş enter ise griş yapacak


            {
                GirisYap();
            }
        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}