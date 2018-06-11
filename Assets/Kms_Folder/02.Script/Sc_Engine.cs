using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Engine : MonoBehaviour {
    private static Sc_Engine m_Instance;

    public static Sc_Engine GetInstance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(Sc_Engine)) as Sc_Engine;

                if(m_Instance == null)
                {
                    GameObject container = new GameObject();
                    container.name = string.Format("Sc_Engine_Container");
                    m_Instance = container.AddComponent(typeof(Sc_Engine)) as Sc_Engine;
                }
            }
            return m_Instance;
        }
    }
	
    public struct POINT
    {
        public int x;
        public int y;
    }

    public enum Building_Kind { eAttack_Speed, eGauge_Speed, eCorrection, eBullet_Speed ,eSkill1, eHeadquarters, eNull }
    public enum Rank { eBronze5, eBronze4, eBronze3, eBronze2, eBronze1, eSilver5, eSilver4, eSilver3, eSilver2, eSilver1, eGold5, eGold4, eGold3, eGold2, eGold1, ePlatinum5,
        ePlatinum4, ePlatinum3, ePlatinum2, ePlatinum1, eDiamond5, eDiamond4, eDiamond3, eDiamond2, eDiamond1, eMaster5, eMaster4, eMaster3, eMaster2, eMaster1, eGrandMaster5,
        eGrandMaster4, eGrandMaster3, eGrandMaster2, eGrandMaster1 }

    public void Change_Sprite(UIAtlas _atlas, Transform _tr, string _spriteName)
    {
        UISprite sprite = _tr.GetComponent<UISprite>();
        sprite.atlas = _atlas;
        sprite.spriteName = _spriteName;
    }
    public void Change_Sprite(UISprite _sprite, UIAtlas _atlas, Color _color, string _spriteName)
    {
        _sprite.atlas = _atlas;
        _sprite.spriteName = _spriteName;
        _sprite.color = _color;
    }
    public void Change_Color(UISprite _sprite, Color _color)
    {
        _sprite.color = _color;
    }

<<<<<<< HEAD
    public enum Building_Kind { eNull, eNormal, eHeadquarters,eAttack_Speed, eGauge_Speed, eRange, eCorrection, eBonus, eSkill1, eSkill2 }

=======
    public void Change_Label(Transform _tr, string _text)
    {
        UILabel label = _tr.GetComponent<UILabel>();
        label.text = _text;
    }
   
>>>>>>> KMS
}
