﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Analytics;



public class controller : MonoBehaviour {
#region Variables

#region Currencies
	public double gold = 0;
	public double diamonds = 0;
	public double prestigeCurrency = 0;
	public double unconvertedPrestigeCurrency = 0;
	public double coal = 0;
	public float diamondChance = 0.05f;
	public float coalChance = 0.1f;
	public float bonusEnemyChance = 0.05f;
	public double bonusEnemyMultiplier = 7;
	public float prestigeCurrencyBase = 1.1f;
	public double prestigeDropItemMultiplier = 1;
	public double prestigeEffectItemMultiplier = 1;
#endregion
#region Enemy Level
	public int level = 1;
	public int highestLevel = 1;
	public int levelCount = 1;
	public int levelMaxCount = 10;
#endregion
#region Gold
	public double baseGoldDrop = 1;
	public double baseGoldMultiplier = 1.07;
	public double goldMultiplier1 = 1;
	public double goldMultiplier2 = 1;
#endregion
#region Health
	public double baseHealth = 5;
	public double healthMultiplier = 1.12;
    public SimpleHealthBar healthBar;
	public double bossLifeItemMultiplier = 1;
#endregion
#region Units
	public double[] units = new double[] {1, 0, 0, 0, 0, 0, 0, 0};
	public double baseUnitMultiplier = 1.04;
	public double[] baseUnits = new double[] {1, 1, 10, 100, 1000, 10000, 100000, 1000000};
	public double[] baseLevelUnits = new double[] {1, 0, 0, 0, 0, 0, 0, 0};
	public double sumofAllUnits = 0;
#endregion
#region Multiplier 1
	public double[] unitM1 = new double[] {1.1, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00};
	public int[] m1Level = new int[] {1, 1, 1, 1, 1, 1, 1, 1};
	public double[] m1UpgradeCost = new double[] {5, 8};
	public double[] m1UpgradeBaseCost = new double[] {5, 8};
	public double[] m1UpgradeCostMultiplier = new double[] {1.08, 1.09};
#endregion
#region Multiplier 2 (Item)
	public double[] unitItemM2 = new double[] {1.0, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00};
#endregion 
#region Multiplier 3 (Achievement)
	public double[] unitAchievementM3 = new double[] {1.0, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00};
#endregion
#region Characters
	public int[] characterLevel = new int[] {1, 0, 0, 0, 0, 0, 0, 0};
	public int[] prevcharacterLevel = new int[] {1, 0, 0, 0, 0, 0, 0, 0};
	public int lastLevel = 0;
	public double[] characterUpgradeCost = new double[] {10, 60, 700, 8000, 90000, 100000, 1100000, 12000000};
	public double[] baseCharacterUpgradeCost = new double[] {10, 60, 700, 8000, 90000, 100000, 1100000, 12000000};
	public double[] characterUpgradeCostMultiplier = new double[] {1.07, 1.1, 1.1, 1.1, 1.1, 1.1, 1.1, 1.1};
	public float[] periods = new float[] {0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f};
	public double[] characterGilds = new double[] {0,0,0,0,0,0,0,0};
	public bool[] characterEverBought = new bool[] {true, false, false, false, false, false, false, false};
	public float criticalClickChance = 0f;
	public double criticalClickMultiplier = 3;
#endregion
#region Prefabs
	public GameObject blueprintPrefab;
    public GameObject chestPrefab;
	public GameObject[] uniqueBossPrefabs;
	public GameObject prestigeDropPrefab;
#endregion
#region Flags
	public bool boss = false;
	public bool uniqueBoss = false;
	public bool modalOpen;
	public bool bossTimeCountdownFlag = false;
	public bool bonusEnemy = false;
	public bool idling = false;
#endregion
#region Enemy Naming
	public string[] enemyNouns;
	public string[] enemyAdjectives;
	public string[] uniqueNouns;
#endregion
#region Controllers
	public upgradeController upgradeController;
	public ItemController itemController;
	public achievementController achievementController;
	public SettingsController settingsController;
	public SaveStateController saveStateController;
	public TutorialController tutorialController;
	public BuildingController buildingController;
	public SwipeCapture swipeCapture;
	public SkillController skillController;
#endregion
#region Diamond Purchases
	public double instaGoldPrice = 20;
	public double instaGoldMultiplier = 200;
	public double instantPrestigePrice = 50;
	public double randomItemPrice = 10;
	public double resetSkillCooldownsPrice = 2;
	public double gildRandomHeroPrice = 15;
#endregion
#region Region
	public int region = 0;
	public bool[] completedRegions = new bool[] {false,false,false,false};
	public int[,] regionLevels = new int[,] {{1,50},{51,120},{111,200},{201,350}};
	public int[] highestRegionLevels = new int[] {1,50,110,200};
	// public ArrayList completedBossLevels = new ArrayList();
#endregion
#region Map
	public int[] uniqueBossLevels;
	public bool[] uniqueBossCompleted = {false, false, false, false, false, false, false, false};
	public GameObject playerIndicator;
	public Vector3 playerIndicatorOffset = new Vector3(-20,0,0);
#endregion
#region Boss Time
	public int bossTime = 30;
	public int bossStartTime = 30;

#endregion
#region Idling
	public int idleTimer = 10;
	public int idleStartTimer = 10;
	public double idleUPSBonus = 1;
	public double idleGoldBonus = 1;
#endregion
#region Text
	public Text[] characterUnitLevelText;
	public Text[] unitMultiplierText;
	public GameObject regionCompleteText;
	public Text levelText = null;
	public int levelStart=1;
	public int levelEnd=1;
	public int bosslevel=-1;
	public string totalPrestigesText;
	public Text amountText;
	public Text goldText = null;
	public Text diamondText = null;
	public Text clickText;
	public Text prestigeText;
	public Text coalText;
	public Text instaGoldText;
	public Text partnerUnitText;
	public Text bossTimeText;
	public Text enemyDescriptionText;
	public Text coalConversionText;
	public Text diamondConversionText;
	public Text instantPrestigeText;
	public Text individualUnitText;
	public Text idlingText;
#endregion
#region Buttons
	public Button levelNavigateUpButton;
	public Button levelNavigateDownButton;
	public Button[] levelUpButton;
	public Button individualLevelUpButton;
	public Button[] unitM1Button;
	public Button instaGoldButton;
	public Button randomItemButton;
	public Button instantPrestigeButton;
	public Button resetSkillCooldownsButton;
	public Button gildRandomHeroButton;
	public GameObject[] regionButtons;
	public Button[] uniqueBossButtons;
	public Button[] shopButtons;
	public Button prestigeButton;
#endregion
#region Panels
	public GameObject goldPanel;
	public GameObject levelArea;
	public GameObject coalModal;
	public GameObject shopPanel;
	public GameObject modalBackDrop;

#endregion
#region Backgrounds
	public GameObject[] regionBackgrounds;
	public GameObject[] shopBackgrounds;
#endregion
#region Statistics
	public double totalBuildings = 0;
	public double totalPrestiges = 0;
	public double totalClicks = 0;
	public double totalGold = 0;
	public double totalUnits = 0;
	public double totalRegionsCompleted = 0;
#endregion
#region Sounds
	public CharacterAudio characterAudio;
	public UIClickAudio uiclickAudio;
#endregion
	public float houseSpawnY = 0.65f;
	public GameObject[] UFOPrefabs;
#endregion

#region Start/Update
	void Awake() {
		saveStateController.LoadData();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//Start repeating methods
		InvokeRepeating("bossTimeCountdown",Time.time,1.0f);
		InvokeRepeating("checkUnitAchievement",Time.time,1.0f);
		InvokeRepeating("idleTimerCountdown", Time.time, 1.0f);

	}
	void Start () {
		Application.runInBackground = true;

<<<<<<< Updated upstream
		SetUp();

		StartCoroutine(SpawnUFOs());
=======
<<<<<<< HEAD
			SetUp();
=======
		SetUp();

		StartCoroutine(SpawnUFOs());
>>>>>>> master
>>>>>>> Stashed changes
		//Should be last
		saveStateController.CheckIdleTime();

		 FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
       	var dependencyStatus = task.Result;
        if (dependencyStatus == DependencyStatus.Available) {
          InitializeFirebase();
        } else {
          Debug.LogError(
            "Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
      });
	}
	

