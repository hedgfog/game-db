using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParametr : MonoBehaviour
{
    public int id;
    public int type_user;
    public int lvl;
    public DatabaseConnector db;
    // Start is called before the first frame update
    void Start()
    {
        var sphere = GetComponentInChildren<Renderer>();
        if (type_user == 0) {
            sphere.material.SetColor("_Color", Color.red);
        }
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.localScale *= lvl;
        // localScale.x += lvl;
        // localScale.y += lvl;
        // localScale.z += lvl;
        // this.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevel(){
        lvl++;

        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.localScale *= lvl;
        db.UpdateLevel()
    }
}
