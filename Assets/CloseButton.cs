using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button button;
    public GameObject WindowToClose;

    public void Awake()
    {
        button.onClick.AddListener(Close);
    }

    public void Close()
    {
        WindowToClose.SetActive(false);
    }
}