	// Handle initialization of the necessary firebase modules:
    public void InitializeFirebase() {
      Debug.Log("Enabling data collection.");
      FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

      Debug.Log("Set user properties.");
      // Set the user's sign up method.
      FirebaseAnalytics.SetUserProperty(
        FirebaseAnalytics.UserPropertySignUpMethod,
        "Google");
      // Set the user ID.
      FirebaseAnalytics.SetUserId("uber_user_510");
      // Set default session duration values.
      FirebaseAnalytics.SetMinimumSessionDuration(new TimeSpan(0, 0, 10));
      FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
      bool firebaseInitialized = true;
	  FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
	  Debug.Log("trying to log 2");
    }
	// Update is called once per frame
	void Update () {
		RecalculateSumUnits();

		//Update Text
		clickText.text = NumberFormat.format(units[0]);
		partnerUnitText.text = NumberFormat.format(sumofAllUnits);
		goldText.text = NumberFormat.format(gold);
		diamondText.text = NumberFormat.format(diamonds);
		prestigeText.text = NumberFormat.format(prestigeCurrency) + " ("+NumberFormat.format(unconvertedPrestigeCurrency)+")";
		coalText.text = NumberFormat.format(coal);

		//Update diamond purchases price
		instaGoldButton.interactable = diamonds >= instaGoldPrice;
		randomItemButton.interactable = diamonds >= randomItemPrice;
		resetSkillCooldownsButton.interactable = (diamonds >= resetSkillCooldownsPrice) && upgradeController.skillTabButton.gameObject.activeSelf;
		// resetSkillCooldownsButton.interactable = false;
		instantPrestigeButton.interactable = diamonds >= instantPrestigePrice && unconvertedPrestigeCurrency > 0;
		instantPrestigeText.text = "+"+NumberFormat.format(unconvertedPrestigeCurrency) + " NOTES";
		gildRandomHeroButton.interactable = diamonds >= gildRandomHeroPrice;
		

	}

	void OnApplicationQuit() {
		levelEnd = level;
		if(levelEnd>levelStart+10){
			//event levels progressed greater than 10
			Debug.Log("levels progressed>10");
			FirebaseAnalytics.LogEvent("Progressed_>_10");
		}
		//else if(levelEnd>levelStart+50){
			//event levels progressed greater than 50
		//	Debug.Log("levels progressed>50");
		//	FirebaseAnalytics.LogEvent("Progressed_>_50");
	//	}
		//else if(levelEnd>levelStart+25){
			//event levels progressed greater than 25
		//	Debug.Log("levels progressed>25");
		//	FirebaseAnalytics.LogEvent("Progressed_>_25");
	//	}
	//	else if(levelEnd>levelStart+100){
	//		//event levels progressed greater than 10
	//		Debug.Log("levels progressed>100");
	//		FirebaseAnalytics.LogEvent("Progressed_>_100");
	//	}	
		else if(levelEnd==levelStart){
			//event no levels progressed
			Debug.Log("levels progressed = 0");
			FirebaseAnalytics.LogEvent("Progressed_0");
		}	
		// saveStateController.SaveData();
	}

	public void SetUp() {
		double health = 0;
		double maxHealth = calculateHealth();
		healthBar.UpdateBar( health, maxHealth );


		//Screen text
		levelText.text = "LEVEL "+level;
		levelStart = level;
		amountText.text = levelCount+" / "+levelMaxCount;
		levelUpButton[0].gameObject.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[0]);
		characterUnitLevelText[0].text = "LEVEL: "+characterLevel[0]+" UNITS: "+units[0];
		totalPrestigesText = NumberFormat.format(totalPrestiges);
		Debug.Log("Set user properties prestige.");
      	// Set the user's sign up method.
      	FirebaseAnalytics.SetUserProperty("Prestige_Level", totalPrestigesText);
		for (int i = 1; i < upgradeController.characterAmount; i++) {
			levelUpButton[i].gameObject.transform.Find("Action Text").GetComponent<Text>().text = "HIRE";
			levelUpButton[i].gameObject.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[i]);
			characterUnitLevelText[i].text = "";
		}
		instaGoldButton.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = instaGoldPrice+"";
		instaGoldText.text = calculateMaxGold()+" GOLD";

		//Deactivate certain objects
		levelNavigateDownButton.gameObject.SetActive(false);
		levelNavigateUpButton.gameObject.SetActive(false);
		prestigeText.gameObject.SetActive(false);
		coalText.gameObject.SetActive(false);
		bossTimeText.gameObject.SetActive(false);
		for (int i = 1; i < regionButtons.Length; i++)
			regionButtons[i].SetActive(false);
		foreach(Button button in uniqueBossButtons)
			button.interactable=false;
		closeCoalModal();
		shopPanel.SetActive(false);

		//Set other variables
		closeModal();
		playerIndicator.transform.position = regionButtons[0].transform.position+playerIndicatorOffset;


		RecalculateAchievementMultipliers();
		for (int i = 0; i < baseCharacterUpgradeCost.Length; i++)
			RecalculateCharacterUpgradeCost(i);
		RecalculateItemMultipliers();
		itemController.refreshInventoryUI();
		achievementController.refreshAchievementsUI();
		if (highestLevel > 1 || levelCount > 1 || totalPrestiges > 0) {
			upgradeController.enableGoldButton();
			settingsController.enableSettings();
			tutorialController.RemoveHousePointer();
		} else{
			tutorialController.PointAtHouse();
		}
		if (level > regionLevels[region,0])
			levelNavigateDownButton.gameObject.SetActive(true);
		if (level < regionLevels[region,1] && level < highestRegionLevels[region])
			levelNavigateUpButton.gameObject.SetActive(true);
		if (highestLevel > 5 || diamonds > 0)
			upgradeController.enableDiamondButton(false);
		if (itemController.inventory.Count > 0)
			upgradeController.enableItemButton(false);
		for (int i = 0; i < achievementController.achievements.Count; i++){
			if (achievementController.achievements[i].completed){
				upgradeController.enableAchievementsButton(false);
				break;
			}
		}
		foreach(String key in skillController.keys){
			if (skillController.skillsBought[key]){
				upgradeController.enableSkillButton(false);
				break;
			}
		}
		if (highestLevel > 20)
			upgradeController.enableMapButton(false);
		for (int i = 0; i < uniqueBossLevels.Length; i++){
			if (highestLevel > uniqueBossLevels[i] - 10 && !uniqueBossCompleted[i]) {
				uniqueBossButtons[i].interactable = true;
			}
		}
		if (highestLevel > 10) 
			upgradeController.enableMultiLevelUpButton();
		
		int numCharactersBought = -1;
		for (int i = 0; i < characterLevel.Length;i++){
			if (i > 0 && characterLevel[i] > 0) {
				numCharactersBought++;
			} else if (characterLevel[i] > 1){
				numCharactersBought++;
			}
		}
		upgradeController.enableBoard(numCharactersBought);

		for (int i = 0;i < units.Length;i++){
			if (characterLevel[i] > 0)
				upgradeController.RefreshCharacterBoard(i);
		}
		for (int i = 0; i < completedRegions.Length;i++) {
			if (completedRegions[i]){
				if (regionButtons.Length > i+1)
					regionButtons[i+1].SetActive(true);
				if (region == i)
					regionCompleteText.SetActive(true);
			}
		}
		if (coal > 0 || highestLevel > 20){
			coalText.gameObject.SetActive(true);
			upgradeController.SetCurrencyPanelCoal();
		}
		if (prestigeCurrency > 0 || unconvertedPrestigeCurrency > 0){
			prestigeText.gameObject.SetActive(true);
			upgradeController.SetCurrencyPanelPrestige();
		}

		SetUpRegion(region);
		regionCompleteText.SetActive(completedRegions[region]);

		m1UpgradeCost[0] = Math.Round(m1UpgradeBaseCost[0]*Math.Pow(m1UpgradeCostMultiplier[0],m1Level[0]));
		unitM1Button[0].transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[0] + "";
		unitMultiplierText[0].text = "CURRENT BONUS: + "+(m1Level[0]-1)*25+"%"; 


		m1UpgradeCost[1] = Math.Round(m1UpgradeBaseCost[1]*Math.Pow(m1UpgradeCostMultiplier[1],m1Level[1]));
		unitM1Button[1].transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[1] + "";
		unitMultiplierText[1].text = "CURRENT BONUS: + "+(m1Level[1]-1)*25+"%"; 
		resetSkillCooldownsButton.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = resetSkillCooldownsPrice + "";

		prestigeButton.gameObject.SetActive(completedRegions[1]);

		bonusEnemy = false;

		buildingController.ChangeLevel(level);

		idleTimer = idleStartTimer;
		idlingText.gameObject.SetActive(false);
	}
