using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Player_Info : MonoBehaviour {
    private static Sc_Player_Info m_Instance;

    public struct Have_Building
    {
        public Sc_Building Building;
        public int Total_Building_Count;
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
    private Dictionary<int, Have_Building> m_dicHave_Building;
    private List<Sc_Deck> m_listHave_Deck;
    private Sc_Deck m_cCurrentDeck;

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

    public void Init()
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

        m_dicHave_Building = new Dictionary<int, Have_Building>();
        m_listHave_Deck = new List<Sc_Deck>();

        AddBuilding(0,Sc_Engine.Building_Kind.eAttack_Speed, 6);
        AddBuilding(1,Sc_Engine.Building_Kind.eRange, 8);
        AddBuilding(2,Sc_Engine.Building_Kind.eGauge_Speed, 3);
        AddBuilding(3, Sc_Engine.Building_Kind.eSkill1, 1);

        for (int i = 0; i < 4; i++)
        {
            Sc_Deck deck = new Sc_Deck();
            AddDeck(deck);
            GetHave_Deck()[i].SetDeck_Name(string.Format("기본덱_{0}", i));
        }
        m_cCurrentDeck = GetHave_Deck()[0];
    }

    private void AddBuilding(int _index, Sc_Engine.Building_Kind _kind, int _num)
    {
        if(m_dicHave_Building.ContainsKey(_index))
        {
            Add_ExistBuilding(_index,_kind, _num);
        }
        else
        {
            Add_NewBuilding(_index, _kind, _num);
        }
    }

    private void Add_NewBuilding(int _index,Sc_Engine.Building_Kind _kind, int _num)
    {
        Have_Building have;
        have.Building = Sc_BuildingManager.GetInstance.GetBuilding()[_kind];
        have.Total_Building_Count = _num;

        m_dicHave_Building.Add(_index, have);
    }
    private void Add_ExistBuilding(int _index,Sc_Engine.Building_Kind _kind,int num)
    {
        Have_Building have;
        have.Building = m_dicHave_Building[_index].Building;
        have.Total_Building_Count = m_dicHave_Building[_index].Total_Building_Count + num;

        m_dicHave_Building[_index] = have;
    }
    private void AddDeck(Sc_Deck _deck)
    {
        m_listHave_Deck.Add(_deck);
    }

    public Dictionary<int, Have_Building> GetHave_Building()
    {
        return m_dicHave_Building;
    }
    public List<Sc_Deck> GetHave_Deck()
    {
        return m_listHave_Deck;
    }
    public Sc_Deck GetCurrentDeck()
    {
        return m_cCurrentDeck;
    }
    public void SetCurrentDeck(int _index)
    {
        m_cCurrentDeck = m_listHave_Deck[_index];
    }
}
