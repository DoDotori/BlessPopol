using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BlockEdit : MonoBehaviour {
    public enum Block_Grid_State { eEnable, eBan, eUse}
    public enum Sprite_Color { eOrigin, eSelect, eEnabel, eBan, eUse }
    public struct Block_Grid
    {
        public UISprite Grid_Sprite;
        public Block_Grid_State eBlock_Grid_State;
    }

    private readonly int m_readiGrid_Size = 5;
    private readonly int m_readiMax_Build_Count = 5;
    private readonly int m_readiGrid_Name_Position_CoordinateY = 16;
    private readonly int m_readiGrid_Name_Position_CoordinateX = 17;
    private readonly int m_readiBuilding_Name_Position_Index = 14;
    private readonly int m_readiBlock_Name_Position_Index = 12;

    private UIAtlas m_ObjectAtlas;
    private UIAtlas m_ScreenAtlas;

    //블럭 관련
    private int m_iCurrent_Block_Index;
    private int m_iPrev_Block_Index;
    private GameObject m_objSelect_Block_Mark;

    //건물 관련
    private Transform m_trBuilding_List_Parent;
    private Transform[] m_trarrBuilding_Count_List;
    private Dictionary<Sc_Engine.Building_Kind, List<Sc_Building>> m_dicHave_Building;
    private Dictionary<Sc_Engine.Building_Kind, List<Sc_Building>> m_dicUse_Building;
    private int m_iSelect_Building_Index;

    //그리드 관련
    private Block_Grid[,] m_starrGrid;
    private Transform m_trGrid_Parent;

    //기타
    private Transform m_trMaxCount;
    private int m_iEnable_Build_Count;
    private Color[] m_Color;
    private bool m_isSelect_Building;
    private GameObject m_objSave_PopUp;
    private bool m_isSave;

    public void Init()
    {
        m_ObjectAtlas = Resources.Load<UIAtlas>("Atlas/Object_Ui");
        m_ScreenAtlas = Resources.Load<UIAtlas>("Atlas/Screen_Ui");

        //블럭 관련
        m_iCurrent_Block_Index = 0;
        m_iPrev_Block_Index = 0;
        m_objSelect_Block_Mark = this.transform.GetChild(4).GetChild(1).GetChild(0).GetChild(1).gameObject;

        Transform Block_List = this.transform.GetChild(4).GetChild(1);
        for(int i=0;i<6;i++)
        {
            Transform blockName = Block_List.GetChild(i).GetChild(0);
            Sc_Engine.GetInstance.Change_Label(blockName, string.Format("{0} 블럭", Sc_BuildingManager.GetInstance.GetBuilding()[(Sc_Engine.Building_Kind)i].GetName));
        }

        //건물 관련
        m_trBuilding_List_Parent = this.transform.GetChild(0).GetChild(0).GetChild(0);
        m_dicHave_Building = new Dictionary<Sc_Engine.Building_Kind, List<Sc_Building>>();
        m_dicUse_Building = new Dictionary<Sc_Engine.Building_Kind, List<Sc_Building>>();
        m_trarrBuilding_Count_List = new Transform[Sc_Player_Info.GetInstance.GetHave_Building().Count];
        m_iSelect_Building_Index = -1;

        for(int i=0;i<Sc_Player_Info.GetInstance.GetHave_Building().Count;i++)
        {
            List<Sc_Building> HaveBuilding_List = new List<Sc_Building>();
            List<Sc_Building> UseBuilding_List = new List<Sc_Building>();
            for (int j=0;j<Sc_Player_Info.GetInstance.GetHave_Building()[(Sc_Engine.Building_Kind)i].Count;j++)
            {
                Sc_Building HaveBuilding = (Sc_Building)Sc_Player_Info.GetInstance.GetHave_Building()[(Sc_Engine.Building_Kind)i][j].Clone();
                HaveBuilding_List.Add(HaveBuilding);
                

                Sc_Building UseBuilding = (Sc_Building)Sc_Player_Info.GetInstance.GetHave_Building()[(Sc_Engine.Building_Kind)i][j].Clone();
                UseBuilding_List.Add(UseBuilding);
                
            }
            m_dicHave_Building.Add(HaveBuilding_List[0].Building_Kind, HaveBuilding_List);
            m_dicUse_Building.Add(UseBuilding_List[0].Building_Kind, UseBuilding_List);

            m_dicUse_Building[(Sc_Engine.Building_Kind)i].Clear();

            m_trarrBuilding_Count_List[i] = m_trBuilding_List_Parent.GetChild(i).GetChild(0).GetChild(3);
            m_trBuilding_List_Parent.GetChild(i).gameObject.SetActive(true);
            //건물 이미지
            Sc_Engine.GetInstance.Change_Sprite(m_ObjectAtlas, m_trBuilding_List_Parent.GetChild(i).GetChild(0).GetChild(0).GetChild(0), m_dicHave_Building[(Sc_Engine.Building_Kind)i][0].GetSpriteName);
            //건물 이름
            Sc_Engine.GetInstance.Change_Label(m_trBuilding_List_Parent.GetChild(i).GetChild(0).GetChild(1), string.Format("{0}",m_dicHave_Building[(Sc_Engine.Building_Kind)i][0].GetName));
            //건물 설명
            Sc_Engine.GetInstance.Change_Label(m_trBuilding_List_Parent.GetChild(i).GetChild(0).GetChild(2), string.Format("{0}", m_dicHave_Building[(Sc_Engine.Building_Kind)i][0].GetAbility));
            //건물 개수
            Sc_Engine.GetInstance.Change_Label(m_trarrBuilding_Count_List[i], string.Format("x{0}", m_dicHave_Building[(Sc_Engine.Building_Kind)i].Count));
        }

        //그리드 관련
        m_starrGrid = new Block_Grid[m_readiGrid_Size, m_readiGrid_Size];
        m_trGrid_Parent = this.transform.Find("Block_Edit_Grid");
        
        int z = 0;
        for(int i =0;i<m_readiGrid_Size;i++)
        {
            for(int j=0;j<m_readiGrid_Size;j++)
            {
                m_starrGrid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
                m_starrGrid[i, j].Grid_Sprite = m_trGrid_Parent.GetChild(z).GetComponent<UISprite>();
                z++;
            }
        }

        //기타
        m_trMaxCount = this.transform.GetChild(5);
        m_iEnable_Build_Count = m_readiMax_Build_Count;
        m_Color = new Color[5];
        m_isSelect_Building = true;
        m_isSave = false;
        m_objSave_PopUp = this.transform.GetChild(this.transform.childCount - 1).gameObject;

        Sc_Engine.GetInstance.Change_Label(m_trMaxCount, string.Format("Max Count : {0}", m_iEnable_Build_Count));
        m_Color[(int)Sprite_Color.eOrigin] = new Color(0.8f, 0.8f, 1.0f);
        m_Color[(int)Sprite_Color.eSelect] = new Color(0.0f, 1.0f, 0.0f);
        m_Color[(int)Sprite_Color.eEnabel] = new Color(0.0f, 1.0f, 0.0f);
        m_Color[(int)Sprite_Color.eBan] = new Color(1.0f, 0.0f, 0.0f);
        m_Color[(int)Sprite_Color.eUse] = new Color(1.0f, 1.0f, 1.0f);
    }

    //블럭 관련
    public void Press_Block(GameObject _object)
    {
        m_iPrev_Block_Index = m_iCurrent_Block_Index;
        m_iCurrent_Block_Index = int.Parse(_object.name[m_readiBlock_Name_Position_Index].ToString());

        if(m_iCurrent_Block_Index != m_iPrev_Block_Index)
        {
            m_objSelect_Block_Mark.transform.parent = _object.transform;
            m_objSelect_Block_Mark.transform.localPosition = Vector3.zero;

            if (!m_isSave)
            {
                m_dicUse_Building[(Sc_Engine.Building_Kind)m_iPrev_Block_Index].Clear();
            }

            if(Sc_Player_Info.GetInstance.GetHave_Deck()[Sc_Player_Info.GetInstance.GetCurrentDeck_Index()].GetBlock_List()[m_iCurrent_Block_Index].GetActive())
            {
                Load_Grid();
            }
            else
            {
                Grid_Init();
            }
            Refresh_Label();
        }

    }   

    //건물 관련
    public void Press_Building(GameObject _object)
    {
        if(m_isSelect_Building)
        {
            m_iSelect_Building_Index = int.Parse(_object.name[m_readiBuilding_Name_Position_Index].ToString());

            Change_Building_Color();
            Grid_Check();
        }
    }

    private void Change_Building_Color()
    {
        for (int i = 0; i < m_dicHave_Building.Count; i++)
        {
            if (i == m_iSelect_Building_Index)
            {
                Sc_Engine.GetInstance.Change_Color(m_trBuilding_List_Parent.GetChild(i).GetComponent<UISprite>(), m_Color[(int)Sprite_Color.eSelect]);
            }
            else
            {
                Sc_Engine.GetInstance.Change_Color(m_trBuilding_List_Parent.GetChild(i).GetComponent<UISprite>(), m_Color[(int)Sprite_Color.eOrigin]);
            }
        }
    }

    //그리드 관련
    private void Grid_Init()
    {
        for(int i=0;i<m_readiGrid_Size;i++)
        {
            for(int j=0;j<m_readiGrid_Size;j++)
            {
                m_starrGrid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
            }
        }
        Grid_Check();
    }
    private void Load_Grid()
    {
        for (int i = 0; i < m_readiGrid_Size; i++)
        {
            for (int j = 0; j < m_readiGrid_Size; j++)
            {
                m_starrGrid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
            }
        }

        List<Sc_Building> CurrentBlock_Building_List = new List<Sc_Building>(Sc_Player_Info.GetInstance.GetHave_Deck()[Sc_Player_Info.GetInstance.GetCurrentDeck_Index()].GetBlock_List()[m_iCurrent_Block_Index].GetBuilding_List());

        for (int i=0;i< CurrentBlock_Building_List.Count;i++)
        {
            m_starrGrid[CurrentBlock_Building_List[i].GetCoordinate.y, CurrentBlock_Building_List[i].GetCoordinate.x].eBlock_Grid_State = Block_Grid_State.eUse;
        }
        Grid_Renderer();
    }

    public void Press_Grid(GameObject _object)
    {
        int x = int.Parse(_object.name[m_readiGrid_Name_Position_CoordinateX].ToString());
        int y = int.Parse(_object.name[m_readiGrid_Name_Position_CoordinateY].ToString());

        int have_Building_Count = m_dicHave_Building[(Sc_Engine.Building_Kind)m_iSelect_Building_Index].Count;
        int use_Building_Count = m_dicUse_Building[(Sc_Engine.Building_Kind)m_iSelect_Building_Index].Count;

        if (m_iSelect_Building_Index >= 0 && m_iSelect_Building_Index < m_dicHave_Building.Count)
        {
            if(m_starrGrid[y,x].eBlock_Grid_State== Block_Grid_State.eEnable)
            {
                if(m_iEnable_Build_Count > 0)
                {
                    if(have_Building_Count - use_Building_Count >0)
                    {
                        if((Sc_Engine.Building_Kind)m_iCurrent_Block_Index == m_dicHave_Building[(Sc_Engine.Building_Kind)m_iSelect_Building_Index][use_Building_Count].Building_Kind)
                        {
                            m_starrGrid[y, x].eBlock_Grid_State = Block_Grid_State.eUse;
                            m_dicHave_Building[(Sc_Engine.Building_Kind)m_iSelect_Building_Index][use_Building_Count].SetCoordinate(x, y);
                            Sc_Building building = (Sc_Building)m_dicHave_Building[(Sc_Engine.Building_Kind)m_iSelect_Building_Index][use_Building_Count].Clone();
                            m_dicUse_Building[(Sc_Engine.Building_Kind)m_iSelect_Building_Index].Add(building);
                            m_isSelect_Building = false;
                            m_isSave = false;

                            Refresh_Label();
                            Grid_Check();
                        }
                    }
                }
            }
        }

        
    }

    private void Grid_Check()
    {
        int z = 0;
        for(int i=0;i<m_readiGrid_Size;i++)
        {
            for(int j=0;j<m_readiGrid_Size;j++)
            {
                if(m_starrGrid[i,j].eBlock_Grid_State == Block_Grid_State.eBan)
                {
                    z++;
                }
                if(m_starrGrid[i,j].eBlock_Grid_State == Block_Grid_State.eUse)
                { 
                     Grid_UDLR_Check(j, i);
                }
            }
        }

        if(z == m_starrGrid.Length)
        {
            m_starrGrid[2, 2].eBlock_Grid_State = Block_Grid_State.eEnable;
            m_isSelect_Building = true;
        }

        Grid_Renderer();
    }
    
    private void Grid_UDLR_Check(int _x, int _y)
    {
        
        //UP
        if (_y - 1 >= 0 && m_starrGrid[_y - 1, _x].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_starrGrid[_y - 1, _x].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //DOWN
        if (_y + 1 < m_readiGrid_Size && m_starrGrid[_y + 1, _x].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_starrGrid[_y + 1, _x].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //LEFT
        if (_x - 1 >= 0 && m_starrGrid[_y, _x - 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_starrGrid[_y, _x - 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //RIGHT
        if (_x + 1 < m_readiGrid_Size && m_starrGrid[_y, _x + 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_starrGrid[_y, _x + 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
    }

    private void Grid_Renderer()
    {
        int z = 0;
        for (int i = 0; i < m_readiGrid_Size; i++)
        {
            for (int j = 0; j < m_readiGrid_Size; j++)
            {
                if (m_starrGrid[i, j].eBlock_Grid_State == Block_Grid_State.eEnable)
                {
                    Sc_Engine.GetInstance.Change_Sprite(m_starrGrid[i, j].Grid_Sprite, m_ScreenAtlas, m_Color[(int)Sprite_Color.eEnabel], "BackGround");
                }
                else if (m_starrGrid[i, j].eBlock_Grid_State == Block_Grid_State.eUse)
                {
                    Sc_Engine.GetInstance.Change_Sprite(m_starrGrid[i, j].Grid_Sprite, m_ObjectAtlas, m_Color[(int)Sprite_Color.eUse],
                        m_dicUse_Building[(Sc_Engine.Building_Kind)m_iCurrent_Block_Index][0].GetSpriteName);
                    z++;  
                }
                else if (m_starrGrid[i, j].eBlock_Grid_State == Block_Grid_State.eBan)
                {
                    Sc_Engine.GetInstance.Change_Sprite(m_starrGrid[i, j].Grid_Sprite, m_ScreenAtlas, m_Color[(int)Sprite_Color.eBan], "BackGround");
                }
            }
        }

        if(z == m_iEnable_Build_Count)
        {
            for(int i=0;i<m_readiGrid_Size;i++)
            {
                for(int j=0;j<m_readiGrid_Size;j++)
                {
                    if (m_starrGrid[i, j].eBlock_Grid_State == Block_Grid_State.eEnable)
                    {
                        m_starrGrid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
                        Sc_Engine.GetInstance.Change_Sprite(m_starrGrid[i, j].Grid_Sprite, m_ScreenAtlas, m_Color[(int)Sprite_Color.eBan], "BackGround");
                    }
                }
            }
        }
    }

    //기타
    public void Button_Save()
    {
        if(m_dicUse_Building[(Sc_Engine.Building_Kind)m_iCurrent_Block_Index].Count != 0)
        {
            for (int i = 0; i < m_dicUse_Building[(Sc_Engine.Building_Kind)m_iCurrent_Block_Index].Count; i++)
            {
                Sc_Player_Info.GetInstance.GetHave_Deck()[Sc_Player_Info.GetInstance.GetCurrentDeck_Index()].GetBlock_List()[m_iCurrent_Block_Index].GetBuilding_List().Add(m_dicUse_Building[(Sc_Engine.Building_Kind)m_iCurrent_Block_Index][i]);
            }
            Sc_Player_Info.GetInstance.GetHave_Deck()[Sc_Player_Info.GetInstance.GetCurrentDeck_Index()].GetBlock_List()[m_iCurrent_Block_Index].SetActive(true);
            m_isSave = true;
        }
        else
        {
            Sc_Player_Info.GetInstance.GetHave_Deck()[Sc_Player_Info.GetInstance.GetCurrentDeck_Index()].GetBlock_List()[m_iCurrent_Block_Index].GetBuilding_List().Clear();
        }

        StartCoroutine(Save_PopUp());
    }

    IEnumerator Save_PopUp()
    {
        m_objSave_PopUp.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        m_objSave_PopUp.SetActive(false);
    }

    public void Button_Initialization()
    {
        m_dicUse_Building[(Sc_Engine.Building_Kind)m_iCurrent_Block_Index].Clear();

        for(int i=0;i<m_readiGrid_Size;i++)
        {
            for(int j=0;j<m_readiGrid_Size;j++)
            {
                m_starrGrid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
            }
        }
        Grid_Check();
        Refresh_Label();
    }

    private void Refresh_Label()
    {
        for (int i = 0; i < m_dicHave_Building.Count; i++)
        {
            Sc_Engine.GetInstance.Change_Label(m_trarrBuilding_Count_List[i], string.Format("x{0}", m_dicHave_Building[(Sc_Engine.Building_Kind)i].Count - m_dicUse_Building[(Sc_Engine.Building_Kind)i].Count));
        }
        
            Sc_Engine.GetInstance.Change_Label(m_trMaxCount, string.Format("Max Count : {0}", m_iEnable_Build_Count - m_dicUse_Building[(Sc_Engine.Building_Kind)m_iCurrent_Block_Index].Count));
    }
}
