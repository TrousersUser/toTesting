using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace FinalVersionOfCourceWork
{
    /// <summary>
    /// Interaction logic for Storage.xaml
    /// </summary>
    public partial class Storage : Window
    {
        SqlConnection _conn = new SqlConnection(@"Data Source=OnlyMyself\YAYA;Database=StorageFinish;Integrated Security=True;");

        public Storage()
        {
            InitializeComponent();
            if (TypeOfUser.User != "Administrator")
            {
                TrasportingBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                productsAdm_Btn.Visibility = Visibility.Hidden;
                recovery_btn.Visibility = Visibility.Hidden;
            }
            GridLoad();
            
        }

        public bool HaveInfoCheck()
        {
            if (productCount_txt.Text == "" && productType_txt.Text == "")
            {
                MessageBox.Show("Так, для начала, информацию введите в пространство соответствующее.", "Anonncement", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
            else
            {
                return true;
            }
        }
        public void ClearTxt()
        {
            productCount_txt.Clear();
            productType_txt.Clear();
            id_txt.Clear();


        }
        public bool HaveIdCheck()
        {
            if (id_txt.Text == "")
            {
                MessageBox.Show("Введите число-индетификатор для записи, что желаете удалить.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return false;
            }
            else
            {
                return true;
            }
        }
        public void GridLoad()
        {
            try
            {
                DataTable _dt = new DataTable();
                _conn.Open();
                if (TypeOfUser.User != "Administrator")
                {
                    SqlCommand _cmd = new SqlCommand("SELECT * FROM MainStorage WHERE DeliveriedByWho = '" + TypeOfUser.CurrentNameOfSupplier + "' ", _conn);
                    SqlDataReader _dataR = _cmd.ExecuteReader();
                    _dt.Load(_dataR);
                    _conn.Close();
                    dataGrid.ItemsSource = _dt.DefaultView;

                }
                else
                {
                    SqlCommand _cmd = new SqlCommand("SELECT * FROM MainStorage",_conn);
                    SqlDataReader _dataR = _cmd.ExecuteReader();
                    _dt.Load(_dataR);
                    _conn.Close();
                    dataGrid.ItemsSource = _dt.DefaultView;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.Source);
            }
        }
        public void DeleteRecord()
        {
            if (HaveIdCheck())
            {
                try
                {
                    _conn.Open();
                    SqlCommand _cmd = new SqlCommand($"DELETE FROM [MainStorage] WHERE ID = {id_txt.Text}", _conn);
                    _cmd.ExecuteNonQuery();
                    _conn.Close();
                    GridLoad();
                    ClearTxt();
                    _conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.Source);
                }
            }
            

        }

        private void TrasportingBtn_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser.LastPosition = "StorageWindow";
            MainWindow _main = new MainWindow();
            _main.Show();
            Close();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTxt();
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HaveInfoCheck())
            {
                try
                {
                    
                    SqlCommand _cmd = new SqlCommand(@"INSERT INTO [MainStorage] VALUES (@ProductType, @ProductCount, @DeliveriedByWho, @Sold)", _conn);
                    _cmd.CommandType = CommandType.Text;

                    _cmd.Parameters.AddWithValue("@ProductType", productType_txt.Text);
                    _cmd.Parameters.AddWithValue("@ProductCount", productCount_txt.Text);
                    _cmd.Parameters.AddWithValue("@DeliveriedByWho", TypeOfUser.CurrentNameOfSupplier);
                    _cmd.Parameters.AddWithValue("@Sold", "Nope yet");
                    _conn.Open();
                    _cmd.ExecuteNonQuery();
                    _conn.Close();
                    GridLoad();
                    ClearTxt();
                    MessageBox.Show("Вы смогли добавить новую запись! \nПоздравление от Вашей компании", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HaveIdCheck())
            {
                if (HaveInfoCheck())
                {
                    try
                    {
                        _conn.Open();
                        SqlCommand _cmd = new SqlCommand("UPDATE [MainStorage] SET ProductType = '" + productType_txt.Text + "', ProductCount = '" + productCount_txt.Text + "' WHERE ID = '"+id_txt.Text+"' ", _conn);
                        _cmd.ExecuteNonQuery();
                        MessageBox.Show($"Информация на позиции {id_txt.Text} была изменена!", "Update-Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    finally
                    {
                        _conn.Close();
                        ClearTxt();
                        GridLoad();

                    }
                }
               

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Return_button
        {
            TypeOfUser.RoadToTheHome();
            Close();
        }

        private void productsAdm_Btn_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser.LastPosition = "StorageWindow";
            ProductsForSellingxaml _products = new ProductsForSellingxaml();
            _products.Show();
            Close();
        }

        private void UnLogin_btn_Click(object sender, RoutedEventArgs e)
        {
            Entering _enter = new Entering();
            _enter.Show();
            Close();
        }


        public void Sorted()
        {
           
            try
            {
                switch (SortComboBox.SelectedIndex)
                {
                    case 0:
                        SqlCommand _cmd3 = new SqlCommand("SELECT * FROM [MainStorage] WHERE Sold LIKE 'S%'", _conn);
                        _conn.Open();
                        SqlDataReader reader3 = _cmd3.ExecuteReader();
                        DataTable _dt3 = new DataTable();
                        _dt3.Load(reader3);
                        _conn.Close();
                        dataGrid.ItemsSource = _dt3.DefaultView;
                        break;

                    case 1:
                        SqlCommand _cmd = new SqlCommand("SELECT * FROM [MainStorage] WHERE Sold LIKE 'O%'", _conn);
                        _conn.Open();
                        SqlDataReader reader = _cmd.ExecuteReader();
                        DataTable _dt = new DataTable();
                        _dt.Load(reader);
                        _conn.Close();
                        dataGrid.ItemsSource = _dt.DefaultView;
                        break;

                    case 2:
                        SqlCommand _cmd2 = new SqlCommand("SELECT * FROM [MainStorage] WHERE Sold LIKE 'N%'", _conn);
                        _conn.Open();
                        SqlDataReader reader2 = _cmd2.ExecuteReader();
                        DataTable _dt2 = new DataTable();
                        _dt2.Load(reader2);
                        _conn.Close();
                        dataGrid.ItemsSource = _dt2.DefaultView;
                        break;

                    default:
                        throw new Exception("Элемент, по которому необходимо проводить сортировку не выбран!");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
           
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) // sort_Button
        {
            Sorted();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // SetDefaultSettings to dataGrid
        {
            GridLoad();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // moving to PasswordRecovery window
        {
            TypeOfUser.LastPosition = "StorageWindow";
            PasswordRecovery _passwordRec = new PasswordRecovery();
            _passwordRec.Show();
            Close();
           
        }
    }
}
