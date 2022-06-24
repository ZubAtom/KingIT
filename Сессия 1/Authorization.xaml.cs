using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Windows.Interop;

namespace KingIT.Сессия_1
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        int count = 0;
        string text_captcha;
        public Authorization()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text.ToLower();
            string password = Password.Password;
            using (var db =new Store_CentresEntities())
            {
                if (count < 3)
                {
                    var employer = db.Employers.Where(s => s.Login.ToLower() == login && s.Password == password).FirstOrDefault();
                    if (employer != null)
                    {
                        if (employer.Role == 3)
                            NavigationService.Navigate(new Manager_C(employer.ID_employer));
                        else
                            MessageBox.Show("Вы не Менеджер С");
                    }
                    else
                    {
                        count++;
                        MessageBox.Show("Введены некорректные данные");
                        if (count == 3)
                        {
                            CreateCaptcha();
                            Captcha.Visibility = Visibility.Visible;
                            Tb_captcha.Visibility = Visibility.Visible;
                        }
                    }
                }
                else
                {
                    var employer = db.Employers.Where(s => s.Login.ToLower() == login && s.Password == password).FirstOrDefault();
                    if (employer != null && text_captcha==Tb_captcha.Text)
                    {
                        NavigationService.Navigate(new Manager_C(employer.ID_employer));
                    }
                    else
                    {
                        MessageBox.Show("Введены некорректные данные");
                        CreateCaptcha();
                    }

                }
            }
        }
        private void CreateCaptcha()
        {
            Random random = new Random();
            Bitmap captcha = new Bitmap(200, 100);
            int Xpos = random.Next(0, 100);
            int Ypos = random.Next(0, 60);
            System.Drawing.Brush[] colors =
            {
                System.Drawing.Brushes.Black,
                System.Drawing.Brushes.Red,
                System.Drawing.Brushes.RoyalBlue,
                System.Drawing.Brushes.Green
            };
            Graphics graph = Graphics.FromImage((System.Drawing.Image)captcha);
            graph.Clear(System.Drawing.Color.LightGray);
            string text = String.Empty;
            string symbols = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            for (int i = 0; i<6; i++)
            {
                text += symbols[random.Next(symbols.Length)];
            }
            text_captcha = text;
            graph.DrawString(text, new Font("Arial", 20), colors[random.Next(colors.Length)], new PointF(Xpos, Ypos));
            graph.DrawLine(Pens.Black, new System.Drawing.Point(0, 0), new System.Drawing.Point(199, 99));
            graph.DrawLine(Pens.Black, new System.Drawing.Point(0, 99), new System.Drawing.Point(199, 0));
            for (int i = 0; i < 199; ++i)
                for (int j = 0; j < 99; ++j)
                    if (random.Next() % 20 == 0)
                        captcha.SetPixel(i, j, System.Drawing.Color.White);
            var handle = captcha.GetHbitmap();
            Captcha.Source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());


        }
    }

}
