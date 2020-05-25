using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public MongoConnector mongodb;
    public PlayerParametr paramsPlayer;
    public float lvlUp = 0.0f;
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        mongodb = GameObject.Find("Database").GetComponent<MongoConnector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            Vector3 position = this.transform.position;
            position.x--;
            this.transform.position = position;
            lvlUp++;
            mongodb.Insert(paramsPlayer.id, position.x, position.y);
        }
        if (Input.GetKeyDown(KeyCode.D)){
            Vector3 position = this.transform.position;
            position.x++;
            this.transform.position = position;
            lvlUp++;
            mongodb.Insert(paramsPlayer.id, position.x, position.y);            
        }
        if (Input.GetKeyDown(KeyCode.W)){
            Vector3 position = this.transform.position;
            position.y++;
            this.transform.position = position;
            lvlUp++;
            mongodb.Insert(paramsPlayer.id, position.x, position.y);
            
        }
        if (Input.GetKeyDown(KeyCode.S)){
            Vector3 position = this.transform.position;
            position.y--;
            this.transform.position = position;
            lvlUp++;
            mongodb.Insert(paramsPlayer.id, position.x, position.y);
        }
        if (lvlUp >= 100) {
            paramsPlayer.UpdateLevel();
            lvlUp = 0.0f;
        }
    }
}
