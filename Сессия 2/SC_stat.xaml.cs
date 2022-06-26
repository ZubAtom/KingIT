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
    /// Логика взаимодействия для SC_stat.xaml
    /// </summary>
    public partial class SC_stat : Page
    {
        public SC_stat()
        {
            InitializeComponent();
            using (var db= new Store_CentresEntities())
            {
                Cb_Name_SC.ItemsSource = db.Store_Centers.Where(s => s.Status != 4).Select(s => s.Name).ToList();
            }
        }

        private void Cb_Name_SC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                Lv.ItemsSource = db.SC_stat(Cb_Name_SC.SelectedItem.ToString());
            }
        }
    }
}
