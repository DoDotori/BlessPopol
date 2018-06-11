using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Block : MonoBehaviour {
    private List<Sc_Building> m_listBuilding;
    private bool m_isActive;
    private Sc_Engine.Building_Kind m_eBlock_Kind;

    public Sc_Block()
    {
        m_listBuilding = new List<Sc_Building>();
        m_isActive = false;
    }

    public void SetBuilding(Sc_Building _building)
    {
        m_listBuilding.Add(_building);
    }
    public void SetActive(bool _isActive)
    {
        m_isActive = _isActive;
    }
    public void SetBlock_Kind(Sc_Engine.Building_Kind _kind)
    {
        m_eBlock_Kind = _kind;
    }
    public bool GetActive()
    {
        return m_isActive;
    }
    public List<Sc_Building> GetBuilding_List()
    {
        return m_listBuilding;
    }
}
