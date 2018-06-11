using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestCannonControl : MonoBehaviour {
    //각도에 영향 받는 헤드
    GameObject cannonHead;
    GameObject createBulletPos;
    //총알
    GameObject bullet;
   
    float Xseta;
    float Yseta;
    public int Power;
    float Radius = 0.0f;
    bool istemp = false;

    float g = 9.8f;

    public static float temp = 0.0f;

    Vector3 Target_Position = new Vector3(0, 0, 0);

    BulletMove bulletMove;
    public void Awake()
    {
        cannonHead = this.transform.Find("CannonHead").gameObject;
        createBulletPos = cannonHead.transform.Find("CreateBullet").gameObject;
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
        bulletMove = bullet.GetComponent<BulletMove>();

        Xseta = 90;
        Yseta = 20;
        Power = 2500;

        float Temp;
        Temp = Radius_To_Seta(Yseta);
        temp = (Power * /*Mathf.Cos(Temp)*/ Mathf.Sin(2 * Temp) / g);
        cannonHead.transform.eulerAngles = Vector3.up * Xseta + Vector3.right * (-Yseta);
    }

    public void MoveCannonHead()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Yseta++;
            float Temp;
            Temp = Radius_To_Seta(Yseta);
            temp = (2 * Power * Mathf.Cos(Temp) * Mathf.Sin(Temp) / g);
            cannonHead.transform.eulerAngles = Vector3.up * Xseta + Vector3.right * (-Yseta);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Yseta--;
            float Temp;
            Temp = Radius_To_Seta(Yseta);
            temp = (2 * Power * 1 * Mathf.Cos(Temp) * Mathf.Sin(Temp) / g);
            cannonHead.transform.eulerAngles = Vector3.up * Xseta + Vector3.right * (-Yseta);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Xseta++;
            cannonHead.transform.eulerAngles = Vector3.up * Xseta + Vector3.right * (-Yseta);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Xseta--;
            cannonHead.transform.eulerAngles = Vector3.up * Xseta + Vector3.right * (-Yseta);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Create_Bullet();
        }

        

        Radius = (Yseta) * Power;



    }

    public static float Radius_To_Seta(float _Seta)
    {
        float temp;

        temp = _Seta * (3.14f / 180);
        return temp;
    }

    public void FixedUpdate()
    {
        MoveCannonHead();
    }

    public void Create_Bullet()
    {
        bulletMove.x = Radius_To_Seta(Xseta);
        bulletMove.y = Radius_To_Seta(Yseta);
        bulletMove.forward = cannonHead.transform.forward;
        bulletMove.power = Power;
     
        GameObject preBullet = Instantiate(bullet, createBulletPos.transform);
    }
}