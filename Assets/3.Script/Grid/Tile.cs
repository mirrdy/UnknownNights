using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int tileNum;

    // 각 타일을 기준점 5로하여 주변 이웃을 할당 - 버프 추가 시 사용 예정
    public Tile[] neighbors;

    public Character character;
    public CharacterSelector.Select selector;

    public Image image;

    private void Awake()
    {
        TryGetComponent(out image);
        GameManager.instance.grid.delegate_Reset += SetOffImage;
    }


    // 해당 타일의 콜라이더 Enter 시 grid의 mousePos 할당
    private void OnMouseEnter()
    {
        GameManager.instance.grid.mousePos = tileNum;
    }


    // Exit 시 mousePos = 0 할당
    private void OnMouseExit()
    {
        GameManager.instance.grid.mousePos = 0;
    }


    // 타일의 기본 이미지 상태
    public void SetOffImage()
    {
        image.sprite = GameManager.instance.nullSprite;
    }
    
    // 진형에 따른 버프 추가 예정
    public void SetBuffImage()
    {
        // image.sprite = (아마 게임매니저의) 버프 이미지(스프라이트)
    }
}
