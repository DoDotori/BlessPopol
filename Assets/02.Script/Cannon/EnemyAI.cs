using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    //각도에 영향 받는 헤드
    GameObject cannonHead;
    GameObject createBulletPos;
    //총알
    GameObject bullet;
    BulletMove bulletMove;

    float setaX;
    float setaY;
    public int cannonPower;
    //x축 회전 --상하-- 제일아래 -20 제일위 -50 ---- setaY
    //y축 회전 --좌우-- 왼쪽끝 -110 오른쪽끝 -70 ---setaX
    int leftSetaX = 110;
    int rightSetaX = 70;
    int downSetaY = 20;
    int upSetaY = 50;
    public float delayTime = 0.1f;  //장전속도

    public void Awake()
    {
        cannonHead = this.transform.Find("CannonHead").gameObject;
        createBulletPos = cannonHead.transform.Find("CreateBullet").gameObject;
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
        bulletMove = bullet.GetComponent<BulletMove>();
      
        setaX = 70;
        setaY = 20;
        cannonPower = 50;

        //초기위치값 엇나간거 잡아주기
        {
            Vector3 pos = cannonHead.transform.position;
            pos.z = 0;
            cannonHead.transform.position = pos;
        }

        cannonHead.transform.eulerAngles = Vector3.up * -setaX + Vector3.right * (-setaY);
    }

    public void PlusSetaX()
    {
        setaX++;
    }
    public void SetSetaX(float _setaX)
    {
        setaX = _setaX;
    }

    public void PlusSetaY()
    {
        setaY++;
    }

    public void SetSetaY(float _setaY)
    {
        setaY = _setaY;
    }

    public void PlusPower()
    {
        cannonPower++;
    }
    public void SetPower(int _power)
    {
        cannonPower = _power;
    }
    public static float Radius_To_Seta(float _Seta)
    {
        float temp;

        temp = _Seta * (3.14f / 180);
        return temp;
    }

    public void Create_Bullet()
    {
        bulletMove.x = Radius_To_Seta(setaX);
        bulletMove.y = Radius_To_Seta(setaY);
        bulletMove.forward = cannonHead.transform.forward;
        bulletMove.power = (int)Mathf.Pow(cannonPower,2);

        bulletMove.startPower = (int)Mathf.Pow(cannonPower, 2);
        bulletMove.startSetaX = setaX;
        bulletMove.startSetaY = setaY;

        GameObject preBullet = Instantiate(bullet, createBulletPos.transform);
    }
    float time = 0;
    public void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time <delayTime)
        {

        }
        else
        {
            AddSeta();
            Create_Bullet();
            time = 0;
        }
    }

    public void AddSeta()
    {
        if (setaY <= upSetaY)
        {
            //y각도를 올린다.
            PlusSetaY();
        }
        else
        {
            if(setaX <= leftSetaX)
            {
                PlusSetaX();
                SetSetaY(20);
            }
            else
            {
                PlusPower();
                SetSetaX(70);
            }
        }
        cannonHead.transform.eulerAngles = Vector3.up * -setaX + Vector3.right * (-setaY);
    }
}

