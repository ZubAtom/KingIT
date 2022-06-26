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
    /// Логика взаимодействия для Pavilions_list.xaml
    /// </summary>
    public partial class Pavilions_list : Page
    {
        private int id_empl;
        private int id;
        public Pavilions_list(int id_empl,int id)
        {
            InitializeComponent();
            this.id_empl = id_empl;
            this.id = id;
            Lv.Items.Clear();
            List<string> squares = new List<string>() { "*", "<50", "50-100", "100-150", "150-200", ">200" };
            Square.ItemsSource = squares;
            GetList();
            GetCbs();
        }

        private void GetList()
        {
            using (var db = new Store_CentresEntities())
            {
                
                
                var pavs = db.Pavilions.Where(s => s.ID_SC == id && s.Status!=4);
                var scs = db.Store_Centers.Where(s => s.ID_SC == id); 
                var pavilions = from pav in pavs
                           join status_pav in db.Status_Pavilion on pav.Status equals status_pav.ID_status
                           select new
                           {
                               Number_pavilion = pav.ID_Pavilion,
                               ID_SC = pav.ID_SC,
                               Status_pav = status_pav.Name_status,
                               Floor = pav.Floor,
                               Square = pav.Square,
                               Coef = pav.Cofficient_of_added_value,
                               Cost_per_square_meter = pav.Cost_per_square_meter
                           };
                var SCs = from sc in db.Store_Centers
                          join status_sc in db.Status_SC on sc.Status equals status_sc.ID_status
                          select new
                          {
                              ID_SC = sc.ID_SC,
                              Name_SC = sc.Name,
                              Status_SC = status_sc.Name_status
                          };
                var list_pavilions = from pav in pavilions
                                     join sc in SCs on pav.ID_SC equals sc.ID_SC
                                     select new
                                     {
                                         Number_pavilion = pav.Number_pavilion,
                                         ID_SC = pav.ID_SC,
                                         Name_SC= sc.Name_SC,
                                         Status_pav = pav.Status_pav,
                                         Floor = pav.Floor,
                                         Square = pav.Square,
                                         Coef = pav.Coef,
                                         Cost_per_square_meter = pav.Cost_per_square_meter,
                                         Status_SC = sc.Status_SC
                                     };
                if (Floor.SelectedItem != null && Floor.SelectedItem.ToString() != "*")
                {
                    int floor = Convert.ToInt32(Floor.SelectedItem);
                    list_pavilions = list_pavilions.Where(s => s.Floor == floor);
                }
                if (Status.SelectedItem != null && Status.SelectedItem.ToString() != "*")
                    list_pavilions = list_pavilions.Where(s => s.Status_pav == Status.SelectedItem.ToString());
                if (Square.SelectedItem != null && Square.SelectedItem.ToString() != "*")
                {
                    char c = Square.SelectedItem.ToString()[0];
                    switch (c)
                    {
                        case '<' :
                            double square_min = Convert.ToDouble(Square.SelectedItem.ToString().Substring(1));
                            list_pavilions = list_pavilions.Where(s => s.Square <= square_min); ;
                            break;
                        case '>':
                            double square_max = Convert.ToDouble(Square.SelectedItem.ToString().Substring(1));
                            list_pavilions = list_pavilions.Where(s => s.Square >= square_max);
                            break;
                        default:
                            string[] str = new string[2];
                            str =Square.SelectedItem.ToString().Split('-');
                            double diapason_min= Convert.ToDouble(str[0]);
                            double diapason_max = Convert.ToDouble(str[1]);
                            list_pavilions = list_pavilions.Where(s => s.Square >= diapason_min && s.Square <= diapason_max);
                            break;
                    }
                }
                Lv.ItemsSource = list_pavilions.ToList();
            }
        }
        private void GetCbs()
        {
            using (var db = new Store_CentresEntities())
            {
                Floor.Items.Clear();
                Floor.Items.Add("*");
                int floors = db.Store_Centers.Where(s => s.ID_SC == id).Select(s => s.Number_of_floors).ToList()[0];
                for (int i = 1; i <= floors; i++)
                    Floor.Items.Add(i);
                Status.Items.Clear();
                Status.Items.Add("*");
                var status = db.Status_Pavilion.Where(s => s.ID_status != 4).Select(s => s.Name_status).Distinct().ToList();
                foreach (var st in status)
                {
                    Status.Items.Add(st);
                }
                Floor.SelectedIndex = 0;
                Status.SelectedIndex = 0;
            }
        }


        

        private void Lv_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Del.Visibility = Visibility.Visible;
            Upd.Visibility = Visibility.Visible;
        }

        


        private void Floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetList();
            Del.Visibility = Visibility.Hidden;
            Upd.Visibility = Visibility.Hidden;
        }


        private void Square_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            if (MessageBox.Show("Вы действительно хотите удалить павильон?", "Подтверждение удаления павильона", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                using (var db = new Store_CentresEntities())
                {
                    try
                    {
                        int id_sc = ((dynamic)Lv.SelectedItem).ID_SC;
                        string id_pav = ((dynamic)Lv.SelectedItem).Number_pavilion;
                        var pav = db.Pavilions.Where(s => s.ID_SC == id_sc && s.ID_Pavilion == id_pav).FirstOrDefault();
                        pav.Status = 4;
                        db.SaveChanges();
                        GetList();
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show("Ошибка удаления ТЦ. Нельзя удалить ТЦ с арендованными или забронированными павильонами");
                    }
                }
            }
        }
        private void Upd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Store_CentresEntities())
            {
                int id_sc = ((dynamic)Lv.SelectedItem).ID_SC;
                string id_pav = ((dynamic)Lv.SelectedItem).Number_pavilion;
                var Pav = db.Pavilions.Where(s => s.ID_SC == id_sc && s.ID_Pavilion == id_pav).FirstOrDefault();
                NavigationService.Navigate(new Pavilion_interface(id_empl,Pav,id));

            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Pavilion_interface(id_empl,null,id));
        }
    }
}
