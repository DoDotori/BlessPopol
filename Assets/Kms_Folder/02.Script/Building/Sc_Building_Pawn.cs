using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_Pawn : Sc_Building {

    public Sc_Building_Pawn(int _x, int _y,Sc_Engine.Building_Kind _kind)
    {
        m_fCorrection++;
        m_ptCoordinate.x = _x;
        m_ptCoordinate.y = _y;
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("파워게이지 보정 +1");
        m_strName = string.Format("폰");
        m_strSprite_Name = string.Format("Pawn");
    }

}
