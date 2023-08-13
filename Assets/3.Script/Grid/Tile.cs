using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int tileNum;

    // �� Ÿ���� ������ 5���Ͽ� �ֺ� �̿��� �Ҵ� - ���� �߰� �� ��� ����
    public Tile[] neighbors;

    public Character character;
    public CharacterSelector.Select selector;

    public Image image;

    private void Awake()
    {
        TryGetComponent(out image);
        GameManager.instance.grid.delegate_Reset += SetOffImage;
    }


    // �ش� Ÿ���� �ݶ��̴� Enter �� grid�� mousePos �Ҵ�
    private void OnMouseEnter()
    {
        GameManager.instance.grid.mousePos = tileNum;
    }


    // Exit �� mousePos = 0 �Ҵ�
    private void OnMouseExit()
    {
        GameManager.instance.grid.mousePos = 0;
    }


    // Ÿ���� �⺻ �̹��� ����
    public void SetOffImage()
    {
        image.sprite = GameManager.instance.nullSprite;
    }
    
    // ������ ���� ���� �߰� ����
    public void SetBuffImage()
    {
        // image.sprite = (�Ƹ� ���ӸŴ�����) ���� �̹���(��������Ʈ)
    }
}
