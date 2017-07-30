using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PowerUpEntry
{
	public Stat Stat;
	public ApplyType ApplyType;
	public float Amount;
}