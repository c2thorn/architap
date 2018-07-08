using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

	public upgradeController uController;
	public bool itemDrop = false;
	public List<Item> inventory = new List<Item>();
	public GameObject itemPanel;
	public GameObject itemContent;
	public GameObject itemSlotPrefab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void refreshInventoryUI() {
		foreach (Transform child in itemContent.transform) {
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < inventory.Count; i++) {
			float x = i % 2 > 0 ? 160f : -160f;
			float y = (i / 2) * 200f;
			GameObject itemIcon = (GameObject) Instantiate(itemSlotPrefab,new Vector3(x,y,0f),Quaternion.Euler(0, 0, 0), itemContent.transform);
		}
	}

	public void addItem(Item item) {
		bool found = false;
		foreach (Item i in inventory) {
			if (i.name.Equals(item.name)) {
				found = true;
				i.count++;
			}
		}
		if (!found)
			inventory.Add(item);
		uController.enableItemButton();
	}

	public Item getCurrentBossItem() {
		Item item = new Item("Hammer");
		return item;
	}
}
