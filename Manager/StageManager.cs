using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : GameManager<StageManager>
{
    public Dictionary<string, List<Light>> stageDictionary = new Dictionary<string, List<Light>>();


    // Variable
    private Player player;
    private dogChaser dog;

    public uint _stageLv { get; private set; }
    public enum STAGE_MAP { FIRST, BOSS, A, B, C, D, NONE }
    public STAGE_MAP stage_map = STAGE_MAP.FIRST;


    private void Start()
    {
        _stageLv = 1;
    }

    private void Update()
    {
        CheckInstance();
    }

    private void CheckInstance()
    {
        if (CharacterManager.Instance.player != null && player == null)
            player = CharacterManager.Instance.player;

        if (CharacterManager.Instance.dogchaser != null && dog == null)
            dog = CharacterManager.Instance.dogchaser;
    }

    public void LevelUpEvent()
    {
        _stageLv++;

        CharacterManager.Instance.spooky.b_SkipEvent = true;
        CharacterManager.Instance.spooky.gameObject.SetActive(false);
        UIManager.Instance.U_TextSay.text = "";
    }

    public void StageLoadingPositionEvent()
    {
        switch (stage_map)
        {
            case STAGE_MAP.FIRST:
                player.transform.position = new Vector3(-221.0f, 0.0f, -192.2f);
                player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                dog.transform.position = new Vector3(-221.0f, 0.0f, -192.2f);
                dog.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                player.SetLightIntensity(500.0f);
                break;
            case STAGE_MAP.BOSS:;
                player.transform.position = new Vector3(-6.0f, 0.0f, -50.0f);
                player.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                dog.transform.position = new Vector3(-6.0f, 0.0f, -50.0f);
                dog.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                player.SetLightIntensity(10.0f);
                break;
            case STAGE_MAP.A:
                player.transform.position = new Vector3(-21.23f, 0.0f, -208.4f);
                player.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                dog.transform.position = new Vector3(-21.23f, 0.0f, -208.4f);
                dog.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                player.SetLightIntensity(10.0f);
                break;
            case STAGE_MAP.B:
                player.transform.position = new Vector3(-252.1f, 0.0f, 104.44f);
                player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                dog.transform.position = new Vector3(-252.1f, 0.0f, 104.44f);
                dog.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                player.SetLightIntensity(10.0f);
                break;
            case STAGE_MAP.C:
                player.transform.position = new Vector3(-46.63f, 0.0f, 153.5f);
                player.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                dog.transform.position = new Vector3(-46.63f, 0.0f, 153.5f);
                dog.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                player.SetLightIntensity(10.0f);
                break;
            case STAGE_MAP.D:
                player.transform.position = new Vector3(292.0f, 0.0f, -126.27f);
                player.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                dog.transform.position = new Vector3(292.0f, 0.0f, -126.27f);
                dog.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                player.SetLightIntensity(10.0f);
                break;
            default:
                Debug.Log("stage_map : " + stage_map);
                break;
        }
    }

    public void JumpscareImageEvent()
    {
        int a = UnityEngine.Random.Range(0, 6);
        Sprite sprite;

        if (a == 0) sprite = Resources.Load<Sprite>("Image/È£¶û»ç¶Ç/È£¶û»ç¶Ç");
        else if (a == 1) sprite = Resources.Load<Sprite>("Image/È£¶û»ç¶Ç/È£¶û»ç¶Ç ¹ÝÀü");
        else if (a == 2) sprite = Resources.Load<Sprite>("Image/È£¶û»ç¶Ç/È£¶û´Ù¿À");
        else if (a == 3) sprite = Resources.Load<Sprite>("Image/È£¶û»ç¶Ç/È£¶û´Ù¿À ¹ÝÀü");
        else if (a == 4) sprite = Resources.Load<Sprite>("Image/È£¶û»ç¶Ç/¸Ó¸Ç");
        else sprite = Resources.Load<Sprite>("Image/È£¶û»ç¶Ç/¸Ó¸Ç ¹ÝÀü");

        UIManager.Instance.ShowImage(sprite, 0.1f);
    }

    public void LightChangeEvent(string name)
    {
        foreach (string key in stageDictionary.Keys)
        {
            if (key == name)
            {
                float R = UnityEngine.Random.Range(0.45f, 1.0f);
                float G = UnityEngine.Random.Range(0.45f, 1.0f);
                float B = UnityEngine.Random.Range(0.45f, 1.0f);

                foreach (Light light in stageDictionary[key])
                {
                    light.color = new Color(R, G, B);
                }
            }
        }
    }

    public void SpawnDog()
    {
        dogChaser dog = CharacterManager.Instance.dogchaser;

        if (!dog.gameObject.activeInHierarchy)  dog.gameObject.SetActive(true);
        dog.speed = 3.8f;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayBGM("BGM2");
    }
}



[Serializable]
public class StageDictionary
{
    [SerializeField] public List<StageList> stageLists = new List<StageList>();

    public Dictionary<string, List<Light>> ToDictionary()
    {
        Dictionary<string, List<Light>> newDic = new Dictionary<string, List<Light>>();

        foreach (StageList item in stageLists)
        {
            newDic.Add(item.area, item.lights);
        }

        return newDic;
    }


}

[Serializable]
public class StageList
{
    [SerializeField] public string area;
    [SerializeField] public List<Light> lights;
}