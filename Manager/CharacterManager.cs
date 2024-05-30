using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : GameManager<CharacterManager>
{
    public Player player { get; set; }

    public Spooky spooky { get; set; }

    public dogChaser dogchaser { get; set; }

    public Door door { get; set; }

    public List<StickTrap> stickTrap { get; set; }

    protected override void Awake()
    {
        base.Awake();

        stickTrap = new List<StickTrap>();
    }
}
