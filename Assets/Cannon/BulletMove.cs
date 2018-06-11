
//포탄
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    float time = 0;
    public int power;
    public float y;
    public float x;
    public Vector3 forward;
    Vector3 temp;

    public float startSetaX;
    public float startSetaY;
    public int startPower;
    public void Awake()
    {
        temp = transform.position;
    }

    public void FixedUpdate()
    {
        time += Time.deltaTime;
        float temp_z = ((Mathf.Sqrt(power) * Mathf.Cos(y)) * time);
        float temp_y = -0.5f * 9.8f * time * time + (Mathf.Sqrt(power) * Mathf.Sin(y) * time);

        float _x = ( temp_z * forward.x);
        float _y = temp_y;
        float _z = ( temp_z * forward.z);
        transform.position = new Vector3(temp.x + _x, temp.y + _y,temp.z+ _z);

        if (transform.position.y < -1)
            Destroy(this.gameObject);
    }

   
}

