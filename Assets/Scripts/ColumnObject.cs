using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnObject : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
