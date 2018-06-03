using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_RedTent : Sc_Building {

    public Sc_Building_RedTent(int _x, int _y,Sc_Engine.Building_Kind _kind)
    {
        m_fRange++;
        m_ptCoordinate.SetPoint(_x, _y);
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("공격범위 증가 +1");
        m_strName = string.Format("빨간텐트");
        m_strSprite_Name = string.Format("Tent_Red");
    }

}
