using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public GameObject EnableUI;

    public virtual void EnableState()
    {
        EnableUI.SetActive(true);
    }

    public void DisableState()
    {
        EnableUI.SetActive(false);
    }
}
