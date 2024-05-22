using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TimeTickSystemDataHandler
{
    public static event Action<uint> OnTick;
    public static void Tick(this TimeTickSystem timeTickSystem, uint tick) => OnTick?.Invoke(tick);
}