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

namespace KingIT.Сессия_1
{
    /// <summary>
    /// Логика взаимодействия для Pavilion_interface.xaml
    /// </summary>
    public partial class Pavilion_interface : Page
    {
        private int id_empl;
        private int id_SC;
        private string id_Pav;
        public Pavilion_interface(int id_empl,Pavilions Pav, int id)
        {
            InitializeComponent();
            this.id_empl = id_empl;
            using (var db = new Store_CentresEntities())
            {
                Cb_Status.ItemsSource = db.Status_Pavilion.Where(s => s.Name_status != "Удален").Select(s => s.Name_status).ToList();
                int floors = db.Store_Centers.Where(s => s.ID_SC == id).Select(s => s.Number_of_floors).ToList()[0];
                for (int i = 1; i <= floors; i++)
                    Cb_Floor.Items.Add(i);
                id_SC = id;
                if (Pav != null)
                {
                    Tb_Number.IsEnabled = false;
                    Lb.Content = "Интерфейс павильона номер " + Pav.ID_Pavilion;
 
                    id_Pav = Pav.ID_Pavilion;
                    Rent.Visibility = Visibility.Visible;
                    Upd.Visibility = Visibility.Visible;
                    Tb_Number.Text = Pav.ID_Pavilion;
                    Tb_Square.Text = Pav.Square.ToString();
                    Tb_Cost_per_meter.Text = string.Format("{0:F2}", Pav.Cost_per_square_meter);
                    Tb_Coef.Text = Pav.Cofficient_of_added_value.ToString();
                    Cb_Status.SelectedIndex = Pav.Status - 1;
                    Cb_Floor.SelectedIndex = Pav.Floor - 1;
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
                Pavilions new_Pav = new Pavilions();
                new_Pav.ID_SC = id_SC;
                new_Pav.ID_Pavilion = Tb_Number.Text;
                new_Pav.Square = Convert.ToDouble(Tb_Square.Text);
                new_Pav.Cost_per_square_meter = Convert.ToDecimal(Tb_Cost_per_meter.Text);
                new_Pav.Floor = Convert.ToInt32(Cb_Floor.SelectedIndex + 1);
                new_Pav.Cofficient_of_added_value = Convert.ToDouble(Tb_Cost_per_meter.Text);
                new_Pav.Status = Cb_Status.SelectedIndex + 1;
                db.Pavilions.Add(new_Pav);
                db.SaveChanges();
                NavigationService.Navigate(new Pavilions_list(id_empl,id_SC));
            }
        }

        private void Upd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                var Pav = db.Pavilions.Where(s => s.ID_SC == id_SC && s.ID_Pavilion==id_Pav).FirstOrDefault();
                Pav.ID_Pavilion = Tb_Number.Text;
                Pav.Square = Convert.ToDouble(Tb_Square.Text);
                Pav.Cost_per_square_meter = Convert.ToDecimal(Tb_Cost_per_meter.Text);
                Pav.Floor = Cb_Floor.SelectedIndex+1;
                Pav.Cofficient_of_added_value = Convert.ToDouble(Tb_Coef.Text);
                Pav.Status = Cb_Status.SelectedIndex + 1;
                db.SaveChanges();
                NavigationService.GoBack();
            }
        }

        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Rent_pavilion(id_Pav, id_SC, id_empl));
        }
    }
}
