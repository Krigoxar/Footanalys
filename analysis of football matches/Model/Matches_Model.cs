using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace analysis_of_football_matches.Model
{
    class Match_Id_Model
    {
        public Match_Id_Model(int Id)
        {
            match_id = Id;
        }
        readonly public int match_id;
    }
    class Matches_Ids_List_Model
    {
        public Matches_Ids_List_Model()
        {
            MatchesList = new List<Match_Id_Model>();
        }
        public List<Match_Id_Model> MatchesList;
    }
    class Match_Info_Model
    {
        public Match_Info_Model(int Id, int season, int tournament,string tour_name, string tour_name_eng, DateTime date, string time)
        {
            this.Id = Id;
            this.season = season;
            this.tournament = tournament;
            this.tour_name = tour_name;
            this.tour_name_eng = tour_name_eng;
            this.date = date;
            this.time = time;
        }
        public int Id { get; set; }
        public int season { get; set; }
        public int tournament { get; set; }
        public string tour_name { get; set; }
        public string tour_name_eng { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }
    }
    class Tournament_And_Id
    {
        public Tournament_And_Id(int Id, int tournament)
        {
            this.Id = Id;
            this.tournament = tournament;
        }
        public int Id { get; set; }
        public int tournament { get; set; }
    }
    class Matches_Infos_List_Model
    {
        public Matches_Infos_List_Model()
        {
            Match_Info = new List<Match_Info_Model>();
        }
        public List<Match_Info_Model> Match_Info;
    }
}