#endregion

#region Calculations
	public double calculateGold() {
		double calc = Math.Round(baseGoldDrop*Math.Pow(baseGoldMultiplier,level)*goldMultiplier1*goldMultiplier2);
		if (idling && idleGoldBonus > 1)
			calc = Math.Round(calc*idleGoldBonus);
		if (skillController.skillFlag["goldBoost"])
			calc = Math.Round(calc*skillController.GetSkillEffect("goldBoost"));
		return calc;
	}

	private double calculateMaxGold() {
		return Math.Round((baseGoldDrop*Math.Pow(baseGoldMultiplier,highestLevel)*goldMultiplier1*goldMultiplier2))*instaGoldMultiplier;
	}
	public double calculateHealth() {
		if (uniqueBoss)
			return Math.Round(baseHealth*Math.Pow(healthMultiplier,level)) * 10 * bossLifeItemMultiplier; 
		if (boss)
			return Math.Round(baseHealth*Math.Pow(healthMultiplier,level)) * 5 * bossLifeItemMultiplier; 
		if (bonusEnemy)
			return Math.Round(baseHealth*Math.Pow(healthMultiplier,level)* 1.5); 
		return Math.Round(baseHealth*Math.Pow(healthMultiplier,level));
	}

	public void RecalculateUnit(int i) {
		units[i] = Math.Round(baseLevelUnits[i]
								*unitM1[i]
								*unitItemM2[i]
								*unitAchievementM3[i]
								*(characterGilds[i]+1)
								*(1+(prestigeCurrency*prestigeEffectItemMultiplier/100)));
		if (idling && i > 0 && idleUPSBonus > 1)
			units[i] = Math.Round(units[i]*idleUPSBonus);
		if (i == 0 && skillController.skillFlag["clickBoost"]) {
			units[i] = Math.Round(units[i]*skillController.GetSkillEffect("clickBoost"));
		} else if (i != 0 && skillController.skillFlag["partnerBoost"]) {
			units[i] = Math.Round(units[i]*skillController.GetSkillEffect("partnerBoost"));
		}
	}

	public void RecalculateAllUnits() {
		for (int i = 0; i < units.Length; i++) {
			RecalculateUnit(i);
		}
	}

	public void RecalculateSumUnits() {
		//Sum total units
		double tempSum = 0;
		for(int i = 0; i < upgradeController.characterAmount; i++) {
			levelUpButton[i].interactable = gold >= characterUpgradeCost[i];
			if (i == upgradeController.selectedCharacter && upgradeController.individualCharacterPanel.activeSelf){
				individualUnitText.text = "LEVEL: "+characterLevel[i]+" UNITS: "+NumberFormat.format(units[i]);			
				individualLevelUpButton.interactable = gold >= characterUpgradeCost[i];
			}
			if (characterLevel[i] > 0) {
				characterUnitLevelText[i].text = "LEVEL: "+characterLevel[i]+" UNITS: "+NumberFormat.format(units[i]);
			}
			if (i < unitM1Button.Length)
				unitM1Button[i].interactable = diamonds >= m1UpgradeCost[i];
			if (i != 0)
				tempSum += units[i];
		}
		sumofAllUnits = tempSum;
	}

	public void IncrementGold(double increment) {
		gold += increment;
		totalGold += increment;
	}

	public void IncrementPrestigeCurrency(double increment) {
		prestigeCurrency += increment;
		RecalculateItemMultipliers();
		//TODO total prestige currency as statistic
	}

	public void IncrementDiamonds(double increment) {
		diamonds += increment;
		//TODO total prestige currency as statistic
	}

	public double CalculateUnconvertedPrestigeCurrency() {
		return Math.Round(
			//prestigeCurrencyBase*
			Math.Pow(prestigeCurrencyBase,(level-1))*prestigeDropItemMultiplier);
	}
#endregion

