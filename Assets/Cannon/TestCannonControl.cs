using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestCannonControl : MonoBehaviour {

    //각도에 영향 받는 헤드
    GameObject cannonHead;

    //총알
    GameObject bullet;

    public TextMesh xRot;
    public TextMesh yRot;

    float Xseta =15;
    float Yseta = 10;
    public void Awake()
    {
        cannonHead = this.transform.Find("HeadAxis").gameObject;
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
        xRot.text = " x : 0 ";
        yRot.text = " y : 0 ";
    }

    public void MoveCannonHead()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cannonHead != null)
            {
                cannonHead.transform.Rotate(Vector3.right, -Yseta);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cannonHead != null)
            {
                cannonHead.transform.Rotate(Vector3.right, Yseta);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (cannonHead != null)
            {
                cannonHead.transform.Rotate(Vector3.up, Xseta);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (cannonHead != null)
            {
                cannonHead.transform.Rotate(Vector3.up, -Xseta);
            }
        }
    }

    public void FixedUpdate()
    {
        MoveCannonHead();
    }
}
