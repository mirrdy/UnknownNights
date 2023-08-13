using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Tile[] tiles; // Ÿ�� ������ Ű�е� ����

    public int pickedCharacter = 0; // �������� ĳ���� ��ġ

    public int mousePos = 0; // ������ Ÿ��(1~9), Ÿ���� �ƴϸ� 0

    public Character selectedCharacter; // ĳ���� ����


    public delegate void Delegate_Reset(); // ��� ������ ��������Ʈ
    public Delegate_Reset delegate_Reset;

    private void Update()
    {
        //��ġ-�� : ĳ���� ���
        if (Input.GetMouseButtonUp(0))
        {
            DropCharacter();
            //Debug.Log("Up");
        }

        //���콺 ��ġ�� Ÿ������ ������ ����
        if (mousePos < 1)
            return;

        //��ġ-�ٿ� : Ÿ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            SelectTile(mousePos);
            //Debug.Log("Down");
        }

        //�巡�� : ĳ���� ����
        if (Input.GetMouseButton(0))
        {
            PickADoll();
            //Debug.Log("Drag");
        }
    }

    // ĳ���� ��ġ
    public void Spawn(Character character, CharacterSelector.Select selector)
    {
        if (selector.gridPos - 1 < 0)
        {
            return;
        }

        // ��ġ�� �� �ִ� ĳ���Ͱ� �� á���� ��� (�޽��� ���)
        if (tiles[selector.gridPos - 1].character != null)
        {
            // �޽��� ���
        }
        else
        {
            character.Spawn(tiles[selector.gridPos - 1].transform);
            // ĳ������ Pos�� �������� gridPos �Ҵ�
            character.pos = selector.gridPos;
            // �ش� Ÿ���� ĳ���Ϳ� ������ ĳ����, ������ �Ҵ�
            // �ش� Ÿ���� �̴Ϲ��� ����
            tiles[selector.gridPos - 1].character = character;
            tiles[selector.gridPos - 1].selector = selector;
        }
        // ĳ������ �����Ϳ� ������ ������ �Ҵ�.
        character.selector = selector;
    }

    // ĳ���� ����
    public void Despawn(int pos)
    {
        //Ÿ�� �� ��ġ�� ����
        if (pos - 1 < 0)
            return;
        //�ش� Ÿ�Ͽ� ĳ���Ͱ� ������ ����
        if (tiles[pos - 1].character == null)
            return;

        // ĳ���� ��Ȱ��, Ÿ�Ͽ� ��ġ�� ĳ���� null, �ش�Ÿ�� ����ǥ�� Off
        tiles[pos - 1].character.Despawn();
        tiles[pos - 1].character = null;
    }

    // Ÿ��-Ÿ�� �� ĳ���� �̵�
    private void MoveTo(int from, int to, bool isNewSpawn)
    {
        // Ÿ���� �����Ѱ� �ƴϸ� ����
        if (from - 1 < 0 || to - 1 < 0)
            return;

        // from�� ĳ���Ͱ� null �̸� ����
        if (tiles[from - 1].character == null)
            return;

        #region ĳ���� ������ ���� ����
        // ĳ���� ������ ���� ����

        // from Ÿ���� ĳ������ localPos�� to Ÿ���� localPos�� �̵�
        tiles[from - 1].character.transform.localPosition = tiles[to - 1].transform.localPosition;
        // ĳ������ Ÿ�� ��ġ to�� �Ҵ�
        tiles[from - 1].character.pos = to;


        // to ��ġ�� ĳ���Ͱ� ���� ���
        if (tiles[to - 1].character != null)
        {
            // to ��ġ�� ĳ������ localPos�� from Ÿ���� localPos�� �̵�
            tiles[to - 1].character.transform.localPosition = tiles[from - 1].transform.localPosition;
            // ĳ������ Ÿ�� ��ġ from ���� �Ҵ�
            tiles[to - 1].character.pos = from;
        }
        else
        {
            // to ��ġ�� ĳ���Ͱ� ������ --
        }
        #endregion

        // Ŭ���ϸ� �ٷ� �����ǵ��� ���� ��
        /*#region Ÿ�� ������ ���� ����
        //Ÿ�Ͽ� ������ chracter swap
        var tempCharacter = tiles[to - 1].character;
        tiles[to - 1].character = tiles[from - 1].character;
        tiles[from - 1].character = tempCharacter;

        // ���� ĳ���͸� ��ġ�ϴ� ���� ����
        if (isNewSpawn)
            return;

        // ��ġ�� ĳ���� �̵�

        // �����Ͱ� ������ ���� �� ��ġ to�� ����.
        tiles[from - 1].selector.gridPos = to;

        //to Ÿ�Ͽ� �����Ͱ� null�� �ƴ� ��� to Ÿ���� �������� ���� ������ġ�� from���� ����
        if (tiles[to - 1].selector != null)
            tiles[to - 1].selector.gridPos = from;

        //Ÿ�Ͽ� ������ ������ swap
        var tempSelecter = tiles[to - 1].selector;
        tiles[to - 1].selector = tiles[from - 1].selector;
        tiles[from - 1].selector = tempSelecter;
        #endregion*/
    }

    
    // �����Ͱ� ����Ű�� ��ġ�� ĳ���� ����
    public void SelectTile(int num)
    {
        //Ÿ�� ���̸� ����
        if (num < 1)
            return;
        //��� Ÿ�Ͽ� Ȱ��ȭ�� �̹����� off�̹����� ����(ü�ε� ��������Ʈ)
        delegate_Reset();

        // ������ ���õǾ��ִ� ĳ���Ͱ� null�� �ƴϰ� ���� ������ Ÿ�ϰ� �ٸ����� ������
        // ĳ���� ���¸� ���� ����
        if (selectedCharacter != null && selectedCharacter != tiles[num - 1].character)
            selectedCharacter.SetState(CharacterState.Idle);

        // ���õ� ĳ���� �ش� Ÿ���� ĳ���ͷ� ����
        selectedCharacter = tiles[num - 1].character;

        //������ Ÿ�Ͽ� ĳ���Ͱ� null�̸� ����
        if (tiles[num - 1].character == null)
            return;
    }


    // ĳ���� ���� �̵�
    public void PickADoll()
    {
        // ������ �����ִ� ĳ���Ͱ� ������ ����
        if (pickedCharacter != 0)
            return;

        // �����ִ� ĳ���Ͱ� ���� ��� ��������ġ(�ش�Ÿ����ġ)�� �Ҵ�
        pickedCharacter = mousePos;

        // �ش� Ÿ�Ͽ� ĳ���Ͱ� null�̸� �����ִ� ĳ���� ����, ����
        if (tiles[pickedCharacter - 1].character == null)
        {
            pickedCharacter = 0;
            return;
        }
    }

    // ĳ���� Ÿ�Ͽ� ����
    public void DropCharacter()
    {
        //�巡������ ĳ���Ͱ� ������ ����
        if (pickedCharacter == 0)
            return;


        if (mousePos == 0)
        {
            //���콺 ��ġ�� Ÿ�� ���� ��ġ�� ����ġ�� ����
            MoveTo(pickedCharacter, pickedCharacter, true);
        }
        else
        {
            //�ش� ���콺 ��ġ�� ĳ���� �̵� �� �ش� ��ġ ����
            MoveTo(pickedCharacter, mousePos, true);
            SelectTile(mousePos);
        }
        // �����ִ� ĳ���� �ʱ�ȭ
        pickedCharacter = 0;
    }

    // �ʱ�ȭ
    public void ResetAll(bool showMsg = true)
    {
        selectedCharacter = null;
        delegate_Reset();

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].character == null)
                continue;
            Despawn(i + 1);
        }

    }
}
