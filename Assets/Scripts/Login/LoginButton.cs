using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Net.Sockets;

public class LoginButton : MonoBehaviour
{
    public Button ButtonToLogIn;
    public Text Username;
    public InputField Password;
    public Text CustomDataSource;

    public ConnectionManager ConnectionManager;

    void Start()
    {
        ButtonToLogIn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        ConnectionManager.SetConnection(user:Username.text, password:Password.text, CustomDataSource.text);
    }
}

