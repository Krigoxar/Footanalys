using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.IO;

namespace analysis_of_football_matches.Controller
{
    class DB_Controller
    {
        public DB_Controller(string ConnectionString)
        {
            connectionString = ConnectionString;
        }
        string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";
        string sqlExpression = "SELECT * FROM Users";
        string procname = "reg_ttd_ask_best_matches_for_analyzer";
        
        public Model.Matches_Ids_List_Model Read_data_from_first_script()
        {
            Model.Matches_Ids_List_Model matches_Ids_List_Model = new Model.Matches_Ids_List_Model();
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(procname, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter nameParam1 = new SqlParameter
                    {
                        ParameterName = "@matches_count",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = 200
                    };
                    command.Parameters.Add(nameParam1);
                    SqlParameter nameParam2 = new SqlParameter
                    {
                        ParameterName = "@msg",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam2);
                    SqlParameter nameParam3 = new SqlParameter
                    {
                        ParameterName = "@mode",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Value = "lineup"
                    };
                    command.Parameters.Add(nameParam3);
                    SqlParameter nameParam4 = new SqlParameter
                    {
                        ParameterName = "@block",
                        SqlDbType = System.Data.SqlDbType.TinyInt,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam4);
                    SqlParameter nameParam5 = new SqlParameter
                    {
                        ParameterName = "@has_short_data_out",
                        SqlDbType = System.Data.SqlDbType.TinyInt,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam5);
                    SqlParameter nameParam6 = new SqlParameter
                    {
                        ParameterName = "@check_mode",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam6);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                matches_Ids_List_Model.MatchesList.Add(new Model.Match_Id_Model(id));
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return matches_Ids_List_Model;
        }

        internal int Get_Id_from_tournament(int v)
        {
            int id = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string procname1 = "reg_ttd_ask_best_matches_for_analyzer";
                    SqlCommand command = new SqlCommand(procname1, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 300;
                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@user_id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = 9823
                    };
                    command.Parameters.Add(nameParam);
                    SqlParameter nameParam1 = new SqlParameter
                    {
                        ParameterName = "@tournament_id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = v
                    };
                    command.Parameters.Add(nameParam1);
                    SqlParameter nameParam2 = new SqlParameter
                    {
                        ParameterName = "@msg",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam2);
                    SqlParameter nameParam3 = new SqlParameter
                    {
                        ParameterName = "@mode",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Value = "lineup"
                    };

                    SqlParameter nameParam7 = new SqlParameter
                    {
                        ParameterName = "@c_football_type",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = 1
                    };
                    command.Parameters.Add(nameParam7); 
                    SqlParameter nameParam8 = new SqlParameter
                    {
                        ParameterName = "@auto_selection",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = 1
                    };
                    command.Parameters.Add(nameParam8);

                    command.Parameters.Add(nameParam3);
                    SqlParameter nameParam4 = new SqlParameter
                    {
                        ParameterName = "@block",
                        SqlDbType = System.Data.SqlDbType.TinyInt,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam4);
                    SqlParameter nameParam5 = new SqlParameter
                    {
                        ParameterName = "@has_short_data_out",
                        SqlDbType = System.Data.SqlDbType.TinyInt,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam5);
                    SqlParameter nameParam6 = new SqlParameter
                    {
                        ParameterName = "@check_mode",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam6);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                id = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return id;
        }

        public Model.Matches_Infos_List_Model Read_data_from_Second_script(Model.Matches_Ids_List_Model Id_List)
        {
            Model.Matches_Infos_List_Model matches_Infos_List = new Model.Matches_Infos_List_Model();
            try
            {
                string idstring = "";
                foreach (Model.Match_Id_Model Id in Id_List.MatchesList)
                {
                    if (idstring != "")
                    {
                        idstring += ",";
                    }
                    idstring += Id.match_id.ToString();
                }
                if (idstring != "")
                {
                    
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand();
                        command.CommandText = "select c.id id, c.season_id season,t.id tournament, t.name tour_name, t.name_eng tour_name_eng,convert(date,c.dt) as date,convert(VARCHAR(10),c.dt,108) as time " +
                        "from calendar c left join Tournaments t (nolock) on t.id=c.tournament_id " +
                        "left join Calendar_additional_data ca (nolock) on ca.match_id=c.id " +
                        "left join c_football_type ft (nolock) on ft.id=ca.c_football_type " +
                        "left join MatchStatuses ms (nolock) on ms.id=c.status_id " +
                        "left join teams tm (nolock) on tm.id=c.first_team_id " +
                        "left join teams tm1 (nolock) on tm1.id=c.second_team_id " +
                        "left join Rounds r (nolock) on r.id=c.round_id " +
                        "where c.dl=0 and (c.id in (" + idstring + ")) " +
                        "order by t.id asc ";
                        command.Connection = connection;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read()) // построчно считываем данные
                                {
                                    int Id = (int)reader.GetValue(0);
                                    int season = (int)reader.GetValue(1);
                                    int tournament = (int)reader.GetValue(2);
                                    string tour_name = (string)reader.GetValue(3);
                                    string tour_name_eng = (string)reader.GetValue(4);
                                    DateTime date = (DateTime)reader.GetValue(5);
                                    string time = (string)reader.GetValue(6);
                                    matches_Infos_List.Match_Info.Add(new Model.Match_Info_Model(Id, season, tournament, tour_name, tour_name_eng, date, time));
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return matches_Infos_List;
        }
    }
}
