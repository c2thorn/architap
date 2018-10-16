﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class upgradeController : MonoBehaviour {
	public controller controller;
	public Button goldButton;
	public Button diamondButton;
	public Button itemButton;
	public Button mapButton;
	public Button architectButton;
	public Button skillTabButton;
	public Button achievementsButton;
	public GameObject multiLevelButton;
	public GameObject goldPanel;
	public GameObject diamondPanel;
	public GameObject itemPanel;
	public GameObject mapPanel;
	public GameObject achievementsPanel;
	public GameObject individualCharacterPanel;
	public GameObject skillPanel;
	public GameObject navigationArea;
	public GameObject panelArea;


	public int characterAmount = 8;
	public GameObject[] characterBoards;
	public bool[] boostBought1 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought2 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought3 = new bool[] { false, false, false, false, false, false, false, false};

	public Button boostButton1;
	public Button boostButton2;
	public Button boostButton3;
	public Button skillBuyButton;

	public Text boost1Description;
	public Text boost2Description;
	public Text boost3Description;
	public Text skillDescription;
	public SVGImage boost1ImageCircle;
	public SVGImage boost2ImageCircle;
	public SVGImage boost3ImageCircle;
	public SVGImage skillImageCircle;

	// public Text characterPanelLevelText;
	public Text characterPanelGildsText;
	public Text characterPanelHeroText;


	public double[] boost1Price = new double[] {100, 250, 25000, 2500000, 2500000, 2500000, 2500000, 2500000};
	public double[] boost2Price = new double[] {1000, 2500, 250000, 25000000, 25000000, 25000000, 25000000, 25000000};
	public double[] boost3Price = new double[] {10000, 25000, 2500000, 250000000, 250000000, 250000000, 250000000, 250000000};
	public double[] skillPrice = new double[] {2000, 5000, 500000, 50000000, 50000000, 50000000, 50000000, 50000000};

	public int[] boostLevelRequirements = new int[] { 10, 20, 40, 25};

	public double[] boostValues = new double[] { 0.5, 1, 2};
	public GameObject tabs;
	public GameObject statsPanel;
	public GameObject currencyPanel;
	public GameObject diamondCountText;
	public int[] multiLevelUpValues;
	public int currentMultiLevelUpIndex;
	public Color gildColor;
	public GameObject toolTip;
	public bool toolTipShowing = false;
	public Vector3 toolTipOffset;
	public ItemController itemController;
	public achievementController achievementController;
	public int selectedCharacter;
	public UIClickAudio uiClickAudio;
	public Text percentageText;
	public int currencyPanelIndex = 0;
	public CharacterAudio characterAudio;
	public SkillController skillController;

	public SwipeCapture swipeCapture;
	public GameObject individualCharacterBreakdown;

	// Use this for initialization
	void Start () {
		currencyPanelIndex = 0;
		restart();
		goldButton.gameObject.SetActive(false);
		diamondButton.gameObject.SetActive(false);
		itemButton.gameObject.SetActive(false);
		mapButton.gameObject.SetActive(false);
		achievementsButton.gameObject.SetActive(false);
		skillTabButton.gameObject.SetActive(false);
		if (controller.diamonds < 1)
			diamondPanel.SetActive(false);
		goldPanel.SetActive(false);
		itemPanel.SetActive(false);
		mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		skillPanel.SetActive(false);
		statsPanel.SetActive(false);
		currencyPanel.SetActive(false);
		// navigationArea.SetActive(false);
		panelArea.SetActive(false);
		individualCharacterPanel.SetActive(false);
		diamondCountText.SetActive(false);
		multiLevelButton.SetActive(false);
		currentMultiLevelUpIndex = 0;
		multiLevelButton.GetComponentInChildren<Text>().text = "x"+multiLevelUpValues[currentMultiLevelUpIndex];
		for (int i = 0; i < characterBoards.Length; i++){ 
			Text gildText = characterBoards[i].transform.Find("Gilds Text").GetComponent<Text>();
			gildText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[i]);
			gildText.gameObject.SetActive(controller.characterGilds[i] > 1);
		}
		toolTipShowing = false;
		toolTip.SetActive(false);
	}

	public void restart() {
		for(int i = 0; i < characterAmount; i++) {
			GameObject boostObject1 = characterBoards[i].transform.Find("Boost 1").gameObject;
			GameObject boostObject2 = characterBoards[i].transform.Find("Boost 2").gameObject;
			GameObject boostObject3 = characterBoards[i].transform.Find("Boost 3").gameObject;
			GameObject skillObject = characterBoards[i].transform.Find("Skill").gameObject;

			boostObject1.gameObject.SetActive(false);
			// boostObject1.transform.Find("Text").GetComponent<Text>().text = NumberFormat.format(boost1Price[i]) + "g";
			// boostObject1.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[0]*100) + "%";
			boostObject2.gameObject.SetActive(false);
			// boostObject2.transform.Find("Text").GetComponent<Text>().text = NumberFormat.format(boost2Price[i]) + "g";
			// boostObject2.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[1]*100) + "%";
			boostObject3.gameObject.SetActive(false);
			// boostObject3.transform.Find("Text").GetComponent<Text>().text = NumberFormat.format(boost3Price[i]) + "g";
			// boostObject3.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[2]*100) + "%";
			skillObject.gameObject.SetActive(false);
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
				GameObject boostObject = characterBoards[i].transform.Find("Boost 1").gameObject;
				SetBoostImageToNormal(boostObject.GetComponent<SVGImage>());
			}
			if (boostBought2[i]) {
				boostBought2[i] = false;
				controller.unitM1[i] -= boostValues[1];
				GameObject boostObject = characterBoards[i].transform.Find("Boost 2").gameObject;
				SetBoostImageToNormal(boostObject.GetComponent<SVGImage>());
			}
			if (boostBought3[i]) {
				boostBought3[i] = false;
				controller.unitM1[i] -= boostValues[2];
				GameObject boostObject = characterBoards[i].transform.Find("Boost 3").gameObject;
				SetBoostImageToNormal(boostObject.GetComponent<SVGImage>());
			}
			if (skillController.skillsBought[skillController.keys[i]]) {
				skillController.skillsBought[skillController.keys[i]] = false;
				GameObject skillObject = characterBoards[i].transform.Find("Skill").gameObject;
				SetBoostImageToNormal(skillObject.GetComponent<SVGImage>());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (individualCharacterPanel.gameObject.activeSelf){
			boostButton1.interactable = boostButton1.gameObject.active && controller.gold >= boost1Price[selectedCharacter] && !boostBought1[selectedCharacter];
			boostButton2.interactable = boostButton2.gameObject.active && controller.gold >= boost2Price[selectedCharacter] && !boostBought2[selectedCharacter];
			boostButton3.interactable = boostButton3.gameObject.active && controller.gold >= boost3Price[selectedCharacter] && !boostBought3[selectedCharacter];
			skillBuyButton.interactable = skillBuyButton.gameObject.active && controller.gold >= skillPrice[selectedCharacter] 
																&& !skillController.skillsBought[skillController.keys[selectedCharacter]];
		}
	}
	
	public void RefreshCharacterBoard(int i) {
		int characterLevel = controller.characterLevel[i];
		controller.characterUnitLevelText[i].text = characterLevel > 0 ? "LEVEL: " + characterLevel + 
		" UNITS: "+NumberFormat.format(controller.units[i]): "";

		if (i != 0 || characterLevel > 1)
			controller.RecalculateCharacterUpgradeCost(i);
		if (characterLevel >= boostLevelRequirements[0]) 
			enableBoost1(i);
		if (characterLevel >= boostLevelRequirements[1]) 
			enableBoost2(i);
		if (characterLevel >= boostLevelRequirements[2]) 
			enableBoost3(i);
		if (characterLevel >= boostLevelRequirements[3])
			enableSkill(i);
		if ((i != 0 && characterLevel > 0 ) || characterLevel > 1)
			enableBoard(i);
		if ( boostBought1[i]){
			GameObject boostObject = characterBoards[i].transform.Find("Boost 1").gameObject;
			SetBoostImageToBought(boostObject.GetComponent<SVGImage>());
		}
		if ( boostBought2[i]){
			GameObject boostObject = characterBoards[i].transform.Find("Boost 2").gameObject;
			SetBoostImageToBought(boostObject.GetComponent<SVGImage>());		
		}
		if ( boostBought3[i]){
			GameObject boostObject = characterBoards[i].transform.Find("Boost 3").gameObject;
			SetBoostImageToBought(boostObject.GetComponent<SVGImage>());		
		}
		if ( skillController.skillsBought[skillController.keys[i]]){
			GameObject skillObject = characterBoards[i].transform.Find("Skill").gameObject;
			SetBoostImageToBought(skillObject.GetComponent<SVGImage>());		
		}

		Text gildText = characterBoards[i].transform.Find("Gilds Text").GetComponent<Text>();
		if (controller.characterGilds[i] > 0) 
			characterBoards[i].GetComponent<Image>().color = gildColor;
		
		gildText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[i]);
		gildText.gameObject.SetActive(controller.characterGilds[i] > 0);
	}

	public void RefreshCharacterPanel() {
		characterPanelHeroText.text = selectedCharacter == 0 ? "Hero" : "Partner "+selectedCharacter;
		boostButton1.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(boost1Price[selectedCharacter]) + "g";
		boost1Description.text = "+"+(boostValues[0]*100) + "%";
		boostButton2.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(boost2Price[selectedCharacter]) + "g";
		boost2Description.text = "+"+(boostValues[1]*100) + "%";
		boostButton3.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(boost3Price[selectedCharacter]) + "g";
		boost3Description.text = "+"+(boostValues[2]*100) + "%";
		skillBuyButton.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(boost3Price[selectedCharacter]) + "g";
		// skillBuyButton.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[2]*100) + "%";

		int characterLevel = controller.characterLevel[selectedCharacter];
		// characterPanelLevelText.text = "Level: "+characterLevel;

		boostButton1.gameObject.SetActive(characterLevel >= boostLevelRequirements[0]);
		boostButton2.gameObject.SetActive(characterLevel >= boostLevelRequirements[1]);
		boostButton3.gameObject.SetActive(characterLevel >= boostLevelRequirements[2]);
		skillBuyButton.gameObject.SetActive(characterLevel >= boostLevelRequirements[3]);

		Update();

		if (selectedCharacter != 0 || characterLevel > 1)
			controller.RecalculateCharacterUpgradeCost(selectedCharacter);
		if (selectedCharacter != 0) {
			percentageText.gameObject.SetActive(true);
			double percentage = Math.Round(controller.units[selectedCharacter]/controller.sumofAllUnits,4)*100;
			percentageText.text = percentage + "% of UPS";
		}
		else{
			percentageText.gameObject.SetActive(false);
		}
		SetBoostImageCircle(boost1ImageCircle, boostBought1[selectedCharacter]);
		SetBoostImageCircle(boost2ImageCircle, boostBought2[selectedCharacter]);
		SetBoostImageCircle(boost3ImageCircle, boostBought3[selectedCharacter]);
		SetBoostImageCircle(skillImageCircle, skillController.skillsBought[skillController.keys[selectedCharacter]]);

		if (controller.characterGilds[selectedCharacter] > 0) 
			individualCharacterPanel.GetComponent<Image>().color = gildColor;
		
		characterPanelGildsText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[selectedCharacter]);
		characterPanelGildsText.gameObject.SetActive(controller.characterGilds[selectedCharacter] > 1);

		ShowIndividualBreakdown();
	}

	public void enableBoard(int i) {		
		if (i+1 < characterAmount && !characterBoards[i+1].active)
			characterBoards[i+1].SetActive(true);
	}

	public void enableBoost1(int index) {
		GameObject boostObject = characterBoards[index].transform.Find("Boost 1").gameObject;
		boostObject.SetActive(true);
		boostButton1.gameObject.SetActive(true);
		boostButton1.interactable = false;
	}
	public void enableBoost2(int index) {
		GameObject boostObject = characterBoards[index].transform.Find("Boost 2").gameObject;
		boostObject.SetActive(true);
		boostButton2.gameObject.SetActive(true);
		boostButton2.interactable = false;
	}
	public void enableBoost3(int index) {
		GameObject boostObject = characterBoards[index].transform.Find("Boost 3").gameObject;
		boostObject.SetActive(true);
		boostButton3.gameObject.SetActive(true);
		boostButton3.interactable = false;
	}
	public void enableSkill(int index) {
		GameObject skillObject = characterBoards[index].transform.Find("Skill").gameObject;
		skillObject.SetActive(true);
		skillBuyButton.gameObject.SetActive(true);
		skillBuyButton.interactable = false;	
	}

	public void buyBoost1() {
		boostBought1[selectedCharacter] = true;
		controller.gold -= boost1Price[selectedCharacter];
		controller.unitM1[selectedCharacter] += boostValues[0];
		controller.RecalculateUnit(selectedCharacter);
		boostButton1.interactable = false;
		SetBoostImageCircle(boost1ImageCircle,true);
		GameObject boostObject = characterBoards[selectedCharacter].transform.Find("Boost 1").gameObject;
		SetBoostImageToBought(boostObject.GetComponent<SVGImage>());
		characterAudio.BoostSound();
		ShowIndividualBreakdown();
	}

	public void buyBoost2() {
		boostBought2[selectedCharacter] = true;
		controller.gold -= boost2Price[selectedCharacter];
		controller.unitM1[selectedCharacter] += boostValues[1];
		controller.RecalculateUnit(selectedCharacter);
		boostButton2.interactable = false;
		SetBoostImageCircle(boost2ImageCircle,true);
		GameObject boostObject = characterBoards[selectedCharacter].transform.Find("Boost 2").gameObject;
		SetBoostImageToBought(boostObject.GetComponent<SVGImage>());
		characterAudio.BoostSound();
		ShowIndividualBreakdown();
	}

	public void buyBoost3() {
		boostBought3[selectedCharacter] = true;
		controller.gold -= boost3Price[selectedCharacter];
		controller.unitM1[selectedCharacter] += boostValues[2];

		controller.RecalculateUnit(selectedCharacter);
		boostButton3.interactable = false;
		SetBoostImageCircle(boost3ImageCircle,true);
		GameObject boostObject = characterBoards[selectedCharacter].transform.Find("Boost 3").gameObject;
		SetBoostImageToBought(boostObject.GetComponent<SVGImage>());
		characterAudio.BoostSound();
		ShowIndividualBreakdown();
	}

	public void BuyIndividualCharacterSkill() {
		skillController.BuySkill(skillController.keys[selectedCharacter]);
		enableSkillButton(false);
		SetBoostImageCircle(skillImageCircle,true);
		GameObject skillObject = characterBoards[selectedCharacter].transform.Find("Skill").gameObject;
		SetBoostImageToBought(skillObject.GetComponent<SVGImage>());
		characterAudio.BoostSound();
		ShowIndividualBreakdown();
		// RefreshCharacterPanel();
		// RefreshCharacterBoard(selectedCharacter);
	}

	public void SetBoostImageToBought(SVGImage image) {
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#FFFFFF", out newCol))
			if (image.color != newCol)
				image.color = newCol;
	}

	
	public void SetBoostImageToNormal(SVGImage image) {
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#5A5A5A", out newCol))
			if (image.color != newCol)
				image.color = newCol;
	}

	public void SetBoostImageCircle(SVGImage circle, bool bought) {
		Color newCol;

		SVGImage image = circle.transform.Find("Boost Image").GetComponent<SVGImage>();

		if (bought) {
			if (ColorUtility.TryParseHtmlString("#EAEAEA", out newCol))
				if (circle.color != newCol)
					circle.color = newCol;
			if (ColorUtility.TryParseHtmlString("#FFFFFF", out newCol))
				if (image.color != newCol)
					image.color = newCol;
		}
		else {
			if (ColorUtility.TryParseHtmlString("#686868", out newCol))
				if (circle.color != newCol)
					circle.color = newCol;
			if (ColorUtility.TryParseHtmlString("#5A5A5A", out newCol))
				if (image.color != newCol)
					image.color = newCol;
		}
	}

	public void ChangeTabIndicators(int i) {
		goldButton.transform.Find("Indicator").gameObject.SetActive(i == 0);
		diamondButton.transform.Find("Indicator").gameObject.SetActive(i == 1);
		itemButton.transform.Find("Indicator").gameObject.SetActive(i == 2);
		achievementsButton.transform.Find("Indicator").gameObject.SetActive(i == 3);
		skillTabButton.transform.Find("Indicator").gameObject.SetActive(i == 4);
		architectButton.transform.Find("Indicator").gameObject.SetActive(i == 5);
	}

	public void goldTab() {
		goldPanel.SetActive(true);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		// mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(false);
		skillPanel.SetActive(false);
		// goldButton.gameObject.GetComponent<tabButton>().stopNotification();
		uiClickAudio.tabSound();
		ChangeTabIndicators(0);
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
		// mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(false);
		skillPanel.SetActive(false);
		diamondButton.gameObject.GetComponent<tabButton>().stopNotification();
		uiClickAudio.tabSound();
		ChangeTabIndicators(1);
	}

	public void itemTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(true);
		// mapPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(false);
		skillPanel.SetActive(false);
		itemButton.gameObject.GetComponent<tabButton>().stopNotification();
		uiClickAudio.tabSound();
		ChangeTabIndicators(2);
	}

	public void mapTab() {
		// goldPanel.SetActive(false);
		// diamondPanel.SetActive(false);
		// itemPanel.SetActive(false);
		if (mapPanel.activeSelf)
			swipeCapture.SnapClose();
		else
			swipeCapture.SnapOpen();
		// achievementsPanel.SetActive(false);
		// individualCharacterPanel.SetActive(false);
		mapButton.gameObject.GetComponent<tabButton>().stopNotification();
		uiClickAudio.tabSound();
	}

	public void achievementsTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		// mapPanel.SetActive(false);
		achievementsPanel.SetActive(true);
		individualCharacterPanel.SetActive(false);
		skillPanel.SetActive(false);
		achievementsButton.gameObject.GetComponent<tabButton>().stopNotification();
		uiClickAudio.tabSound();
		ChangeTabIndicators(3);
	}

	public void skillTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(false);
		itemPanel.SetActive(false);
		achievementsPanel.SetActive(false);
		individualCharacterPanel.SetActive(false);
		skillPanel.SetActive(true);
		skillTabButton.gameObject.GetComponent<tabButton>().stopNotification();
		uiClickAudio.tabSound();
		ChangeTabIndicators(4);
	}

	public void openCharacterPanel(int i) {
		if (controller.characterLevel[i] > 0) {
			goldPanel.SetActive(false);
			diamondPanel.SetActive(false);
			itemPanel.SetActive(false);
			// mapPanel.SetActive(false);
			achievementsPanel.SetActive(false);
			controller.individualLevelUpButton.interactable = controller.gold >= controller.characterUpgradeCost[i];
			individualCharacterPanel.SetActive(true);

			selectedCharacter = i;
			RefreshCharacterPanel();
			uiClickAudio.clickSound();
		}
	}

	public void enableGoldButton() {
		if (!goldPanel.active){
			// navigationArea.SetActive(true);
			panelArea.SetActive(true);
			goldPanel.SetActive(true);
			statsPanel.SetActive(true);
			currencyPanel.SetActive(true);
			goldButton.gameObject.SetActive(true);
			SetCurrencyPanelGold();
			ChangeTabIndicators(0);
		}
	}

	public void enableDiamondButton(bool notify) {
		if (!diamondButton.gameObject.active){{}
			diamondButton.gameObject.SetActive(true);
			diamondCountText.SetActive(true);
			SetCurrencyPanelDiamond();
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
			swipeCapture.mapSwipeable = true;
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
	public void enableSkillButton(bool notify) {
		if (!skillTabButton.gameObject.active){
			skillTabButton.gameObject.SetActive(true);
		}
		if (notify)
			skillTabButton.gameObject.GetComponent<tabButton>().startNotification();
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
		uiClickAudio.clickSound();
	}

	public void individualCharacterLevelUp() {
		controller.levelUp(selectedCharacter);
		controller.RecalculateSumUnits();
		RefreshCharacterPanel();
		// int characterLevel = controller.characterLevel[selectedCharacter];
		// characterPanelLevelText.text = "Level: "+characterLevel;
	}

	public void ShowIndividualBreakdown() {
		int i = selectedCharacter;

		Text text = individualCharacterBreakdown.transform.Find("Text").GetComponent<Text>();
		text.text = "Base " + NumberFormat.format(controller.baseLevelUnits[i]) 
		+ " x" + Math.Round(controller.unitM1[i],2) + " Upgrades"
		+ (itemController.inventory.Count > 0 ? " x" + Math.Round(controller.unitItemM2[i],2) + " Items" : "")
		+ (achievementController.achievements.Count > 0 ? " x" + Math.Round(controller.unitAchievementM3[i],2) + " Achievements" : "")
		+ (controller.characterGilds[i] > 0 ? " x" + Math.Round(controller.characterGilds[i]+1,2) + " Gilds" : "")
		+ (controller.prestigeCurrency > 0 ? " x" + Math.Round(1+(controller.prestigeCurrency*controller.prestigeEffectItemMultiplier/100),2) 
				+ " Prestige" : "");
	}



	public void showToolTip(int i) {
		if (individualCharacterPanel.activeSelf)
			i = selectedCharacter;
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
					+ (controller.prestigeCurrency > 0 ? " x" + Math.Round(1+(controller.prestigeCurrency*controller.prestigeEffectItemMultiplier/100),2) 
							+ " Prestige" : "");
				}
			}
		}
	}

	public void hideToolTip() {
		toolTipShowing = false;
		toolTip.SetActive(false);
	}

	public void SetCurrencyPanelGold() {
		if (currencyPanelIndex < 1){
			RectTransform rectTransform = currencyPanel.GetComponent<RectTransform>();
			rectTransform.offsetMin = new Vector2(520f, rectTransform.offsetMin.y);
			currencyPanelIndex = 1;
		}
	}

	public void SetCurrencyPanelDiamond() {
		if (currencyPanelIndex < 2){
			RectTransform rectTransform = currencyPanel.GetComponent<RectTransform>();
			rectTransform.offsetMin = new Vector2(370f, rectTransform.offsetMin.y);
			currencyPanelIndex = 2;
		}
	}

	public void SetCurrencyPanelCoal() {
		if (currencyPanelIndex < 3){
			RectTransform rectTransform = currencyPanel.GetComponent<RectTransform>();
			rectTransform.offsetMin = new Vector2(220f, rectTransform.offsetMin.y);
			currencyPanelIndex = 3;
		}
	}

	public void SetCurrencyPanelPrestige() {
		if (currencyPanelIndex < 4){
			RectTransform rectTransform = currencyPanel.GetComponent<RectTransform>();
			rectTransform.offsetMin = new Vector2(-2f, rectTransform.offsetMin.y);
			currencyPanelIndex = 4;
		}
	}
}
