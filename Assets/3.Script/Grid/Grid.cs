using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Tile[] tiles; // 타일 순서는 키패드 기준

    public int pickedCharacter = 0; // 선택중인 캐릭터 위치

    public int mousePos = 0; // 선택한 타일(1~9), 타일이 아니면 0

    public Character selectedCharacter; // 캐릭터 참조


    public delegate void Delegate_Reset(); // 모두 내리기 델리게이트
    public Delegate_Reset delegate_Reset;

    private void Update()
    {
        //터치-업 : 캐릭터 드랍
        if (Input.GetMouseButtonUp(0))
        {
            DropCharacter();
            //Debug.Log("Up");
        }

        //마우스 위치가 타일위에 없으면 리턴
        if (mousePos < 1)
            return;

        //터치-다운 : 타일 선택
        if (Input.GetMouseButtonDown(0))
        {
            SelectTile(mousePos);
            //Debug.Log("Down");
        }

        //드래그 : 캐릭터 끌기
        if (Input.GetMouseButton(0))
        {
            PickADoll();
            //Debug.Log("Drag");
        }
    }

    // 캐릭터 배치
    public void Spawn(Character character, CharacterSelector.Select selector)
    {
        if (selector.gridPos - 1 < 0)
        {
            return;
        }

        // 배치할 수 있는 캐릭터가 다 찼으면 취소 (메시지 출력)
        if (tiles[selector.gridPos - 1].character != null)
        {
            // 메시지 출력
        }
        else
        {
            character.Spawn(tiles[selector.gridPos - 1].transform);
            // 캐릭터의 Pos에 셀렉터의 gridPos 할당
            character.pos = selector.gridPos;
            // 해당 타일의 캐릭터에 스폰한 캐릭터, 셀렉터 할당
            // 해당 타일의 미니버프 설정
            tiles[selector.gridPos - 1].character = character;
            tiles[selector.gridPos - 1].selector = selector;
        }
        // 캐릭터의 셀렉터에 스폰한 셀렉터 할당.
        character.selector = selector;
    }

    // 캐릭터 디스폰
    public void Despawn(int pos)
    {
        //타일 밖 위치면 리턴
        if (pos - 1 < 0)
            return;
        //해당 타일에 캐릭터가 없으면 리턴
        if (tiles[pos - 1].character == null)
            return;

        // 캐릭터 비활성, 타일에 위치한 캐릭터 null, 해당타일 버프표기 Off
        tiles[pos - 1].character.Despawn();
        tiles[pos - 1].character = null;
    }

    // 타일-타일 간 캐릭터 이동
    private void MoveTo(int from, int to, bool isNewSpawn)
    {
        // 타일을 선택한게 아니면 종료
        if (from - 1 < 0 || to - 1 < 0)
            return;

        // from의 캐릭터가 null 이면 리턴
        if (tiles[from - 1].character == null)
            return;

        #region 캐릭터 데이터 변경 로직
        // 캐릭터 데이터 변경 로직

        // from 타일의 캐릭터의 localPos를 to 타일의 localPos로 이동
        tiles[from - 1].character.transform.localPosition = tiles[to - 1].transform.localPosition;
        // 캐릭터의 타일 위치 to로 할당
        tiles[from - 1].character.pos = to;


        // to 위치에 캐릭터가 있을 경우
        if (tiles[to - 1].character != null)
        {
            // to 위치의 캐릭터의 localPos를 from 타일의 localPos로 이동
            tiles[to - 1].character.transform.localPosition = tiles[from - 1].transform.localPosition;
            // 캐릭터의 타일 위치 from 으로 할당
            tiles[to - 1].character.pos = from;
        }
        else
        {
            // to 위치에 캐릭터가 없으면 --
        }
        #endregion

        // 클릭하면 바로 스폰되도록 만들 것
        /*#region 타일 데이터 변경 로직
        //타일에 참조된 chracter swap
        var tempCharacter = tiles[to - 1].character;
        tiles[to - 1].character = tiles[from - 1].character;
        tiles[from - 1].character = tempCharacter;

        // 새로 캐릭터를 배치하는 경우면 리턴
        if (isNewSpawn)
            return;

        // 터치로 캐릭터 이동

        // 셀렉터가 다음에 소폰 할 위치 to로 변경.
        tiles[from - 1].selector.gridPos = to;

        //to 타일에 셀렉터가 null이 아닐 경우 to 타일의 셀렉터의 다음 스폰위치는 from으로 변경
        if (tiles[to - 1].selector != null)
            tiles[to - 1].selector.gridPos = from;

        //타일에 참조된 셀렉터 swap
        var tempSelecter = tiles[to - 1].selector;
        tiles[to - 1].selector = tiles[from - 1].selector;
        tiles[from - 1].selector = tempSelecter;
        #endregion*/
    }

    
    // 포인터가 가리키는 위치의 캐릭터 선택
    public void SelectTile(int num)
    {
        //타일 밖이면 리턴
        if (num < 1)
            return;
        //모든 타일에 활성화된 이미지를 off이미지로 변경(체인된 딜리게이트)
        delegate_Reset();

        // 기존에 선택되어있는 캐릭터가 null이 아니고 지금 선택한 타일과 다른곳에 있으면
        // 캐릭터 상태를 대기로 변경
        if (selectedCharacter != null && selectedCharacter != tiles[num - 1].character)
            selectedCharacter.SetState(CharacterState.Idle);

        // 선택된 캐릭터 해당 타일의 캐릭터로 변경
        selectedCharacter = tiles[num - 1].character;

        //선택한 타일에 캐릭터가 null이면 리턴
        if (tiles[num - 1].character == null)
            return;
    }


    // 캐릭터 선택 이동
    public void PickADoll()
    {
        // 기존에 끌고있는 캐릭터가 있으면 리턴
        if (pickedCharacter != 0)
            return;

        // 끌고있는 캐릭터가 없을 경우 포인터위치(해당타일위치)로 할당
        pickedCharacter = mousePos;

        // 해당 타일에 캐릭터가 null이면 끌고있는 캐릭터 없음, 리턴
        if (tiles[pickedCharacter - 1].character == null)
        {
            pickedCharacter = 0;
            return;
        }
    }

    // 캐릭터 타일에 놓기
    public void DropCharacter()
    {
        //드래그중인 캐릭터가 없으면 리턴
        if (pickedCharacter == 0)
            return;


        if (mousePos == 0)
        {
            //마우스 위치가 타일 외의 위치면 원위치로 복귀
            MoveTo(pickedCharacter, pickedCharacter, true);
        }
        else
        {
            //해당 마우스 위치로 캐릭터 이동 및 해당 위치 선택
            MoveTo(pickedCharacter, mousePos, true);
            SelectTile(mousePos);
        }
        // 끌고있는 캐릭터 초기화
        pickedCharacter = 0;
    }

    // 초기화
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
