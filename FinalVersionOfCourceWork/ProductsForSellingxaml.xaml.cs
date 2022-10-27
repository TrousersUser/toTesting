using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Interaction logic for ProductsForSellingxaml.xaml
    /// </summary>
    public partial class ProductsForSellingxaml : Window
    {
        
        SqlConnection _conn = new SqlConnection(@"Data Source=OnlyMyself\YAYA;Database=StorageFinish;Integrated Security=True;");

        public void GridLoadMain()
        {
            try
            {
                _conn.Open();
                DataTable _dt = new DataTable();
                if (TypeOfUser.User == "Administrator")
                {
                    SqlCommand _cmd = new SqlCommand("SELECT * FROM [Orders]", _conn);

                    SqlDataReader _sqlR = _cmd.ExecuteReader();
                    _dt.Load(_sqlR);
                    _conn.Close();
                    dataGrid.ItemsSource = _dt.DefaultView;

                }
                else
                {
                    SqlCommand _cmd = new SqlCommand("SELECT * FROM [Orders] WHERE OrderedByWho = '" + TypeOfUser.CurrentNameOfBuyer + "' ", _conn);
                    SqlDataReader _sqlR = _cmd.ExecuteReader();
                    _dt.Load(_sqlR);
                    _conn.Close();
                    dataGrid.ItemsSource = _dt.DefaultView;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void ChangingInfoStorage()
        {
            _conn.Open();
            SqlCommand _cmdChange = new SqlCommand("UPDATE [MainStorage] SET Sold = 'Ordered-" + Count_txt.Text + "' WHERE ProductType = '" + Product_txt.Text + "' ", _conn);
            _cmdChange.ExecuteNonQuery();
            _conn.Close();
            GridLoadSecondary();
            
            _conn.Close();
            Clearing();
        }
        public void GridLoadSecondary()
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("SELECT * FROM [MainStorage] ", _conn);
                DataTable _dt = new DataTable();
                _conn.Open();
                SqlDataReader _sqlR = _cmd.ExecuteReader() ;
                _dt.Load(_sqlR);
                _conn.Close();
                dataGrisd.ItemsSource = _dt.DefaultView;


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void Clearing()
        {
            Count_txt.Clear();
            Product_txt.Clear();
            delievierTo_txt.Clear();
            ID_txt.Clear();
        }

        public bool IsInfoCheck()
        {
            if (Count_txt.Text != "" && Product_txt.Text != "" && delievierTo_txt.Text != "")
            {
                return true;
            }
            else 
            { 
                MessageBox.Show("Информацию, что желаете добавить, для начала - создайте!", "Anouncement", MessageBoxButton.OK, MessageBoxImage.Hand);
                return false;
            }
        }


        public bool IsIdCheck()
        {
            if (ID_txt.Text != "")
            {
                return true;

            }
            else { MessageBox.Show("Введите номер той записи, что желаете изменить!", "Warning", MessageBoxButton.OK, MessageBoxImage.Hand); return false; }
        }

        public void DeleteRecord()
        {
            if (IsIdCheck())
            {
                _conn.Open();
                SqlCommand _cmd = new SqlCommand($"DELETE FROM [Orders] WHERE ID = {ID_txt.Text}", _conn);
                _cmd.ExecuteNonQuery();
                _conn.Close();
                GridLoadMain();
            }
            

        }

        public void UpdateInfo()
        {
            if (IsIdCheck())
            {


                try
                {
                    SqlCommand _cmd = new SqlCommand("UPDATE [MainStorage] SET Sold = 'Sold: " + Count_txt.Text + "' WHERE ID = '" + ID_txt.Text + "'", _conn);
                    _conn.Open();
                    _cmd.ExecuteNonQuery();
                    _conn.Close();

                    Clearing();
                    GridLoadSecondary();
                    MessageBox.Show($"Заказ был подтверждён!", "Annoncement", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
        }
            public void InsertingInform()
        {

            

            if (IsInfoCheck())
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable table = new DataTable();
                    string queryString = $"Select ID, ProductType from [MainStorage] WHERE ProductType = '{Product_txt.Text}'"; //check on, working or not

                    SqlCommand _command = new SqlCommand(queryString, _conn);
                    adapter.SelectCommand = _command;
                    adapter.Fill(table);


                    if (table.Rows.Count == 1)
                    {

                        SqlCommand _cmd = new SqlCommand("INSERT INTO [Orders] VALUES (@ProductName, @ProductCount, @OrderedByWho, @DelieverTo)", _conn);
                        _cmd.CommandType = CommandType.Text;


                        _cmd.Parameters.AddWithValue(@"ProductName", Product_txt.Text);
                        _cmd.Parameters.AddWithValue(@"ProductCount", Count_txt.Text);
                        _cmd.Parameters.AddWithValue(@"OrderedByWho", TypeOfUser.CurrentNameOfBuyer);
                        _cmd.Parameters.AddWithValue(@"DelieverTo", delievierTo_txt.Text);

               


                        _conn.Open();
                        _cmd.ExecuteNonQuery();
                        _conn.Close();

                        GridLoadMain();

                        ChangingInfoStorage();
                        

                        MessageBox.Show("Новый заказ был добавлен, благодарим за сотрудничество!", "Thanks", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else { MessageBox.Show("Товар, что вы пытаетесь заказать, отсутствует на складе", "Don't Be Upset", MessageBoxButton.OK, MessageBoxImage.Warning); }


                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                 
                


            }
        }
        public ProductsForSellingxaml()
        {
            
            InitializeComponent();
            if (TypeOfUser.User != "Administrator")
            {
                adm_storage_button.Visibility = Visibility.Hidden;
                adm_users_button.Visibility = Visibility.Hidden;

            }
            
            GridLoadMain();
            GridLoadSecondary();
        }
        

        private void dataGrisd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e) //Adding Button
        {
            InsertingInform();
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e) // ClearTXT's button
        {
            Clearing();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Delete Button
        {
            DeleteRecord();
        }
        
        private void Button_Click_4(object sender, RoutedEventArgs e) // Confirm Button
        {
            UpdateInfo();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // Return Btn
        {
            TypeOfUser.RoadToTheHome();
            Close();
        }

        private void adm_storage_button_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser.LastPosition = "ProductsWindow";

            Storage _storage = new Storage();
            _storage.Show();
            Close();
        }

        private void adm_users_button_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser.LastPosition = "ProductsWindow";

            MainWindow _main = new MainWindow();
            _main.Show();
            Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // UnLogin_Button
        {
            Entering _enter = new Entering();
            _enter.Show();
            Close();
        }
    }
}
