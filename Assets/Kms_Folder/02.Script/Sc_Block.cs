using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Block : MonoBehaviour {
    private Sc_Deck m_ParentDeck;
    private List<Sc_Building> m_listBuilding;

    public Sc_Block()
    {
        m_listBuilding = new List<Sc_Building>();
    }

    public void SetBuilding(Sc_Building _building)
    {
        m_listBuilding.Add(_building);
    }
    public void SetParent_Deck(Sc_Deck _deck)
    {
        m_ParentDeck = _deck;
    }
    public List<Sc_Building> GetBuilding_List()
    {
        return m_listBuilding;
    }
}
