using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSettings : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
