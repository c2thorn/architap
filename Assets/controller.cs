using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

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
	public GameObject[] enemyPrefabs;
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
		// saveStateController.LoadData();
		Application.runInBackground = true;
		double health = 0;
		double maxHealth = calculateHealth();
		healthBar.UpdateBar( health, maxHealth );

		//Screen text
		levelText.text = "LEVEL "+level;
		amountText.text = levelCount+" / "+levelMaxCount;
		levelUpButton[0].gameObject.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[0]);
		characterUnitLevelText[0].text = "LEVEL: "+characterLevel[0]+" UNITS: "+units[0];
		// characterLevelText[1].text = "Partner";
		for (int i = 1; i < upgradeController.characterAmount; i++) {
			levelUpButton[i].gameObject.transform.Find("Action Text").GetComponent<Text>().text = "HIRE";
			levelUpButton[i].gameObject.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[i]);
			characterUnitLevelText[i].text = "";
		}
		instaGoldButton.GetComponentInChildren<Text>().text = instaGoldPrice+" diamonds";
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
		modalOpen = false;
		playerIndicator.transform.position = regionButtons[0].transform.position+playerIndicatorOffset;
		// totalBuildings = 0;
		SetUp();
		// if (highestLevel < 2)
		// 	gold = 1000000;
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
		//resetSkillCooldownsButton.interactable = diamonds >= resetSkillCooldownsPrice;
		resetSkillCooldownsButton.interactable = false;
		instantPrestigeButton.interactable = diamonds >= instantPrestigePrice && unconvertedPrestigeCurrency > 0;
		instantPrestigeText.text = "+"+NumberFormat.format(unconvertedPrestigeCurrency) + " NOTES";
		gildRandomHeroButton.interactable = diamonds >= gildRandomHeroPrice;
		

	}

	void OnApplicationQuit() {
		// saveStateController.SaveData();
	}

	public void SetUp() {
		RecalculateAchievementMultipliers();
		for (int i = 0; i < baseCharacterUpgradeCost.Length; i++)
			RecalculateCharacterUpgradeCost(i);
		RecalculateItemMultipliers();
		itemController.refreshInventoryUI();
		achievementController.refreshAchievementsUI();
		if (highestLevel > 1 || levelCount > 1 || totalPrestiges > 0) {
			upgradeController.enableGoldButton();
			settingsController.enableSettings();
			tutorialController.RemovePointer();
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
			// else {
			// 	levelUpButton[i].gameObject.transform.Find("Action Text").GetComponent<Text>().text = "HIRE";
			// 	levelUpButton[i].gameObject.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[i]);
			// 	characterUnitLevelText[i].text = "";
			// }
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
		// setRegionBackground(region);
		SetUpRegion(region);
		regionCompleteText.SetActive(completedRegions[region]);

		m1UpgradeCost[0] = Math.Round(m1UpgradeBaseCost[0]*Math.Pow(m1UpgradeCostMultiplier[0],m1Level[0]));
		unitM1Button[0].transform.Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[0] + "";
		unitMultiplierText[0].text = "CURRENT BONUS: + "+(m1Level[0]-1)*25+"%"; 


		m1UpgradeCost[1] = Math.Round(m1UpgradeBaseCost[1]*Math.Pow(m1UpgradeCostMultiplier[1],m1Level[1]));
		unitM1Button[1].transform.Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[1] + "";
		unitMultiplierText[1].text = "CURRENT BONUS: + "+(m1Level[1]-1)*25+"%"; 

		prestigeButton.gameObject.SetActive(completedRegions[1]);

		bonusEnemy = false;

		idleTimer = idleStartTimer;
		idlingText.gameObject.SetActive(false);

		//Should be last
		saveStateController.CheckIdleTime();
	}
#endregion

#region Calculations
	public double calculateGold() {
		double calc = Math.Round(baseGoldDrop*Math.Pow(baseGoldMultiplier,level)*goldMultiplier1*goldMultiplier2);
		if (idling && idleGoldBonus > 1)
			calc = Math.Round(calc*idleGoldBonus);
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
			units[i] = Math.Round(units[i]*skillController.skillEffect["clickBoost"]);
		} else if (i != 0 && skillController.skillFlag["partnerBoost"]) {
			units[i] = Math.Round(units[i]*skillController.skillEffect["partnerBoost"]);
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
		int numLevels = upgradeController.multiLevelUpValues[upgradeController.currentMultiLevelUpIndex];
		characterLevel[i] += numLevels;
		gold -= characterUpgradeCost[i];
		upgradeController.RefreshCharacterBoard(i);
		LevelUpUnit(i,numLevels);
		if (!characterEverBought[i]){
			characterEverBought[i] = true;
			if ((i+1) < upgradeController.characterAmount)
				upgradeController.ScrollABit();
		}
		saveStateController.SaveData();
		// characterAudio.levelUpSound();
	}


#endregion
	
#region Upgrades/Purchases
	public void RecalculateCharacterUpgradeCost(int i) {
		int numLevels = upgradeController.multiLevelUpValues[upgradeController.currentMultiLevelUpIndex];
		double sum = 0;
		for (int j = 0; j < numLevels; j++)
			sum += Math.Round(baseCharacterUpgradeCost[i]*Math.Pow(characterUpgradeCostMultiplier[i],characterLevel[i]+j));
		characterUpgradeCost[i] = sum;
		levelUpButton[i].gameObject.transform.Find("Action Text").GetComponent<Text>().text = characterLevel[i] > 0 ? "LEVEL UP" : "HIRE";
		levelUpButton[i].gameObject.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[i]);
		individualLevelUpButton.transform.Find("Price Text").GetComponent<Text>().text = NumberFormat.format(characterUpgradeCost[i]);
	}
	
	public void buyClickM1Up() {
		int i = 0;
		m1Level[i]++;
		diamonds -= m1UpgradeCost[i];
		diamondText.text = "Diamonds: "+diamonds;
		m1UpgradeCost[i] = Math.Round(m1UpgradeBaseCost[i]*Math.Pow(m1UpgradeCostMultiplier[i],m1Level[i]));
		unitM1Button[i].transform.Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[i]+"";
		unitM1[i] += .25f;
		unitMultiplierText[i].text = "CURRENT BONUS: + "+(m1Level[i]-1)*25+"%"; 
		RecalculateUnit(i);
		saveStateController.SaveData();
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
		unitM1Button[1].transform.Find("Price Text").GetComponent<Text>().text = m1UpgradeCost[1]+"";
		unitMultiplierText[1].text = "CURRENT BONUS: + "+(m1Level[1]-1)*25+"%"; 
		saveStateController.SaveData();
	}

	public void buyInstaGold() {
		IncrementGold(calculateMaxGold());
		diamonds -= instaGoldPrice;
		saveStateController.SaveData();
	}

	public void buyRandomItem() {
		if (!itemController.itemDrop || modalOpen) {
			Item item = itemController.getRandomItem();
			DropItem(new Vector3(0,-2,-5),item);
			diamonds -= randomItemPrice;
			saveStateController.SaveData();
		}
	}

	public void buyInstantPrestige() {
		IncrementPrestigeCurrency(unconvertedPrestigeCurrency);
		unconvertedPrestigeCurrency = 0;
		diamonds -= instantPrestigePrice;
		saveStateController.SaveData();
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
	}

	public void gildCharacter(int index) {
		characterGilds[index]++;
		upgradeController.RefreshCharacterBoard(index);
		RecalculateUnit(index);
		saveStateController.SaveData();
	}
