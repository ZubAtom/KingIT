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
    /// Логика взаимодействия для Manager_C.xaml
    /// </summary>
    public partial class SC_list : Page
    {
        private int id_empl;
        public SC_list(int id_empl)
        {
            InitializeComponent();
            this.id_empl = id_empl;
            Lv.Items.Clear();
            City.Items.Clear();
            Status.Items.Clear();
            GetList();
            GetCbs();
            City.SelectedIndex = 0;
            Status.SelectedIndex = 0;
        }

        private void GetList()
        {
            using (var db = new Store_CentresEntities())
            {
                
                var sc = from SC in db.Store_Centers
                         join st in db.Status_SC on SC.Status equals st.ID_status
                         select new
                         {
                             id = SC.ID_SC,
                             Name = SC.Name,
                             Status = st.Name_status,
                             City = SC.City,
                             Cost = SC.Cost,
                             Quantity_pavilions = SC.Quantity_pavilions,
                             Number_of_floors = SC.Number_of_floors,
                             Cofficient_of_added_value = SC.Cofficient_of_added_value,
                             id_status = st.ID_status
                         };
                if (City.SelectedItem != null && City.SelectedItem.ToString() != "*")
                    sc = sc.Where(s => s.City == City.SelectedItem.ToString());
                if (Status.SelectedItem != null && Status.SelectedItem.ToString() != "*")
                    sc = sc.Where(s => s.Status == Status.SelectedItem.ToString());
                var table= sc.Where(s => s.Status != "Удаление").OrderBy(s => s.City).ThenBy(s => s.id_status);
                Lv.ItemsSource = table.ToList();
            }
        }
        private void GetCbs()
        {
            using (var db = new Store_CentresEntities())
            {
                City.Items.Clear();
                City.Items.Add("*");
                var city = db.Store_Centers.Where(s => s.Status != 4).Select(s => s.City).Distinct().ToList();
                foreach (var c in city)
                {
                    City.Items.Add(c);
                }
                Status.Items.Clear();
                Status.Items.Add("*");
                var status = db.Status_SC.Where(s => s.ID_status != 4).Select(s => s.Name_status).Distinct().ToList();
                foreach (var st in status)
                {
                    Status.Items.Add(st);
                }
                City.SelectedIndex = 0;
                Status.SelectedIndex = 0;
            }
        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetList();
            Del.Visibility = Visibility.Hidden;
            Upd.Visibility = Visibility.Hidden;
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetList();
            Del.Visibility = Visibility.Hidden;
            Upd.Visibility = Visibility.Hidden;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить ТЦ?", "Подтверждение удаления ТЦ", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                using (var db = new Store_CentresEntities())
                {
                    try
                    {
                        int id = ((dynamic)Lv.SelectedItem).id;
                        var SC = db.Store_Centers.Where(s => s.ID_SC == id).FirstOrDefault();
                        SC.Status = 4;
                        db.SaveChanges();
                        GetList();
                        GetCbs();
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show("Ошибка удвления павильона. Нельзя удалить арендованный или забронированный павильон");
                    }
                }
            }
        }

        private void Lv_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Del.Visibility = Visibility.Visible;
            Upd.Visibility = Visibility.Visible;
        }

        private void Upd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                int id = ((dynamic)Lv.SelectedItem).id;
                var SC = db.Store_Centers.Where(s => s.ID_SC == id).FirstOrDefault();
                NavigationService.Navigate(new SC_interface(id_empl,SC));

            }
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SC_interface(id_empl,null));
        }
    }
    
}
