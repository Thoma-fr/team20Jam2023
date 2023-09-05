using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG1_BaloonParent : MonoBehaviour
{
    public void Checkcolor(Color color)
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<SpriteRenderer>();
        }

    }
}
