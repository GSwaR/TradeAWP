using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialiseManager : MonoBehaviour
{
    public Text UserName;
    public Text ManagerID;

    private void Start()
    {
        ManagerID.text = UserName.text;
    }

}
