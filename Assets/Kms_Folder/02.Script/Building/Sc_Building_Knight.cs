using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_Knight : Sc_Building {

    public Sc_Building_Knight(int _x,int _y, Sc_Engine.Building_Kind _kind)
    {
        m_fAttack_Speed = 1;
        m_ptCoordinate.x = _x;
        m_ptCoordinate.y = _y;
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("공격속도 증가 +1");
        m_strName = string.Format("나이트");
        m_strSprite_Name = string.Format("Knight");
        
    }
   

}
