using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    public Rigidbody rig;
    public GameObject createBulletPoint;
    public int pow;//= 1000; //최소 1000 - 최대 1600

    public void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        rig.mass = 24;
        
        if(createBulletPoint != null)
        {
            rig.AddForce(this.transform.forward * pow, ForceMode.Impulse);
        }

    }

    public void FixedUpdate()
    {
        if(this.transform.position.y <= -2)
        {
            Debug.Log(string.Format("x : {0} , z : {1} ", this.transform.position.x, this.transform.position.z));

            Destroy(this.gameObject);
        }
    }
}
