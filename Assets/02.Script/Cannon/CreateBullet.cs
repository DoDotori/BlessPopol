using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour {

    public GameObject bullet;
	void Start () {
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
        bullet.GetComponent<BulletMove>().createBulletPoint = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            
            GameObject preBullet = Instantiate(bullet, this.transform.position, this. transform.rotation);
            Debug.Log(string.Format("{0}도", this.transform.eulerAngles.x-360));
        }
	}
}
