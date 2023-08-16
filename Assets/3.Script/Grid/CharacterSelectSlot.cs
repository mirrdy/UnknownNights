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

        // 슬롯의 자식오브젝트 UI가 클릭을 가리지 않도록 비활성화
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
            // Resources 폴더에서 스프라이트 가져오기
            portrait.sprite = Resources.Load<Sprite>("Images/" + characterData.imageName);
        }
        text_Rank.text = characterData.name +"\n";
        for (int i=0; i<characterData.rank; i++)
        {
            text_Rank.text += "★";
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 이벤트 - 그리드에 캐릭터 배치
        GameManager.instance.grid.Spawn(character);

    }
}
