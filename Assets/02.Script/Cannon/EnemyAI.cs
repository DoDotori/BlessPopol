using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public enum eAI_STATE
    {
        AI_EASY, AI_NORMAL, AI_HARD, AI_END
    }
    public eAI_STATE state;
    public GameObject CannonHead;
    public GameObject bullet;
    public BulletMove scBulletMove;
    public GameObject createPoint;
    public GameObject preBullet;
    //상하좌우 각도, 파워
    public float setaX;
    public float setaY;
    public float seta;
    public int power;
    
    private void Awake()
    {
        //포대 기본셋팅
        CannonHead = this.transform.Find("CannonHead").gameObject;
        createPoint = CannonHead.transform.Find("CreateBullet").gameObject;
        state = eAI_STATE.AI_EASY;
        seta = 2;
        setaX = -20;
        setaY = -110;
        power = 1000;

        CannonHead.transform.rotation = Quaternion.Euler(setaX, setaY, 0);

        //총알 기본셋
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
        scBulletMove = bullet.GetComponent<BulletMove>();
    }
    public int testCount = 0;
    private void FixedUpdate()
    {
      
        StartCoroutine("AI_Start");
       
    }
    public IEnumerator AI_Start()
    {
       while(testCount <100)
        {
            if (preBullet == null)
                myCreate_Bullet();

            //총알 생성되고 사라질때까지 기다린다.
            yield return new WaitUntil(() => preBullet == null);

            if (-50 < setaX && setaX <= -20)
            {
                myUp_XAxis();
            }
            else if(setaX <= -50)
            {
                myInit_SetaX();
                if(-110 <= setaY && setaY <-70)
                {
                    myRight_YAxis();
                }
                else if(setaY >= -70)
                {
                    myInit_SetaY();
                }
            }
           
        }
       
      
    }

    //총알 생성
    public void myCreate_Bullet()
    {
        scBulletMove.createBulletPoint = createPoint;
        scBulletMove.pow = power;
        preBullet = Instantiate(bullet, scBulletMove.createBulletPoint.transform);
        
    }
    //X축을 기준으로 움직인다. -는 위로 +는 아래로
    public void myUp_XAxis()
    {
        setaX -= seta;
        Debug.Log(setaX.ToString());
        CannonHead.transform.rotation = Quaternion.Euler(setaX, setaY, 0);
    }
    public void myDown_XAxis()
    {
        setaX += seta;
        CannonHead.transform.rotation = Quaternion.Euler(setaX, setaY, 0);
    }
    public void myInit_SetaX()
    {
        setaX = -20;
        CannonHead.transform.rotation = Quaternion.Euler(setaX, setaY, 0);
    }

    //Y축을 기분으로 움직인다                         
    public void myRight_YAxis() {
        setaY += seta;
        CannonHead.transform.rotation = Quaternion.Euler(setaX, setaY, 0);
    }
    public void myLeft_YAxis()
    {
        setaY -= seta;
        CannonHead.transform.rotation = Quaternion.Euler(setaX, setaY, 0);
    }
    public void myInit_SetaY()
    {
        setaY = -110;
        CannonHead.transform.rotation = Quaternion.Euler(setaX, setaY, 0);
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
    //power
    public void myInit_Power() { power = 1000; }
    public void myPlus_Power() { power += 100; }
    public void myMinus_Power() { power -= 100; }
   
}
