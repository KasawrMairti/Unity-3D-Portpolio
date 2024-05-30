using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Spooky : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;

    private bool[] _eventScene = { false, false, false, false };

    public bool b_SkipEvent = false;

    // Audio
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CharacterManager.Instance.spooky = this;

        _textMeshPro = UIManager.Instance.U_TextSay;
    }

    
    private void Update()
    {
        if (!b_SkipEvent) eventSurprise();
        else _textMeshPro.text = "";
    }

    private void eventSurprise()
    {
        void event_end()
        {
            if (!b_SkipEvent)
            {
                audioSource.Stop();
                _textMeshPro.text = "";
                gameObject.SetActive(false);
            }
        }
        void event_8th()
        {
            if (!b_SkipEvent)
            {
                audioSource.clip = audioClip[7];
                _textMeshPro.text = "Anyway, just.. just go.";
                DOVirtual.DelayedCall(7.2f, event_end, false);
                audioSource.Play();

                _eventScene[3] = true;
            }
        }
        void event_7th()
        {
            if (!b_SkipEvent)
            {
                audioSource.clip = audioClip[6];
                _textMeshPro.text = "Cuz i don't really know.";
                DOVirtual.DelayedCall(2.352f, event_8th, false);
                audioSource.Play();
            }
        }
        void event_6th()
        {
            if (!b_SkipEvent)
            {
                audioSource.clip = audioClip[5];
                _textMeshPro.text = "Or is there even an end?";
                DOVirtual.DelayedCall(1.224f, event_7th, false);
                audioSource.Play();
            }
        }
        void event_5th()
        {
            if (!b_SkipEvent)
            {
                audioSource.clip = audioClip[4];
                _textMeshPro.text = "Can you find, what lies at the end?";
                DOVirtual.DelayedCall(2.352f, event_6th, false);
                audioSource.Play();
            }
        }
        void event_4th()
        {
            if (!b_SkipEvent)
            {
                audioSource.clip = audioClip[3];
                _textMeshPro.text = "make it through a thousand rooms?";
                DOVirtual.DelayedCall(2.184f, event_5th, false);
                audioSource.Play();
            }
        }
        void event_3rd()
        {
            if (!b_SkipEvent)
            {
                audioSource.Stop();
                audioSource.clip = audioClip[2];
                _textMeshPro.text = "Can you, humble player,";
                DOVirtual.DelayedCall(1.92f, event_4th, false);
                audioSource.Play();
            }
        }
        void event_2nd()
        {
            if (!b_SkipEvent)
            {
                audioSource.Stop();
                audioSource.clip = audioClip[1];
                _textMeshPro.text = "And this is my home.";
                DOVirtual.DelayedCall(3.144f, event_3rd, false);
                audioSource.Play();
            }
        }
        void event_1st()
        {
            if (!b_SkipEvent)
            {
                audioSource.Stop();
                audioSource.clip = audioClip[0];
                _textMeshPro.text = "Hello, I am Spooky!";
                DOVirtual.DelayedCall(8.184f, event_2nd, false);
                DOVirtual.DelayedCall(3.0f, event_FirstText, false);
                audioSource.Play();
            }
        }
        void event_FirstText()
        {
            _textMeshPro.text = "";
        }
        
        if (!_eventScene[0])
        {
            _eventScene[0] = true;

            
            event_1st();
        }
        else if (!_eventScene[1])
        {
            if (transform.position.z > -97.6f)
                transform.Translate(0.0f, 0.0f, -1.0f * Time.deltaTime * 10.0f);
            else _eventScene[1] = true;
        }

        if (_eventScene[3])
        {
            if (transform.position.y < 67.5f)
            {
                transform.Translate(0.0f, 1.0f * Time.deltaTime * 10.0f, 0.0f);
            }
            else _eventScene[3] = false;
        }
    }
}
