using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Configs
{
    public float turnRateConfig;
    public float fovConfig;

    public Configs()
    {
        this.turnRateConfig = 53;
        this.fovConfig = 95; //would've done 120 but most people consider that too high so i did the middle of the slider (low as 70)
    }
}
