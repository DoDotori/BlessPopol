using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BlockEdit : MonoBehaviour {
    [SerializeField]
    private List<Transform> m_listBuilding;
    private UIAtlas m_Atlas;

	void Start () {
        m_Atlas = Resources.Load<UIAtlas>("Atlas/Object_Ui");

        if(IsExist(Sc_Engine.Building_Kind.eAttack_Speed))
        {
            for(int i =0; i<Sc_Player_Info.GetInstance.GetHave_Building()[Sc_Engine.Building_Kind.eAttack_Speed].Number_Of_Building;i++)
            {

            }

        }

	}

    private bool IsExist(Sc_Engine.Building_Kind _kind)
    {
        if(Sc_Player_Info.GetInstance.GetHave_Building().ContainsKey(_kind))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RepeatBuilding(Sc_Engine.Building_Kind _kind)
    {
        Sc_Player_Info.Have_Building building;
        building = Sc_Player_Info.GetInstance.GetHave_Building()[_kind];

        for(int i =0;i<building.Number_Of_Building;i++)
        {
            
        }
    }
	private void Change_Sprite(Transform _tr, string _name)
    {
        UISprite spr = _tr.GetChild(0).GetChild(0).GetComponent<UISprite>();
        spr.atlas = m_Atlas;
        spr.spriteName = _name;
    }
}
