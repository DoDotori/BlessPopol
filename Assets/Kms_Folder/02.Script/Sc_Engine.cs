using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Engine : MonoBehaviour {
    //private static Sc_Engine m_Instance;

    /*public static Sc_Engine GetInstance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(Sc_Engine)) as Sc_Engine;

                if(m_Instance == null)
                {
                    GameObject container = new GameObject();
                    container.name = string.Format("Sc_Engine_Container");
                    m_Instance = container.AddComponent(typeof(Sc_Engine)) as Sc_Engine;
                }
            }
            return m_Instance;
        }
    }*/
	
    public struct POINT
    {
        private int x;
        private int y;

        public void SetPoint(int _x,int _y)
        {
            if ((_x < 0 || _x > 4) && (_y < 0 || _y > 4))
            {
                Debug.LogError("입력한 값이 허용범위를 넘었습니다. (0~4)");
                Debug.LogError("입력한 x의 값" + _x + "입력한 y의 값 " + _y);
            }
            else
            {
                x = _x;
                y = _y;
            }
        }
        public int GetPoint_X()
        { return x; }

        public int GetPoint_Y()
        { return y; }

        public POINT GetPoint()
        {
            POINT pt;
            pt.x = x;
            pt.y = y;

            return pt;
        }
    }

    public enum Building_Kind { eNull, eHeadquarters,eAttack_Speed, eGauge_Speed, eRange, eCorrection, eBonus, eSkill1, eSkill2 }
}
