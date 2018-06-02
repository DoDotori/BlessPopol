using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour {

    // Use this for initialization
    List<GameObject> tileList;
    static int rowMax = 11;
    static int colMax = 11;
    static int maxTile = rowMax * colMax;
    GameObject tile;
	void Start () {
        tile = Resources.Load<GameObject>("Prefab/TestTile");
        tileList = new List<GameObject>();
        for(int i =0; i<rowMax; i++)
        {
            for(int j=0; j<colMax; j++)
            {
                GameObject t = Instantiate(tile, new Vector3(-50 + (j*10), 0, 30+(i*10)), this.transform.rotation);
                tileList.Add(t);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
