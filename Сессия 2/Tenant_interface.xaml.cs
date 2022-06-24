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
        public Tenant_interface(Tenants tenant)
        {
            InitializeComponent();
        }
    }
}
