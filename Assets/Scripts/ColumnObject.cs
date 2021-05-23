using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnObject : MonoBehaviour
{
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}
