using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class MongoConnector : MonoBehaviour
{
    public class Transformation{
        public int   user_id { get; set; }
        public float x  { get; set; }
        public float y  { get; set; }
    }
    private MongoClient client;
    private MongoServer server;
    private MongoDatabase db;
    public MongoCollection<BsonDocument> transformations;
    string connectionString = "mongodb://localhost:27017";
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        // client = new MongoClient("mongodb://Hedg:fucda2-riffod-diHxyw@game-db-shard-00-00-1jphh.mongodb.net:27017,game-db-shard-00-01-1jphh.mongodb.net:27017,game-db-shard-00-02-1jphh.mongodb.net:27017/test");
        client = new MongoClient(connectionString);
        server = client.GetServer();
        // server.Connect();
        
        db = server.GetDatabase("game-db");
        transformations  = db.GetCollection<BsonDocument>("Moving");

        // foreach (var document in transformations.FindAll()) {
			// Debug.Log ("4. SELECT ALL DOCS: \n" + document);
		// }

    }
    private int counter = 0;
    private bool work = true;
    protected void OnGUI(){
         GUI.Label(new Rect(40, 40, 200, 20), "Inserts: " + counter);
         if (GUI.Button(new Rect(40, 70, 200, 20), "Stop"))
             work = false;
         if (GUI.Button(new Rect(40, 100, 200, 40), "Show"))
            StartCoroutine(ShowMoving());
     }
    int id = -1;
    public void Insert(int id, float x, float y){
        id = id;
        var transformation = new Transformation{
            user_id = id,
            x  = x,
            y  = y
        };
        if (counter < 100000 && work){
            counter++;
            transformations.Insert(new BsonDocument{
                { "user_id", id },
                { "x", x },
                { "y", y }
		    });
        }   
        // Debug.Log (db.CollectionExists ("Transformation").ToString ());
        
    }

    IEnumerator ShowMoving(){
        var obj = Instantiate(prefab);
        obj.transform.position = new Vector3(0f, 0f, 0f);
        foreach (var document in transformations.FindAll()) {
            
            var trans = obj.transform.position;
            trans.x = (float)document["x"].ToDouble();
            trans.y = (float)document["y"].ToDouble();;
            obj.transform.position = trans;
            yield return new WaitForSeconds(0.2f);
			Debug.Log ("4. SELECT ALL DOCS: \n" + document);
		}
        Destroy(obj, 3f);        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
