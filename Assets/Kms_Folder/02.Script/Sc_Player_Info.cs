using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Player_Info : MonoBehaviour {
    private static Sc_Player_Info m_Instance;

    private string m_strName;
    private int m_iRank;
    private int m_iGold;
    private float m_fAttack_Speed;  //장전속도
    private float m_fGauge_Speed;   //파워게이지 속도
    private float m_fCorrection;    //파워게이지 보정
    private float m_fBullet_Speed;  //포탄 속도
    private int m_iWin;
    private int m_iDraw;
    private int m_iLose;
    private int m_iTotal_Record;
    private Dictionary<Sc_Engine.Building_Kind,List<Sc_Building>> m_dicHave_Building;
    private List<Sc_Deck> m_listHave_Deck;
    private int m_iCurrent_Deck_Index;
    private Transform m_trInformation_Board;

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
        m_fCorrection = 0;
        m_fBullet_Speed = 0;
        m_iWin = 0;
        m_iDraw = 0;
        m_iLose = 0;
        m_iTotal_Record = 0;
        m_iCurrent_Deck_Index = 0;
        m_dicHave_Building = new Dictionary<Sc_Engine.Building_Kind, List<Sc_Building>>();
        m_listHave_Deck = new List<Sc_Deck>();
        m_trInformation_Board = GameObject.Find("UI Root").transform.GetChild(1).GetChild(6);


        AddBuilding(0, Sc_Engine.Building_Kind.eAttack_Speed, 6);
        AddBuilding(1, Sc_Engine.Building_Kind.eCorrection, 8);
        AddBuilding(2, Sc_Engine.Building_Kind.eGauge_Speed, 3);
        AddBuilding(3, Sc_Engine.Building_Kind.eSkill1, 1);
        AddBuilding(4, Sc_Engine.Building_Kind.eBullet_Speed, 5);
        AddBuilding(0, Sc_Engine.Building_Kind.eAttack_Speed, 2);
        AddBuilding(5, Sc_Engine.Building_Kind.eHeadquarters, 1);

        for (int i = 0; i < 4; i++)
        {
            Sc_Deck deck = new Sc_Deck();
            AddDeck(deck);
            GetHave_Deck()[i].SetDeck_Name(string.Format("기본덱_{0}", i));
            for (int j = 0; j < 6; j++)
            {
                Sc_Block block = new Sc_Block();
                block.SetBlock_Kind((Sc_Engine.Building_Kind)i);
                GetHave_Deck()[i].GetBlock_List().Add(block);
            }
        }
        Information_Board_Init();
    }

    private void AddBuilding(int _index,Sc_Engine.Building_Kind _kind, int _num)
    {
        if(m_dicHave_Building.ContainsKey(_kind))
        {
            Exist_AddBuilding(_kind, _num);
        }
        else
        {
            New_AddBuilding(_kind, _num);
        }
        
    }
    private void New_AddBuilding(Sc_Engine.Building_Kind _kind,int _num)
    {
        List<Sc_Building> listBuilding = new List<Sc_Building>();
        for (int i = 0; i < _num; i++)
        {
            Sc_Building building = (Sc_Building)Sc_BuildingManager.GetInstance.GetBuilding()[_kind].Clone();
            listBuilding.Add(building);
        }
        m_dicHave_Building.Add(_kind,listBuilding);
    }
    private void Exist_AddBuilding(Sc_Engine.Building_Kind _kind, int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            Sc_Building building = (Sc_Building)Sc_BuildingManager.GetInstance.GetBuilding()[_kind].Clone();
            m_dicHave_Building[_kind].Add(building);
        }
    }

    public void Information_Board_Init()
    {
        int Building_Sum = 0;
        for(int i=0;i<m_dicHave_Building.Count;i++)
        {
            Building_Sum += m_dicHave_Building[(Sc_Engine.Building_Kind)i].Count;
        }
        m_trInformation_Board.Find("Block_Value").GetComponent<UILabel>().text = Building_Sum.ToString();
        m_trInformation_Board.Find("Gold_Value").GetComponent<UILabel>().text = m_iGold.ToString();
    }

    private void AddDeck(Sc_Deck _deck)
    {
        m_listHave_Deck.Add(_deck);
    }

    public Dictionary<Sc_Engine.Building_Kind,List<Sc_Building>> GetHave_Building()
    {
        return m_dicHave_Building;
    }
    public List<Sc_Deck> GetHave_Deck()
    {
        return m_listHave_Deck;
    }
    public int GetCurrentDeck_Index()
    {
        return m_iCurrent_Deck_Index;
    }
    public void SetCurrentDeck_Index(int _index)
    {
        m_iCurrent_Deck_Index = _index;
    }
}
