using ScriptableObjects.PathFinding;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetGameParameters : MonoBehaviour
{
    [SerializeField] private List<PathFindingSO> allPathFinding;
    
    [SerializeField] private TMP_Dropdown dropdownPlayerOrAI;
    [SerializeField] private TMP_Dropdown dropdownAlgo;
    [SerializeField] private Toggle toggleFunMode;

    [SerializeField] private TankParametersSO tankParametersSO;
    [SerializeField] private GameManagerSO gamemanager;
    
    private void Awake()
    {
        dropdownPlayerOrAI.value = gamemanager.isPlayer ? 1 : 0;

        dropdownAlgo.ClearOptions();
        for (int i = 0; i < allPathFinding.Count; i++)
        {
            var newOoption = new TMP_Dropdown.OptionData();
            newOoption.text = allPathFinding[i].name;
            dropdownAlgo.options.Add(newOoption);

            if (tankParametersSO.PathFinding == allPathFinding[i])
            {
                dropdownAlgo.value = i;
            }
        }

        toggleFunMode.isOn = gamemanager.gameParametersSo.funMode;
    }

    public void SetPlayerOrAI()
    {
        gamemanager.isPlayer = dropdownPlayerOrAI.value == 1;
    }

    public void ChangeAlgo()
    {
        tankParametersSO.PathFinding = allPathFinding[dropdownAlgo.value];
    }

    public void setFunMode(bool isFunMode)
    {
        gamemanager.gameParametersSo.funMode = isFunMode;
    }
}