#region Level Up Units
	public void LevelUpUnit(int i, int numLevels) {
		for (int j = 0; j < numLevels; j++){
			//TODO: exponential or fixed returns?
			// baseLevelUnits[i] = characterLevel[i] == 0 ? 0 : baseLevelUnits[i]+Math.Round(baseUnits[i]*Math.Pow(baseUnitMultiplier,characterLevel[i]));
			baseLevelUnits[i] = characterLevel[i] == 0 ? 0 : baseLevelUnits[i]+baseUnits[i];
		}
		RecalculateUnit(i);
	}

	//When you purchase a level up
	public void levelUp(int i) {
		int numLevels = upgradeController.GetNumLevels(i);
		int previousLevel = characterLevel[i];
		lastLevel = previousLevel;
		characterLevel[i] += numLevels;
		gold -= characterUpgradeCost[i];
		upgradeController.RefreshCharacterBoard(i);
		LevelUpUnit(i,numLevels);

		if (!characterEverBought[i]){
			characterEverBought[i] = true;
			Debug.Log("Builder bought event");
			FirebaseAnalytics.LogEvent("char_gen_builder_bought");
			if (i == 1 ){
			Debug.Log("Reeda bought event");
			FirebaseAnalytics.LogEvent("char_Reeda_bought");
			}
			//else if (i == 2 ){
			//Debug.Log("Billy bought event");
			//FirebaseAnalytics.LogEvent("char_Billy_bought");
			//}
			//else if (i == 3 ){
			//Debug.Log("Brick bought event");
			//FirebaseAnalytics.LogEvent("char_Brick_bought");
			//}
			//else if (i == 4 ){
			//Debug.Log("connor bought event");
			//FirebaseAnalytics.LogEvent("char_Connor_bought");
			//}
			//else if (i == 5 ){
			//Debug.Log("chris bought event");
			//FirebaseAnalytics.LogEvent("char_Chris_bought");
			//}
			//else if (i == 6 ){
			//Debug.Log("alena bought event");
			//FirebaseAnalytics.LogEvent("char_Alena_bought");
			//}
			//else if (i == 7 ){
			//Debug.Log("dan bought event");
			//FirebaseAnalytics.LogEvent("char_Dan_bought");
			//}
			//else if (i == 8 ){
			//Debug.Log("bill bought event");
			//FirebaseAnalytics.LogEvent("char_Nye_bought");
			//}
		}
		//check for student bought
		if (previousLevel == 1 && i == 0){
			//student bought event
			Debug.Log("student bought event");
			FirebaseAnalytics.LogEvent("char_Student_bought");
			Debug.Log("Builder bought event");
			FirebaseAnalytics.LogEvent("char_gen_builder_bought");
		}
		//check for upgrades
		if (previousLevel > 1 && i == 0){
			Debug.Log("student upgrade event");
			FirebaseAnalytics.LogEvent("char_Student_upgraded");
			Debug.Log("Builder upgrade event");
			FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		}
		if (previousLevel > 0 && i == 1){
			Debug.Log(" Reeda upgrade event");
			FirebaseAnalytics.LogEvent("char_Reeda_upgraded");
			Debug.Log("Builder upgrade event");
			FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		}
		//if (previousLevel > 0 && i == 2){
		//	Debug.Log(" billy upgrade event");
		//	FirebaseAnalytics.LogEvent("char_Billy_upgraded");
		//	Debug.Log("Builder upgrade event");
		//	FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		//}
		//if (previousLevel > 0 && i == 3){
		//	Debug.Log(" brick upgrade event");
		//	FirebaseAnalytics.LogEvent("char_Brick_upgraded");
		//	Debug.Log("Builder upgrade event");
		//	FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		//}
		//if (previousLevel > 0 && i == 4){
		//	Debug.Log(" connnor upgrade event");
		//	FirebaseAnalytics.LogEvent("char_Connor_upgraded");
		//	Debug.Log("Builder upgrade event");
		//	FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		//}
		//if (previousLevel > 0 && i == 5){
		//	Debug.Log(" chris upgrade event");
		//	FirebaseAnalytics.LogEvent("char_Chris_upgraded");
		//	Debug.Log("Builder upgrade event");
		//	FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		//}
		//if (previousLevel > 0 && i == 6){
		//	Debug.Log(" alena upgrade event");
		//	FirebaseAnalytics.LogEvent("char_Alena_upgraded");
		//	Debug.Log("Builder upgrade event");
		//	FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		//}
		//if (previousLevel > 0 && i == 7){
		//	Debug.Log(" dan upgrade event");
		//	FirebaseAnalytics.LogEvent("char_Dan_upgraded");
		//	Debug.Log("Builder upgrade event");
		//	FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		//}//
		//if (previousLevel > 0 && i == 8){
		//	Debug.Log(" bill upgrade event");
		//	FirebaseAnalytics.LogEvent("char_Nye_upgraded");
		//	Debug.Log("Builder upgrade event");
		//	FirebaseAnalytics.LogEvent("char_gen_builder_upgrade");
		//}
		if (previousLevel == 0 || (previousLevel == 1 && i == 0))
			if ((i+1) < upgradeController.characterAmount)
				upgradeController.ScrollABit();
		upgradeController.CalculalteMaxMultiLevelUp();
		saveStateController.SaveData();
		// characterAudio.levelUpSound();
	}


#endregion
	
#region Upgrades/Purchases
	public void RecalculateCharacterUpgradeCost(int i) {
		double sum = 0;
		int numLevels = upgradeController.GetNumLevels(i);
		for (int j = 0; j < numLevels; j++)
			sum += Math.Round(baseCharacterUpgradeCost[i]*Math.Pow(characterUpgradeCostMultiplier[i],characterLevel[i]+j));
		characterUpgradeCost[i] = sum;
		levelUpButton[i].gameObject.transform.Find("Action Text").GetComponent<Text>().text = characterLevel[i] > 0 ? "LEVEL UP" : "HIRE";
		levelUpButton[i].gameObject.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[i]);
		individualLevelUpButton.transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[i]);
	}
	
	public void buyClickM1Up() {
		int i = 0;
		m1Level[i]++;
		diamonds -= m1UpgradeCost[i];
		diamondText.text = "Diamonds: "+diamonds;
		m1UpgradeCost[i] = Math.Round(m1UpgradeBaseCost[i]*Math.Pow(m1UpgradeCostMultiplier[i],m1Level[i]));
		unitM1Button[i].transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[i]+"";
		unitM1[i] += .25f;
		unitMultiplierText[i].text = "CURRENT BONUS: + "+(m1Level[i]-1)*25+"%"; 
		RecalculateUnit(i);
		saveStateController.SaveData();
		//event buy click boost
			Debug.Log("clicks diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_clicks");
			Debug.Log("diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_general");
	}

	public void buyAllPartnersM1Up() {
		for (int i = 1; i < upgradeController.characterAmount; i++) {
			m1Level[i]++;
			unitM1[i] += .25f;
			RecalculateUnit(i);
		}
		diamonds -= m1UpgradeCost[1];
		diamondText.text = "Diamonds: "+diamonds;
		m1UpgradeCost[1] = Math.Round(m1UpgradeBaseCost[1]*Math.Pow(m1UpgradeCostMultiplier[1],m1Level[1]));
		unitM1Button[1].transform.Find("Level Up Layout").Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[1]+"";
		unitMultiplierText[1].text = "CURRENT BONUS: + "+(m1Level[1]-1)*25+"%"; 
		saveStateController.SaveData();
		//event buy partners 25%
			Debug.Log("partners diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_partner_boost");
			Debug.Log("diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_general");
	}

	public void buyInstaGold() {
		IncrementGold(calculateMaxGold());
		diamonds -= instaGoldPrice;
		saveStateController.SaveData();
		//event buy insta gold
			Debug.Log("gold diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_instagold");
			Debug.Log("diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_general");
	}

	public void buyRandomItem() {
		if (!itemController.itemDrop || modalOpen) {
			Item item = itemController.getRandomItem();
			DropItem(new Vector3(0,-2,-5),item);
			diamonds -= randomItemPrice;
			saveStateController.SaveData();
		}
		//event buy item
			Debug.Log("items diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_items");
			Debug.Log("diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_general");
	}

	public void buyInstantPrestige() {
		IncrementPrestigeCurrency(unconvertedPrestigeCurrency);
		unconvertedPrestigeCurrency = 0;
		diamonds -= instantPrestigePrice;
		saveStateController.SaveData();
		//event buy instaprestige
			Debug.Log("prestige diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_prestige");
			Debug.Log("diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_general");
	}

	public void gildRandomCharacter() {
		diamonds -= gildRandomHeroPrice;
		List<int> heroIndices = new List<int>();
		for (int i = 0;i < characterLevel.Length; i++) {
			if (characterLevel[i] > 0)
				heroIndices.Add(i);
		}
		System.Random rnd = new System.Random();
		int index = rnd.Next(heroIndices.Count);
		gildCharacter(index);
		saveStateController.SaveData();
		//event buy partners 25%
			Debug.Log("gild diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_gild");
			Debug.Log("diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_general");
	}

	public void gildCharacter(int index) {
		characterGilds[index]++;
		upgradeController.RefreshCharacterBoard(index);
		RecalculateUnit(index);
		saveStateController.SaveData();
		Debug.Log("gildgen char");
		FirebaseAnalytics.LogEvent("char_gen_gild");
		if(index==1){
			Debug.Log("gild reeda");
			FirebaseAnalytics.LogEvent("char_Reeda_gild",new Parameter(FirebaseAnalytics.ParameterLevel, characterGilds[index]));
		}
	}

	public void buyResetCooldowns() {
		diamonds -= resetSkillCooldownsPrice;
		skillController.ResetSkillCooldowns();
		saveStateController.SaveData();
		//event buy reset cooldown
			Debug.Log("reset cooldown diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_cooldown");
			Debug.Log("diamond purchase");
			FirebaseAnalytics.LogEvent("diamond_purchase_general");
	}
