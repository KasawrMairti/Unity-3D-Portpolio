using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickTrap : MonoBehaviour
{
    private GameObject o_stick;
    [SerializeField] private Renderer[] renderer;

    [SerializeField] private Texture[] o_Sprite;

    // Variable
    public uint i_limit_lv = 2;
    public float f_Appearance = 5.0f;

    public bool b_Event { get; private set; }

    private void Start()
    {
        CharacterManager.Instance.stickTrap.Add(this);

        b_Event = true;
    }

    public void SetActivateEvent(GameObject o_object)
    {
        b_Event = false;

        o_object.SetActive(true);
        o_stick = o_object;

        int count = Random.Range(0, o_Sprite.Length);
        o_object.transform.Find("Image").transform.Find("Front").GetComponent<Renderer>().material.SetTexture("_MainTex", o_Sprite[count]);
        o_object.transform.Find("Image").transform.Find("Back").GetComponent<Renderer>().material.SetTexture("_MainTex", o_Sprite[count]);


        SoundManager.Instance.PlaySE("Sound2");
    }

    public void HideObject()
    {
        foreach (Renderer texture in renderer)
        {
            texture.material.SetTexture("_MainTex", null);
        }

        if (o_stick != null) o_stick.SetActive(false);
        b_Event = true;
    }
}
