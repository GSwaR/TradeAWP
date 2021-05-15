using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSuccessHandler : MonoBehaviour
{
    public GameObject Workspace;
    public GameObject LoginSpace;

    public void Handle()
    {
        Workspace.SetActive(true);
        LoginSpace.SetActive(false);
    }
}
