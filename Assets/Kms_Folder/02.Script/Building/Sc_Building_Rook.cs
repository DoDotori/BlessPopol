using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_Rook : Sc_Building {

    public Sc_Building_Rook(int _x, int _y,Sc_Engine.Building_Kind _kind)
    {
        m_fBullet_Speed++;
        m_ptCoordinate.x = _x;
        m_ptCoordinate.y = _y;
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("포탄 속도 증가 +1");
        m_strName = string.Format("룩");
        m_strSprite_Name = string.Format("Rook");
    }

}
