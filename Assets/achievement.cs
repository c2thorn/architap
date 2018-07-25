using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class achievement {
	public string name;
	public string requirement;
	public double requirementValue;
	public string effect;
	public float effectValue;
	public bool completed;

	public achievement(string name) {
		this.name = name;
	}
}
