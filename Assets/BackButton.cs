using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(0);
        }
        else {
            StateManager.SetCurrentState(StateManager.MenuState);
        } 
    }
}
