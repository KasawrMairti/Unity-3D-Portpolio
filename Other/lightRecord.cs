using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRecord : MonoBehaviour
{
    [SerializeField] private StageDictionary stageList;

    private void Start()
    {
        StageManager.Instance.stageDictionary = stageList.ToDictionary();
    }
}
