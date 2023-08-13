using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���� ����
public enum CharacterState
{
    Idle,
    Attack,
    Follow,
}

public class Character : MonoBehaviour
{
    public CharacterState state;

    public int id;

    // ĳ���͵�����
    public CharacterData characterData;

    public int pos = 0;
    public CharacterSelector.Select selector;

    private void Awake()
    {

    }

    private void Update()
    {
        
    }

    public void Spawn(Transform tr)
    {
        transform.localPosition = tr.localPosition;
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    // ĳ���� ���º�ȭ
    public void SetState(CharacterState state)
    {
        this.state = state;
        switch (state)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Attack:
                break;
            case CharacterState.Follow:
                break;
            default:
                break;
        }
    }
}
