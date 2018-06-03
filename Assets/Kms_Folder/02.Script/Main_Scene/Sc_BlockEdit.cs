using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BlockEdit : MonoBehaviour {
    public enum Block_Grid_State { eEnable, eBan, eUse }
    public struct Block_Grid
    {
        public UISprite Sprite;
        public Block_Grid_State eBlock_Grid_State;
    }

    //건물리스트
    private UIAtlas m_ObjectAtlas;
    private UIAtlas m_ScreenAtlas;
    private Transform m_trList_Grid;                            //건물 리스트 부모
    private List<Sc_Building> m_listBuilding;                   //건물 리스트
    private List<UISprite> m_listButton_Background;             //건물 리스트 배경
    //블럭그리드
    private Block_Grid[,] m_stBlock_Grid;                       //배치 그리드
    private List<Sc_Building> m_listUse_Building;               //배치된 건물
    private Transform m_trBlock_Grid;                           //배치 그리드 부모
    private int m_iGrid_Size;                                   //그리드 크기
    //기타
    private UILabel m_laMaxCount;                               //블럭에 설정가능한 최대 건물개수 표시
    private List<int> m_listBuilding_Count;                     //각 건물 개수
    private List<int> m_listPossible_Build_Count;               //각 블럭당 설치가능 개수
    private int m_iSelect_Block;                                //선택된 블럭
    private int m_iSelect_Building_Count;                       //선택된 건물의 개수
    private int m_iCurrent_Select_Building;                     //현재 선택된 건물
    private int m_iPrev_Select_Building;                        //이전에 선택된 건물
    private readonly int m_iPossible_Max_Bulid_Count = 5;       //최대 설치가능한 건물개수
    private Color m_Select_Color;                               //선택시 색
    private Color m_Origin_Color;                               //원래 색
    private Color m_Ban_Color;                                  //설치금지 색
    private Color m_Use_Color;                                  //설치된 색

    public void Init() {
        m_ObjectAtlas = Resources.Load<UIAtlas>("Atlas/Object_Ui");
        m_ScreenAtlas = Resources.Load<UIAtlas>("Atlas/Screen_Ui");
        m_trList_Grid = this.transform.GetChild(0).GetChild(0);
        m_trBlock_Grid = this.transform.GetChild(1);
        m_laMaxCount = GameObject.Find("Building_MaxCount").GetComponent<UILabel>();
        m_listBuilding = new List<Sc_Building>();
        m_listButton_Background = new List<UISprite>();
        m_listBuilding_Count = new List<int>();
        m_listPossible_Build_Count = new List<int>();
        m_listUse_Building = new List<Sc_Building>();
        m_iCurrent_Select_Building = 0;
        m_iPrev_Select_Building = 0;
        m_iSelect_Block = 0;
        m_iGrid_Size = 5;
        m_Select_Color = new Color(0.0f, 1.0f, 0.0f);
        m_Origin_Color = new Color(0.8f, 0.8f, 1.0f);
        m_Ban_Color = new Color(1.0f, 0.0f, 0.0f);
        m_Use_Color = new Color(1.0f, 1.0f, 1.0f);
        m_stBlock_Grid = new Block_Grid[m_iGrid_Size, m_iGrid_Size];


        for (int i = 0; i < m_trList_Grid.childCount; i++)
        {
            m_listButton_Background.Add(m_trList_Grid.GetChild(i).GetComponent<UISprite>());
        }

        for (int i = 0; i < 6; i++)
        {
            m_listPossible_Build_Count.Add(m_iPossible_Max_Bulid_Count);
        }

        int z = 0;
        for (int i = 0; i < m_iGrid_Size; i++)
        {
            for (int j = 0; j < m_iGrid_Size; j++)
            {
                m_stBlock_Grid[i, j].Sprite = m_trBlock_Grid.GetChild(z).GetComponent<UISprite>();
                m_stBlock_Grid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
                z++;
            }
        }
        m_laMaxCount.text = string.Format("{0}/{1}", m_listPossible_Build_Count[m_iSelect_Block], m_iPossible_Max_Bulid_Count); 
        BuildingList_Init();
        Change_Background_Color();
        Grid_Temp();
    }

    public void BuildingList_Init()
    {
        for (int i = 0; i < Sc_Player_Info.GetInstance.GetHave_Building().Count; i++)
        {
            Sc_Building building;
            building = Sc_Player_Info.GetInstance.GetHave_Building()[i].Building;
            m_listBuilding.Add(building);

            Transform tr = m_trList_Grid.GetChild(i);
            tr.gameObject.SetActive(true);

            m_listBuilding_Count.Add(Sc_Player_Info.GetInstance.GetHave_Building()[i].Total_Building_Count);

            Change_Sprite(tr, m_listBuilding[i].GetSpriteName);
            Change_Name(tr, m_listBuilding[i].GetName);
            Change_AbilityText(tr, m_listBuilding[i].GetAbility);
            Change_Number(tr, m_listBuilding_Count[i]);
        }

        m_iSelect_Building_Count = m_listBuilding_Count[0];
    }
    //블럭그리드
    private void Grid_Temp()
    {
        int z = 0;
        for (int i = 0; i < m_iGrid_Size; i++)
        {
            for (int j = 0; j < m_iGrid_Size; j++)
            {
                if (m_stBlock_Grid[i, j].eBlock_Grid_State == Block_Grid_State.eBan)
                {
                    z++;
                }

                if (m_stBlock_Grid[i, j].eBlock_Grid_State == Block_Grid_State.eUse)
                {
                    UDLR_Check(i, j);
                }
            }
        }

        if (z == m_stBlock_Grid.Length)
        {
            m_stBlock_Grid[2, 2].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        Grid_Enable_Render();
    }

    private void UDLR_Check(int _i, int _j)
    {
        //Up
        if (_i - 1 >= 0 && m_stBlock_Grid[_i - 1, _j].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i - 1, _j].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //Up_Left
        if ((_i - 1 >= 0 && _j - 1 >= 0) && m_stBlock_Grid[_i - 1, _j - 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i - 1, _j - 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //Up_Right
        if ((_i - 1 >= 0 && _j + 1 < m_iGrid_Size) && m_stBlock_Grid[_i - 1, _j + 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i - 1, _j + 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //Down
        if (_i + 1 < m_iGrid_Size && m_stBlock_Grid[_i + 1, _j].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i + 1, _j].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //Down_Left
        if ((_i + 1 < m_iGrid_Size && _j - 1 >= 0) && m_stBlock_Grid[_i + 1, _j - 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i + 1, _j - 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //Down_Right
        if ((_i + 1 < m_iGrid_Size && _j + 1 < m_iGrid_Size) && m_stBlock_Grid[_i + 1, _j + 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i + 1, _j + 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //Left
        if (_j - 1 >= 0 && m_stBlock_Grid[_i, _j - 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i, _j - 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
        //Right
        if (_j + 1 < m_iGrid_Size && m_stBlock_Grid[_i, _j + 1].eBlock_Grid_State != Block_Grid_State.eUse)
        {
            m_stBlock_Grid[_i, _j + 1].eBlock_Grid_State = Block_Grid_State.eEnable;
        }
    }

    private void Grid_Enable_Render()
    {
        for (int i = 0; i < m_iGrid_Size; i++)
        {
            for (int j = 0; j < m_iGrid_Size; j++)
            {
                if (m_stBlock_Grid[i, j].eBlock_Grid_State == Block_Grid_State.eEnable)
                {
                    m_stBlock_Grid[i, j].Sprite.atlas = m_ScreenAtlas;
                    m_stBlock_Grid[i, j].Sprite.spriteName = "BackGround";
                    m_stBlock_Grid[i, j].Sprite.color = m_Select_Color;
                }
                else if (m_stBlock_Grid[i, j].eBlock_Grid_State == Block_Grid_State.eUse)
                {
                    if (m_iCurrent_Select_Building == m_iPrev_Select_Building)
                    {
                        m_stBlock_Grid[i, j].Sprite.color = m_Use_Color;
                        m_stBlock_Grid[i, j].Sprite.atlas = m_ObjectAtlas;
                        m_stBlock_Grid[i, j].Sprite.spriteName = m_listBuilding[m_iCurrent_Select_Building].GetSpriteName;
                        m_iPrev_Select_Building = m_iCurrent_Select_Building;
                    }
                }
                else if(m_stBlock_Grid[i,j].eBlock_Grid_State == Block_Grid_State.eBan)
                {
                    m_stBlock_Grid[i, j].Sprite.atlas = m_ScreenAtlas;
                    m_stBlock_Grid[i, j].Sprite.spriteName = "BackGround";
                    m_stBlock_Grid[i, j].Sprite.color = m_Ban_Color;
                }
            }
        }
    }

    private void Change_Building()
    {
        for(int i=0;i< m_iGrid_Size; i++)
        {
            for(int j=0;j< m_iGrid_Size; j++)
            {
                if (m_listPossible_Build_Count[m_iSelect_Block] < m_iPossible_Max_Bulid_Count)
                {
                    if (m_iCurrent_Select_Building != m_iPrev_Select_Building)
                    {
                        if (m_stBlock_Grid[i, j].eBlock_Grid_State == Block_Grid_State.eEnable)
                        {
                            m_stBlock_Grid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
                        }
                    }
                }
            }
        }

        if (m_listPossible_Build_Count[m_iSelect_Block] == m_iPossible_Max_Bulid_Count)
        {
            m_iPrev_Select_Building = m_iCurrent_Select_Building;
            m_iSelect_Building_Count = m_listBuilding_Count[m_iCurrent_Select_Building];
        }

        if (m_iCurrent_Select_Building == m_iPrev_Select_Building)
        {
            Grid_Temp();
        }
        Grid_Enable_Render();
    }

    public void Grid_Press(GameObject _obj)
    {
        int i = int.Parse(_obj.name[16].ToString());
        int j = int.Parse(_obj.name[17].ToString());

        if (m_stBlock_Grid[i, j].eBlock_Grid_State == Block_Grid_State.eEnable)
        {
            if (m_listPossible_Build_Count[m_iSelect_Block] > 0)
            {
                if (m_iSelect_Building_Count > 0)
                {
                    m_stBlock_Grid[i, j].eBlock_Grid_State = Block_Grid_State.eUse;
                    Grid_Temp();
                    m_listBuilding[m_iCurrent_Select_Building].Coordinate.SetPoint(j, i);
                    m_listUse_Building.Add(m_listBuilding[m_iCurrent_Select_Building]);
                    m_listPossible_Build_Count[m_iSelect_Block]--;
                    m_iSelect_Building_Count--;
                }
            }
            Change_Number(m_trList_Grid.GetChild(m_iCurrent_Select_Building), m_iSelect_Building_Count);
            m_laMaxCount.text = string.Format("{0}/{1}", m_listPossible_Build_Count[m_iSelect_Block], m_iPossible_Max_Bulid_Count);
        }
        
    }
    public void Grid_Initialization()
    {
        for(int i=0;i< m_iGrid_Size; i++)
        {
            for(int j=0;j< m_iGrid_Size; j++)
            {
                m_stBlock_Grid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
            }
        }
       /* if (Sc_Player_Info.GetInstance.GetCurrentDeck().GetBlock_List().Contains(m_cCurrentBlock))
        {
            Sc_Player_Info.GetInstance.GetCurrentDeck().GetBlock_List().Remove(m_cCurrentBlock);
        }*/
        m_listUse_Building.Clear();
        m_listPossible_Build_Count[m_iSelect_Block] = m_iPossible_Max_Bulid_Count;
        m_iSelect_Building_Count = m_listBuilding_Count[m_iCurrent_Select_Building];
        m_iPrev_Select_Building = m_iCurrent_Select_Building;
        m_laMaxCount.text = string.Format("{0}/{1}", m_listPossible_Build_Count[m_iSelect_Block], m_iPossible_Max_Bulid_Count);
        
        
        for (int i = 0; i < m_listBuilding.Count; i++)
        {
            Change_Number(m_trList_Grid.GetChild(i), m_listBuilding_Count[i]);
        }
        Grid_Temp();
    }

    public void Grid_Save()
    {
        Sc_Deck currentDeck = Sc_Player_Info.GetInstance.GetCurrentDeck();
        Sc_Block block = new Sc_Block();
        if (!currentDeck.GetBlock_List().Contains(block))
        {
            currentDeck.GetBlock_List().Add(block);
        }
        else
        {
            currentDeck.GetBlock_List()[m_iSelect_Block] = block;
        }
        int z = 0;
       for(int i=0;i< m_iGrid_Size;i++)
        {
            for(int j=0;j< m_iGrid_Size;j++)
            {
                 if (m_stBlock_Grid[i,j].eBlock_Grid_State == Block_Grid_State.eUse)
                {
                    currentDeck.GetBlock_List()[m_iSelect_Block].GetBuilding_List().Add(m_listUse_Building[z]);
                    z++;
                }
            }
        }

        currentDeck.GetBlock_List()[m_iSelect_Block].SetParent_Deck(currentDeck);
    }

    //건물 리스트
    private void Change_Sprite(Transform _tr, string _name)
    {
        UISprite spr = _tr.GetChild(0).GetChild(0).GetChild(0).GetComponent<UISprite>();
        spr.atlas = m_ObjectAtlas;
        spr.spriteName = _name;
    }
    private void Change_Name(Transform _tr, string _name)
    {
        UILabel label = _tr.GetChild(0).GetChild(1).GetComponent<UILabel>();
        label.text = _name;
    }
    private void Change_AbilityText(Transform _tr, string _text)
    {
        UILabel label = _tr.GetChild(0).GetChild(2).GetComponent<UILabel>();
        label.text = _text;
    }
    private void Change_Number(Transform _tr, int _num)
    {
        UILabel label = _tr.GetChild(0).GetChild(3).GetComponent<UILabel>();
        label.text = string.Format("X{0}", _num);
    }

    private void Change_Background_Color()
    { 
        for(int i=0;i<m_listBuilding.Count;i++)
        {
            if(i == m_iCurrent_Select_Building)
            {
                m_listButton_Background[i].color = m_Select_Color;
            }
            else
            {
                m_listButton_Background[i].color = m_Origin_Color;
            }
        }
    }

    public void Select_Building(GameObject _list)
    {
        m_iCurrent_Select_Building = int.Parse(_list.name[14].ToString());

        Change_Background_Color();
        Change_Building();
    }

    
}
