using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class upgradeController : MonoBehaviour {
	public controller controller;
	public Button diamondButton;
	public Button itemButton;
	public Button mapButton;
	public GameObject goldPanel;
	public GameObject diamondPanel;
	public GameObject itemPanel;
	public GameObject mapPanel;

	public int characterAmount = 8;
	public GameObject[] characterBoards;
	public bool[] boostBought1 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought2 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought3 = new bool[] { false, false, false, false, false, false, false, false};

	public Button[] boost1;
	public Button[] boost2;
	public Button[] boost3;

	public int[] boost1Price = new int[] {100, 250, 25000, 2500000, 2500000, 2500000, 2500000, 2500000};
	public int[] boost2Price = new int[] {1000, 2500, 250000, 25000000, 25000000, 25000000, 25000000, 25000000};
	public int[] boost3Price = new int[] {10000, 25000, 2500000, 250000000, 250000000, 250000000, 250000000, 250000000};
	public GameObject tabs;
	public GameObject statsPanel;
	public GameObject diamondCountText;

	// Use this for initialization
	void Start () {
		restart();
		diamondButton.gameObject.SetActive(false);
		itemButton.gameObject.SetActive(false);
		mapButton.gameObject.SetActive(false);
		if (controller.diamonds < 1)
			diamondPanel.SetActive(false);
		goldPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		tabs.SetActive(false);
		statsPanel.SetActive(false);
		diamondCountText.SetActive(false);
	}

	public void restart() {
		for(int i = 0; i < characterAmount; i++) {
			boost1[i].gameObject.SetActive(false);
			boost1[i].interactable = false;
			boost2[i].gameObject.SetActive(false);
			boost2[i].interactable = false;
			boost3[i].gameObject.SetActive(false);
			boost3[i].interactable = false;
			if (i != 0)
				characterBoards[i].SetActive(false);
		}	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < characterAmount; i++) {
			if (boost1[i].gameObject.active && controller.gold >= boost1Price[i] && !boostBought1[i])
				boost1[i].interactable = true;
			if (boost2[i].gameObject.active && controller.gold >= boost2Price[i] && !boostBought2[i])
				boost2[i].interactable = true;
			if (boost3[i].gameObject.active && controller.gold >= boost3Price[i] && !boostBought3[i])
				boost3[i].interactable = true;
		}

	}

	public void enableBoard(int i) {		
		if (i+1 < characterAmount && !characterBoards[i+1].active)
			characterBoards[i+1].SetActive(true);
	}

	public void enableBoost1(int index) {
		boost1[index].gameObject.SetActive(true);
		boost1[index].interactable = false;
	}
	public void enableBoost2(int index) {
		boost2[index].gameObject.SetActive(true);
		boost2[index].interactable = false;
	}
	public void enableBoost3(int index) {
		boost3[index].gameObject.SetActive(true);
		boost3[index].interactable = false;
	}

	public void buyBoost1(int i) {
		boostBought1[i] = true;
		controller.gold -= boost1Price[i];
		controller.unitM1[i] += .25f;
		controller.RecalculateUnit(i);
		boost1[i].interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			boost1[i].GetComponent<Image>().color = newCol;
	}

	public void buyBoost2(int i) {
		boostBought2[i] = true;
		controller.gold -= boost2Price[i];
		controller.unitM1[i] += .5f;
		controller.RecalculateUnit(i);
		boost2[i].interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			boost2[i].GetComponent<Image>().color = newCol;
	}

	public void buyBoost3(int i) {
		boostBought3[i] = true;
		controller.gold -= boost3Price[i];
		controller.unitM1[i] += 1f;
		controller.RecalculateUnit(i);
		boost3[i].interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			boost3[i].GetComponent<Image>().color = newCol;
	}

	public void goldTab() {
		goldPanel.SetActive(true);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
	}
	public void resetScroll() {
		goldPanel.GetComponentInChildren<Scrollbar>().value = 1;
		diamondPanel.GetComponentInChildren<Scrollbar>().value = 1;
		itemPanel.GetComponentInChildren<Scrollbar>().value = 1;
		mapPanel.GetComponentInChildren<Scrollbar>().size = (float)Math.Round(mapPanel.GetComponentInChildren<Scrollbar>().size,1);
		mapPanel.GetComponentInChildren<Scrollbar>().value = 0;
	}
	public void diamondTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(true);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
	}

	public void itemTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(true);
		mapPanel.SetActive(false);
	}

	public void mapTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(true);
	}

	public void enableGoldButton() {
		if (!goldPanel.active){
			goldPanel.SetActive(true);
			tabs.SetActive(true);
			statsPanel.SetActive(true);
		}
	}

	public void enableDiamondButton() {
		if (!diamondButton.gameObject.active){{}
			diamondButton.gameObject.SetActive(true);
			diamondCountText.SetActive(true);
		}
	}
	public void enableItemButton() {
		if (!itemButton.gameObject.active) {
			itemButton.gameObject.SetActive(true);
		}
	}

	public void enableMapButton() {
		if (!mapButton.gameObject.active)
			mapButton.gameObject.SetActive(true);
	}
}
