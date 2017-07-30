using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

[Serializable]
public struct PowerUpEntry {
    public StatType StatType;
    public ApplyType ApplyType;
    public float Amount;
}