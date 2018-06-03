using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_GridManager : MonoBehaviour {
    private static Sc_GridManager m_Instance;

    public struct POINT
    {
        public int x;
        public int y;
    }
    public enum Grid_State { eUser_Character, eUser_Cannon, eUser_Tile, eEnemy_Character, eEnemy_Cannon, eEnemy_Tile, eNull }

    public GameObject m_objGird_Prefab;
    public GameObject m_objCharacter;
    public GameObject m_objCannon;
    public Transform m_trUser_Base;
    public Transform m_trEnemy_Base;

    private Transform m_trGrid_Parent;
    private Dictionary<POINT, GameObject> m_dicGrid;

    public static Sc_GridManager GetInstance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<Sc_GridManager>() as Sc_GridManager;

                if (m_Instance == null)
                    Debug.Log("Is Not Find Sc_GridManager");
            }
            return m_Instance;
        }
    }

	void Awake () {
        m_dicGrid = new Dictionary<POINT, GameObject>();
        m_trGrid_Parent = this.transform;
        int size = 11;
        POINT grid_size;
        POINT player_cannon;
        POINT player_character;
        POINT player_tile_start;
        POINT enemy_cannon;
        POINT enemy_character;
        POINT enemy_tile_start;
        grid_size.x = 35;
        grid_size.y = 19;
        player_character.x = 1;
        player_character.y = 9;
        player_cannon.x = 2;
        player_cannon.y = 9;
        player_tile_start.x = 3;
        player_tile_start.y = 3;
        enemy_character.x = 33;
        enemy_character.y = 9;
        enemy_cannon.x = 32;
        enemy_cannon.y = 9;
        enemy_tile_start.x = 19;
        enemy_tile_start.y = 3;

        for (int i=0;i< grid_size.y; i++)
        {
            for(int j=0;j< grid_size.x; j++)
            {
                GameObject grid = Instantiate(m_objGird_Prefab, m_trGrid_Parent);
                POINT pt;
                pt.x = j;
                pt.y = i;
                grid.transform.position = new Vector3(-170 + j * 10, 0, 90 - i * 10);
                grid.name = string.Format("Grid_{0}_{1}", i, j);

                if (i == player_character.y && j == player_character.x)
                {
                    GameObject character = Instantiate(m_objCharacter,m_trUser_Base);
                    character.transform.position = grid.transform.position;
                    grid.GetComponent<Sc_Grid>().SetState(Grid_State.eUser_Character, pt);
                }
                else if(i== player_cannon.y && j == player_cannon.x)
                {
                    GameObject cannon = Instantiate(m_objCannon, m_trUser_Base);
                    cannon.transform.position = grid.transform.position;
                    grid.GetComponent<Sc_Grid>().SetState(Grid_State.eUser_Cannon, pt);
                }
                else if((i > player_tile_start.y && i <= player_tile_start.y+size) && (j > player_tile_start.x && j <= player_tile_start.x+size))
                {
                    grid.GetComponent<Sc_Grid>().SetState(Grid_State.eUser_Tile, pt);
                }
                else if (i == enemy_character.y && j == enemy_character.x)
                {
                    GameObject character = Instantiate(m_objCharacter, m_trEnemy_Base);
                    character.transform.position = grid.transform.position;
                    character.transform.rotation = Quaternion.Euler(Vector3.up * -90);
                    grid.GetComponent<Sc_Grid>().SetState(Grid_State.eEnemy_Character, pt);
                }
                else if (i == enemy_cannon.y && j == enemy_cannon.x)
                {
                    GameObject cannon = Instantiate(m_objCannon, m_trEnemy_Base);
                    cannon.transform.position = grid.transform.position;
                    cannon.transform.rotation = Quaternion.Euler(Vector3.up * -90);
                    grid.GetComponent<Sc_Grid>().SetState(Grid_State.eEnemy_Cannon, pt);
                    cannon.AddComponent<EnemyAI>();
                }
                else if ((i > enemy_tile_start.y && i <= enemy_tile_start.y+size) && (j > enemy_tile_start.x && j <= enemy_tile_start.x+size))
                {
                    grid.GetComponent<Sc_Grid>().SetState(Grid_State.eEnemy_Tile, pt);
                }
                else
                {
                    grid.GetComponent<Sc_Grid>().SetState(Grid_State.eNull, pt);
                }

                m_dicGrid.Add(pt, grid);
                grid.GetComponent<Sc_Grid>().Init();
            }
        }
	}

	void Update () {
		
	}
}
