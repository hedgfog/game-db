using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;
using UnityEngine.UI;

public class DatabaseConnector : MonoBehaviour
{
    NpgsqlConnection dbcon;
    
    public GameObject player;
    public GameObject authCanvas;

    // Start is called before the first frame update
    void Start()
    {
        string connectionString =
            "Port = 5432;"+
            "Server=rajje.db.elephantsql.com;" +
            "Database=zfbcnkrv;" +
            "User ID=zfbcnkrv;" +
            "Password=triWoO-GqbKz51J8mNENT0HptoRU8cgU;";

                    
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

    }
    public void Auth(){
        var username = GameObject.Find("Username").GetComponent<Text>();
        if (username.text.Equals("")){
            Debug.Log("Error usename is Empty");
            return;
        }
        var password = GameObject.Find("Password").GetComponent<Text>();
        if (password.text.Equals("")){
            Debug.Log("Error password is Empty");
            return;
        }      
        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        string sql = "SELECT id, type_user, lvl " +
                       "FROM users " +
                      "WHERE name     = '" + username.text  + "'" + 
                        " AND password = '" + password.text + "'";
        dbcmd.CommandText = sql;
        
        bool isUserFound = false;
        GameObject playerClone = new GameObject();
        NpgsqlDataReader reader = dbcmd.ExecuteReader();
        int id = -1;
        while (reader.Read()){
            id        = (reader.IsDBNull(0)) ? -1 : reader.GetInt32(0);
            var type_user = (reader.IsDBNull(1)) ? -1 : reader.GetInt16(1);
            var lvl       = (reader.IsDBNull(2)) ? -1 : reader.GetInt32(2);

            Debug.Log("Id: "   + id + "; Type: " + type_user + "; lvl: "  + lvl);
           
            playerClone = Instantiate(player);    
            playerClone.transform.position = new Vector3(0f, 0f, 0f);
            
            playerClone.GetComponent<PlayerParametr>().id        = id;            
            playerClone.GetComponent<PlayerParametr>().lvl       = lvl;
            playerClone.GetComponent<PlayerParametr>().type_user = type_user;
            
            authCanvas.SetActive(false);
            isUserFound = true;
        }


        if (isUserFound){
            dbcmd = dbcon.CreateCommand();
            sql = "SELECT name, value " +
                    "FROM characteristic " +
                   "WHERE user_id = " + id;
            dbcmd.CommandText = sql;
            reader = dbcmd.ExecuteReader();

            while (reader.Read()){
                if (reader.GetString(0).Equals("speed")){
                    playerClone.GetComponent<PlayerMove>().speed = reader.GetInt32(1);
                    
                }
       
            }
           
        }
    }

    public void UpdateLevel(int lvl, int id){
        dbcmd = dbcon.CreateCommand();
            sql = "UPDATE users SET lvl = " + lvl + " WHERE id = " + id ";"
        dbcmd.CommandText = sql;
        dbcmd.ExecuteReader();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
