using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_BlueTent : Sc_Building {

    public Sc_Building_BlueTent(int _x,int _y, Sc_Engine.Building_Kind _kind)
    {
        m_fAttack_Speed = 1;
        m_ptCoordinate.SetPoint(_x, _y);
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("공격속도 증가 +1");
        m_strName = string.Format("파란텐트");
        m_strSprite_Name = string.Format("Tent_Blue");
        
    }
   

}
