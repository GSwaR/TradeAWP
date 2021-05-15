using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button Button;
    public StateManager StateManager;
    public ConnectionManager ConnectionManager;
    public LoginButton LoginButton;

    void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (StateManager.CurrentState == StateManager.MenuState)
        {
            StateManager.SetCurrentState(StateManager.LoginState);
            ConnectionManager.CloseConnection();
            LoginButton.Username.text = "";
            LoginButton.Password.text = "";
        }
        else {
            StateManager.SetCurrentState(StateManager.MenuState);
        } 
    }
}
