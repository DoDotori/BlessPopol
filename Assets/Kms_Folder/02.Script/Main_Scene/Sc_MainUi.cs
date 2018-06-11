using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MainUi : MonoBehaviour {
    public GameObject m_objLobby_Window;
    public GameObject m_objDeck_Edit_Window;
    public GameObject m_objDeck_Edit_Board;
    public GameObject m_objBlock_Edit_Board;
    public GameObject m_objState_Board;

    

    public void Deck_Edit_Window_Open()
    {
        m_objDeck_Edit_Window.SetActive(true);
        m_objLobby_Window.SetActive(false);
    }

	public void Lobby_Window_Open()
    {
        m_objLobby_Window.SetActive(true);
        m_objDeck_Edit_Window.SetActive(false);
    }

    public void Deck_Edit_Board_Open()
    {
        m_objDeck_Edit_Board.SetActive(true);
        m_objBlock_Edit_Board.SetActive(false);
        m_objState_Board.SetActive(false);
    }
    public void Block_Edit_Board_Open()
    {
        m_objDeck_Edit_Board.SetActive(false);
        m_objBlock_Edit_Board.SetActive(true);
        m_objState_Board.SetActive(false);
    }
    public void State_Board()
    {
        m_objDeck_Edit_Board.SetActive(false);
        m_objBlock_Edit_Board.SetActive(false);
        m_objState_Board.SetActive(true);
    }
}
