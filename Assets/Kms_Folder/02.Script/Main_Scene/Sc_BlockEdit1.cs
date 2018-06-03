using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BlockEdit1 : MonoBehaviour {
    public enum Block_Grid_State { eEnable, eBan, eUse}
    public struct Block_Grid
    {
        public UISprite Grid_Sprite;
        public Block_Grid_State eBlock_Grid_State;
    }

    private readonly int m_iGrid_Size = 5;
    private readonly int m_iMax_Build_Count = 5;
    private readonly int m_iName_Position_CoordinateY = 16;
    private readonly int m_iName_Position_CoordinateX = 17;

    private UIAtlas m_ObjectAtlas;
    private UIAtlas m_ScreenAtlas;

    //건물 리스트
    private Transform m_trBuilding_List_Parent;                           //건물리스트 부모
    private List<Sc_Player_Info.Have_Building> m_listHave_Building;       //보유 건물 리스트
    private List<Sc_Building> m_listUse_Building;                         //배치된 건물 리스트
    //배치 그리드
    private Block_Grid[,] m_starrGrid;                                    //그리드 배열
    private Transform m_trGrid_Parent;                                    //그리드 부모

    //기타
    private UILabel m_labelMaxCount;
    private int m_iCurrent_Block_Index;
    

    public void Init()
    {
        m_ObjectAtlas = Resources.Load<UIAtlas>("Atlas/Object_Ui");
        m_ScreenAtlas = Resources.Load<UIAtlas>("Atlas/Screen_Ui");
        m_labelMaxCount = this.transform.Find("Build_MaxCount_Label").GetComponent<UILabel>();


        Building_Init();
        Grid_Init();
    }

    public void Building_Init()
    {
        m_trBuilding_List_Parent = this.transform.Find("Building_List");
        m_listUse_Building = new List<Sc_Building>();
       
        for(int i=0;i<Sc_Player_Info.GetInstance.GetHave_Building().Count;i++)
        {
            m_listHave_Building.Add(Sc_Player_Info.GetInstance.GetHave_Building()[i]);
        }

    }

    public void Grid_Init()
    {
        m_trGrid_Parent = this.transform.Find("Block_Edit_Grid");
        int z = 0;
        for(int i=0;i<m_iGrid_Size;i++)
        {
            for(int j=0;j<m_iGrid_Size;j++)
            {
                m_starrGrid[i, j].eBlock_Grid_State = Block_Grid_State.eBan;
                m_starrGrid[i, j].Grid_Sprite = m_trGrid_Parent.GetChild(z).GetComponent<UISprite>();
                z++;
            }
        }
    }
    
}
