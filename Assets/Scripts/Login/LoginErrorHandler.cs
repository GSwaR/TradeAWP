using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginErrorHandler : MonoBehaviour
{
    public GameObject Errorspace;
    public Text ErrorText;

    public void Handle(string errorText)
    {
        ErrorText.text = errorText;
        Errorspace.SetActive(true);
    }
}
