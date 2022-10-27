using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace FinalVersionOfCourceWork
{
    /// <summary>
    /// Interaction logic for Entering.xaml
    /// </summary>
    public partial class Entering : Window
    {
        SqlConnection _connAut = new SqlConnection(@"Data Source=OnlyMyself\YAYA;Database=StorageFinish;Integrated Security=True;");
        public Entering()
        {           
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Enter_button
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
         
            string queryString = $"Select ID, Login, Password, Permissons from [User] where Login = '{login_txt.Text}' and Password = '{password_txt.Text}' and Permissons = 'Buyer'";
            SqlCommand command = new SqlCommand(queryString, _connAut);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (login_txt.Text == "owner" && password_txt.Text == "owner123@")
            {
                TypeOfUser.LastPosition = "Entering";

                TypeOfUser.User = "Administrator";
                TypeOfUser.CurrentNameOfSupplier = login_txt.Text;
                Storage _st = new Storage();
                _st.Show();
                Close();
            }
            else if (table.Rows.Count == 1)
            {
                TypeOfUser.LastPosition = "Entering";

                TypeOfUser.User = "Buyer";
                TypeOfUser.CurrentNameOfBuyer = login_txt.Text;
                ProductsForSellingxaml _prdWindow = new ProductsForSellingxaml();
                _prdWindow.Show();
                Close();
                
            }


            else
            {
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                DataTable table1 = new DataTable();
                string queryString1 = $"Select ID, Login, Password, Permissons from [User] where Login = '{login_txt.Text}' and Password = '{password_txt.Text}' and Permissons = 'Supplier'";
                SqlCommand command1 = new SqlCommand(queryString1, _connAut);
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                if (table1.Rows.Count == 1)
                {
                    TypeOfUser.User = "Supplier";
                    TypeOfUser.CurrentNameOfSupplier = login_txt.Text;
                    TypeOfUser.LastPosition = "Entering";

                    Storage _st = new Storage();
                    _st.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Такого аккаунта не существует!", "Аккаунт не найден", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TypeOfUser.LastPosition = "Entering";
            TypeOfUser.User = "Standart";
            MainWindow Maino = new MainWindow();
            Maino.Show();
            Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            IAP _info = new IAP();
            _info.Show();
            Hide();
        }
    }
}
