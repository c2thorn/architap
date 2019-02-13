using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Analytics;

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
	public RectTransform contentRect;
	public GameObject characterHeader;

	[HideInInspector]
	public string[] characterNames = new string[] {
		"The Student", "Reeda Weaver", "Billy Hammerton", "Brick Sanchez",
		"Connor Crete", "Chris Shingle", "Alena Wrench", "Dan Buldozian", "Build Nye"
	};
	public GameObject[] characterProfilePrefabs;
	public int characterAmount = 8;
	public GameObject[] characterBoards;
	public bool[] boostBought1 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought2 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought3 = new bool[] { false, false, false, false, false, false, false, false};
	[HideInInspector]
	public string[,,] boostInfo = new string[,,] {
		{{"Super Clicks",""}, { "Mega Clicks",""}, { "Ultra Clicks",""}, { "Auto Click","Automatically click while this skill is active. (10 clicks per second)"}}, 
		{{"Wet Felting",""}, { "Spinning wheel",""}, { "Friggjar rockr",""}, { "Multi-Threading","Clicks are +100% more effective while this skill is active."}},
		{{"Stronger Swing",""}, { "Harder Hit",""}, { "Hammer Tar",""}, { "Grand Slam","All builders gain +100% UPS while this skill is active."}},
		{{"Tiny Brick",""}, { "Brick Infused Mortar ",""}, { "Pickled Brick",""}, { "Gold Bricks","Gain x2 gold from buildings while this skill is active."}},
		{{"Quick Drying Bond",""}, { "Absorbing Concrete",""}, { "Reinforced Concrete ",""}, { "Crete Head","Gain +50% critical click chance while this skill is active."}},
		{{"Stronger Roof",""}, { "Nonslip Shingles",""}, { "LED Shingles",""}, { "Festive Spirit","All buildings are golden while this skill is active."}},
		{{"Angled Handle",""}, { "Hydraulics",""}, { "Extra Torque",""}, { "Efficient Tools","The effect of your next skill is doubled."}},
		{{"Better Traction",""}, { "Quick Loading",""}, { "Armored Vehicle",""}, { "Wide Load","Decrease the building requirement by 1 while this skill is active."}},
		{{"Chemical Mixture",""}, { "Automated Tools",""}, { "3D Printed Parts",""}, { "Science Rules","Reduce the cooldown of all other skills."}}
		};

	public Button boostButton1;
	public Button boostButton2;
	public Button boostButton3;
	public Button skillBuyButton;

	public Text boost1Description;
	public Text boost2Description;
	public Text boost3Description;
	public Text skillDescription;
	public Text boost1NameText;
	public Text boost2NameText;
	public Text boost3NameText;
	public Text skillNameText;
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
	public ScrollRect goldScrollRect;
	public bool multiLevelUpEnabled;
	public int[] maxLevelUpAmounts;

	// Use this for initialization
	void Start () {
		maxLevelUpAmounts = new int[characterAmount];
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
		multiLevelUpEnabled = false;
		currentMultiLevelUpIndex = 0;
		multiLevelButton.GetComponentInChildren<Text>().text = "x"+multiLevelUpValues[currentMultiLevelUpIndex];
		for (int i = 0; i < characterBoards.Length; i++){ 
			Text gildText = characterBoards[i].transform.Find("Gilds Text").GetComponent<Text>();
			gildText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[i]);
			gildText.gameObject.SetActive(controller.characterGilds[i] > 1);
		}
		toolTipShowing = false;
		toolTip.SetActive(false);
		InvokeRepeating("CalculalteMaxMultiLevelUp",Time.time,4.0f);
	}

	public void restart() {
		for(int i = 0; i < characterAmount; i++) {
			GameObject boostObject1 = characterBoards[i].transform.Find("Boost 1").gameObject;
			GameObject boostObject2 = characterBoards[i].transform.Find("Boost 2").gameObject;
			GameObject boostObject3 = characterBoards[i].transform.Find("Boost 3").gameObject;
			GameObject skillObject = characterBoards[i].transform.Find("Skill").gameObject;

			boostObject1.gameObject.SetActive(false);
			boostObject2.gameObject.SetActive(false);
			boostObject3.gameObject.SetActive(false);
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
				SetBoostImageIcon(i,"Boost 1", false);
			}
			if (boostBought2[i]) {
				boostBought2[i] = false;
				controller.unitM1[i] -= boostValues[1];
				GameObject boostObject = characterBoards[i].transform.Find("Boost 2").gameObject;
				SetBoostImageIcon(i,"Boost 2", false);
			}
			if (boostBought3[i]) {
				boostBought3[i] = false;
				controller.unitM1[i] -= boostValues[2];
				GameObject boostObject = characterBoards[i].transform.Find("Boost 3").gameObject;
				SetBoostImageIcon(i,"Boost 3", false);
			}
			if (skillController.skillsBought[skillController.keys[i]]) {
				skillController.skillsBought[skillController.keys[i]] = false;
				GameObject skillObject = characterBoards[i].transform.Find("Skill").gameObject;
				SetBoostImageIcon(i,"Skill", false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (individualCharacterPanel.gameObject.activeSelf){
			boostButton1.interactable = boostButton1.gameObject.active && controller.gold >= boost1Price[selectedCharacter] 
									&& !boostBought1[selectedCharacter] && controller.characterLevel[selectedCharacter] >= boostLevelRequirements[0];
			boostButton2.interactable = boostButton2.gameObject.active && controller.gold >= boost2Price[selectedCharacter] 
									&& !boostBought2[selectedCharacter] && controller.characterLevel[selectedCharacter] >= boostLevelRequirements[1];
			boostButton3.interactable = boostButton3.gameObject.active && controller.gold >= boost3Price[selectedCharacter] 
									&& !boostBought3[selectedCharacter] && controller.characterLevel[selectedCharacter] >= boostLevelRequirements[2];
			skillBuyButton.interactable = skillBuyButton.gameObject.active && controller.gold >= skillPrice[selectedCharacter] 
				&& !skillController.skillsBought[skillController.keys[selectedCharacter]] && controller.characterLevel[selectedCharacter] >= boostLevelRequirements[3];
		}
	}
	
	public void RefreshCharacterBoard(int i) {
		int characterLevel = controller.characterLevel[i];
		controller.characterUnitLevelText[i].text = characterLevel > 0 ? "LEVEL: " + characterLevel + 
		" UNITS: "+NumberFormat.format(controller.units[i]): "";
		int prevLevel = controller.lastLevel;
		if (i != 0 || characterLevel > 1)
			controller.RecalculateCharacterUpgradeCost(i);
		if (characterLevel >= boostLevelRequirements[0]) {
			enableBoost1(i);
			Debug.Log ("gen boost 1 available");
			FirebaseAnalytics.LogEvent("char_gen_boost_1_available");
			if(prevLevel< boostLevelRequirements[0]){
				if(i==1){
					Debug.Log ("reeda boost 1 available");
					FirebaseAnalytics.LogEvent("char_Reeda_boost_1_available");
				}
			}
		}
		if (characterLevel >= boostLevelRequirements[1]) {
			enableBoost2(i);
			Debug.Log ("gen boost 2 available");
			FirebaseAnalytics.LogEvent("char_gen_boost_2_available");
			if(prevLevel< boostLevelRequirements[0]){
				if(i==1){
					Debug.Log ("reeda boost 2 available");
					FirebaseAnalytics.LogEvent("char_Reeda_boost_2_available");
				}
			}
		}
		if (characterLevel >= boostLevelRequirements[2]) {
			enableBoost3(i);
			Debug.Log ("gen boost 3 available");
			FirebaseAnalytics.LogEvent("char_gen_boost_3_available");
			if(prevLevel< boostLevelRequirements[0]){
				if(i==1){
					Debug.Log ("reeda boost 1 available");
					FirebaseAnalytics.LogEvent("char_Reeda_boost_3_available");
				}
			}
		}
		if (characterLevel >= boostLevelRequirements[3]){
			enableSkill(i);
			Debug.Log ("gen skill available");
			FirebaseAnalytics.LogEvent("char_gen_skill_available");
			if(prevLevel< boostLevelRequirements[0]){
				if(i==1){
					Debug.Log ("reeda boost 1 available");
					FirebaseAnalytics.LogEvent("char_Reeda_skill_available");
				}
			}
		}
		if ((i != 0 && characterLevel > 0 ) || characterLevel > 1)
			enableBoard(i);
	
		SetBoostImageIcon(i,"Boost 1", boostBought1[i]);
		SetBoostImageIcon(i,"Boost 2", boostBought2[i]);
		SetBoostImageIcon(i,"Boost 3", boostBought3[i]);
		SetBoostImageIcon(i,"Skill", skillController.skillsBought[skillController.keys[i]]);

		Text gildText = characterBoards[i].transform.Find("Gilds Text").GetComponent<Text>();
		if (controller.characterGilds[i] > 0) 
			characterBoards[i].GetComponent<Image>().color = gildColor;
		
		gildText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[i]);
		gildText.gameObject.SetActive(controller.characterGilds[i] > 0);
	}

	public void RefreshCharacterPanel() {
		characterPanelHeroText.text = characterNames[selectedCharacter];

		foreach (ProfileCircle circle in characterHeader.GetComponentsInChildren<ProfileCircle>()) {
			Destroy(circle.gameObject);
		}
		GameObject characterProfileCircle = (GameObject) Instantiate(characterProfilePrefabs[selectedCharacter],
													new Vector3(0,0,0),Quaternion.Euler(0, 0, 0),characterHeader.transform);
		
		RectTransform rect = characterProfileCircle.GetComponent<RectTransform>();
		rect.localScale = new Vector3(0.75f,0.75f,1);
		rect.anchoredPosition = new Vector2(100f,0f);

		boost1NameText.text = boostInfo[selectedCharacter,0,0];
		boost2NameText.text = boostInfo[selectedCharacter,1,0];
		boost3NameText.text = boostInfo[selectedCharacter,2,0];
		skillNameText.text = boostInfo[selectedCharacter,3,0];

		string measurement = selectedCharacter == 0 ? "% units per click." : "% UPS.";

		boostButton1.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(boost1Price[selectedCharacter]);
		boost1Description.text = boostInfo[selectedCharacter,0,1] + characterNames[selectedCharacter]+" gains +"+(boostValues[0]*100) + measurement;
		boostButton2.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(boost2Price[selectedCharacter]);
		boost2Description.text = boostInfo[selectedCharacter,1,1] + characterNames[selectedCharacter]+" gains +"+(boostValues[1]*100) + measurement;
		boostButton3.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(boost3Price[selectedCharacter]);
		boost3Description.text = boostInfo[selectedCharacter,2,1] + characterNames[selectedCharacter]+" gains +"+(boostValues[2]*100) + measurement;
		skillBuyButton.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(skillPrice[selectedCharacter]);
		skillDescription.text = boostInfo[selectedCharacter,3,1];
		// skillBuyButton.transform.Find("Boost Bonus Text").GetComponent<Text>().text = "+"+(boostValues[2]*100) + "%";

		int characterLevel = controller.characterLevel[selectedCharacter];
		// characterPanelLevelText.text = "Level: "+characterLevel;

		bool isSkillBought = skillController.skillsBought[skillController.keys[selectedCharacter]];

		boostButton1.gameObject.SetActive(!boostBought1[selectedCharacter]);
		boostButton2.gameObject.SetActive(!boostBought2[selectedCharacter]);
		boostButton3.gameObject.SetActive(!boostBought3[selectedCharacter]);
		skillBuyButton.gameObject.SetActive(!isSkillBought);
		boostButton1.transform.Find("Action Text").GetComponent<Text>().text = 
						characterLevel >= boostLevelRequirements[0] ? "BUY" : "Unlock at Level "+boostLevelRequirements[0];
		boostButton1.transform.Find("Level Up Layout").Find("Price Text").gameObject.SetActive(characterLevel >= boostLevelRequirements[0]);
		boostButton1.transform.Find("Level Up Layout").Find("Coin Image").gameObject.SetActive(characterLevel >= boostLevelRequirements[0]);

		boostButton2.transform.Find("Action Text").GetComponent<Text>().text = 
						characterLevel >= boostLevelRequirements[1] ? "BUY" : "Unlock at Level "+boostLevelRequirements[1];
		boostButton2.transform.Find("Level Up Layout").Find("Price Text").gameObject.SetActive(characterLevel >= boostLevelRequirements[1]);
		boostButton2.transform.Find("Level Up Layout").Find("Coin Image").gameObject.SetActive(characterLevel >= boostLevelRequirements[1]);

		boostButton3.transform.Find("Action Text").GetComponent<Text>().text = 
						characterLevel >= boostLevelRequirements[2] ? "BUY" : "Unlock at Level "+boostLevelRequirements[2];
		boostButton3.transform.Find("Level Up Layout").Find("Price Text").gameObject.SetActive(characterLevel >= boostLevelRequirements[2]);
		boostButton3.transform.Find("Level Up Layout").Find("Coin Image").gameObject.SetActive(characterLevel >= boostLevelRequirements[2]);

		skillBuyButton.transform.Find("Action Text").GetComponent<Text>().text = 
						characterLevel >= boostLevelRequirements[3] ? "BUY" : "Unlock at Level "+boostLevelRequirements[3];
		skillBuyButton.transform.Find("Level Up Layout").Find("Price Text").gameObject.SetActive(characterLevel >= boostLevelRequirements[3]);
		skillBuyButton.transform.Find("Level Up Layout").Find("Coin Image").gameObject.SetActive(characterLevel >= boostLevelRequirements[3]);



		Update();
		

		// if (selectedCharacter != 0 || characterLevel > 1)
			controller.RecalculateCharacterUpgradeCost(selectedCharacter);
		if (selectedCharacter != 0) {
			percentageText.gameObject.SetActive(true);
			controller.RecalculateSumUnits();
			double percentage = Math.Round(controller.units[selectedCharacter]/controller.sumofAllUnits,4)*100;
			percentageText.text = percentage + "% of UPS";
		}
		else{
			percentageText.gameObject.SetActive(false);
		}
		SetBoostImageCircle(boost1ImageCircle, boostBought1[selectedCharacter]);
		SetBoostImageCircle(boost2ImageCircle, boostBought2[selectedCharacter]);
		SetBoostImageCircle(boost3ImageCircle, boostBought3[selectedCharacter]);
		SetBoostImageCircle(skillImageCircle, isSkillBought);

		if (controller.characterGilds[selectedCharacter] > 0) 
			individualCharacterPanel.GetComponent<Image>().color = gildColor;
		
		characterPanelGildsText.text = "Gilds: " + NumberFormat.format(controller.characterGilds[selectedCharacter]);
		characterPanelGildsText.gameObject.SetActive(controller.characterGilds[selectedCharacter] > 1);

		ShowIndividualBreakdown();
	}

	public void enableBoard(int i) {		
		if (i+1 < characterAmount) {
			if (!characterBoards[i+1].active){
				characterBoards[i+1].SetActive(true);
			}
			if (controller.characterLevel[i+1] == 0 || i == -1)
				contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x,Mathf.Min(1280f,(i+2)*160f));
		}
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
		SetBoostImageIcon(selectedCharacter,"Boost 1", true);
		characterAudio.BoostSound();
		RefreshCharacterPanel();
		Debug.Log("bought boost 1");
		FirebaseAnalytics.LogEvent("char_boost_1_gen_bought");
		if (selectedCharacter == 0 ){
			Debug.Log("student bought boost 1");
			FirebaseAnalytics.LogEvent("char_Student_boost_1_bought");
			}
//			else if (selectedCharacter == 1 ){
//			Debug.Log("Reeda bought boost 1");
//			FirebaseAnalytics.LogEvent("Reeda_boost_1_bought");
//			}
//			else if (selectedCharacter == 2 ){
//			Debug.Log("Billy bought boost 1");
//			FirebaseAnalytics.LogEvent("Billy_boost_1_bought");
//			}
//			else if (selectedCharacter == 3 ){
///			Debug.Log("Brick bought boost 1");
//			FirebaseAnalytics.LogEvent("Brick_boost_1_bought");
//			}
//			else if (selectedCharacter == 4 ){
//			Debug.Log("connor bought boost 1");
//			FirebaseAnalytics.LogEvent("Connor_boost_1_bought");
//			}
//			else if (selectedCharacter == 5 ){
//			Debug.Log("chris bought boost 1");
//			FirebaseAnalytics.LogEvent("Chris_boost_1_bought");
//			}
//			else if (selectedCharacter == 6 ){
//			Debug.Log("alena bought boost 1");
//			FirebaseAnalytics.LogEvent("Alena_boost_1_bought");
//			}
//			else if (selectedCharacter == 7 ){
//			Debug.Log("dan bought boost 1");
//			FirebaseAnalytics.LogEvent("Dan_boost_1_bought");
//			}
//			else if (selectedCharacter == 8 ){
//			Debug.Log("bill bought boost 1");
//			FirebaseAnalytics.LogEvent("Nye_boost_1_bought");
//			}

	}

	public void buyBoost2() {
		boostBought2[selectedCharacter] = true;
		controller.gold -= boost2Price[selectedCharacter];
		controller.unitM1[selectedCharacter] += boostValues[1];
		controller.RecalculateUnit(selectedCharacter);
		boostButton2.interactable = false;
		SetBoostImageIcon(selectedCharacter,"Boost 2", true);
		characterAudio.BoostSound();
		RefreshCharacterPanel();
		Debug.Log("bought boost 2");
		FirebaseAnalytics.LogEvent("char_gen_boost_2_bought");
		if (selectedCharacter == 0 ){
			Debug.Log("student bought boost 2");
			FirebaseAnalytics.LogEvent("char_Student_boost_2_bought");
			}
//			else if (selectedCharacter == 1 ){
//			Debug.Log("Reeda bought boost 2");
//			FirebaseAnalytics.LogEvent("Reeda_boost_2_bought");
//			}
//			else if (selectedCharacter == 2 ){
//			Debug.Log("Billy bought boost 2");
//			FirebaseAnalytics.LogEvent("Billy_boost_2_bought");
//			}
//			else if (selectedCharacter == 3 ){
//			Debug.Log("Brick bought boost 2");
//			FirebaseAnalytics.LogEvent("Brick_boost_2_bought");
//			}
//			else if (selectedCharacter == 4 ){
//			Debug.Log("connor bought boost 2");
//			FirebaseAnalytics.LogEvent("Connor_boost_2_bought");
//			}
//			else if (selectedCharacter == 5 ){
//			Debug.Log("chris bought boost 2");
//			FirebaseAnalytics.LogEvent("Chris_boost_2_bought");
//			}
//			else if (selectedCharacter == 6 ){
//			Debug.Log("alena bought boost 2");
//			FirebaseAnalytics.LogEvent("Alena_boost_2_bought");
//			}
//			else if (selectedCharacter == 7 ){
//			Debug.Log("dan bought boost 2");
//			FirebaseAnalytics.LogEvent("Dan_boost_2_bought");
//			}
//			else if (selectedCharacter == 8 ){
//			Debug.Log("bill bought boost 2");
//			FirebaseAnalytics.LogEvent("Nye_boost_2_bought");
//			}
	}

	public void buyBoost3() {
		boostBought3[selectedCharacter] = true;
		controller.gold -= boost3Price[selectedCharacter];
		controller.unitM1[selectedCharacter] += boostValues[2];
		controller.RecalculateUnit(selectedCharacter);
		boostButton3.interactable = false;
		SetBoostImageIcon(selectedCharacter,"Boost 3", true);
		characterAudio.BoostSound();
		RefreshCharacterPanel();
		Debug.Log("bought boost 3");
		FirebaseAnalytics.LogEvent("char_gen_boost_3_bought");
		if (selectedCharacter == 0 ){
			Debug.Log("student bought boost 3");
			FirebaseAnalytics.LogEvent("char_Student_boost_3_bought");
			}
//			else if (selectedCharacter == 1 ){
//			Debug.Log("Reeda bought boost 3");
//			FirebaseAnalytics.LogEvent("Reeda_boost_3_bought");
//			}
//			else if (selectedCharacter == 2 ){
//			Debug.Log("Billy bought boost 3");
//			FirebaseAnalytics.LogEvent("Billy_boost_3_bought");
//			}
//			else if (selectedCharacter == 3 ){
//			Debug.Log("Brick bought boost 3");
//			FirebaseAnalytics.LogEvent("Brick_boost_3_bought");
//			}
//			else if (selectedCharacter == 4 ){
//			Debug.Log("connor bought boost 3");
//			FirebaseAnalytics.LogEvent("Connor_boost_3_bought");
//			}
//			else if (selectedCharacter == 5 ){
//			Debug.Log("chris bought boost 3");
//			FirebaseAnalytics.LogEvent("Chris_boost_3_bought");
//			}
//			else if (selectedCharacter == 6 ){
//			Debug.Log("alena bought boost 3");
//			FirebaseAnalytics.LogEvent("Alena_boost_3_bought");
//			}
//			else if (selectedCharacter == 7 ){
//			Debug.Log("dan bought boost 3");
//			FirebaseAnalytics.LogEvent("Dan_boost_3_bought");
//			}
//			else if (selectedCharacter == 8 ){
//			Debug.Log("bill bought boost 3");
//			FirebaseAnalytics.LogEvent("Nye_boost_3_bought");
//			}
	}

	public void BuyIndividualCharacterSkill() {
		skillController.BuySkill(skillController.keys[selectedCharacter]);
		enableSkillButton(false);
		SetBoostImageIcon(selectedCharacter,"Skill", true);
		characterAudio.BoostSound();
		RefreshCharacterPanel();
		Debug.Log("bought skill");
		FirebaseAnalytics.LogEvent("char_gen_skill_bought");
		if (selectedCharacter == 0 ){
			Debug.Log("student bought skill");
			FirebaseAnalytics.LogEvent("char_Student_skill_bought");
			}
//			else if (selectedCharacter == 1 ){
//			Debug.Log("Reeda bought skill");
//			FirebaseAnalytics.LogEvent("Reeda_skill_bought");
//			}
//			else if (selectedCharacter == 2 ){
//			Debug.Log("Billy bought skill");
//			FirebaseAnalytics.LogEvent("Billy_skill_bought");
//			}
//			else if (selectedCharacter == 3 ){
//			Debug.Log("Brick bought skill");
//			FirebaseAnalytics.LogEvent("Brick_skill_bought");
//			}
//			else if (selectedCharacter == 4 ){
//			Debug.Log("connor bought skill");
//			FirebaseAnalytics.LogEvent("Connor_skill_bought");
//			}
//			else if (selectedCharacter == 5 ){
//			Debug.Log("chris bought skill");
//			FirebaseAnalytics.LogEvent("Chris_skill_bought");
//			}
//			else if (selectedCharacter == 6 ){
//			Debug.Log("alena bought skill");
//			FirebaseAnalytics.LogEvent("Alena_skill_bought");
//			}
//			else if (selectedCharacter == 7 ){
//			Debug.Log("dan bought skill");
//			FirebaseAnalytics.LogEvent("Dan_skill_bought");
//			}
//			else if (selectedCharacter == 8 ){
//			Debug.Log("bill bought skill");
//			FirebaseAnalytics.LogEvent("Nye_skill_bought");
//			}
	}

	public void SetBoostImageIcon(int i, String name, bool bought) {
		SVGImage image = characterBoards[i].transform.Find(name).GetComponent<SVGImage>();
		Color newCol;
		if (bought) {
			if (ColorUtility.TryParseHtmlString("#FFFFFF", out newCol))
				if (image.color != newCol)
					image.color = newCol;
		} else {
			if (ColorUtility.TryParseHtmlString("#5A5A5A", out newCol))
				if (image.color != newCol)
					image.color = newCol;
		}
	}

	public void SetBoostImageCircle(SVGImage circle, bool bought) {
		Color newCol;

		SVGImage image = circle.transform.Find("Boost Image").GetComponent<SVGImage>();
		Image backgroundImage = circle.transform.parent.GetComponent<Image>();

		if (bought) {
			if (ColorUtility.TryParseHtmlString("#EAEAEA", out newCol))
				if (circle.color != newCol)
					circle.color = newCol;
			if (ColorUtility.TryParseHtmlString("#FFFFFF", out newCol))
				if (image.color != newCol)
					image.color = newCol;
			if (ColorUtility.TryParseHtmlString("#FFFFFF", out newCol))
				if (backgroundImage.color != newCol)
					backgroundImage.color = newCol;

		}
		else {
			if (ColorUtility.TryParseHtmlString("#686868", out newCol))
				if (circle.color != newCol)
					circle.color = newCol;
			if (ColorUtility.TryParseHtmlString("#5A5A5A", out newCol))
				if (image.color != newCol)
					image.color = newCol;
			if (ColorUtility.TryParseHtmlString("#C8C8C8", out newCol))
				if (backgroundImage.color != newCol)
					backgroundImage.color = newCol;
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
		if (multiLevelUpEnabled)
			multiLevelButton.SetActive(true);
		ChangeTabIndicators(0);
	}
	public void resetScroll() {
		goldPanel.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1;
		diamondPanel.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1;
		itemPanel.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1;
		// mapPanel.GetComponentInChildren<Scrollbar>().size = (float)Math.Round(mapPanel.GetComponentInChildren<Scrollbar>().size,1);
		mapPanel.GetComponentInChildren<ScrollRect>().horizontalNormalizedPosition = 0;
		achievementsPanel.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1;
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
		multiLevelButton.SetActive(false);
		ChangeTabIndicators(1);
		//event opened diamond tab
			Debug.Log("event open diamond tab");
			FirebaseAnalytics.LogEvent("diamond_tab_opened");
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
		multiLevelButton.SetActive(false);
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
		multiLevelButton.SetActive(false);
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
		multiLevelButton.SetActive(false);
		ChangeTabIndicators(4);
		//get coal event
			Debug.Log("evet open skill tab");
			FirebaseAnalytics.LogEvent("skill_tab_opened");
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
			if (multiLevelUpEnabled)
				multiLevelButton.SetActive(true);
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
		multiLevelUpEnabled = true;
		if (goldPanel.activeSelf || individualCharacterPanel.activeSelf)
			multiLevelButton.SetActive(true);
	}

	public void changeMultiLevelUp() {
		currentMultiLevelUpIndex++;
		if (currentMultiLevelUpIndex == multiLevelUpValues.Length )
			currentMultiLevelUpIndex = 0;
		
		if (currentMultiLevelUpIndex == multiLevelUpValues.Length - 1) {
			CalculalteMaxMultiLevelUp();
			multiLevelButton.GetComponentInChildren<Text>().text = "MAX";
		} else {
			for (int i = 0; i < controller.levelUpButton.Length; i++)
				controller.RecalculateCharacterUpgradeCost(i);
			multiLevelButton.GetComponentInChildren<Text>().text = "x"+multiLevelUpValues[currentMultiLevelUpIndex];
		}

		if (individualCharacterPanel.activeSelf)
			RefreshCharacterPanel();
		uiClickAudio.clickSound();
	}

	public void CalculalteMaxMultiLevelUp() {
		if (currentMultiLevelUpIndex == multiLevelUpValues.Length - 1) {
			for (int i = 0; i < maxLevelUpAmounts.Length; i++) {
				double sum = 0;
				int counter = 0;
				do {
					sum += Math.Round(controller.baseCharacterUpgradeCost[i]*Math.Pow(controller.characterUpgradeCostMultiplier[i],controller.characterLevel[i]+counter));
					counter++;
				}while (sum < controller.gold);
				counter = Mathf.Max(1, counter - 1);
				maxLevelUpAmounts[i] = counter;
				controller.RecalculateCharacterUpgradeCost(i);
			}
		}
	}

	public int GetNumLevels(int i) {
		int numLevels;
		if (currentMultiLevelUpIndex == multiLevelUpValues.Length - 1)
			numLevels = maxLevelUpAmounts[i];
		else
			numLevels = multiLevelUpValues[currentMultiLevelUpIndex];
		return numLevels;
	}

	public void individualCharacterLevelUp() {
		controller.levelUp(selectedCharacter);
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
		// if (individualCharacterPanel.activeSelf)
		// 	i = selectedCharacter;
		// if (controller.characterLevel[i] > 0) {
		// 	if (toolTipShowing) {
		// 		toolTipShowing = false;
		// 		toolTip.SetActive(false);
		// 	} else {
		// 		if (!controller.modalOpen) {
		// 			toolTipShowing = true;
		// 			toolTip.SetActive(true);
		// 			toolTip.transform.position = Input.mousePosition + toolTipOffset;
		// 			Text text = toolTip.transform.Find("Text").GetComponent<Text>();
		// 			text.text = "Base " + NumberFormat.format(controller.baseLevelUnits[i]) 
		// 			+ " x" + Math.Round(controller.unitM1[i],2) + " Upgrades"
		// 			+ (itemController.inventory.Count > 0 ? " x" + Math.Round(controller.unitItemM2[i],2) + " Items" : "")
		// 			+ (achievementController.achievements.Count > 0 ? " x" + Math.Round(controller.unitAchievementM3[i],2) + " Achievements" : "")
		// 			+ (controller.characterGilds[i] > 0 ? " x" + Math.Round(controller.characterGilds[i]+1,2) + " Gilds" : "")
		// 			+ (controller.prestigeCurrency > 0 ? " x" + Math.Round(1+(controller.prestigeCurrency*controller.prestigeEffectItemMultiplier/100),2) 
		// 					+ " Prestige" : "");
		// 		}
		// 	}
		// }
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

	public void ScrollABit() {
		StartCoroutine(Scroll());
	}

	IEnumerator Scroll() {
		float timeLimit = 5f;
		float start = goldScrollRect.verticalNormalizedPosition;
		float destination = -0.08f;
		float distance = start - destination;
		float rate = .06f*(distance+.5f)/1.5f;

		while (timeLimit > 0f){
			goldScrollRect.verticalNormalizedPosition = Mathf.Lerp(goldScrollRect.verticalNormalizedPosition,destination,rate);
			timeLimit -= .1f;
			yield return null;	
		}
    }
}
