using ScriptableObjects;
using ScriptableObjects.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAlgo : MonoBehaviour
{
    [SerializeField] private List<PathFindingSO> allPathFinding;
    [SerializeField] private TankParametersSO tankParametersSO;
    [SerializeField] private Dropdown dropdown;

    private void Start()
    {
        dropdown.ClearOptions();

        for(int i = 0; i < allPathFinding.Count; i++)
        {
            var newOoption = new Dropdown.OptionData();
            newOoption.text = allPathFinding[i].name;
            dropdown.options.Add(newOoption);
            
            if(tankParametersSO.PathFinding == allPathFinding[i])
            {
                dropdown.value = i;
            }
        }
    }

    public void ChangeAlgo()
    {
        tankParametersSO.PathFinding = allPathFinding[dropdown.value];
    }
}