#endregion

#region Items
	public void RecalculateItemMultipliers() {
		List<Item> itemsLeft = new List<Item>(itemController.inventory);
		goldMultiplier1 = 1;
		bossLifeItemMultiplier = 1;
		bossStartTime = 30;
		bonusEnemyChance = 0.02f;		
		if (skillController.skillFlag["goldHouseChanceBoost"])
			bonusEnemyChance += (float)skillController.GetSkillEffect("goldHouseChanceBoost");
		bonusEnemyMultiplier = 7;
		prestigeDropItemMultiplier = 1;
		prestigeEffectItemMultiplier = 1;
		criticalClickChance = 0;
		if (skillController.skillFlag["criticalClickChanceBoost"])
			criticalClickChance += (float)skillController.GetSkillEffect("criticalClickChanceBoost");
		criticalClickMultiplier = 3;
		idleGoldBonus = 1;
		idleUPSBonus = 1;

		for (int i = itemsLeft.Count - 1; i >= 0; i--) {
			Item item = itemsLeft[i];
			if (item.effect == "Gold") {
				goldMultiplier1 += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			}
			else if (item.effect ==  "Boss Life"){
				bossLifeItemMultiplier += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			}
			else if (item.effect ==  "Boss Timer"){
				bossStartTime += (int)item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} 
			else if (item.effect ==  "Bonus Building Chance"){
				bonusEnemyChance += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} 
			else if (item.effect ==  "Bonus Building Gold"){
				bonusEnemyMultiplier += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} 
			else if (item.effect ==  "Notes"){
				prestigeDropItemMultiplier += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} 
			else if (item.effect ==  "Note Effect"){
				prestigeEffectItemMultiplier += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} else if (item.effect == "Critical Click Chance") {
				criticalClickChance += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} else if (item.effect == "Critical Click Units") {
				criticalClickMultiplier += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} else if (item.effect == "Idle UPS") {
				idleUPSBonus += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			} else if (item.effect == "Idle Gold") {
				idleGoldBonus += item.effectValue*item.count;
				itemsLeft.RemoveAt(i);
			}
		}

		for (int i = 0; i < upgradeController.characterAmount; i++) {
			double multiplier = 1.0;
			foreach(Item item in itemsLeft) {
				if (item.effect == "All Partners UPS" && i != 0)
						multiplier += item.effectValue*item.count;
				else if (item.effect ==  "Click Units" && i == 0)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Reeda Weaver UPS" && i == 1)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Billy Hammerton UPS" && i == 2)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Brick Sanchez UPS" && i == 3)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Connor Crete UPS" && i == 4)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Chris Shingle UPS" && i == 5)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Alena Wrench UPS" && i == 6)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Dan Buldozian UPS" && i == 7)
						multiplier += item.effectValue*item.count;
				else if (item.effect == "Build Nye UPS" && i == 8)
						multiplier += item.effectValue*item.count;
			}
			unitItemM2[i] = multiplier;

			RecalculateUnit(i);
		}
		bossLifeItemMultiplier = Math.Max(0.5,bossLifeItemMultiplier);
	}

	public void DropItem(Vector3 pos, Item item) {
		GameObject chest = (GameObject) Instantiate(chestPrefab,pos+new Vector3(0,2.5f,-10f),Quaternion.Euler(0, 0, 0));
		itemController.addItemToInventory(item);
		chest.GetComponentInChildren<chest>().SetItem(item);
		for (int i = 0;i< uniqueBossButtons.Length;i++) {
			if (level == uniqueBossLevels[i]){
				uniqueBossButtons[i].interactable = false;
				uniqueBossCompleted[i] = true;
			}
		}
	}
#endregion

#region Achievements
	public void RecalculateAchievementMultipliers() {
		for (int i = 0; i < upgradeController.characterAmount; i++) {
			double multiplier = 1.0;
			foreach(achievement achievement in achievementController.achievements) {
				if (achievement.completed) {
					if (achievement.effect == "partners" && i != 0)
							multiplier += achievement.effectValue;
					else if (achievement.effect ==  "unitIndex" + i)
							multiplier += achievement.effectValue;
				}
			}
			unitAchievementM3[i] = multiplier;
			RecalculateUnit(i);
		}
	}

	public void checkUnitAchievement() {
		achievementController.checkAchievement("units",sumofAllUnits);
		achievementController.checkAchievement("unitIndex0",units[0]);
	}
#endregion

