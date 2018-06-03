using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Grid : MonoBehaviour {
    private Sc_GridManager.Grid_State m_eState;
    private Sc_GridManager.POINT m_ptCoordinates;
    private bool m_isDestroy;
    private bool m_isInstallation_Enable;

    private MeshRenderer mesh;
    private void Awake()
    {
        mesh = this.GetComponent<MeshRenderer>();
    }

    public void Init()
    {
        switch(m_eState)
        {
            case Sc_GridManager.Grid_State.eUser_Character:
                Grid_Properties(false, false);
                Grid_Color(1, Color.blue);
                break;
            case Sc_GridManager.Grid_State.eUser_Cannon:
                Grid_Properties(false, false);
                Grid_Color(1, Color.red);
                break;
            case Sc_GridManager.Grid_State.eUser_Tile:
                Grid_Properties(false, true);
                Grid_Color(1, Color.green);
                break;
            case Sc_GridManager.Grid_State.eEnemy_Character:
                Grid_Properties(false, false);
                Grid_Color(1, Color.blue);
                break;
            case Sc_GridManager.Grid_State.eEnemy_Cannon:
                Grid_Properties(false, false);
                Grid_Color(1, Color.red);
                break;
            case Sc_GridManager.Grid_State.eEnemy_Tile:
                Grid_Properties(false, false);
                Grid_Color(1, Color.black);
                break;
            case Sc_GridManager.Grid_State.eNull:
                Grid_Properties(false, false);
                Grid_Color(0, Color.black);
                Grid_Color(1, Color.white);
                this.transform.GetComponent<MeshRenderer>().enabled = false;
                break;
        }
    }

    public void SetState(Sc_GridManager.Grid_State _state, Sc_GridManager.POINT _pt)
    {
        m_eState = _state;
        m_ptCoordinates = _pt;
    }
	
    private void Grid_Properties(bool _isDestroy, bool _isInstallation)
    {
        m_isDestroy = _isDestroy;
        m_isInstallation_Enable = _isInstallation;
    }
    private void Grid_Color(int _index, Color _color)
    {
        mesh.materials[_index].color = _color;
    }
	
}
