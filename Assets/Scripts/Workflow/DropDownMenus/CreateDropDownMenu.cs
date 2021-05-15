using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateDropDownMenu : MonoBehaviour
{
    public StateManager StateManager;
    public Dropdown DropdownMenu;
    public State[] StateToSet;

    void Start()
    {
        DropdownMenu.onValueChanged.AddListener(delegate { OnValueChanged(DropdownMenu); });
    }

    void OnValueChanged(Dropdown change)
    {
        for (int i = 0; i < StateToSet.Length; i++)
        {
            if (i == change.value)
            {
                StateManager.SetCurrentState(StateToSet[i]);
            }
        }

    }

}
