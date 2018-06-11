using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MainSceneManager : MonoBehaviour {

    private void Awake()
    {
        Transform m_trUIRoot = GameObject.Find("UI Root").transform;

        Sc_DeckEdit m_cDeckEdit = m_trUIRoot.GetChild(2).GetChild(0).GetChild(1).GetComponent<Sc_DeckEdit>();
        Sc_BlockEdit m_cBlockEdit = m_trUIRoot.GetChild(2).GetChild(0).GetChild(2).GetComponent<Sc_BlockEdit>();

        Sc_BuildingManager.GetInstance.Init();
        Sc_Player_Info.GetInstance.Init();

        if (m_cDeckEdit != null)
            m_cDeckEdit.Init();

        if (m_cBlockEdit != null)
            m_cBlockEdit.Init();
    }

    public void AIBattleScene_Load()
    {
        Sc_Loading_Scene.Loading("ObjectScene");
    }

}
