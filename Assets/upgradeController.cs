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
	public Button achievementsButton;
	public GameObject multiLevelButton;
	public GameObject goldPanel;
	public GameObject diamondPanel;
	public GameObject itemPanel;
	public GameObject mapPanel;
	public GameObject achievementsPanel;

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
	public int[] multiLevelUpValues;
	public int currentMultiLevelUpIndex;

	// Use this for initialization
	void Start () {
		restart();
		diamondButton.gameObject.SetActive(false);
		itemButton.gameObject.SetActive(false);
		mapButton.gameObject.SetActive(false);
		achievementsButton.gameObject.SetActive(false);
		if (controller.diamonds < 1)
			diamondPanel.SetActive(false);
		goldPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		tabs.SetActive(false);
		statsPanel.SetActive(false);
		diamondCountText.SetActive(false);
		multiLevelButton.SetActive(false);
		currentMultiLevelUpIndex = 0;
		multiLevelButton.GetComponentInChildren<Text>().text = "x"+multiLevelUpValues[currentMultiLevelUpIndex];
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
		SetBoostButtonToBought(boost1[i]);
	}

	public void buyBoost2(int i) {
		boostBought2[i] = true;
		controller.gold -= boost2Price[i];
		controller.unitM1[i] += .5f;
		controller.RecalculateUnit(i);
		boost2[i].interactable = false;
		SetBoostButtonToBought(boost2[i]);
	}

	public void buyBoost3(int i) {
		boostBought3[i] = true;
		controller.gold -= boost3Price[i];
		controller.unitM1[i] += 1f;
		controller.RecalculateUnit(i);
		boost3[i].interactable = false;
		SetBoostButtonToBought(boost3[i]);
	}

	public void SetBoostButtonToBought(Button button) {
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			button.GetComponent<Image>().color = newCol;
	}

	public void goldTab() {
		goldPanel.SetActive(true);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		// goldButton.gameObject.GetComponent<tabButton>().stopNotification();
	}
	public void resetScroll() {
		goldPanel.GetComponentInChildren<Scrollbar>().value = 1;
		diamondPanel.GetComponentInChildren<Scrollbar>().value = 1;
		itemPanel.GetComponentInChildren<Scrollbar>().value = 1;
		mapPanel.GetComponentInChildren<Scrollbar>().size = (float)Math.Round(mapPanel.GetComponentInChildren<Scrollbar>().size,1);
		mapPanel.GetComponentInChildren<Scrollbar>().value = 0;
		achievementsPanel.GetComponentInChildren<Scrollbar>().value = 0;
	}
	public void diamondTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(true);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		diamondButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void itemTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(true);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		itemButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void mapTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(true);
		achievementsPanel.SetActive(false);
		mapButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void achievementsTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(true);
		achievementsButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void enableGoldButton() {
		if (!goldPanel.active){
			goldPanel.SetActive(true);
			tabs.SetActive(true);
			statsPanel.SetActive(true);
		}
	}

	public void enableDiamondButton(bool notify) {
		if (!diamondButton.gameObject.active){{}
			diamondButton.gameObject.SetActive(true);
			diamondCountText.SetActive(true);
			if (notify)
				diamondButton.gameObject.GetComponent<tabButton>().startNotification();
		}
	}
	public void enableItemButton(bool notify) {
		if (!itemButton.gameObject.active) {
			itemButton.gameObject.SetActive(true);
		}
		if (notify)
			itemButton.GetComponent<tabButton>().startNotification();
	}

	public void enableMapButton(bool notify) {
		if (!mapButton.gameObject.active){
			mapButton.gameObject.SetActive(true);
		}
		if (notify)
			mapButton.gameObject.GetComponent<tabButton>().startNotification();
	}
	public void enableAchievementsButton(bool notify) {
		if (!achievementsButton.gameObject.active){
			achievementsButton.gameObject.SetActive(true);
		}
		if (notify)
			achievementsButton.gameObject.GetComponent<tabButton>().startNotification();
	}

	public void enableMultiLevelUpButton() {
		multiLevelButton.SetActive(true);
	}

	public void changeMultiLevelUp() {
		currentMultiLevelUpIndex++;
		if (currentMultiLevelUpIndex == multiLevelUpValues.Length )
			currentMultiLevelUpIndex = 0;
		multiLevelButton.GetComponentInChildren<Text>().text = "x"+multiLevelUpValues[currentMultiLevelUpIndex];
		for (int i = 0; i < controller.levelUpButton.Length; i++)
			controller.RecalculateCharacterUpgradeCost(i);
	}
}
