using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectSlot : MonoBehaviour
{
    private Image portrait;
    private Text text_Rank;

    private void Awake()
    {
        portrait = GetComponentInChildren<Image>();
        text_Rank = GetComponentInChildren<Text>();
    }

    public void SetSlot(CharacterData character)
    {
        portrait.sprite = null;
        text_Rank.text = character.name +"\n";
        for (int i=0; i<character.rank; i++)
        {
            text_Rank.text += "¡Ú";
        }
    }
}
