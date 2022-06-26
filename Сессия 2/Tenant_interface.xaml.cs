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

namespace KingIT.Сессия_2
{
    /// <summary>
    /// Логика взаимодействия для Tenant_interface.xaml
    /// </summary>
    public partial class Tenant_interface : Page
    {
        private int id;
        public Tenant_interface(Tenants tenant)
        {
            InitializeComponent();
            using (var db = new Store_CentresEntities())
            {
                if (tenant != null)
                {
                    Lb.Content = "Интерфейс арендатора " + tenant.Name_tеnant;
                    id = tenant.ID_Tenant;
                    Upd.Visibility = Visibility.Visible;
                    Tb_Name.Text = tenant.Name_tеnant;
                    Tb_Phone_number.Text = tenant.Phone_number;
                    Tb_City.Text = tenant.City;
                    Tb_Street.Text = tenant.Street;
                    Tb_House.Text = tenant.House;
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
                Tenants new_tenant = new Tenants();
                new_tenant.ID_Tenant = db.Tenants.Max(s => s.ID_Tenant) + 1;
                new_tenant.Name_tеnant = Tb_Name.Text;
                new_tenant.Phone_number = Tb_Phone_number.Text;
                new_tenant.City = Tb_City.Text;
                new_tenant.Street =Tb_Street.Text;
                new_tenant.House = Tb_House.Text;
                new_tenant.Deleted = false;
                db.Tenants.Add(new_tenant);
                db.SaveChanges();
                NavigationService.Navigate(new Tenants_list());
            }
        }

        private void Upd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                var tenant = db.Tenants.Where(s => s.ID_Tenant == id).FirstOrDefault();
                tenant.Name_tеnant = Tb_Name.Text;
                tenant.Phone_number = Tb_Phone_number.Text;
                tenant.City = Tb_City.Text;
                tenant.Street = Tb_Street.Text;
                tenant.House = Tb_House.Text;
                db.SaveChanges();
                NavigationService.Navigate(new Tenants_list());
            }
        }
    }
}
