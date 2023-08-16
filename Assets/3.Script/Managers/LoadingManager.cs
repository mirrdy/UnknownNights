using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class LoadingManager : MonoBehaviour
{
    // 캐릭터 데이터
    public TextAsset characterJson;

    // 로드 후 활성화할 오브젝트들
    public GameObject[] afterLoadingObjects;

    // 로드 후 비활성화할 오브젝트들
    public GameObject beforeLoadingObject;


    // 데이터 할당 전 캐릭터 프리팹
    [SerializeField] private Character prefab_Character;

    // 로드 진행 슬라이더 바
    public Slider slider_Loading;

    private void Awake()
    {
        
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        StartCoroutine(LoadDatas(JsonMapper.ToObject(characterJson.text)));
    }

    // 캐릭터 정보 로드
    private IEnumerator LoadDatas(JsonData characterDatas)
    {
        var count = characterDatas.Count;
        var list = new List<CharacterData>();

        for (int i = 0; i < count; i++)
        {
            var characterData = new CharacterData();
            try
            {
                characterData.id = (int)characterDatas[i]["id"];
                characterData.name = characterDatas[i]["name"].ToString();
                characterData.rank = (int)characterDatas[i]["rank"];
                characterData.classType = (CharacterClass)(int)characterDatas[i]["classType"];
                characterData.synergyType = (CharacterSynergy)(int)characterDatas[i]["synergyType"];
                characterData.imageName = (string)characterDatas[i]["imageName"];
                characterData.atk = (int)characterDatas[i]["atk"];
                characterData.hp = (int)characterDatas[i]["hp"];
                characterData.def = (int)characterDatas[i]["def"];
                characterData.defpen = (int)characterDatas[i]["defpen"];
                characterData.atkspeed = (int)characterDatas[i]["atkspeed"];
                characterData.critdmg = (int)characterDatas[i]["critdmg"];
                characterData.critprob = (int)characterDatas[i]["critprob"];
            }
            catch
            {
                Debug.Log("Json read fail " + i);
                continue;
            }

            list.Add(characterData);
            Character character = Instantiate(prefab_Character, transform);
            character.gameObject.SetActive(false);
            character.characterData = characterData;

            GameManager.instance.list_Character.Add(character);
        }   
        
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
