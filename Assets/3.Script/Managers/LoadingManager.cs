using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    // 캐릭터 데이터
    public TextAsset characterJson;

    // 로드 후 활성화할 오브젝트들
    public GameObject[] afterLoadingObjects;

    // 로드 후 비활성화할 오브젝트들
    public GameObject beforeLoadingObject;

    // 로드 진행 슬라이더 바
    public Slider slider_Loading;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        StartCoroutine(LoadDatas());
    }

    // 캐릭터 정보 로드
    private IEnumerator LoadDatas()
    {


        var count = data.Count;
        var list = new List<CharacterData>();


        for (int i = 0; i < count; i++)
        {
            var characterData = new CharacterData();
            try
            {
                characterData.id = (int)data[i]["id"];
                characterData.name = data[i]["name"].ToString();
                characterData.rank = (int)data[i]["rank"];
                characterData.classType = (CharacterClass)(int)data[i]["classType"];
                characterData.synergyType = (CharacterSynergy)(int)data[i]["synergyType"];
                characterData.atk = (int)data[i]["atk"];
                characterData.hp = (int)data[i]["hp"];
                characterData.def = (int)data[i]["def"];
                characterData.defpen = (int)data[i]["defpen"];
                characterData.atkspeed = (int)data[i]["atkspeed"];
                characterData.critdmg = (int)data[i]["critdmg"];
                characterData.critprob = (int)data[i]["critprob"];
            }
            catch
            {
                Debug.Log("Wrong json data " + i);
                continue;
            }

            list.Add(characterData);
        }
        GameManager.instance.list_CharacterData = list;
        float startTime = Time.time;

        // 로드할 데이터 정해지면 실제 로드에 맞게 로딩률 동기화할것
        while(true)
        {
            float currentTime = Time.time;
            if(currentTime - startTime > 3)
            {
                break;
            }
            slider_Loading.value = (currentTime - startTime) / 3.0f;

            yield return null;
        }

        beforeLoadingObject.SetActive(false);
        for (int i = 0; i < afterLoadingObjects.Length; i++)
        {
            afterLoadingObjects[i].SetActive(true);
        }
    }
}
