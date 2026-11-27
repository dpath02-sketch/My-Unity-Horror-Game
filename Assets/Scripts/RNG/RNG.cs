using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RNG
{
    public T WeightedChoice<T>(List<T> choices, List<int> weights)
    {
        int roll = UnityEngine.Random.Range(0, weights.Sum()) + 1;
        int bar = 0;
        for (int i = 0; i < weights.Count; i += 1)
        {
            bar += weights[i];
            if (roll <= bar)
            {
                return choices[i];
            }
        }
        Debug.LogError("If your reading this the very concept of number logic has ceased to exist, or your really dumb.");
        return choices[0];
    }
}
