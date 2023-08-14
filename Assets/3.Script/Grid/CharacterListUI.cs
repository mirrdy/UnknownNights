using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListUI : MonoBehaviour
{
    [SerializeField] private GameObject prefab_CharacterSlot;
    private CharacterSelectSlot[] CharacterSlots;
    private void Awake()
    {
        int characterCount = GameManager.instance.list_CharacterData.Count;
        CharacterSlots = new CharacterSelectSlot[characterCount];

        for(int i=0; i<characterCount; i++)
        {
            Instantiate(prefab_CharacterSlot, transform).TryGetComponent(out CharacterSlots[i]);
            CharacterSlots[i].SetSlot(GameManager.instance.list_CharacterData[i]);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
