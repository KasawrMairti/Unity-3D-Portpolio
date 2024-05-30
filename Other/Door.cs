using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;

    private void Start()
    {
        CharacterManager.Instance.door = this;
    }

    public void MaterialSwap(string name)
    {
        if (name == "highlight")
        {
            if (gameObject.GetComponent<Renderer>().material != highlightMaterial)
                gameObject.GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {
            if (gameObject.GetComponent<Renderer>().material != defaultMaterial)
                gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }
    }
}
