using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Toolkit.Uwp.Notifications;
using analysis_of_football_matches.Model;
using System.Threading;

namespace analysis_of_football_matches.Controller
{
    class Controller
    {
        public Controller()
        {
            DB = new DB_Controller(GetConnectionString());
        }
        List<Model.Tournament_And_Id> tournament_And_Ids = new List<Model.Tournament_And_Id>();

        DB_Controller DB;
        List<Model.Match_Info_Model> matches_Infos_change_Models = new List<Model.Match_Info_Model>();
        public List<Model.Match_Info_Model> Get_Infos_list(List<int> Id_List)
        {
            List<Model.Match_Info_Model> matches_Infos_Models = new List<Model.Match_Info_Model>();
            Model.Matches_Infos_List_Model matches_Infos_List_Model = new Model.Matches_Infos_List_Model();
            try
            {
                matches_Infos_List_Model = DB.Read_data_from_Second_script(DB.Read_data_from_first_script());
                using (StreamWriter Writer = new StreamWriter("Log.txt", true))
                {
                    if(matches_Infos_List_Model.Match_Info.Count != 0)
                    {
                            
                            
                    }
                }
            }
            catch (Exception ex)
            {
                matches_Infos_List_Model.Match_Info.Add(new Match_Info_Model(123123, 123, 123, "", "", DateTime.Now, ""));
            }
            
            foreach(int Id in Id_List)
            {
                foreach(Model.Match_Info_Model match_Info_Model in matches_Infos_List_Model.Match_Info)
                {
                    if(match_Info_Model.tournament == Id)
                    {
                        matches_Infos_Models.Add(match_Info_Model);
                    }
                }
            }
            bool Isnewchange = false;
            foreach (Model.Match_Info_Model match_Info_Model in matches_Infos_Models)
            {
                Isnewchange = true;
                foreach (Model.Match_Info_Model match_Info_change_Model in matches_Infos_change_Models)
                {
                    if (match_Info_Model.Id == match_Info_change_Model.Id)
                    {
                        Isnewchange = false;
                    }
                }
                if (Isnewchange)
                {
                    new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Появилось новое совпадение")
                    .Show();
                }
            }
            matches_Infos_change_Models = matches_Infos_Models;
            return matches_Infos_Models;
        }

        internal string GetIDStr()
        {
            string path = "IDs.txt";
            string str = "";
            using (StreamReader reader = new StreamReader(path))
            {
                str = reader.ReadToEnd();
            }
            return str;
        }
        internal void SetIDStr(string str)
        {
            string path = "IDs.txt";
            using (StreamWriter reader = new StreamWriter(path))
            {
                reader.Write(str);
            }
        }

        private string GetConnectionString()
        {

            string path = "Connection.txt";
            string text = "";
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }

        internal void Set_id_for_tournament(int v)
        {
            int Id = 0;
            Model.Tournament_And_Id tournament_And_Id = new Model.Tournament_And_Id(0, v);
            tournament_And_Ids.Add(tournament_And_Id);
            try
            {
                Id = DB.Get_Id_from_tournament(v);
                if (tournament_And_Ids.Remove(tournament_And_Id))
                {
                    tournament_And_Ids.Add(new Model.Tournament_And_Id(Id, v));
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter Writer = new StreamWriter("Log.txt", true))
                {
                    Writer.WriteLine(ex.Message);
                }
                Thread.Sleep(2000);
                tournament_And_Ids.Remove(tournament_And_Id);
                tournament_And_Ids.Add(new Model.Tournament_And_Id(10, v));
            }
        }

        internal void DeleteIdTornamentAndId(Tournament_And_Id selectedItem)
        {
            tournament_And_Ids.Remove(selectedItem);
        }

        internal List<Tournament_And_Id> Get_id_and_tournament()
        {
            return tournament_And_Ids;
        }


        internal void Save_curent_res(List<Model.Match_Info_Model> match_Info_Models)
        {
            using (StreamWriter Writer = new StreamWriter("Saves.txt", false))
            {
                Writer.WriteLine("Совпадения:");
            }
            using (StreamWriter Writer = new StreamWriter("Saves.txt", true))
            {
                foreach (Model.Match_Info_Model Info in match_Info_Models)
                {
                    Writer.WriteLine(Info.Id.ToString() + " " + Info.season.ToString() + " " + Info.tournament.ToString() + " " + Info.tour_name + " " + Info.tour_name_eng + Convert.ToString(Info.date) + " " + Info.time);
                }
                
            }
        }

        internal void ClearIdTornamentAndId()
        {
            tournament_And_Ids.Clear();
        }
    }
}