#region Enemy
	private void enemyLevelUp(bool advanceLevel) {
		if (level == regionLevels[region,1]) {
			if (!completedRegions[region]) {
				completeRegion();
			}
			levelCount = levelMaxCount;
		} else {
			highestRegionLevels[region] = level+1;
			if (highestRegionLevels[region] > highestLevel)
				highestLevel = highestRegionLevels[region];

			if (advanceLevel) {
				level++;
				levelCount = 1;
				buildingController.LevelHasChanged();
				buildingController.RefreshBuildingPreviews();
			}
			instaGoldText.text = calculateMaxGold()+" GOLD";
			levelNavigateDownButton.gameObject.SetActive(true);
		}
	}

	public double getEnemyGold() {
		double goldIncrement = boss ? calculateGold()*10 : bonusEnemy ? calculateGold()*bonusEnemyMultiplier : calculateGold();
		return goldIncrement;
	}

	public void enemyDied (bool spawn, bool advanceLevel) {
		if (bonusEnemy == true){
			Debug.Log("defeated gold house");
			FirebaseAnalytics.LogEvent("gold_house_defeated");
			bonusEnemy= false;
		}
		bonusEnemy = false;
		if (uniqueBoss || boss)
			endBossTime();	
		if (!uniqueBoss) {
			if (level == 1 && levelCount == 1){
				upgradeController.enableGoldButton();
				settingsController.enableSettings();
				//End of tutorial
				Debug.Log("Logging a tutorial end event.");
     			 FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventTutorialComplete);
			}

			if (level == highestRegionLevels[region]){
				if (boss) {
					boss = false;
					// completedBossLevels.Add(level);
					enemyLevelUp(advanceLevel);
					if ((level-1) % 20 == 0) {
						unconvertedPrestigeCurrency += CalculateUnconvertedPrestigeCurrency();
						prestigeText.gameObject.SetActive(true);
						upgradeController.SetCurrencyPanelPrestige();
						GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
						if (enemy)
							Instantiate(prestigeDropPrefab,enemy.transform.position+new Vector3(0,2f,-3f),Quaternion.Euler(0, 0, 0));

					}
				}
				else {
					if (levelCount >= levelMaxCount) {
						if (level%10 == 0 
						// && !completedBossLevels.Contains(level) 
						&& advanceLevel 
						&& highestLevel <= level
						&& !completedRegions[region])
							boss = true;
						else
							enemyLevelUp(advanceLevel);
					} 
					else
						levelCount++;
				}
			} else if (levelCount != levelMaxCount) {
				levelCount = levelMaxCount;
			}
			if (spawn)
				spawnNewEnemy(false);
		}
		totalBuildings++;
		// IncrementGold(goldIncrement);
		saveStateController.SaveData();
	}

	private void spawnNewEnemy(bool delay) {
		//Ensure everything is destroyed
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("enemy")) {
			Destroy(obj);
		}
		if (boss) {
			levelText.text = "LEVEL "+level;
			amountText.text = "BOSS FIGHT!";
			// enemySelector = 5;
			startBossTime();
		}
		else {
			levelText.text = "LEVEL "+level;
			amountText.text = levelCount+" / "+levelMaxCount;
			bonusEnemy = UnityEngine.Random.value <= bonusEnemyChance;
		}

		GameObject newEnemy = (GameObject) Instantiate(buildingController.currentBuildingPrefab, new Vector3(0f,houseSpawnY,-5f),Quaternion.Euler(0,0, 0));
		enemyDescriptionText.text = enemyAdjectives[((level-regionLevels[region,0])/buildingController.levelBuildingLists[region].buildings.Length) %20] +" "+ buildingController.currentBuildingPrefab.name;
		if (delay)
			newEnemy.GetComponent<House>().delay();
	}

	//Remove the enemy and get the reward if the player navigates to a new stage
	public void finishOffEnemy() {
		GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
		if (enemy) {
			House house = enemy.GetComponent<House>();
			if (house.health >= house.maxHealth && !uniqueBoss){
				enemyDied(false, false);
			}
			endBossTime();
			Destroy(enemy);
		}
		GameObject[] drops = GameObject.FindGameObjectsWithTag("currency_drop");
		for (int i = 0; i < drops.Length; i++) {
			coin coin = drops[i].GetComponent<coin>();
			if (coin){
				coin.CashOut();
			}
			else {
				Destroy(drops[i]);
			}
		}
	}

#endregion

