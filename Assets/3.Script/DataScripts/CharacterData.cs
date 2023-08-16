using System;

public enum CharacterClass
{
    // 전사, 탱커, 법사, 저격, 암살, 서폿
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
    // 빛, 어둠, 물, 불, 대지, 바람, 기계
    ALL = 0, // ETC
    LIGHT,
    DARKNESS,
    WATER,
    FIRE,
    EARTH,
    WIND,
    MACHINE
}

// 캐릭터 데이터 클래스 Json 데이터 사용
[System.Serializable]
public class CharacterData
{
    public int id;
    public string name;
    public int rank; // 별 수치
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
