using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class SetIaOrPlayer : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] GameManagerSO gamemanager;
    private void Start()
    {
        gamemanager.isPlayer = dropdown.value == 1;
    }
    public void SetIaPlayer() {
        gamemanager.isPlayer = dropdown.value == 1;
    }

}
