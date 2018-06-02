using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestCannonControl : MonoBehaviour {

    //각도에 영향 받는 헤드
    GameObject cannonHead;
    float Xseta =2;
    float Yseta = 2;
    public void Awake()
    {
        cannonHead = this.transform.Find("HeadAxis").gameObject;
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
              this.transform.Rotate(Vector3.up, Xseta);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
              this.transform.Rotate(Vector3.up, -Xseta);
        }
    }

    public void FixedUpdate()
    {
        MoveCannonHead();
    }
}
