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
    public partial class Manager_C : Page
    {
        private int id_empl;
        public Manager_C(int id_empl)
        {
            InitializeComponent();
            this.id_empl = id_empl;
        }

        private void SCs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SC_list(id_empl));
        }

        private void Tenants_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Сессия_2.Tenants_list());
        }
    }
}
