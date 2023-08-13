using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Grid grid;
    public CharacterSelector characterSelector;

    public List<CharacterData> list_CharacterData;

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

}
