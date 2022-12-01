using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class SetIaOrPlayer : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] GameManagerSO gamemanager;
    private void Start()
    {
        dropdown.value = gamemanager.isPlayer ? 1 : 0;
    }
    public void SetIaPlayer() {
        gamemanager.isPlayer = dropdown.value == 1;
    }

}
