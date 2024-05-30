using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

public class dogChaser : MonoBehaviour
{
    
    // Variable
    private Player player;
    
    public float speed = 5.0f;



    private void Awake()
    {
        CharacterManager.Instance.dogchaser = this;
    }

    private void Start()
    {
        player = CharacterManager.Instance.player;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        move();
    }

    private void move()
    {
        Vector3 pos = player.transform.position - transform.position;
        
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.LookRotation(pos.normalized);

        if (speed < 8.0f) speed += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.b_CanMove = false;

            gameObject.SetActive(false);

            UIManager.Instance.jumpScareEvent.jumpScareEvent();
        }
    }
}
