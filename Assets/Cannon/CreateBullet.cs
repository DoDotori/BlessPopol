using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour {

    public GameObject bullet;
	void Start () {
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject preBullet = Instantiate(bullet, this.transform.position, this. transform.localRotation);
        }
	}
}