#region Boss

	public void startBossTime() {
		bossTime = bossStartTime;
		bossTimeText.gameObject.SetActive(true);
		bossTimeText.text = bossTime.ToString();
		bossTimeCountdownFlag = true;
	}

	public void endBossTime() {
		bossTimeText.gameObject.SetActive(false);
		bossTimeCountdownFlag = false;
	}

	void bossTimeCountdown() {
		if (bossTimeCountdownFlag){
			bossTime--;
			bossTimeText.text = bossTime.ToString();
			if (bossTime == 0) {
				if (uniqueBoss){
					changeRegion(0);
				} else {
					levelNavigateDown();
				}
				endBossTime();
			}
		}
    }

	public bool unlockUniqueBoss() {
		bool dropBlueprint = false;
		for (int i = 0;i< uniqueBossButtons.Length;i++) {
			if (level == uniqueBossLevels[i]-10){
				uniqueBossButtons[i].interactable = true;
				dropBlueprint = true;
				//Unlock a boss
			Debug.Log("Logging a boss unlocked event.");
			FirebaseAnalytics.LogEvent("boss_gen_unlocked",FirebaseAnalytics.ParameterLevel, i);
			if (i==0){
				Debug.Log("Logging boss 1 unlocked event.");
				FirebaseAnalytics.LogEvent("boss_1_unlocked");
			}
		//	if (i==1){
		//		Debug.Log("Logging boss 2 unlocked event.");
		//		FirebaseAnalytics.LogEvent("boss_2_unlocked");
		//	}
		//	if (i==2){
		//		Debug.Log("Logging boss 3 unlocked event.");
		//		FirebaseAnalytics.LogEvent("boss_3_unlocked");
		//	}
		//	if (i==3){
		///		Debug.Log("Logging boss 4 unlocked event.");
		//		FirebaseAnalytics.LogEvent("boss_4_unlocked");
		//	}
		//	if (i==4){
		//		Debug.Log("Logging boss 5 unlocked event.");
		//		FirebaseAnalytics.LogEvent("boss_5_unlocked");
		//	}
		//	if (i==5){
		//		Debug.Log("Logging boss 6 unlocked event.");
		//		FirebaseAnalytics.LogEvent("boss_6_unlocked");
		//	}
		//	if (i==6){
		//		Debug.Log("Logging boss 7 unlocked event.");
		//		FirebaseAnalytics.LogEvent("boss_7_unlocked");
		//	}
		//	if (i==7){
		//		Debug.Log("Logging boss 8 unlocked event.");
		//		FirebaseAnalytics.LogEvent("boss_8_unlocked");
		//	}
			break;
			}
		}
		if (dropBlueprint){
			upgradeController.enableMapButton(true);
			// upgradeController.mapTab();
		}
		return dropBlueprint;
	}

	private void spawnUnique(int i) {
		level = uniqueBossLevels[i];
		uniqueBoss = true;
		boss=true;
		// maxHealth = calculateHealth()*10;
		levelText.text = "LEVEL "+level;
		amountText.text = "Unique #"+(i+1)+"!";

		GameObject newUnique = (GameObject) Instantiate(uniqueBossPrefabs[i], new Vector3(0f,houseSpawnY,-5f),Quaternion.Euler(0,0,0));
		newUnique.GetComponent<House>().health = 0;
		enemyDescriptionText.text = uniqueNouns[i];
		// enemyDescriptionText.text = enemyAdjectives[((level-regionLevels[region,0])/buildingController.levelBuildingLists[region].buildings.Length) %20] +" "+ uniqueNouns[i];
		newUnique.GetComponent<House>().delay();
		startBossTime();
		//Log a boss startes event
			Debug.Log("Logging a boss started event.");
			FirebaseAnalytics.LogEvent("boss_gen_started",FirebaseAnalytics.ParameterLevel, i);
			bosslevel = i;
			if (i==0){
				Debug.Log("Logging boss 1 started event.");
				FirebaseAnalytics.LogEvent("boss_1_started");
			}
		//	if (i==1){
		//		Debug.Log("Logging boss 2 started event.");
		//		FirebaseAnalytics.LogEvent("boss_2_started");
		//	}
		//	if (i==2){
		//		Debug.Log("Logging boss 3 started event.");
		//		FirebaseAnalytics.LogEvent("boss_3_started");
		//	}
		//	if (i==3){
		//		Debug.Log("Logging boss 4 started event.");
		//		FirebaseAnalytics.LogEvent("boss_4_started");
		//	}
		//	if (i==4){
		//		Debug.Log("Logging boss 5 started event.");
		//		FirebaseAnalytics.LogEvent("boss_5_started");
		//	}
		//	if (i==5){
		//		Debug.Log("Logging boss 6 started event.");
		//		FirebaseAnalytics.LogEvent("boss_6_started");
		//	}
		//	if (i==6){
		//		Debug.Log("Logging boss 7 started event.");
		//		FirebaseAnalytics.LogEvent("boss_7_started");
		//	}
		//	if (i==7){
		//		Debug.Log("Logging boss 8 started event.");
		//		FirebaseAnalytics.LogEvent("boss_8_started");
		//	}
	}

	public void checkBossReward(Vector3 pos) {
		if (uniqueBoss) {
			endBossTime();
			Item item = itemController.getRandomItem();
			DropItem(pos, item);
			//Log a boss defeted event
			Debug.Log("Logging a boss defeated event.");
			FirebaseAnalytics.LogEvent("boss_gen_defeated",FirebaseAnalytics.ParameterLevel, levelText.text);
			if (bosslevel==0){
				Debug.Log("Logging boss 1 defeated event.");
				FirebaseAnalytics.LogEvent("boss_1_defeated");
			}
		//	if (bosslevel==1){
		//		Debug.Log("Logging boss 2 defeated event.");
		//		FirebaseAnalytics.LogEvent("boss_2_defeated");
		//	}
		//	if (bosslevel==2){
		//		Debug.Log("Logging boss 3 defeated event.");
		//		FirebaseAnalytics.LogEvent("boss_3_defeated");
		//	}
		//	if (bosslevel==3){
		//		Debug.Log("Logging boss 4 defeated event.");
		//		FirebaseAnalytics.LogEvent("boss_4_defeated");
		//	}
		//	if (bosslevel==4){
		//		Debug.Log("Logging boss 5 defeated event.");
		//		FirebaseAnalytics.LogEvent("boss_5_defeated");
		//	}
		//	if (bosslevel==5){
		//		Debug.Log("Logging boss 6 defeated event.");
		//		FirebaseAnalytics.LogEvent("boss_6_defeated");
		//	}
		//	if (bosslevel==6){
		//		Debug.Log("Logging boss 7 defeated event.");
		//		FirebaseAnalytics.LogEvent("boss_7_defeated");
		//	}
		//	if (bosslevel==7){
		//		Debug.Log("Logging boss 8 defeated event.");
		//		FirebaseAnalytics.LogEvent("boss_8_defeated");
		//	}

		} else {
			if(unlockUniqueBoss()){
				GameObject blueprint = (GameObject) Instantiate(blueprintPrefab,pos+new Vector3(0,2.25f,-3f),Quaternion.Euler(0, 0, 0));
			}
		}
		upgradeController.enableMultiLevelUpButton();
	}

#endregion

#region Single Region Navigation
	public void levelNavigateUp() {
		finishOffEnemy();
		level++;
		levelCount = level == highestRegionLevels[region] ? 1: levelMaxCount;
		if (level >= highestRegionLevels[region] || level == regionLevels[region,1])
			levelNavigateUpButton.gameObject.SetActive(false);
		levelNavigateDownButton.gameObject.SetActive(true);
		spawnNewEnemy(true);
		saveStateController.SaveData();
		uiclickAudio.navigateSound();
	}

	public void levelNavigateDown() {
		if (!itemController.itemDrop && !modalOpen) {
			finishOffEnemy();
			level--;
			levelCount = levelMaxCount;
			boss = false;
			if (level <= regionLevels[region,0])
				levelNavigateDownButton.gameObject.SetActive(false);
			levelNavigateUpButton.gameObject.SetActive(true);
			spawnNewEnemy(true);
		}
		saveStateController.SaveData();
		uiclickAudio.navigateSound();
	}

	public bool levelJump(int i) {
		if (!itemController.itemDrop && !modalOpen && i <= highestRegionLevels[region]) {
			finishOffEnemy();
			level = i;
			levelCount = level == highestRegionLevels[region] ? 1: levelMaxCount;
			boss = false;
			spawnNewEnemy(true);
			
			// saveStateController.SaveData();
			uiclickAudio.navigateSound();
			return true;
		}
		return false;
	}

	public void completeRegion() {
		upgradeController.enableMapButton(true);
		completedRegions[region] = true;
		regionCompleteText.SetActive(true);
		Debug.Log("region complete event");
		FirebaseAnalytics.LogEvent("region_gen_complete");
		if (regionButtons.Length > region+1)
			regionButtons[region+1].SetActive(true);
		achievementController.checkAchievement("region",region);
		if (region == 1) {
			prestigeButton.gameObject.SetActive(true);
			itemController.modern = true;
			// Log a prestige unlock event  need to update parameter
			Debug.Log("Logging a pprestige unlock event.");
			FirebaseAnalytics.LogEvent("prestige_unlock",FirebaseAnalytics.ParameterLevel, "prestige");
			//region 2 complete
		//	Debug.Log("region 2 complete event");
		//	FirebaseAnalytics.LogEvent("region_2_complete");
		}
		if (region == 0) {
			//region 1 complete
			Debug.Log("region 1 complete event");
			FirebaseAnalytics.LogEvent("region_1_complete");
		}
	//	if (region == 2) {
	//		//region 3 complete
	//		Debug.Log("region 3 complete event");
	//		FirebaseAnalytics.LogEvent("region_3_complete");
	//	}
	//	if (region == 3) {
	//		//region 4 complete
	//		Debug.Log("region 4 complete event");
	//		FirebaseAnalytics.LogEvent("region_4_complete");
	//	}
	//	if (region == 4) {
	//		//region 5 complete
	//		Debug.Log("region 5 complete event");
	//		FirebaseAnalytics.LogEvent("region_5_complete");
	//	}
		totalRegionsCompleted++;
		saveStateController.SaveData();
	}

#endregion

