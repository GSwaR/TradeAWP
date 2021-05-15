using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public ConnectionManager ConnectionManager;
    public string NotTargets;

    public void Awake()
    {
        Debug.LogError(ConnectionManager.Membership == "reader");
        if (ConnectionManager.Membership == "reader")
        {
            gameObject.SetActive(false);
        }
    }
}
