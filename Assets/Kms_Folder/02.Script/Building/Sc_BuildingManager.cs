using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BuildingManager : MonoBehaviour {
    private static Sc_BuildingManager m_Instance;

    private Dictionary<Sc_Engine.Building_Kind, Sc_Building> m_dicBuilding;

    public static Sc_BuildingManager GetInstance
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

    public void Init()
    {
        m_dicBuilding = new Dictionary<Sc_Engine.Building_Kind, Sc_Building>();

        AddBuilding();
        
        
     }

    private void AddBuilding()
    {
        Sc_Building_King King = new Sc_Building_King(0, 0, Sc_Engine.Building_Kind.eHeadquarters);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eHeadquarters, King);
        Sc_Building_Knight Knight = new Sc_Building_Knight(0, 0, Sc_Engine.Building_Kind.eAttack_Speed);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eAttack_Speed, Knight);
        Sc_Building_Queen Queen = new Sc_Building_Queen(0, 0, Sc_Engine.Building_Kind.eGauge_Speed);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eGauge_Speed, Queen);
        Sc_Building_Pawn Pawn = new Sc_Building_Pawn(0, 0, Sc_Engine.Building_Kind.eCorrection);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eCorrection, Pawn);
        Sc_Building_Rook Rook = new Sc_Building_Rook(0, 0, Sc_Engine.Building_Kind.eBullet_Speed);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eBullet_Speed, Rook);
        Sc_Building_Bishop Bishop = new Sc_Building_Bishop(0, 0, Sc_Engine.Building_Kind.eSkill1);
        m_dicBuilding.Add(Sc_Engine.Building_Kind.eSkill1, Bishop);
        
    }

    public Dictionary<Sc_Engine.Building_Kind, Sc_Building> GetBuilding()
    {
        return m_dicBuilding;
    }

}