#region Map Navigation

	public void changeRegion(int i) {
		if (!itemController.itemDrop && !modalOpen) {
			swipeCapture.SnapClose();
			levelArea.SetActive(true);
			shopPanel.SetActive(false);
			finishOffEnemy();
			region = i;
			regionCompleteText.SetActive(completedRegions[i]);
			// level = regionLevels[i,completedRegions[i] ? 1 : 0];
			uniqueBoss = false;
			boss=false;
			level = highestRegionLevels[i];
			levelCount = 1;
			if (completedRegions[i]) {
				levelNavigateDownButton.gameObject.SetActive(true);
				levelNavigateUpButton.gameObject.SetActive(false);
			} else {
				levelNavigateDownButton.gameObject.SetActive(level > regionLevels[i,0]);
				levelNavigateUpButton.gameObject.SetActive(false);	
			}
			SetUpRegion(i);
			spawnNewEnemy(true);
			saveStateController.SaveData();
		}
	}

	public void SetUpRegion(int i) {
		setRegionBackground(i);
		buildingController.CreateBuildingNavigation();
	}

	public void setRegionBackground(int i) {
		for(int j = 0; j < regionBackgrounds.Length; j++) {
			regionBackgrounds[j].SetActive(j==(i+1));
		}
		for(int j = 0; j < shopBackgrounds.Length; j++)
			shopBackgrounds[j].SetActive(false);
		playerIndicator.transform.position = regionButtons[i].transform.position+playerIndicatorOffset;
	}

	public void goToUnique(int i) {
		if (!itemController.itemDrop && !modalOpen) {
			swipeCapture.SnapClose();
			levelArea.SetActive(true);
			shopPanel.SetActive(false);
			finishOffEnemy();
			levelNavigateUpButton.gameObject.SetActive(false);
			levelNavigateDownButton.gameObject.SetActive(false);
			for(int j = 0; j < regionBackgrounds.Length; j++)
				regionBackgrounds[j].SetActive(j==0);
			for(int j = 0; j < shopBackgrounds.Length; j++)
				shopBackgrounds[j].SetActive(false);
			levelCount = 1;
			spawnUnique(i);
			playerIndicator.transform.position = uniqueBossButtons[i].transform.position+playerIndicatorOffset;
			regionCompleteText.SetActive(false);
			buildingController.HideNavigationScrollView();
		}
	}
	public void goToShop(int i) {
		if (!itemController.itemDrop && !modalOpen) {
			swipeCapture.SnapClose();
			for(int j = 0; j < regionBackgrounds.Length; j++)
				regionBackgrounds[j].SetActive(false);
			for(int j = 0; j < shopBackgrounds.Length; j++)
				shopBackgrounds[j].SetActive(j==i);
			finishOffEnemy(); 
			playerIndicator.transform.position = shopButtons[i].transform.position+playerIndicatorOffset;
			levelArea.SetActive(false);
			shopPanel.SetActive(true);
			buildingController.HideNavigationScrollView();
			//opened store event
			Debug.Log("opened shop event");
			FirebaseAnalytics.LogEvent("shop_opened");
		}
	}

	public void prestige() {
		changeRegion(0);
		units[0] = 1;
		baseLevelUnits[0] = 1;
		characterLevel[0] = 1;
		for(int i =1; i<units.Length;i++){
			units[i] = 0;
			baseLevelUnits[i] = 0;
			characterLevel[i] = 0;
		}
		gold = 0;
		level = 1;
		highestLevel = 1;
		levelCount = 1;
		// characterUpgradeCost = new double[] {3,30,400,5000,60000,700000,8000000,90000000};
		boss = false;
		uniqueBoss = false;
		for (int i = 0; i < completedRegions.Length;i++){
			completedRegions[i] = false;
			highestRegionLevels[i] = regionLevels[i,0];
		}
		for (int i = 0; i < uniqueBossButtons.Length;i++){
			uniqueBossButtons[i].interactable = false;
		}
		for (int i = 0; i < uniqueBossCompleted.Length;i++) {
			uniqueBossCompleted[i] = false;
		}
		//TODO change this
		// Start();
		SetUp();
		upgradeController.restart();
		upgradeController.UndoBoosts();
		upgradeController.goldTab();
		upgradeController.resetScroll();
		achievementController.checkAchievement("prestige",1);
		IncrementPrestigeCurrency(unconvertedPrestigeCurrency);
		unconvertedPrestigeCurrency = 0;
		prestigeText.gameObject.SetActive(true);
		upgradeController.SetCurrencyPanelPrestige();
		totalPrestiges++;
		saveStateController.SaveData();
		//event prestige
			Debug.Log("prestige event");
			FirebaseAnalytics.LogEvent("prestiged");			
	}

#endregion

#region Diamonds
	public void getDiamond() {
		IncrementDiamonds(1);
		if (diamonds > 0){ 
			upgradeController.enableDiamondButton(true);
		}
	}
#endregion

#region Coal
	public void getCoal() {
		IncrementCoal(1);
	}

	public void IncrementCoal(double increment) {
		coal += increment;
		coalText.gameObject.SetActive(true);
		upgradeController.SetCurrencyPanelCoal();
		//TODO Total coal increment goes here if the stat exists
	}

	public void convertCoal() {
		IncrementDiamonds(coal);
		coal = 0;
		saveStateController.SaveData();
		closeCoalModal();
	}

	public void activateModal() {
		modalBackDrop.SetActive(true);
		modalOpen = true;
	}

	public void closeModal() {
		modalBackDrop.SetActive(false);
		modalOpen = false;
	}

	public void showCoalModal() {
		coalConversionText.text = NumberFormat.format(coal);
		diamondConversionText.text = NumberFormat.format(coal);
		coalModal.SetActive(true);
		activateModal();
	}

	public void closeCoalModal() {
		coalModal.SetActive(false);
		closeModal();
	}
#endregion

#region Idling
	public void idleTimerCountdown() {
		if ((idleGoldBonus > 1.0 || idleUPSBonus > 1.0) && !idling) {
			idleTimer--;
			if (idleTimer <= 0){
				idling = true;
				idlingText.gameObject.SetActive(true);
				RecalculateAllUnits();
			}

		}
	}

	public void StopIdling() {
		bool wasIdling = idling;
		idling = false;
		idleTimer = idleStartTimer;
		idlingText.gameObject.SetActive(false);
		if (wasIdling)
			RecalculateAllUnits();
	}
#endregion

	IEnumerator SpawnUFOs() {
		while (true) {
			float waitSeconds = UnityEngine.Random.Range(10f, 100f);
			// float waitSeconds = UnityEngine.Random.Range(5f, 10f);
			yield return new WaitForSeconds(waitSeconds);
			int ufoIndex = Mathf.RoundToInt(UnityEngine.Random.Range(0,UFOPrefabs.Length));
			GameObject ufo = Instantiate(UFOPrefabs[ufoIndex],Vector3.zero,Quaternion.Euler(0, 0, 0));
			ufo.GetComponent<UFO>().value = Math.Round((3*baseGoldDrop*Math.Pow(baseGoldMultiplier,highestLevel)*goldMultiplier1*goldMultiplier2)*(1+waitSeconds/100));
		}
	}
}