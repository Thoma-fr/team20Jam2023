using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG2_Gauge : MonoBehaviour
{
    private Transform _greenZone;
    private Transform _indicator;

    private void Awake()
    {
        _greenZone = transform.Find("GreenZone");
        _indicator = transform.Find("Indicator");
    }
}
