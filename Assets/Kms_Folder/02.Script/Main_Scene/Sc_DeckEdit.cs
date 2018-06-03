using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DeckEdit : MonoBehaviour {
    private UIAtlas m_Atlas;
    private Transform m_trList_Grid;
    private Transform m_trExplain;
    private int m_iSelect_Deck_Num;

    public void Init()
    {
        m_Atlas = Resources.Load<UIAtlas>("Atlas/Screen_Ui");
        m_trList_Grid = this.transform.GetChild(0).GetChild(1);
        m_trExplain = this.transform.GetChild(1);
        m_iSelect_Deck_Num = 0;

        for (int i = 0; i < 4; i++)
        {
            m_trList_Grid.GetChild(i).GetChild(0).GetComponent<UILabel>().text = Sc_Player_Info.GetInstance.GetHave_Deck()[i].GetDeck_Name();
        }

        m_trExplain.GetChild(1).GetChild(0).GetComponent<UILabel>().text = Sc_Player_Info.GetInstance.GetCurrentDeck().GetDeck_Name();
    }

    public void Select_Deck(UILabel _label)
    {
        m_iSelect_Deck_Num = int.Parse(_label.text[4].ToString());
        m_trExplain.GetChild(1).GetChild(0).GetComponent<UILabel>().text = _label.text;
    }
    public void Deck_Active()
    {
        Sc_Player_Info.GetInstance.SetCurrentDeck(m_iSelect_Deck_Num);
    }
}
