using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MainSceneManager : MonoBehaviour {
    private Sc_BlockEdit m_cBlockEdit;
    private Sc_DeckEdit m_cDeckEdit;
    private void Awake()
    {
        m_cBlockEdit = FindObjectOfType(typeof(Sc_BlockEdit)) as Sc_BlockEdit;
        m_cDeckEdit = FindObjectOfType(typeof(Sc_DeckEdit)) as Sc_DeckEdit;

        Sc_BuildingManager.GetInstance.Init();
        Sc_Player_Info.GetInstance.Init();
        if(m_cBlockEdit != null)
            m_cBlockEdit.Init();

        if(m_cDeckEdit != null)
            m_cDeckEdit.Init();
    }
}
