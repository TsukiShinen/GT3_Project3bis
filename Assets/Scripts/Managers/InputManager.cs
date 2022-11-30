using Complete;
using Engine.Utils;
using Library.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<MonoBehaviour>
{
    [SerializeField] private GameManagerSO gameManagerSO;
    
    protected override void Update()
    {
        if (Input.GetButton("Fire"))
        {
            CommandManager.Instance.AddCommand(new FireCommand(gameManagerSO.tankPlayer));
        }

        if (Input.GetAxis("Vertical") != 0) 
        {
            CommandManager.Instance.AddCommand(new MoveCommand(gameManagerSO.tankPlayer, Mathf.Sign(Input.GetAxis("Vertical"))));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            CommandManager.Instance.AddCommand(new TurnCommand(gameManagerSO.tankPlayer, Mathf.Sign(Input.GetAxis("Horizontal"))));
        }

    }
}
