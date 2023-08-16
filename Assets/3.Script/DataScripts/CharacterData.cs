using System;

public enum CharacterClass
{
    // ����, ��Ŀ, ����, ����, �ϻ�, ����
    ALL = 0, // ETC
    WARRIOR = 1,
    TANK = 2,
    MAGE = 3,
    SNIPER = 4,
    ASSASSIN = 5,
    SUPPORTER = 6
}
public enum CharacterSynergy
{
    // ��, ���, ��, ��, ����, �ٶ�, ���
    ALL = 0, // ETC
    LIGHT,
    DARKNESS,
    WATER,
    FIRE,
    EARTH,
    WIND,
    MACHINE
}

// ĳ���� ������ Ŭ���� Json ������ ���
[System.Serializable]
public class CharacterData
{
    public int id;
    public string name;
    public int rank; // �� ��ġ
    public CharacterClass classType;
    public CharacterSynergy synergyType;
    public string imageName;

    public int atk;
    public int hp;
    public int def;
    public int defpen;
    public float atkspeed;
    public int critdmg;
    public int critprob;
}
