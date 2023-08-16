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

    // ������ �Ҵ� �� ĳ���� ������
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
