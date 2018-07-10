using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemController : MonoBehaviour {

	public controller controller;
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
			float y = 425 - (i / 2) * 200f;
			Vector3 pos = new Vector3(x,y,0f);
			GameObject itemIcon = (GameObject) Instantiate(itemSlotPrefab,pos,Quaternion.Euler(0, 0, 0));
			itemIcon.transform.SetParent(itemContent.transform, false);
			itemIcon.GetComponent<RectTransform>().anchoredPosition = pos;

			// itemIcon.GetComponent<RectTransform>().position = pos;
			foreach (Transform child in itemIcon.transform) {
				GameObject obj = child.gameObject;
				if(obj.name == "Item Name") {
					obj.GetComponent<Text>().text = inventory[i].name;
				}
				else if (obj.name == "Item Count") {
					obj.GetComponent<Text>().text = inventory[i].count.ToString();
				}
			}
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
		refreshInventoryUI();
		controller.RecalculateItemMultipliers();
	}

	public Item getCurrentBossItem() {
		Item item = new Item("Hammer");
		item.effect = "partners";
		item.effectValue = 0.5f;
		return item;
	}
}
