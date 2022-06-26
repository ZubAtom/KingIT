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
    /// Логика взаимодействия для Tenants_list.xaml
    /// </summary>
    public partial class Tenants_list : Page
    {
        public Tenants_list()
        {
            InitializeComponent();
            Lv.Items.Clear();
            GetList();
        }

        private void GetList()
        {
            using (var db = new Store_CentresEntities())
            {
                var tenants = from ten in db.Tenants
                              where ten.Deleted == false
                              select new
                              {
                                  ID_Tenant = ten.ID_Tenant,
                                  Name_tenant = ten.Name_tеnant,
                                  Phone_number = ten.Phone_number,
                                  Adress = ten.City + "," + ten.Street + "," + ten.House
                              };
                if (Tb_Name.Text.Trim(' ') != "")
                {
                    string name = Tb_Name.Text.Trim(' ');
                    tenants = tenants.Where(s => s.Name_tenant.Contains(name) == true);
                }
                Lv.ItemsSource = tenants.ToList();
            }
        }

        private void Lv_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Del.Visibility = Visibility.Visible;
            Upd.Visibility = Visibility.Visible;
        }





        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить арендатора?", "Подтверждение удаления арендатора", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                using (var db = new Store_CentresEntities())
                {
                    int id_tenant = ((dynamic)Lv.SelectedItem).ID_Tenant;
                    var tenant = db.Tenants.Where(s => s.ID_Tenant == id_tenant).FirstOrDefault();
                    tenant.Deleted = true;
                    db.SaveChanges();
                    GetList();
                }
            }
        }
        private void Upd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                int id_tenant = ((dynamic)Lv.SelectedItem).ID_Tenant;
                var tenant = db.Tenants.Where(s => s.ID_Tenant== id_tenant).FirstOrDefault();
                NavigationService.Navigate(new Tenant_interface(tenant));

            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Tenant_interface( null));
        }

        private void Tb_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Del.Visibility = Visibility.Hidden;
            Upd.Visibility = Visibility.Hidden;
            GetList();
        }
    }
}
