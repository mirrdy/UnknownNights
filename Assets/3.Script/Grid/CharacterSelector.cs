using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 
public class CharacterSelector : MonoBehaviour
{
    [System.Serializable]
    public class Select
    {
        public int gridPos;
        public Button btn;
        public Image image;
    }

    public Select[] selects;

    private void Awake()
    {
        int characterCount = GameManager.instance.list_CharacterData.Count;
        for(int i=0; i<characterCount; i++)
        {

        }
        for (int i = 0; i < selects.Length; i++)
        {
            GameManager.instance.grid.tiles[i].selector = selects[i];
        }
    }


    public void ResetAll()
    {

        for (int i = 0; i < GameManager.instance.grid.tiles.Length; i++)
        {
            GameManager.instance.grid.tiles[i].selector = null;
        }

        for (int i = 0; i < selects.Length; i++)
        {
            GameManager.instance.grid.tiles[i].selector = selects[i];
            selects[i].gridPos = i + 1;
        }
    }
}
