using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace analysis_of_football_matches
{
    /// <summary>
    /// Логика взаимодействия для Id_window.xaml
    /// </summary>
    public partial class Id_window : Window
    {
        Controller.Controller controller = new Controller.Controller();
        public Id_window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> id_list = new List<int>();
            try
            {
                controller.SetIDStr(text_box.Text);
                foreach (string str in text_box.Text.Split(","))
                {
                    id_list.Add(Convert.ToInt32(str));
                }
            }
            catch
            {

            }
            ((MainWindow)Owner).Id_List = id_list;
            ((MainWindow)Owner).update();
            this.Close();
        }
    }
}