#endregion

#region Items
	public void RecalculateItemMultipliers() {
		List<Item> itemsLeft = new List<Item>(itemController.inventory);
		goldMultiplier1 = 1;
		bossLifeItemMultiplier = 1;
		bossStartTime = 30;
		bonusEnemyChance = 0.02f;
		bonusEnemyMultiplier = 7;
		prestigeDropItemMultiplier = 1;
		prestigeEffectItemMultiplier = 1;
		criticalClickChance = 0;
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
				buildingController.centerOnButton();
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
		bonusEnemy = false;
		if (uniqueBoss || boss)
			endBossTime();	
		if (!uniqueBoss) {
			if (level == 1 && levelCount == 1){
				upgradeController.enableGoldButton();
				settingsController.enableSettings();
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
		int enemySelector;
		if (boss) {
			levelText.text = "LEVEL "+level;
			amountText.text = "BOSS FIGHT!";
			enemySelector = 5;
			startBossTime();
		}
		else {
			levelText.text = "LEVEL "+level;
			amountText.text = levelCount+" / "+levelMaxCount;
			enemySelector = ((level-1)/2)%5;
			bonusEnemy = UnityEngine.Random.value <= bonusEnemyChance;
		}

		GameObject newEnemy = (GameObject) Instantiate(enemyPrefabs[enemySelector], new Vector3(0f,0.3f,-5f),Quaternion.Euler(0,0, 0));
		enemyDescriptionText.text = enemyAdjectives[((level-1)/10)%20] +" "+ enemyNouns[enemySelector];
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

		GameObject newUnique = (GameObject) Instantiate(uniqueBossPrefabs[i], new Vector3(0f,0.3f,-5f),Quaternion.Euler(0,0,0));
		newUnique.GetComponent<House>().health = 0;
		enemyDescriptionText.text = enemyAdjectives[((level-1)/10)%20] +" "+ uniqueNouns[i];
		newUnique.GetComponent<House>().delay();
		startBossTime();
	}

	public void checkBossReward(Vector3 pos) {
		if (uniqueBoss) {
			endBossTime();
			Item item = itemController.getRandomItem();
			DropItem(pos, item);
		} else {
			if(unlockUniqueBoss()){
				GameObject blueprint = (GameObject) Instantiate(blueprintPrefab,pos+new Vector3(0,2f,-3f),Quaternion.Euler(0, 0, 0));
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
			
			saveStateController.SaveData();
			uiclickAudio.navigateSound();
			return true;
		}
		return false;
	}

	public void completeRegion() {
		upgradeController.enableMapButton(true);
		completedRegions[region] = true;
		regionCompleteText.SetActive(true);
		if (regionButtons.Length > region+1)
			regionButtons[region+1].SetActive(true);
		achievementController.checkAchievement("region",region);
		if (region == 1) {
			prestigeButton.gameObject.SetActive(true);
			itemController.modern = true;
		}
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
		Start();
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

	public void showCoalModal() {
		coalConversionText.text = NumberFormat.format(coal);
		diamondConversionText.text = NumberFormat.format(coal);
		coalModal.SetActive(true);
		modalOpen = true;
	}

	public void closeCoalModal() {
		coalModal.SetActive(false);
		modalOpen = false;
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
}