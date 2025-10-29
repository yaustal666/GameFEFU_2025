using System;
using UnityEngine;

[Serializable]
public class Timer
{
    [SerializeField] private float startTime = 0f;
    [SerializeField] private float duration;
    //private bool isRunning;

    public Timer(float duration)
    {
        this.duration = duration;
    }

    public void Start()
    {
        if (!IsRunning())
        {
            startTime = Time.time;
        }
    }

    public void Stop()
    {
        startTime = 0f;
    }

    public bool IsRunning()
    {
        return Time.time - startTime <= duration;
    }
}