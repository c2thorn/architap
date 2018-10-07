using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable] 
public class Item {
	public Sprite sprite;
	public string name;
	public int count;
	public string effect;
	public float effectValue;
	public int rarity;

	public Item(string name) {
		this.name = name;
		count = 1;
	}
}
