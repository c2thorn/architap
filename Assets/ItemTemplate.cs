using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable] 
public class ItemTemplate {
	public Sprite sprite;
	public string name;
	public string effect;
	public float[] effectValue = new float[3];
	public string flavorText;
}
