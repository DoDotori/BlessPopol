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

        m_trExplain.GetChild(1).GetChild(0).GetComponent<UILabel>().text = Sc_Player_Info.GetInstance.GetHave_Deck()[Sc_Player_Info.GetInstance.GetCurrentDeck_Index()].GetDeck_Name();
    }

    public void Select_Deck(GameObject _object)
    {
        m_iSelect_Deck_Num = int.Parse(_object.name[6].ToString());
        m_trExplain.GetChild(1).GetChild(0).GetComponent<UILabel>().text = Sc_Player_Info.GetInstance.GetHave_Deck()[m_iSelect_Deck_Num].GetDeck_Name();
    }
    public void Deck_Active()
    {
        Sc_Player_Info.GetInstance.SetCurrentDeck_Index(m_iSelect_Deck_Num);
    }
}
