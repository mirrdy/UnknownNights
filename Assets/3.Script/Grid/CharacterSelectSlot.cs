using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterSelectSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image portrait;
    [SerializeField] private Text text_Rank;
    private Character character;
    private CharacterData characterData;

    private void Awake()
    {
        //portrait = GetComponentInChildren<Image>();
        //text_Rank = GetComponentInChildren<Text>();

        // ������ �ڽĿ�����Ʈ UI�� Ŭ���� ������ �ʵ��� ��Ȱ��ȭ
        portrait.raycastTarget = false;
        text_Rank.raycastTarget = false;
    }

    public void SetSlot(Character character)
    {
        this.character = character;
        characterData = character.characterData;
        portrait.sprite = null;

        if(characterData.imageName != null)
        {
            // Resources �������� ��������Ʈ ��������
            portrait.sprite = Resources.Load<Sprite>("Images/" + characterData.imageName);
        }
        text_Rank.text = characterData.name +"\n";
        for (int i=0; i<characterData.rank; i++)
        {
            text_Rank.text += "��";
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ŭ�� �̺�Ʈ - �׸��忡 ĳ���� ��ġ
        GameManager.instance.grid.Spawn(character);

    }
}
