using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for PasswordRecovery.xaml
    /// </summary>
    public partial class PasswordRecovery : Window
    {
        SqlConnection _conn = new SqlConnection(@"Data Source=OnlyMyself\YAYA;Database=StorageFinish;Integrated Security=True;");

        public void GridLoadMain()
        {
            SqlCommand _cmd = new SqlCommand("SELECT * FROM [ApplicationsR]", _conn);
            _conn.Open();
            SqlDataReader _DataR = _cmd.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_DataR);
            _conn.Close();
            dataGridMain.ItemsSource = _dt.DefaultView;
        }
        public void GridLoadSecondary()
        {
            SqlCommand _cmd = new SqlCommand("SELECT * FROM [User]", _conn);
            _conn.Open();
            SqlDataReader _sDataRead = _cmd.ExecuteReader();
            DataTable _Dtbl = new DataTable();
            _Dtbl.Load(_sDataRead);
            _conn.Close();
            dataGridSecond.ItemsSource = _Dtbl.DefaultView;

        }
        public PasswordRecovery()
        {
            InitializeComponent();
            GridLoadMain();
            GridLoadSecondary();
        }

        public bool isInfoAboutPasswordLog()
        {
            if (LoginOfWhoAsking_txt.Text != "" && password_txt.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните необходимые ячейки [Login, Password] ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
        public void CleaningTxt()
        {
            LoginOfWhoAsking_txt.Clear();
            password_txt.Clear();
            id_count_txt.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // inserting information to txt_btn
        {
            if (isInfoAboutPasswordLog())
            {
                StreamWriter _txtWriter = new StreamWriter(@"C:\password_user.txt", true); //True - for giving a yes on asking permission to write all of the new information on the new line after the past
                _txtWriter.WriteLine($"Login:{LoginOfWhoAsking_txt.Text} Password: {password_txt.Text}");
                _txtWriter.Close();
                MessageBox.Show($"Information were added to TXT file");

                CleaningTxt();
            }
            
        }


        public bool IsIDHas()
        {
            if (id_count_txt.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните необходимыую ячейку [IdOfAskingPerson] ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) //SetCompleted To Request_btn
        {
             if (IsIDHas())
             {
                DateTime _dt;
                _dt = DateTime.Now;
                SqlCommand _cmd = new SqlCommand($"UPDATE [ApplicationsR] SET RecoveryStatus = 'Done', DateOfRecovery = '"+_dt+"'  WHERE ID = '" + id_count_txt.Text + "'", _conn);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _conn.Close();
                GridLoadMain();
                MessageBox.Show($"Information from {id_count_txt.Text} point was changed to 'Done'");
             }
             
           
           
        }
    }
}
