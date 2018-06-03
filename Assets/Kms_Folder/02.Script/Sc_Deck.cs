using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Deck : MonoBehaviour {
    private List<Sc_Block> m_listBlock;
    private string m_strDeck_Name;
    public Sc_Deck()
    {
        m_listBlock = new List<Sc_Block>();
    }

    public void AddBlock(Sc_Block _block)
    {
        m_listBlock.Add(_block);
    }
    public List<Sc_Block> GetBlock_List()
    {
        return m_listBlock;
    }
    public string GetDeck_Name()
    {
        return m_strDeck_Name;
    }
    public void SetDeck_Name(string _name)
    {
        m_strDeck_Name = _name;
    }
}
