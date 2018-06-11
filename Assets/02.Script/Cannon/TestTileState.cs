using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileState : MonoBehaviour {
    public bool show = false;
    public Sc_Engine.Building_Kind state;
    public MeshRenderer mesh;
    public int num;
    public void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        if (this.tag == "TestTile")
        {
            switch (state)
            {
                case Sc_Engine.Building_Kind.eNull:
                    mesh.materials[1].color = Color.white;
                    break;
                case Sc_Engine.Building_Kind.eNormal:
                    mesh.materials[1].color = Color.blue;
                    break;
                case Sc_Engine.Building_Kind.eAttack_Speed:
                    mesh.materials[1].color = Color.yellow;
                    break;
                case Sc_Engine.Building_Kind.eBonus:
                    mesh.materials[1].color = Color.green;
                    break;
                case Sc_Engine.Building_Kind.eCorrection:
                    mesh.materials[1].color = Color.gray;
                    break;
                case Sc_Engine.Building_Kind.eGauge_Speed:
                    mesh.materials[1].color = Color.magenta;
                    break;
                case Sc_Engine.Building_Kind.eHeadquarters:
                    mesh.materials[1].color = Color.cyan;
                    break;
                case Sc_Engine.Building_Kind.eRange:
                    mesh.materials[1].color = Color.red;
                    break;
            }//switch
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Bullet")
        {
            if (this.tag != "TestTile") return;
            if (show == true) return;

            if (show == false)
            {
                state = Sc_Engine.Building_Kind.eNormal;
                Destroy(other.gameObject);
                show = true;
            }
        }
    }//trigger

}
