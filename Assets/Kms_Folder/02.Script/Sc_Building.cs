using UnityEngine;
using System;

public class Sc_Building : MonoBehaviour, ICloneable {

    protected Sc_Engine.POINT m_ptCoordinate;   //좌표
    protected bool m_isAlive;
    protected bool m_isSkill1;                  //스킬1
    protected float m_fAttack_Speed;            //장전속도
    protected float m_fGauge_Speed;             //파워게이지 속도
    protected float m_fCorrection;              //파워게이지 보정
    protected float m_fBullet_Speed;            //포탄 속도
    protected string m_strAbility;              //능력 설명
    protected string m_strName;                 //이름
    protected string m_strSprite_Name;          //스프라이트 이름
    protected Sc_Engine.Building_Kind m_eBuilding_Kind;

    public Sc_Building()
    {
        m_ptCoordinate = new Sc_Engine.POINT();
        m_isAlive = false;
        m_isSkill1 = false;
        m_fAttack_Speed = 0;
        m_fGauge_Speed = 0;
        m_fCorrection = 0;
        m_strAbility = string.Empty;
        m_strName = string.Empty;
        m_strSprite_Name = string.Empty;
        m_eBuilding_Kind = Sc_Engine.Building_Kind.eNull;
    }

    public Sc_Engine.POINT GetCoordinate    { get { return m_ptCoordinate; } }
    public bool GetIsAlive                  { get { return m_isAlive; } set {m_isAlive = GetIsAlive; } }
    public bool GetIsSkill1                 { get { return m_isSkill1; } set {m_isSkill1 = GetIsSkill1; } }
    public float GetAttack_Speed            { get { return m_fAttack_Speed; } set {m_fAttack_Speed = GetAttack_Speed; } }
    public float GetGauge_Speed             { get { return m_fGauge_Speed; } set {m_fGauge_Speed = GetGauge_Speed; } }
    public float GetCorrection              { get { return m_fCorrection; } set {m_fCorrection = GetCorrection; } }
    public float GetBullet_Speed            { get { return m_fBullet_Speed; } set { m_fBullet_Speed = GetBullet_Speed; } }
    public string GetAbility                { get { return m_strAbility; } set {m_strAbility = GetAbility; } }
    public string GetName                   { get { return m_strName; } set {m_strName = GetName; } }
    public string GetSpriteName             { get { return m_strSprite_Name; } set {m_strSprite_Name = GetSpriteName; } }
    public Sc_Engine.Building_Kind Building_Kind { get { return m_eBuilding_Kind; } set { m_eBuilding_Kind = Building_Kind; } }

    public void SetCoordinate(int _x, int _y)
    {
        m_ptCoordinate.x = _x;
        m_ptCoordinate.y = _y;
    }

    public System.Object Clone()
    {
        Sc_Building building = new Sc_Building();
        building.m_ptCoordinate.x = this.m_ptCoordinate.x;
        building.m_ptCoordinate.y = this.m_ptCoordinate.y;
        building.m_isAlive = this.m_isAlive;
        building.m_isSkill1 = this.m_isSkill1;
        building.m_fAttack_Speed = this.m_fAttack_Speed;
        building.m_fGauge_Speed = this.m_fGauge_Speed;
        building.m_fCorrection = this.m_fCorrection;
        building.m_fBullet_Speed = this.m_fBullet_Speed;
        building.m_strAbility = this.m_strAbility;
        building.m_strName = this.m_strName;
        building.m_strSprite_Name = this.m_strSprite_Name;
        building.m_eBuilding_Kind = this.m_eBuilding_Kind;
        
        return building;
    }
}
