using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickTrapTrigger : MonoBehaviour
{
    [SerializeField] private GameObject stickTrap_Act;
    private StickTrap stickTrap;


    private void Start()
    {
        stickTrap = GetComponentInParent<StickTrap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (StageManager.Instance._stageLv >= stickTrap.i_limit_lv)
            {
                float f_Appearance = Random.Range(0.0f, 100.0f);
                if (StageManager.Instance._stageLv >= 10)
                {
                    if (f_Appearance <= stickTrap.f_Appearance + StageManager.Instance._stageLv)
                    {
                        if (stickTrap.b_Event)
                            stickTrap.SetActivateEvent(stickTrap_Act);
                    }
                }
                else
                {
                    if (f_Appearance <= stickTrap.f_Appearance)
                    {
                        if (stickTrap.b_Event)
                            stickTrap.SetActivateEvent(stickTrap_Act);
                    }
                }
            }
        }
    }
}
