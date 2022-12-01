using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class SetIaOrPlayer : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] GameManagerSO gamemanager;
    public void SetIaPlayer() {
        gamemanager.isPlayer = dropdown.value == 1;
    }

}
