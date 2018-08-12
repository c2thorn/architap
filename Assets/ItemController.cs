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
	public GameObject itemModal;


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
			itemIcon.GetComponent<RectTransform>().anchoredPosition = pos;
			itemIcon.transform.SetParent(itemContent.transform, false);
			setItemIcon(itemIcon,inventory[i]);
		}
	}

	public void setItemIcon(GameObject itemIcon, Item item) {
		switch (item.rarity) {
				case 0:
					itemIcon.GetComponent<Image>().color = new Color(152,152,152,255);
					break;
				case 1:
					itemIcon.GetComponent<Image>().color = Color.cyan;
					break;
				case 2:
					itemIcon.GetComponent<Image>().color = Color.yellow;
					break;
			}
		// itemIcon.GetComponent<RectTransform>().position = pos;
		foreach (Transform child in itemIcon.transform) {
			GameObject obj = child.gameObject;
			if(obj.name == "Item Name") {
				obj.GetComponent<Text>().text = item.name;
			}
			else if (obj.name == "Item Count") {
				obj.GetComponent<Text>().text = item.count.ToString();
			} else if (obj.name == "Item Description") {
				obj.GetComponent<Text>().text = item.effect + " + " + item.effectValue*100 + "%";
			}
		}
	}

	public void showItemModal(Item item) {
		itemModal.SetActive(true);
		GameObject itemSlot = GameObject.Find("Item Modal Slot");
		setItemIcon(itemSlot,item);
	}

	public void closeItemModal() {
		itemModal.SetActive(false);
		itemDrop = false;
	}

	public void showItem(Item item) {
		uController.enableItemButton();
		refreshInventoryUI();
		controller.RecalculateItemMultipliers();
		showItemModal(item);
	}

	public void addItemToInventory(Item item) {
		bool found = false;
		foreach (Item i in inventory) {
			if (i.name.Equals(item.name)) {
				found = true;
				i.count++;
			}
		}
		if (!found)
			inventory.Add(item);
	}

	public Item getCurrentBossItem() {
		float itemCategoryVal = Random.value;
		if (itemCategoryVal <= .5f) {
			float val = Random.value;
			if (val <= .6f) {
				return createFromName("Common Gloves");
			} else if (val <= .9f) {
				return createFromName("Rare Gloves");
			} else {
				return createFromName("Legendary Gloves");
			}
		} else {
			float val = Random.value;
			if (val <= .6f) {
				return createFromName("Common Hammer");
			} else if (val <= .9f) {
				return createFromName("Rare Hammer");
			} else {
				return createFromName("Legendary Hammer");
			}	
		}
	

	}

	public Item createFromName(string name) {
		if (name.Length == 0)
			return null;
		Item item = new Item(name);
		switch (name) {
			case "Common Gloves":
				item.effect = "partners";
				item.effectValue = 0.15f;
				item.rarity = 0;
				break;
			case "Rare Gloves":
				item.effect = "partners";
				item.effectValue = 0.25f;
				item.rarity = 1;
				break;
			case "Legendary Gloves":
				item.effect = "partners";
				item.effectValue = 0.5f;
				item.rarity = 2;
				break;
			case "Common Hammer":
				item.effect = "unitIndex0";
				item.effectValue = 0.15f;
				item.rarity = 0;
				break;
			case "Rare Hammer":
				item.effect = "unitIndex0";
				item.effectValue = 0.25f;
				item.rarity = 1;
				break;
			case "Legendary Hammer":
				item.effect = "unitIndex0";
				item.effectValue = 0.5f;
				item.rarity = 2;
				break;
			default:
				return null;
		}
		return item;
	}
}
