using System;
using UnityEngine;

[Serializable]
public class RandomValueInt
{
    [SerializeField] private int min;
    [SerializeField] private int max;
    public float Max => max;
    public float Min => min;

    public RandomValueInt(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public int GetValue()
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static implicit operator int(RandomValueInt randomValue) { 
        return randomValue.GetValue();
    }
}

[Serializable]
public class RandomValueFloat
{
    [SerializeField] private float min;
    [SerializeField] private float max;

    public float Max => max;
    public float Min => min;

    public RandomValueFloat(float min, float max)
    {
        this.min = min; 
        this.max = max;
    }

    public float GetValue()
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static implicit operator float(RandomValueFloat randomValue)
    {
        return randomValue.GetValue();
    }
}

public static class Chance
{
    public static bool Roll(float chance)
    {
        return UnityEngine.Random.value < chance;
    }
}