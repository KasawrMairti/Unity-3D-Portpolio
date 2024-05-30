using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GameManager<UIManager>
{
    [SerializeField] private Image image;
    [SerializeField] private Slider sliderRun;
    [SerializeField] private Image sliderImage;
    [SerializeField] private GameObject buttenE;
    [SerializeField] private Image hideInOut;
    [SerializeField] private TextMeshProUGUI U_TMPro;
    public TextMeshProUGUI U_TextSay;

    public JumpScareEvent jumpScareEvent;

    // Variable
    private float time = 0.0f;
    private float timeMax = 0.0f;
    private float hideAlpha = 1.0f;
    private bool hideIn = false;

    public float HideAlpha
    {
        get
        {
            return hideAlpha;
        }
    }

    private void Update()
    {
        SetStageText();

        hideAlpha = hideInOut.color.a;

        if (time < timeMax)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0.0f;

            if (hideIn)
            {
                if (hideAlpha < 1.0f)
                {
                    hideAlpha += Time.deltaTime;

                    hideInOut.color = new Color(0.0f, 0.0f, 0.0f, hideAlpha);
                }
            }
            else
            {
                if (hideAlpha > 0.0f)
                {
                    hideAlpha -= Time.deltaTime;

                    hideInOut.color = new Color(0.0f, 0.0f, 0.0f, hideAlpha);
                }
            }
        }
    }

    public void ShowImage(Sprite images, float time = 0.0f, string sound = "Sound3")
    {
        image.sprite = images;
        image.enabled = true;

        if (time != 0.0f)
        {
            DOVirtual.DelayedCall(time, HideImage, false);
            SoundManager.Instance.PlaySE(sound);
        }
    }

    public void HideImage()
    {
        image.enabled = false;
    }

    public void Rungauge(float gauge)
    {
        sliderRun.value = gauge;


        sliderImage.color = new Color(1, gauge, gauge);
    }

    public void ButtenEnable(bool boolean)
    {
        buttenE.SetActive(boolean);
    }

    public void HideIn(float time = 0.0f)
    {
        this.time = 0.0f;
        this.timeMax = time;
        hideIn = true;
    }

    public void HideOut(float time = 0.0f)
    {
        this.time = 0.0f;
        this.timeMax = time;
        hideIn = false;
    }

    public void SetStageText()
    {
        U_TMPro.text = StageManager.Instance._stageLv + " Stage";
    }
}
