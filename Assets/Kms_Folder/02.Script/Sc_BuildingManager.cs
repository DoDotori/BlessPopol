using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BuildingManager : MonoBehaviour {
    private static Sc_BuildingManager m_Instance;

    private Dictionary<Sc_Engine.Building_Kind, Sc_Building> m_dicBuilding;

    public static Sc_BuildingManager getInstance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(Sc_BuildingManager)) as Sc_BuildingManager;

                if(m_Instance == null)
                {
                    GameObject container = new GameObject();
                    container.name = string.Format("Sc_BuildingManager_Container");
                    m_Instance = container.AddComponent(typeof(Sc_BuildingManager)) as Sc_BuildingManager;
                }
            }
            return m_Instance;
        }
    }

    private void Awake()
    {
        m_dicBuilding = new Dictionary<Sc_Engine.Building_Kind, Sc_Building>();

        AddBuilding();
        
        
     }

    private void AddBuilding()
    {
        Sc_Building_BlueTent blue_tent = new Sc_Building_BlueTent(0, 0, Sc_Engine.Building_Kind.eAttack_Speed);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eAttack_Speed,blue_tent);
        Sc_Building_RedTent red_tent = new Sc_Building_RedTent(0, 0, Sc_Engine.Building_Kind.eRange);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eRange, red_tent);
        Sc_Building_PurpleTent purple_tent = new Sc_Building_PurpleTent(0, 0, Sc_Engine.Building_Kind.eGauge_Speed);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eGauge_Speed, purple_tent);
        Sc_Building_Headquarters headquarters_tent = new Sc_Building_Headquarters(0, 0, Sc_Engine.Building_Kind.eHeadquarters);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eHeadquarters, headquarters_tent);
        Sc_Building_SkillTower1 skill1_tent = new Sc_Building_SkillTower1(0, 0, Sc_Engine.Building_Kind.eSkill1);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eSkill1, skill1_tent);
        
    }

    public Dictionary<Sc_Engine.Building_Kind, Sc_Building> GetBuilding()
    {
        return m_dicBuilding;
    }

}
