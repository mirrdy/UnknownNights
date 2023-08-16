using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int maxUnitCount { get; private set; } = 5;

    public Grid grid;
    public CharacterSelector characterSelector;

    public List<Character> list_Character;
    //public List<CharacterData> list_CharacterData;

    // 데이터 할당 전 캐릭터 프리팹
    [SerializeField] GameObject prefab_Character;

    [Header("Sprites")]
    public Sprite nullSprite;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void DeployCharacter()
    {

    }
}
