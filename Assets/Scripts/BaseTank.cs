﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTank : MonoBehaviour {

	public float health = 100f;

	public float GetHealth(){
		return health;
	}

	public void TakeDamage(float amount){
		health -= amount;
	}
}
