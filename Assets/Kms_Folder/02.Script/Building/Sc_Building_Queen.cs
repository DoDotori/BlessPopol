using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_Queen : Sc_Building {

	public Sc_Building_Queen(int _x,int _y, Sc_Engine.Building_Kind _kind)
    {
        m_fGauge_Speed++;
        m_ptCoordinate.x = _x;
        m_ptCoordinate.y = _y;
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("파워게이지 속도 -1");
        m_strName = string.Format("퀸");
        m_strSprite_Name = string.Format("Queen");
    }
}
