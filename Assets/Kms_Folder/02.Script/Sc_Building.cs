using UnityEngine;

public class Sc_Building : MonoBehaviour {

    protected Sc_Engine.POINT m_ptCoordinate;   //좌표
    protected bool m_isAlive;
    protected bool m_isSkill1;                  //스킬1
    protected bool m_isSkill2;                  //스킬2
    protected float m_fAttack_Speed;            //장전속도
    protected float m_fGauge_Speed;             //파워게이지 속도
    protected float m_fRange;                   //폭발범위
    protected float m_fCorrection;              //파워게이지 보정
    protected float m_fBonus;                   //보너스
    protected string m_strAbility;              //능력 설명
    protected string m_strName;                    //이름
    protected string m_strSprite_Name;          //스프라이트 이름
    protected Sc_Engine.Building_Kind m_eBuilding_Kind;

    public Sc_Building()
    {
        m_ptCoordinate.SetPoint(0, 0);
        m_isAlive = false;
        m_isSkill1 = false;
        m_isSkill2 = false;
        m_fAttack_Speed = 0;
        m_fGauge_Speed = 0;
        m_fRange = 0;
        m_fCorrection = 0;
        m_fBonus = 0;
        m_strAbility = string.Empty;
        m_strName = string.Empty;
        m_strSprite_Name = string.Empty;
        m_eBuilding_Kind = Sc_Engine.Building_Kind.eNull;
    }

    public Sc_Engine.POINT GetCoordinate    { get { return m_ptCoordinate; } }
    public bool GetIsAlive                  { get { return m_isAlive; } }
    public bool GetIsSkill1                 { get { return m_isSkill1; } }
    public bool GetIsSkill2                 { get { return m_isSkill2; } }
    public float GetAttack_Speed            { get { return m_fAttack_Speed; } }
    public float GetGauge_Speed             { get { return m_fGauge_Speed; } }
    public float GetRange                   { get { return m_fRange; } }
    public float GetCorrection              { get { return m_fCorrection; } }
    public float GetBonus                   { get { return m_fBonus; } }
    public string GetAbility                { get { return m_strAbility; } }
    public string GetName                   { get { return m_strName; } }
    public string GetSpriteName             { get { return m_strSprite_Name; } }
    
}
