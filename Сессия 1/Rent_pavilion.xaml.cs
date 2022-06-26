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
    /// Логика взаимодействия для Rent_pavilion.xaml
    /// </summary>
    public partial class Rent_pavilion : Page
    {
        private string id_Pav;
        private int id_SC;
        private int id_empl;
        public Rent_pavilion(string id_Pav, int id_SC, int id_empl)
        {
            InitializeComponent();
            this.id_Pav = id_Pav;
            this.id_SC = id_SC;
            this.id_empl = id_empl;
            Lb.Content = "Аренда павильона номер " + id_Pav;
            using (var db = new Store_CentresEntities())
            {
                Cb_Tenant.ItemsSource = db.Tenants.Select(s => s.Name_tеnant).ToList();
                Tb_Employer.Text = db.Employers.Where(s => s.ID_employer == this.id_empl).Select(s => s.Secondname).FirstOrDefault();
            }
        }

        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                Pavilions pavilion = db.Pavilions.Where(s => s.ID_SC == id_SC && s.ID_Pavilion == id_Pav).FirstOrDefault();
                try
                {
                    db.new_Rent(id_SC, id_Pav, Cb_Tenant.SelectedIndex + 1, id_empl, Dp_Start_rent.SelectedDate, Dp_Finish_rent.SelectedDate);
                    MessageBox.Show("Павильон успешно арендован");
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message);
                }
                NavigationService.Navigate(new Pavilion_interface(id_empl, pavilion, id_SC));
            }
            
        }
    }
}
