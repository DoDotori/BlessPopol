using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_King : Sc_Building {

	public Sc_Building_King(int _x, int _y, Sc_Engine.Building_Kind _kind)
    {
        m_isAlive = true;
        m_ptCoordinate.x = _x;
        m_ptCoordinate.y = _y;
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("본진");
        m_strName = string.Format("킹");
        m_strSprite_Name = string.Format("King");
    }
}
