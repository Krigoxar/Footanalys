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
using System.Threading;
using System.Windows.Threading;

namespace analysis_of_football_matches
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Model.Match_Info_Model> match_Info_Models = new List<Model.Match_Info_Model>();
        Controller.Controller controller = new Controller.Controller();
        public List<int> Id_List = new List<int>();
        Id_window id_Window;
        List<Model.Tournament_And_Id> tournament_And_Ids = new List<Model.Tournament_And_Id>();
        List<Model.Tournament_And_Id> tournament_And_Ids_in_list = new List<Model.Tournament_And_Id>();
        void timer_Tick(object sender, EventArgs e)
        {
            update();
        }
        private void timer_Tick1(object sender, EventArgs e)
        {
            ListBox.ItemsSource = match_Info_Models;
        }
        private void timer_Tick2(object sender, EventArgs e)
        {
            change_id_and_tournaments();
        }
        void change_id_and_tournaments()
        {
            tournament_And_Ids = controller.Get_id_and_tournament();
            if (!tournament_And_Idseq(tournament_And_Ids_in_list, tournament_And_Ids) || tournament_And_Ids.Count != tournament_And_Ids_in_list.Count)
            {
                ListBox1.ItemsSource = null;
                ListBox1.ItemsSource = tournament_And_Ids;
                tournament_And_Ids_in_list = tournament_And_Ids.GetRange(0, tournament_And_Ids.Count);
            }
        }
        bool tournament_And_Idseq(List<Model.Tournament_And_Id> a, List<Model.Tournament_And_Id> b)
        {
            bool res = false;
            foreach(Model.Tournament_And_Id aa in a)
            {
                res = false;
                foreach (Model.Tournament_And_Id bb in b)
                {
                    if(aa.Id == bb.Id && aa.tournament == bb.tournament)
                    {
                        res = true;
                    }
                }
            }
            foreach (Model.Tournament_And_Id bb in b)
            {
                res = false;
                foreach (Model.Tournament_And_Id aa in a)
                {
                    if (aa.Id == bb.Id && aa.tournament == bb.tournament)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }
        public MainWindow()
        {
            InitializeComponent();
            update();
            
            try
            {
                foreach (string str in controller.GetIDStr().Split(","))
                {
                    Id_List.Add(Convert.ToInt32(str));
                }
            }
            catch
            {

            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += timer_Tick;
            timer.Start();

            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(1);
            timer1.Tick += timer_Tick1;
            timer1.Start();

            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += timer_Tick2;
            timer2.Start();
        }
        
        public void update()
        {
            Thread thread = new Thread(load);
            thread.Start();
        }
        public void load()
        {
            match_Info_Models = controller.Get_Infos_list(Id_List);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            update();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            id_Window = new Id_window();
            
            string str = controller.GetIDStr();
            id_Window.text_box.Text = str;
            id_Window.Owner = this;
            id_Window.ShowDialog();
        }

        private void Take_match_Click(object sender, RoutedEventArgs e)
        {
            if(TournamentId.Text != "") 
            {
                Thread thread = new Thread(Set_tournament_for_id); 
                thread.Start(Convert.ToInt32(TournamentId.Text));
            }
        }

        private void Take_match_from_list_Click(object sender, RoutedEventArgs e)
        {
            if((Model.Match_Info_Model)ListBox.SelectedItem != null)
            {
                Thread thread = new Thread(Set_tournament_for_id);
                thread.Start(Convert.ToInt32(((Model.Match_Info_Model)ListBox.SelectedItem).tournament));
            }
        }
        
        void Set_tournament_for_id(object Id)
        {
            controller.Set_id_for_tournament((int)Id);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                controller.DeleteIdTornamentAndId((Model.Tournament_And_Id)ListBox1.SelectedItem);
                tournament_And_Ids.Remove((Model.Tournament_And_Id)ListBox1.SelectedItem);
            }
            change_id_and_tournaments();
        }

        private void Delete_all_Click(object sender, RoutedEventArgs e)
        {
            controller.ClearIdTornamentAndId();
            tournament_And_Ids.Clear();
            change_id_and_tournaments();
        }
    }
}
