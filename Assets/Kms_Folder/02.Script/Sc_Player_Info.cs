using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Player_Info : MonoBehaviour {
    private static Sc_Player_Info m_Instance;

    public struct Have_Building
    {
        public Sc_Building Building;
        public int Number_Of_Building;
    }

    private string m_strName;
    private int m_iRank;
    private int m_iGold;
    private float m_fAttack_Speed;  //장전속도
    private float m_fGauge_Speed;   //파워게이지 속도
    private float m_fRange;         //폭발범위
    private float m_fCorrection;    //파워게이지 보정
    private float m_fBonus;         //보너스
    private int m_iWin;
    private int m_iDraw;
    private int m_iLose;
    private int m_iTotal_Record;
    private Dictionary<Sc_Engine.Building_Kind, Have_Building> m_dicHave_Building;
    

    public static Sc_Player_Info GetInstance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(Sc_Player_Info)) as Sc_Player_Info;
                if(m_Instance == null)
                {
                    GameObject container = new GameObject();
                    container.name = string.Format("Sc_Player_Info_Container");
                    m_Instance = container.AddComponent(typeof(Sc_Player_Info)) as Sc_Player_Info;
                }
            }
            return m_Instance;
        }
    }

    private void Awake()
    {
        m_strName = string.Format("Temp_Name");
        m_iRank = 0;
        m_iGold = 0;
        m_fAttack_Speed = 1;
        m_fGauge_Speed = 1;
        m_fRange = 1;
        m_fCorrection = 0;
        m_fBonus = 0;
        m_iWin = 0;
        m_iDraw = 0;
        m_iLose = 0;
        m_iTotal_Record = 0;

        m_dicHave_Building = new Dictionary<Sc_Engine.Building_Kind, Have_Building>();

        AddBuilding(Sc_Engine.Building_Kind.eAttack_Speed, 3);
        AddBuilding(Sc_Engine.Building_Kind.eRange, 4);
        AddBuilding(Sc_Engine.Building_Kind.eGauge_Speed, 2);
    }

    private void AddBuilding(Sc_Engine.Building_Kind _kind, int _num)
    {
        if(m_dicHave_Building.ContainsKey(_kind))
        {
            Add_ExistBuilding(_kind, _num);
        }
        else
        {
            Add_NewBuilding(_kind, _num);
        }
    }

    private void Add_NewBuilding(Sc_Engine.Building_Kind _kind, int _num)
    {
        Have_Building have;
        have.Building = Sc_BuildingManager.getInstance.GetBuilding()[_kind];
        have.Number_Of_Building = _num;

        m_dicHave_Building.Add(_kind, have);
    }
    private void Add_ExistBuilding(Sc_Engine.Building_Kind _kind,int num)
    {
        Have_Building have;
        have.Building = m_dicHave_Building[Sc_Engine.Building_Kind.eAttack_Speed].Building;
        have.Number_Of_Building = m_dicHave_Building[Sc_Engine.Building_Kind.eAttack_Speed].Number_Of_Building + num;

        m_dicHave_Building[Sc_Engine.Building_Kind.eAttack_Speed] = have;
    }

    public Dictionary<Sc_Engine.Building_Kind, Have_Building> GetHave_Building()
    {
        return m_dicHave_Building;
    }
}
