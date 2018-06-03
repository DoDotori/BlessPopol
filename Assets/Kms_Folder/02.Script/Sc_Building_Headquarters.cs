using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_Headquarters : Sc_Building {

	public Sc_Building_Headquarters(int _x, int _y, Sc_Engine.Building_Kind _kind)
    {
        m_isAlive = true;
        m_ptCoordinate.SetPoint(_x, _y);
        m_eBuilding_Kind = _kind;
    }
}
