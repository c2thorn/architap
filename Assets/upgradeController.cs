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
	public GameObject individualCharacterPanel;

	public int characterAmount = 8;
	public GameObject[] characterBoards;
	public bool[] boostBought1 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought2 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought3 = new bool[] { false, false, false, false, false, false, false, false};

	public Button boostButton1;
	public Button boostButton2;
	public Button boostButton3;

	public double[] boost1Price = new double[] {100, 250, 25000, 2500000, 2500000, 2500000, 2500000, 2500000};
	public double[] boost2Price = new double[] {1000, 2500, 250000, 25000000, 25000000, 25000000, 25000000, 25000000};
	public double[] boost3Price = new double[] {10000, 25000, 2500000, 250000000, 250000000, 250000000, 250000000, 250000000};

	public int[] boostLevelRequirements = new int[] { 10, 20, 40};

	public double[] boostValues = new double[] { 0.5, 1, 2};
	public GameObject tabs;
	public GameObject statsPanel;
	public GameObject diamondCountText;
	public int[] multiLevelUpValues;
	public int currentMultiLevelUpIndex;
	public Image[] characterPanels = new Image[]{};
	public Color gildColor;
	public GameObject toolTip;
	public bool toolTipShowing = false;
	public Vector3 toolTipOffset;
	public ItemController itemController;
	public achievementController achievementController;

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
		individualCharacterPanel.SetActive(false);
		diamondCountText.SetActive(false);
		multiLevelButton.SetActive(false);
		currentMultiLevelUpIndex = 0;
		multiLevelButton.GetComponentInChildren<Text>().text = "x"+multiLevelUpValues[currentMultiLevelUpIndex];
		for (int i = 0; i < characterPanels.Length; i++){ 
			Text gildText = characterPanels[i].transform.Find("Gilds Text").GetComponent<Text>();
			gildText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[i]);
			gildText.gameObject.SetActive(controller.characterGilds[i] > 1);
		}
		toolTipShowing = false;
		toolTip.SetActive(false);
	}

	public void restart() {
		for(int i = 0; i < characterAmount; i++) {

			if (i != 0)
				characterBoards[i].SetActive(false);
			controller.RecalculateUnit(i);
		}	
	}

	public void UndoBoosts() {
		for(int i = 0; i < characterAmount; i++) {
			if (boostBought1[i]) {
				boostBought1[i] = false;
				controller.unitM1[i] -= boostValues[0];
				characterBoards[i].get
				GameObject boostObject = characterBoards[i].gameObject.Find("Boost 1");
				SetBoostImageToNormal(boostButtons1[i]);
			}
			if (boostBought2[i]) {
				boostBought2[i] = false;
				controller.unitM1[i] -= boostValues[1];
				SetBoostImageToNormal(boostButtons2[i]);
			}
			if (boostBought3[i]) {
				boostBought3[i] = false;
				controller.unitM1[i] -= boostValues[2];
				SetBoostImageToNormal(boostButtons3[i]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < characterAmount; i++) {
			if (boostButtons1[i].gameObject.active && controller.gold >= boost1Price[i] && !boostBought1[i])
				boostButtons1[i].interactable = true;
			if (boostButtons2[i].gameObject.active && controller.gold >= boost2Price[i] && !boostBought2[i])
				boostButtons2[i].interactable = true;
			if (boostButtons3[i].gameObject.active && controller.gold >= boost3Price[i] && !boostBought3[i])
				boostButtons3[i].interactable = true;
		}

	}
	
	public void RefreshCharacterBoard(int i) {
		string preText = i == 0 ? "Hero Level: " : "Partner "+i+" Level: ";
		int characterLevel = controller.characterLevel[i];
		controller.characterLevelText[i].text = preText+characterLevel;

		if (i != 0 || characterLevel > 1)
			controller.RecalculateCharacterUpgradeCost(i);
		if (characterLevel >= boostLevelRequirements[0]) 
			enableBoost1(i);
		if (characterLevel >= boostLevelRequirements[1]) 
			enableBoost2(i);
		if (characterLevel >= boostLevelRequirements[2]) 
			enableBoost3(i);
		if ((i != 0 && characterLevel > 0 ) || characterLevel > 1)
			enableBoard(i);
		if ( boostBought1[i]){
			Button 
			SetBoostButtonToBought(boostButtons1[i]);
		}
		if ( boostBought2[i]){
			SetBoostButtonToBought(boostButtons2[i]);
		}
		if ( boostBought3[i]){
			SetBoostButtonToBought(boostButtons3[i]);
		}

		Text gildText = characterPanels[i].transform.Find("Gilds Text").GetComponent<Text>();
		if (controller.characterGilds[i] > 0) 
			characterPanels[i].color = gildColor;
		
		gildText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[i]);
		gildText.gameObject.SetActive(controller.characterGilds[i] > 1);
	}

	public void RefreshCharacterPanel(int i) {
		string preText = i == 0 ? "Hero Level: " : "Partner "+i+" Level: ";
		int characterLevel = controller.characterLevel[i];
		controller.characterLevelText[i].text = preText+characterLevel;

		if (i != 0 || characterLevel > 1)
			controller.RecalculateCharacterUpgradeCost(i);
		if (characterLevel >= boostLevelRequirements[0]) 
			enableBoost1(i);
		if (characterLevel >= boostLevelRequirements[1]) 
			enableBoost2(i);
		if (characterLevel >= boostLevelRequirements[2]) 
			enableBoost3(i);
		if ((i != 0 && characterLevel > 0 ) || characterLevel > 1)
			enableBoard(i);
		if ( boostBought1[i]){
			SetBoostButtonToBought(boostButtons1[i]);
		}
		if ( boostBought2[i]){
			SetBoostButtonToBought(boostButtons2[i]);
		}
		if ( boostBought3[i]){
			SetBoostButtonToBought(boostButtons3[i]);
		}

		Text gildText = characterPanels[i].transform.Find("Gilds Text").GetComponent<Text>();
		if (controller.characterGilds[i] > 0) 
			characterPanels[i].color = gildColor;
		
		gildText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[i]);
		gildText.gameObject.SetActive(controller.characterGilds[i] > 1);
	}

	public void enableBoard(int i) {		
		if (i+1 < characterAmount && !characterBoards[i+1].active)
			characterBoards[i+1].SetActive(true);
	}

	public void enableBoost1(int index) {
		boostButtons1[index].gameObject.SetActive(true);
		boostButtons1[index].interactable = false;
	}
	public void enableBoost2(int index) {
		boostButtons2[index].gameObject.SetActive(true);
		boostButtons2[index].interactable = false;
	}
	public void enableBoost3(int index) {
		boostButtons3[index].gameObject.SetActive(true);
		boostButtons3[index].interactable = false;
	}

	public void buyBoost1(int i) {
		boostBought1[i] = true;
		controller.gold -= boost1Price[i];
		controller.unitM1[i] += boostValues[0];
		controller.RecalculateUnit(i);
		boostButtons1[i].interactable = false;
		SetBoostButtonToBought(boostButtons1[i]);
	}

	public void buyBoost2(int i) {
		boostBought2[i] = true;
		controller.gold -= boost2Price[i];
		controller.unitM1[i] += boostValues[1];
		controller.RecalculateUnit(i);
		boostButtons2[i].interactable = false;
		SetBoostButtonToBought(boostButtons2[i]);
	}

	public void buyBoost3(int i) {
		boostBought3[i] = true;
		controller.gold -= boost3Price[i];
		controller.unitM1[i] += boostValues[2];
		controller.RecalculateUnit(i);
		boostButtons3[i].interactable = false;
		SetBoostButtonToBought(boostButtons3[i]);
	}

	public void SetBoostImageToBought(Image image) {
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			image.color = newCol;
	}

	
	public void SetBoostImageToNormal(Image image) {
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#FFFFFF", out newCol))
			image.color = newCol;
	}

	public void goldTab() {
		goldPanel.SetActive(true);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(false);
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
		individualCharacterPanel.SetActive(false);
		diamondButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void itemTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(true);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(false);
		itemButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void mapTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(true);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(false);
		mapButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void achievementsTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(true);
		individualCharacterPanel.SetActive(false);
		achievementsButton.gameObject.GetComponent<tabButton>().stopNotification();
	}

	public void openCharacterPanel() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(true);

		boostButton1.gameObject.SetActive(false);
		boostButton1.interactable = false;
		boostButton1.transform.Find("Text").GetComponent<Text>().text = NumberFormat.format(boost1Price[i]) + "g";
		boostButton1.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[0]*100) + "%";
		boostButton2.gameObject.SetActive(false);
		boostButton2.interactable = false;
		boostButton2.transform.Find("Text").GetComponent<Text>().text = NumberFormat.format(boost2Price[i]) + "g";
		boostButton2.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[1]*100) + "%";
		boostButton3.gameObject.SetActive(false);
		boostButton3.interactable = false;
		boostButton3.transform.Find("Text").GetComponent<Text>().text = NumberFormat.format(boost3Price[i]) + "g";
		boostButton3.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[2]*100) + "%";
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

	public void showToolTip(int i) {
		if (controller.characterLevel[i] > 0) {
			if (toolTipShowing) {
				toolTipShowing = false;
				toolTip.SetActive(false);
			} else {
				if (!controller.modalOpen) {
					toolTipShowing = true;
					toolTip.SetActive(true);
					toolTip.transform.position = Input.mousePosition + toolTipOffset;
					Text text = toolTip.transform.Find("Text").GetComponent<Text>();
					text.text = "Base " + NumberFormat.format(controller.baseLevelUnits[i]) 
					+ " x" + Math.Round(controller.unitM1[i],2) + " Upgrades"
					+ (itemController.inventory.Count > 0 ? " x" + Math.Round(controller.unitItemM2[i],2) + " Items" : "")
					+ (achievementController.achievements.Count > 0 ? " x" + Math.Round(controller.unitAchievementM3[i],2) + " Achievements" : "")
					+ (controller.characterGilds[i] > 0 ? " x" + Math.Round(controller.characterGilds[i]+1,2) + " Gilds" : "")
					+ (controller.prestigeCurrency > 0 ? " x" + Math.Round(1+(controller.prestigeCurrency/100),2) + " Prestige" : "");
				}
			}
		}
	}

	public void hideToolTip() {
		toolTipShowing = false;
		toolTip.SetActive(false);
	}
}
