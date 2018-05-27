using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    public Rigidbody rig;
    public void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        rig.AddForce(Vector3.forward*2000, ForceMode.Force);
    }
}
