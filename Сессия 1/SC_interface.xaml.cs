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
using Microsoft.Win32;


namespace KingIT.Сессия_1
{
    /// <summary>
    /// Логика взаимодействия для SC_interface.xaml
    /// </summary>
    public partial class SC_interface : Page
    {
        private int id_empl;
        public byte[] IMAGES { get; set; }
        private int id;

        public SC_interface(int id_empl,Store_Centers SC)
        {
            InitializeComponent();
            this.id_empl = id_empl;
            using (var db = new Store_CentresEntities())
            {
                Cb_Status.ItemsSource = db.Status_SC.Where(s=>s.Name_status != "Удаление").Select(s => s.Name_status).ToList();
                if (SC!=null)
                {
                    Lb.Content = "Интерфейс ТЦ " + SC.Name;
                    id = SC.ID_SC;
                    Upd.Visibility = Visibility.Visible;
                    List_pav.Visibility = Visibility.Visible;
                    Tb_Name.Text = SC.Name;
                    Tb_City.Text = SC.City;
                    Tb_Cost.Text = string.Format("{0:F2}",SC.Cost);
                    Tb_Floors.Text = SC.Number_of_floors.ToString();
                    Tb_Koef.Text = SC.Cofficient_of_added_value.ToString();
                    Tb_Quant_pav.Text = SC.Quantity_pavilions.ToString();
                    Cb_Status.SelectedIndex = SC.Status - 1;
                    IMAGES = SC.Image;
                    img.DataContext = this;
                }
                else
                {
                    Add.Visibility = Visibility.Visible;

                }
            }
        }
 

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                Store_Centers new_SC = new Store_Centers();
                new_SC.ID_SC = db.Store_Centers.Max(s => s.ID_SC)+1;
                new_SC.City = Tb_City.Text;
                new_SC.Cost = Convert.ToDecimal(Tb_Cost.Text);
                new_SC.Number_of_floors = Convert.ToInt32(Tb_Floors.Text);
                new_SC.Name = Tb_Name.Text;
                new_SC.Cofficient_of_added_value = Convert.ToDouble(Tb_Koef.Text);
                new_SC.Quantity_pavilions = Convert.ToInt32(Tb_Quant_pav.Text);
                new_SC.Status = Cb_Status.SelectedIndex + 1;
                new_SC.Image = IMAGES;
                db.Store_Centers.Add(new_SC);
                db.SaveChanges();
                NavigationService.Navigate(new SC_list(id_empl));
            }
        }

        private void Upd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                var SC = db.Store_Centers.Where(s => s.ID_SC == id).FirstOrDefault();
                SC.City = Tb_City.Text;
                SC.Cost = Convert.ToDecimal(Tb_Cost.Text);
                SC.Number_of_floors = Convert.ToInt32(Tb_Floors.Text);
                SC.Name = Tb_Name.Text;
                SC.Cofficient_of_added_value = Convert.ToDouble(Tb_Koef.Text);
                SC.Quantity_pavilions = Convert.ToInt32(Tb_Quant_pav.Text);
                SC.Status = Cb_Status.SelectedIndex + 1;
                SC.Image = IMAGES;
                db.SaveChanges();
                NavigationService.Navigate(new SC_list(id_empl));
            }
        }

        private void Bt_img_Click(object sender, RoutedEventArgs e)
        {
            var filedialog = new OpenFileDialog();
            filedialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            filedialog.InitialDirectory = @"C:\Users\АТОМ\Documents\Практика по БД №2\Image ТЦ";
            if (filedialog.ShowDialog() == true)
            {
                byte[] photo = File.ReadAllBytes(filedialog.FileName);
                IMAGES = photo;
                img.DataContext = null;
                img.DataContext = this;
            }

        }

        private void List_pav_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pavilions_list(id_empl,id));
        }
    }
}
