using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button Button;
    public ConnectionManager ConnectionManager;

    void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (ConnectionManager.Connection != null)
        {
            ConnectionManager.CloseConnection();
        }

        Application.Quit();
    }
}
