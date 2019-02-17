using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using Firebase;
using Firebase.Analytics;
public class ItemController : MonoBehaviour {

	public controller controller;
	public upgradeController uController;
	public bool itemDrop = false;
	public List<Item> inventory = new List<Item>();
	public GameObject itemPanel;
	public GameObject itemContent;
	public GameObject itemSlotPrefab;
	public GameObject itemModal;
	public ItemTemplate[] characterPool;
	public ItemTemplate[] ancientPool;
	public ItemTemplate[] modernPool;
	public bool modern = false;
	//public int level = 1;
	//public int highestLevel = 1;
	//public int levelCount = 1;
	//public int levelMaxCount = 10;
	//public Text levelText = null;

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
			float y = i * -160f - 80;
			Vector3 pos = new Vector3(0,y,0f);
			GameObject itemIcon = (GameObject) Instantiate(itemSlotPrefab,pos,Quaternion.Euler(0, 0, 0));
			itemIcon.GetComponent<RectTransform>().localPosition = pos;
			itemIcon.transform.SetParent(itemContent.transform, false);
			setItemIcon(itemIcon,inventory[i]);
		}
		RectTransform rect = itemContent.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(rect.sizeDelta.x, 160*inventory.Count);

	}

	public void setItemIcon(GameObject itemIcon, Item item) {
		String rarityText = "";
		switch (item.rarity) {
				case 0:
					// itemIcon.transform.Find("Profile Circle").GetComponent<SVGImage>().color = new Color(152,152,152,255);
					itemIcon.transform.Find("Profile Circle").GetComponent<SVGImage>().color = Color.gray;
					rarityText = "Common";
					break;
				case 1:
					itemIcon.transform.Find("Profile Circle").GetComponent<SVGImage>().color = Color.cyan;
					rarityText = "Rare";
					break;
				case 2:
					itemIcon.transform.Find("Profile Circle").GetComponent<SVGImage>().color = Color.yellow;
					rarityText = "Legendary";
					break;
			}
		foreach (Transform child in itemIcon.transform) {
			GameObject obj = child.gameObject;
			if(obj.name == "Item Name") {
				obj.GetComponent<Text>().text = rarityText + " " + item.name;
			}
			else if (obj.name == "Item Count") {
				obj.GetComponent<Text>().text = "x"+item.count.ToString();
			} else if (obj.name == "Item Description") {
				if (item.effectValue < 0)
					obj.GetComponent<Text>().text = item.effect + "\n- " + item.effectValue*-100 + "%";
				else if (item.effect != "Boss Timer")
					obj.GetComponent<Text>().text = item.effect + "\n+ " + item.effectValue*100 + "%";
				else
					obj.GetComponent<Text>().text = item.effect + "\n+ " + item.effectValue + " seconds";

			}
			else if (obj.name == "Profile Circle") {
				child.Find("Character Image").GetComponent<SVGImage>().m_Sprite = item.sprite;
				RectTransform rectTransform = child.Find("Character Image").GetComponent<RectTransform>();
				Rect spriteRect = item.sprite.rect;
				float denom = spriteRect.width > spriteRect.height ? 60f / spriteRect.width : 60f / spriteRect.height;
				rectTransform.sizeDelta = new Vector2(spriteRect.width*denom, spriteRect.height*denom);
			}
		}
	}

	public void showItemModal(Item item) {
		itemModal.SetActive(true);
		controller.activateModal();
		GameObject itemSlot = GameObject.Find("Item Modal Slot");
		setItemIcon(itemSlot,item);
	}

	public void closeItemModal() {
		itemModal.SetActive(false);
		controller.closeModal();
		itemDrop = false;
	}

	public void showItem(Item item) {
		uController.enableItemButton(true);
		refreshInventoryUI();
		controller.RecalculateItemMultipliers();
		showItemModal(item);
	}

	public void addItemToInventory(Item item) {
		bool found = false;
		foreach (Item i in inventory) {
			if (i.name.Equals(item.name) && i.rarity == item.rarity) {
				found = true;
				i.count++;
				// Log an event 
				Debug.Log("Logging a duplicate item unlock event.");
				FirebaseAnalytics.LogEvent(
				"item_duplicate",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name),
				new Parameter(FirebaseAnalytics.ParameterItemVariant, item.rarity));
				if(item.rarity==0){
					Debug.Log("logging dup common item found)");
					FirebaseAnalytics.LogEvent(
				"item_common_duplicate",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name));
				}
				if(item.rarity==1){
					Debug.Log("logging dup rare item found)");
					FirebaseAnalytics.LogEvent(
				"item_rare_duplicate",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name));
				}
				if(item.rarity==2){
					Debug.Log("logging dup legendary item found)");
					FirebaseAnalytics.LogEvent(
				"item_legendary_duplicate",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name));
				}
			}
		}
		if (!found)
			inventory.Add(item);
			    
			// Log an event 
			Debug.Log("Logging a new item unlock event.");
			FirebaseAnalytics.LogEvent(
				"item_new",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name),
				new Parameter(FirebaseAnalytics.ParameterItemVariant, item.rarity));
				if(item.rarity==0){
					Debug.Log("logging common item found)");
					FirebaseAnalytics.LogEvent(
				"item_common_new",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name));
				}
				if(item.rarity==1){
					Debug.Log("logging rare item found)");
					FirebaseAnalytics.LogEvent(
				"item_rare_new",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name));
				}
				if(item.rarity==2){
					Debug.Log("logging legendary item found)");
					FirebaseAnalytics.LogEvent(
				"item_legendary_new",
				//new Parameter(FirebaseAnalytics.ParameterLevel, level),
				new Parameter(FirebaseAnalytics.ParameterItemName, item.name));
				}
	}

	public Item getRandomItem() {
		int characterUnlockedSize = 0;
		foreach (bool bought in controller.characterEverBought){
			if (bought)
				characterUnlockedSize++;
		}
		int poolSize = modern ? ancientPool.Length + modernPool.Length - 1 : ancientPool.Length - 0;
		poolSize += characterUnlockedSize;
		System.Random r = new System.Random();
		int index = r.Next(0, poolSize);
		if (index < characterUnlockedSize)
			return createFromTemplate(characterPool[index]);
		else if (index < ancientPool.Length + characterUnlockedSize)
			return createFromTemplate(ancientPool[index - characterUnlockedSize]);
		else
			return createFromTemplate(modernPool[index-ancientPool.Length - characterUnlockedSize]);
	}

	public Item createFromTemplate(ItemTemplate template) {
		int rarity = 0;

		float itemRarityChance = UnityEngine.Random.value;
		if (itemRarityChance <= .6f) 
			rarity = 0;
		else if (itemRarityChance <= 0.9f)
			rarity = 1;
		else
			rarity = 2;

		Item item = new Item(template.name);
		item.effect = template.effect;
		item.effectValue = template.effectValue[rarity];
		item.count = 1;
		item.rarity = rarity;
		item.sprite = template.sprite;

		return item;
	}

	public Item createFromName(string input) {
		try {
			if (input.Length == 0)
					return null;
				string name = input;
				int count = 1;
				int rarity = 0;
				if (input.Contains("$")) {
					name = input.Split('$')[0];
					count = int.Parse(input.Split('$')[1]);
					rarity = int.Parse(input.Split('$')[2]);
				}

				Item item = new Item(name);
				item.count = count;
				item.rarity = rarity;
				foreach (ItemTemplate template in ancientPool) {
					if (name.Equals(template.name)) {
						item.effect = template.effect;
						item.effectValue = template.effectValue[rarity];
						item.sprite = template.sprite;
						break;
					}
				}
				if (item.sprite == null) {
					foreach (ItemTemplate template in characterPool) {
						if (name.Equals(template.name)) {
							item.effect = template.effect;
							item.effectValue = template.effectValue[rarity];
							item.sprite = template.sprite;
							break;
						}
					}
				}
				if (item.sprite == null) {
					foreach (ItemTemplate template in modernPool) {
						if (name.Equals(template.name)) {
							item.effect = template.effect;
							item.effectValue = template.effectValue[rarity];
							item.sprite = template.sprite;
							break;
						}
					}
				}

				return item;
		} catch (Exception e) {
			Debug.LogError(e.StackTrace);
			return null;
		}
	
	}
}
