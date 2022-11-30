using Library.Command;
using Library;
using ScriptableObjects;
using UnityEngine;

public class InputManager : Singleton<MonoBehaviour>
{
    [SerializeField] private GameManagerSO gameManagerSo;
    
    protected override void Update()
    {
        if (Input.GetButton("Fire"))
        {
            CommandManager.Instance.AddCommand(new FireCommand(gameManagerSo.tankPlayer));
        }

        if (Input.GetAxis("Vertical") != 0) 
        {
            CommandManager.Instance.AddCommand(new MoveCommand(gameManagerSo.tankPlayer, Mathf.Sign(Input.GetAxis("Vertical"))));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            CommandManager.Instance.AddCommand(new TurnCommand(gameManagerSo.tankPlayer, Mathf.Sign(Input.GetAxis("Horizontal"))));
        }

    }
}
