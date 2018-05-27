using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Building_SkillTower1 : Sc_Building {

	public Sc_Building_SkillTower1(int _x,int _y,Sc_Engine.Building_Kind _kind)
    {
        m_isSkill1 = true;
        m_ptCoordinate.SetPoint(_x, _y);
        m_eBuilding_Kind = _kind;
        m_strAbility = string.Format("스킬1");
        m_strName = string.Format("스킬1타워");
        m_strSprite_Name = string.Format("Tower_Skill1");
    }
}
