using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace partsystem
{
    class PartSystem
    {
        private PartSystem() { }
        public static MySqlConnection Connection;
        private static String Sesja;

        public string Session()
        {

            return Sesja;
        }

        public static bool TryLogin(string user, string password)
        {
            ConnectDB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from users where login=@user and password=@pass";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Connection = Connection;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                Sesja = user;
                return true;
                
            }
            else
            {
                return false;
            }
        }
        public static  List<ExisitingPart> PartList()
        {
            var list = new List<ExisitingPart>();
            if (ConnectDB() == true)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM parts", Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    list.Add(new ExisitingPart
                    {
                        id = int.Parse(dataReader["id"].ToString()),
                        part_id = int.Parse(dataReader["part_id"].ToString()),
                        name = dataReader["name"].ToString(),
                        description = dataReader["description"].ToString(),
                        type = dataReader["type"].ToString(),
                        ext_name = dataReader["ext_name"].ToString()
                    });
                }
                dataReader.Close();
                Connection.Close();
                  return list;
            }
            else
            {
                return list;
            }
        }



        static bool ConnectDB()
        {
              try
               {
                   Connection = new MySqlConnection(
                       "SERVER = localhost; " +
                       "DATABASE = partsystem; " + 
                       "UID = root; " + 
                       "PASSWORD=1; " + 
                       "sslmode = none; ");
                   Connection.Open();
                   return true;
               }
               catch (Exception e)
               {
                   return false;
               }
          




            }
        public static void AddUpdate(string part_id, string name, string description, string type, string ext_name, int id) {
            if (ConnectDB() == true)
            {
                string cmdText;
                if (id!=0)
              cmdText = String.Format("UPDATE parts " + "SET part_id = '{0}'," + "name = '{1}'," + " description = '{2}',type = '{3}', ext_name = '{4}' WHERE id = '{5}' "
                    , part_id
                    , name
                    , description
                    , type
                    , ext_name
                    , id);
                else
                    cmdText = String.Format("INSERT INTO parts(part_id, name, description, type, ext_name) VALUES('{0}', '{1}', '{2}', '{3}', '{4}') "
                    , part_id
                    , name
                    , description
                    , type
                    , ext_name
                    );



                MySqlCommand cmd = new MySqlCommand(cmdText, 
                   Connection);
                cmd.ExecuteNonQuery();
                Connection.Close();
               

            }


        }
        public static List<ExisitingPart> SearchInList(List<ExisitingPart> list, string text,string typeCB)
        {
            List<ExisitingPart> found = new List<ExisitingPart>();
            foreach(ExisitingPart part in list)
            {
                if (part.GetType().GetProperty(typeCB).GetValue(part).ToString().IndexOf(text)!=-1) {
                    found.Add(part);
                }
                
            }

           
            return found;
        }
        }
    class ExisitingPart
    {
        public int id { get; set; }
        public int part_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string ext_name { get; set; }
    }
}
