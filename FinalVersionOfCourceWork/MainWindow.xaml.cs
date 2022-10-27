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
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;

namespace FinalVersionOfCourceWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsVisiblePassword;

        public MainWindow()
        {

            InitializeComponent();

            if (TypeOfUser.User != "Administrator")
            {

                comboBox.Items.RemoveAt(0);
                dataGrid.Visibility = Visibility.Hidden;
                SortComboBox.Visibility = Visibility.Hidden;
                Sort_button.Visibility = Visibility.Hidden;

                Passwordo_txt.Margin = new Thickness(244, 43, 0, 0);
                Password_txt.Margin = new Thickness(244, 43, 0, 0);
                password_label.Margin = new Thickness(364, 43, 0, 0);

                Login_txt.Margin = new Thickness(114, 43, 0, 0);
                login_label.Margin = new Thickness(55, 43, 0, 0);

                comboBox.Margin = new Thickness(174, 95, 0, 0);
                pemissions_label.Margin = new Thickness(197, 115, 0, 0);

                ID_number_txt.Margin = new Thickness(173, 180, 0, 0);
                ID_label.Margin = new Thickness(217, 193, 0, 0);

                return_button.Margin = new Thickness(184, 140, 0, 0);

                Application.Current.MainWindow = this;
                Application.Current.MainWindow.Height = 276;


            }
            else
            {
                dataGrid.Visibility = Visibility.Visible;
            }
            GridLoad();



        }
        SqlConnection _conn = new SqlConnection(@"Data Source=OnlyMyself\YAYA;Database=StorageFinish;Integrated Security=True;");
        public void GridLoad()
        {

            try
            {
                DataTable _DT = new DataTable();
                _conn.Open();
                 SqlCommand _cmd = new SqlCommand($"SELECT * FROM [User]", _conn); // с целью, для уточнения, что обращение к таблице происходит, добавляем ограду в лице скобок квадратных.
                 SqlDataReader _dataRead = _cmd.ExecuteReader();
                 _DT.Load(_dataRead);
                 _conn.Close();
                 dataGrid.ItemsSource = _DT.DefaultView;
                
             



            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            

        }
        public bool RightPassword()
        {
            string[] armen = new string[] { "@", "!", "#", "$", "%", "&", "*" };
            if (IsVisiblePassword == false)
            {
                if (Passwordo_txt.Password.Length >= 4 && Passwordo_txt.Password.Any(Char.IsLetter) && Passwordo_txt.Password.Any(Char.IsDigit) && Passwordo_txt.Password.Contains(armen[0]) || Passwordo_txt.Password.Contains(armen[1]) || Passwordo_txt.Password.Contains(armen[2]) || Passwordo_txt.Password.Contains(armen[3]) || Passwordo_txt.Password.Contains(armen[4]) || Passwordo_txt.Password.Contains(armen[5]))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("В Вашем пароле должно присутствовать: \nНе менее 4-х символов, любого вида. \nКак минимум - один 'специальный' символ(т.е @,$ и т.п). \nНе менее 1-й цифры.");
                    return false;
                }
            }
            else
            {
                if (Password_txt.Text.Length >= 4 && Password_txt.Text.Any(Char.IsLetter) && Password_txt.Text.Any(Char.IsDigit) && Password_txt.Text.Contains(armen[0]) || Password_txt.Text.Contains(armen[1]) || Password_txt.Text.Contains(armen[2]) || Password_txt.Text.Contains(armen[3]) || Password_txt.Text.Contains(armen[4]) || Password_txt.Text.Contains(armen[5]))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("В Вашем пароле должно присутствовать: \nНе менее 4-х символов, любого вида. \nКак минимум - один 'специальный' символ(т.е @,$ и т.п). \nНе менее 1-й цифры.");
                    return false;
                }
            }

        }
        public bool IsRight()
        {
            if (IsVisiblePassword == false)
            {
                if (Login_txt.Text == "" && Passwordo_txt.Password == "")
                {
                    MessageBox.Show("Введите, для начала: пароль, совместно с логином. \nНо попытка - хорошая!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }
                else if (comboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите тип для пользователя, что желаете добавить", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                else
                {

                    return true;
                }
            }
            else
            {
                if (Login_txt.Text == "" && Password_txt.Text == "")
                {
                    MessageBox.Show("Введите, для начала: пароль, совместно с логином. \nНо попытка - хорошая!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }
                else if (comboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите тип для пользователя, что желаете добавить", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                else
                {

                    return true;
                }
            }

        }
        public void ClearTextBoxes()
        {
            Login_txt.Clear();
            Password_txt.Clear();
            ID_number_txt.Clear();
            Passwordo_txt.Clear();


        }

        public bool IdCheck()
        {
            if (ID_number_txt.Text == "")
            {
                return false;
            }
            else { return true; }
        }

        public void Sorting()
        {
            try
            {
                switch (SortComboBox.SelectedIndex)
                {
                    case 0:
                        dataGrid.Items.SortDescriptions.Clear();
                        dataGrid.Items.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Descending));
                        dataGrid.Items.Refresh();
                        break;
                    case 1:
                        dataGrid.Items.SortDescriptions.Clear();
                        dataGrid.Items.SortDescriptions.Add(new SortDescription("Permissons", ListSortDirection.Descending));
                        dataGrid.Items.Refresh();
                        break;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Button_Click(object sender, RoutedEventArgs e) // insertBtn
        {
            try
            {

                if (IsRight())
                {
                    if (RightPassword())
                    {
                        var item = (ComboBoxItem)comboBox.SelectedValue; // Помещение выделенного значения из объекта - комбо_контейнера, что связан с классом в виде значенией, в переменную.
                        var content = (string)item.Content; // преобразорвание того, во внутренней части у переменной что в наличии, в необходимый, строковой формат.


                        SqlCommand _cmd = new SqlCommand("INSERT INTO [User] VALUES (@Login, @Password, @Permissons)", _conn);
                        _cmd.CommandType = CommandType.Text;
                        
                        _cmd.Parameters.AddWithValue("@Login", Login_txt.Text);

                        // Варианты для двух различных, что могут быть активны по-отдельности, textBox'es |

                        if (IsVisiblePassword == false)
                        {
                            _cmd.Parameters.AddWithValue("@Password", Passwordo_txt.Password.ToString());
                        }
                        else { _cmd.Parameters.AddWithValue("@Password", Password_txt.Text.ToString()); } 

                        // Password, параметр - тот же элемент Text, но с иным названием, для данного компонента. |

                        _cmd.Parameters.AddWithValue("@Permissons", content);
                        _conn.Open();
                        _cmd.ExecuteNonQuery();
                        _conn.Close();
                        GridLoad();
                        ClearTextBoxes();
                        MessageBox.Show("Информация была добавлена, апплодисменты для Вас!", "congratulationsWindow", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


       
        public void DeleteCell()
        {
            if (IdCheck())
            {
                _conn.Open();
                SqlCommand _cmd = new SqlCommand($"DELETE FROM [User] WHERE ID = {ID_number_txt.Text}", _conn);
                try
                {

                    _cmd.ExecuteNonQuery();
                    MessageBox.Show($"Информация из точки {ID_number_txt.Text} была удалена.", "Апплодисменты", MessageBoxButton.OK, MessageBoxImage.Hand);
                    _conn.Close();
                    ClearTextBoxes();
                    GridLoad();
                    _conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else { MessageBox.Show("Введите для начала индетификатор позиции, очистку которой желаете произвести", "Warning",MessageBoxButton.OK,MessageBoxImage.Warning); }
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //Delete_Button
        {
            DeleteCell();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //ClearTxt's_Buttons
        {
            ClearTextBoxes();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //UpdateButton
        {
            if (IsRight())
            {
                if (RightPassword())
                {
                    try
                    {
                        var item = (ComboBoxItem)comboBox.SelectedValue; // Помещение выделенного значения из объекта - комбо_контейнера, что связан с классом в виде значенией, в переменную.
                        var content = (string)item.Content; // преобразорвание того, во внутренней части у переменной что в наличии, в необходимый, строковой формат.

                        _conn.Open();
                        if (IsVisiblePassword == false)
                        {
                            SqlCommand _cmd = new SqlCommand("UPDATE [User] SET Login = '" + Login_txt.Text + "', Password = '" + Passwordo_txt.Password + "', Permissons = '" + content + "' WHERE ID = '" + ID_number_txt.Text + "' ", _conn);
                            _cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand _cmd = new SqlCommand("UPDATE [User] SET Login = '" + Login_txt.Text + "', Password = '" + Password_txt.Text + "', Permissons = '" + content + "' WHERE ID = '" + ID_number_txt.Text + "' ", _conn);
                            _cmd.ExecuteNonQuery();
                        }



                        MessageBox.Show($"Данные из позиции - {ID_number_txt.Text}, были изменены, с успехом!", "UpdateEvent", MessageBoxButton.OK, MessageBoxImage.Information);


                    }

                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    finally
                    {

                        _conn.Close();
                        ClearTextBoxes();
                        GridLoad();

                    }
                }
               
            }
        
        }

        private void radioINVISIBLE_Checked(object sender, RoutedEventArgs e)
        {
            Password_txt.Visibility = Visibility.Hidden;
            Passwordo_txt.Clear();
            Passwordo_txt.Visibility = Visibility.Visible;
            IsVisiblePassword = false;
            
        }

        private void radioVISIBLE_Checked(object sender, RoutedEventArgs e)
        {
            Passwordo_txt.Visibility = Visibility.Hidden;
            Password_txt.Clear();
            Password_txt.Visibility = Visibility.Visible;
            IsVisiblePassword = true;
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // Sort_Button
        {
            Sorting();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // return button
        {
            TypeOfUser.RoadToTheHome();
            Close();
           
        }


        public bool IsLoginCheck()
        {
            if (Login_txt.Text == "")
            {
                MessageBox.Show("Введите свой login,чтобы мы продолжили процедуру!", "NewTaskForU", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else { return true;  }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e) //ForgetPassword_btn  
        {

            if (IsLoginCheck())
            {
               MessageBoxResult result = MessageBox.Show("Вы уверены в том, что Вам необходимо восстановить пароль?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DateTime _dt;
                        _dt = DateTime.Now;

                        SqlCommand _cmd = new SqlCommand("INSERT INTO [ApplicationsR] VALUES  (@UserWhoAsking, @RecoveryStatus, @DateOfRecovery, @DateOfCreatingApplication)", _conn);
                        _cmd.Parameters.AddWithValue("@UserWhoAsking", Login_txt.Text);
                        _cmd.Parameters.AddWithValue("@RecoveryStatus", "In Progress");
                        _cmd.Parameters.AddWithValue("@DateOfRecovery", "");
                        _cmd.Parameters.AddWithValue("DateOfCreatingApplication", _dt);

                        _conn.Open();
                        _cmd.ExecuteNonQuery();
                        _conn.Close();

                        ClearTextBoxes();
                        MessageBox.Show("Запрос был передан составу администраторов, с Вами свяжутся. Ожидайте!", "Request Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
    }
}
