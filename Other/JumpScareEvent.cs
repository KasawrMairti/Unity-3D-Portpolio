using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

public class JumpScareEvent : MonoBehaviour
{
    private Animator anim;
    private AnimatorPro _animatorPro;

    private float _resetGame = 0.0f;
    private bool b_resertGame = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        _animatorPro = GetComponent<AnimatorPro>();
        _animatorPro.Init(anim);
    }

    private void Update()
    {
        if (_resetGame > 0.0f)
        {
            _resetGame -= Time.deltaTime;

            StageManager.Instance.StageLoadingPositionEvent();

            Debug.Log("reset");
        }
        else
        {
            if (b_resertGame)
            {
                b_resertGame = false;

                CharacterManager.Instance.player.b_CanMove = true;

                DOVirtual.DelayedCall(6.0f, StageManager.Instance.SpawnDog, false);
                UIManager.Instance.HideOut();

            }
        }
    }

    public void jumpScareEvent()
    {
        _animatorPro.SetParam("_Event", true);

    }

    public void jumpScareEvnetEnd()
    {

        _animatorPro.SetParam("_Event", false);

        UIManager.Instance.HideIn();

        b_resertGame = true;
        _resetGame = 1.0f;

        foreach (var stickTrap in CharacterManager.Instance.stickTrap)
        {
            stickTrap.HideObject();
        }
    }
}
