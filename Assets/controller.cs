using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	public double[] units = new double[] {1, 0, 0, 0, 0, 0, 0, 0};
	public double gold = 0;
	public double diamonds = 0;
	public float diamondChance = 0.05f;
	public int level = 1;
	public int highestLevel = 1;
	public int levelCount = 1;
	public int levelMaxCount = 10;
	public double baseGoldDrop = 1;
	public double baseGoldMultiplier = 1.07;
	public double baseHealth = 5;
	public double healthMultiplier = 1.12;
	public double goldMultiplier1 = 1;
	public double goldMultiplier2 = 1;
	public double baseUnitMultiplier = 1.04;
	public double[] baseUnits = new double[] {1, 1, 10, 100, 1000, 10000, 100000, 1000000};
	public double[] baseLevelUnits = new double[] {1, 0, 0, 0, 0, 0, 0, 0};
	public float[] periods = new float[] {0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f};
	public double[] unitM1 = new double[] {1.1, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00};
	public int[] m1Level = new int[] {1, 1, 1, 1, 1, 1, 1, 1};
	public double[] m1UpgradeCost = new double[] {5, 8};
	public double[] m1UpgradeBaseCost = new double[] {5, 8};
	public double[] m1UpgradeCostMultiplier = new double[] {1.08, 1.09};
	public double[] unitItemM2 = new double[] {1.0, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00};
	public double[] unitAchievementM3 = new double[] {1.0, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00};
	public int[] characterLevel = new int[] {1, 0, 0, 0, 0, 0, 0, 0};
	public double[] characterUpgradeCost = new double[] {10, 60, 700, 8000, 90000, 100000, 1100000, 12000000};
	public double[] baseCharacterUpgradeCost = new double[] {10, 60, 700, 8000, 90000, 100000, 1100000, 12000000};
	public double[] characterUpgradeCostMultiplier = new double[] {1.07, 1.1, 1.1, 1.1, 1.1, 1.1, 1.1, 1.1};
	public Text[] characterLevelText;
	public Button[] levelUpButton;
	public Text[] unitText;
	public Text[] unitMultiplierText;
	public Button[] unitM1Button;
	public GameObject[] enemyPrefabs;
    public SimpleHealthBar healthBar;
	public Text levelText = null;
	public Text goldText = null;
	public Text diamondText = null;
	public bool boss = false;
	public bool uniqueBoss = false;
	public Text enemyDescriptionText;
	public string[] enemyNouns;
	public string[] enemyAdjectives;
	public upgradeController upgradeController;
	public ItemController itemController;

	public GameObject goldPanel;
	public Button instaGoldButton;
	public Text instaGoldText;
	public double instaGoldPrice = 20;
	public double instaGoldMultiplier = 200;
	public Button levelNavigateUpButton;
	public Button levelNavigateDownButton;
	public int region = 0;
	public bool[] completedRegions = new bool[] {false,false,false,false};
	public int[,] regionLevels = new int[,] {{1,50},{50,120},{110,200},{200,350}};
	public int[] highestRegionLevels = new int[] {1,50,110,200};
	public ArrayList completedBossLevels = new ArrayList();
	public GameObject regionCompleteText;
	public GameObject[] regionBackgrounds;
	public GameObject[] regionButtons;
	public Text partnerUnitText;
	public GameObject blueprintPrefab;
    public GameObject chestPrefab;
	public Button[] uniqueBossButtons;
	public GameObject[] uniqueBossPrefabs;
	public string[] uniqueNouns;
	public int[] uniqueBossLevels;
	public GameObject playerIndicator;
	public Vector3 playerIndicatorOffset = new Vector3(-20,0,0);
	public Text bossTimeText;
	public int bossTime = 30;
	public int bossStartTime = 30;
	public bool bossTimeCountdownFlag = false;
	public double sumUnits = 0;
	public achievementController achievementController;

	void Start () {
		Application.runInBackground = true;
		double health = 0;
		double maxHealth = calculateHealth();
		healthBar.UpdateBar( health, maxHealth );
		levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMaxCount;

		characterLevelText[0].text = "Hero Level: "+characterLevel[0];
		levelUpButton[0].GetComponentInChildren<Text>().text = "Level Up: "+characterUpgradeCost[0]+"g";
		unitText[0].text = "Units: "+units[0];
		
		characterLevelText[1].text = "Partner";
		for (int i = 1; i < upgradeController.characterAmount; i++) {
			levelUpButton[i].GetComponentInChildren<Text>().text = "Hire: "+characterUpgradeCost[i]+"g";
			unitText[i].text = "";
		}
		instaGoldButton.GetComponentInChildren<Text>().text = instaGoldPrice+" diamonds";
		instaGoldText.text = calculateMaxGold()+" gold!";
		levelNavigateDownButton.gameObject.SetActive(false);
		levelNavigateUpButton.gameObject.SetActive(false);
		for (int i = 1; i < regionButtons.Length; i++)
			regionButtons[i].SetActive(false);
		foreach(Button button in uniqueBossButtons)
			button.interactable=false;
		playerIndicator.transform.position = regionButtons[0].transform.position+playerIndicatorOffset;
		bossTimeText.gameObject.SetActive(false);
		InvokeRepeating("bossTimeCountdown",Time.time,1.0f);
		InvokeRepeating("checkUnitAchievement",Time.time,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		double tempSum = 0;
		for(int i = 0; i < upgradeController.characterAmount; i++) {
			levelUpButton[i].interactable = gold >= characterUpgradeCost[i];
			if (characterLevel[i] > 0) {
				unitText[i].text = "Units: "+NumberFormat.format(units[i]);				
			}
			if (i < unitM1Button.Length)
				unitM1Button[i].interactable = diamonds >= m1UpgradeCost[i];
			if (i != 0)
				tempSum += units[i];
		}
		sumUnits = tempSum;
		partnerUnitText.text = "Units: " + NumberFormat.format(sumUnits);
		goldText.text = "Gold: "+NumberFormat.format(gold);
		diamondText.text = "Diamonds: "+diamonds;
		instaGoldButton.interactable = diamonds >= instaGoldPrice;
	}

	public void checkUnitAchievement() {
		achievementController.checkAchievement("units",sumUnits);
		achievementController.checkAchievement("unitIndex0",units[0]);
	}

	private void enemyLevelUp(bool advanceLevel) {
		if (level == regionLevels[region,1]) {
			if (!completedRegions[region]) {
				completeRegion();
			}
			levelCount = levelMaxCount;
		} else {
			highestLevel++;
			highestRegionLevels[region] = level+1;
			if (advanceLevel) {
				level++;
				levelCount = 1;
			}
			instaGoldText.text = calculateMaxGold()+" gold!";
			levelNavigateDownButton.gameObject.SetActive(true);
		}
	}

	private double calculateGold() {
		return Math.Round(baseGoldDrop*Math.Pow(baseGoldMultiplier,level)*goldMultiplier1*goldMultiplier2);
	}

	private double calculateMaxGold() {
		return Math.Round((baseGoldDrop*Math.Pow(baseGoldMultiplier,highestLevel)*goldMultiplier1*goldMultiplier2))*instaGoldMultiplier;
	}

	public void RecalculateItemMultipliers() {
		for (int i = 0; i < upgradeController.characterAmount; i++) {
			double multiplier = 1.0;
			foreach(Item item in itemController.inventory) {
				if (item.effect == "partners" && i != 0)
						multiplier += item.effectValue*item.count;
				else if (item.effect ==  "unitIndex" + i)
						multiplier += item.effectValue*item.count;
			}
			unitItemM2[i] = multiplier;
			RecalculateUnit(i);
		}
	}

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

	public void RecalculateUnit(int i) {
		units[i] = Math.Round(baseLevelUnits[i]*unitM1[i]*unitItemM2[i]*unitAchievementM3[i]);
	}

	public void LevelUpUnit(int i, int numLevels) {
		for (int j = 0; j < numLevels; j++){
			//TODO: exponential or fixed returns?
			// baseLevelUnits[i] = characterLevel[i] == 0 ? 0 : baseLevelUnits[i]+Math.Round(baseUnits[i]*Math.Pow(baseUnitMultiplier,characterLevel[i]));
			baseLevelUnits[i] = characterLevel[i] == 0 ? 0 : baseLevelUnits[i]+baseUnits[i];
		}
		RecalculateUnit(i);
	}

	public double calculateHealth() {
		if (uniqueBoss)
			return Math.Round(baseHealth*Math.Pow(healthMultiplier,level)) * 10; 
		if (boss)
			return Math.Round(baseHealth*Math.Pow(healthMultiplier,level)) * 5; 
		return Math.Round(baseHealth*Math.Pow(healthMultiplier,level));
	}
	public double enemyDied (bool spawn, bool advanceLevel) {
		double goldIncrement = boss ? calculateGold()*10 : calculateGold();
		if (uniqueBoss || boss)
			endBossTime();	
		if (!uniqueBoss) {
			if (level == 1 && levelCount == 1)
				upgradeController.enableGoldButton();

			if (level == highestRegionLevels[region]){
				if (boss) {
					boss = false;
					completedBossLevels.Add(level);
					enemyLevelUp(advanceLevel);
				}
				else {
					levelCount++;
					if (levelCount > levelMaxCount) {
						if (level%10 == 0 && !completedBossLevels.Contains(level) && advanceLevel)
							boss = true;
						else
							enemyLevelUp(advanceLevel);
					}
				}
			}
			if (spawn)
				spawnNewEnemy(false);
		}
		gold += goldIncrement;
		return goldIncrement;
	}

	private void spawnNewEnemy(bool delay) {
		int enemySelector;
		if (boss) {
			levelText.text = "Level "+level+"\nBOSS FIGHT!";
			enemySelector = 5;
			startBossTime();
		}
		else {
			levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMaxCount;
			enemySelector = ((level-1)/2)%5;
		}

		GameObject newEnemy = (GameObject) Instantiate(enemyPrefabs[enemySelector], new Vector3(0f,-5f,-5f),Quaternion.Euler(0, UnityEngine.Random.value*360f, 0));
		enemyDescriptionText.text = enemyAdjectives[((level-1)/10)%20] +" "+ enemyNouns[enemySelector];
		if (delay)
			newEnemy.GetComponent<House>().delay();
	}

	public void levelUp(int i) {
		int numLevels = upgradeController.multiLevelUpValues[upgradeController.currentMultiLevelUpIndex];
		characterLevel[i] += numLevels;
		gold -= characterUpgradeCost[i];
		string preText = i == 0 ? "Hero Level: " : "Partner "+i+" Level: ";
		characterLevelText[i].text = preText+characterLevel[i];
		RecalculateCharacterUpgradeCost(i);
		LevelUpUnit(i,numLevels);
		if (characterLevel[i] >= 5) 
			upgradeController.enableBoost1(i);
		if (characterLevel[i] >= 10) 
			upgradeController.enableBoost2(i);
		if (characterLevel[i] >= 20) 
			upgradeController.enableBoost3(i);
		upgradeController.enableBoard(i);
	}

	public void RecalculateCharacterUpgradeCost(int i) {
		int numLevels = upgradeController.multiLevelUpValues[upgradeController.currentMultiLevelUpIndex];
		characterUpgradeCost[i] = Math.Round(baseCharacterUpgradeCost[i]*Math.Pow(characterUpgradeCostMultiplier[i],characterLevel[i])*numLevels);
		string preText = numLevels == 1 ? (characterLevel[i] == 0 ? "Hire: ": "Level Up: ") : "x"+numLevels+": ";
		levelUpButton[i].GetComponentInChildren<Text>().text = preText+NumberFormat.format(characterUpgradeCost[i])+"g";
	}

	public void getDiamond() {
		diamonds++;
		if (diamonds > 0){ 
			upgradeController.enableDiamondButton();
		}
	}

	public void buyClickM1Up() {
		int i = 0;
		m1Level[i]++;
		diamonds -= m1UpgradeCost[i];
		diamondText.text = "Diamonds: "+diamonds;
		m1UpgradeCost[i] = Math.Round(m1UpgradeBaseCost[i]*Math.Pow(m1UpgradeCostMultiplier[i],m1Level[i]));
		unitM1Button[i].GetComponentInChildren<Text>().text = m1UpgradeCost[i]+" diamonds";
		unitM1[i] += .25f;
		unitMultiplierText[i].text = " + "+(m1Level[i]-1)*25+"%"; 
		RecalculateUnit(i);
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
		unitM1Button[1].GetComponentInChildren<Text>().text = m1UpgradeCost[1]+" diamonds";
		unitMultiplierText[1].text = " + "+(m1Level[1]-1)*25+"%"; 
	}

	public void buyInstaGold() {
		gold += calculateMaxGold();
		diamonds -= instaGoldPrice;
	}

	public void levelNavigateUp() {
		finishOffEnemy();
		level++;
		levelCount = level == highestRegionLevels[region] ? 1: levelMaxCount;
		if (level >= highestRegionLevels[region] || level == regionLevels[region,1])
			levelNavigateUpButton.gameObject.SetActive(false);
		levelNavigateDownButton.gameObject.SetActive(true);
		spawnNewEnemy(true);
	}

	public void levelNavigateDown() {
		if (!itemController.itemDrop) {
			finishOffEnemy();
			level--;
			levelCount = levelMaxCount;
			boss = false;
			if (level <= regionLevels[region,0])
				levelNavigateDownButton.gameObject.SetActive(false);
			levelNavigateUpButton.gameObject.SetActive(true);
			spawnNewEnemy(true);
		}
	}

	public void changeRegion(int i) {
		if (!itemController.itemDrop) {
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
			for(int j = 0; j < regionBackgrounds.Length; j++) {
				regionBackgrounds[j].SetActive(j==(i+1));
			}
			spawnNewEnemy(true);
			playerIndicator.transform.position = regionButtons[i].transform.position+playerIndicatorOffset;
		}
	}

	public void finishOffEnemy() {
		GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
		House house = enemy.GetComponent<House>();
		if (house.health >= house.maxHealth){
			enemyDied(false, false);
		}
		endBossTime();
		Destroy(enemy);
	}

	public void goToUnique(int i) {
		if (!itemController.itemDrop) {
			levelNavigateUpButton.gameObject.SetActive(false);
			levelNavigateDownButton.gameObject.SetActive(false);
			for(int j = 0; j < regionBackgrounds.Length; j++)
				regionBackgrounds[j].SetActive(j==0);
			finishOffEnemy();
			levelCount = 1;
			spawnUnique(i);
			playerIndicator.transform.position = uniqueBossButtons[i].transform.position+playerIndicatorOffset;
		}
	}

	private void spawnUnique(int i) {
		level = uniqueBossLevels[i];
		uniqueBoss = true;
		boss=true;
		// maxHealth = calculateHealth()*10;
		levelText.text = "Level "+level+"\nUnique #"+(i+1)+"!";

		GameObject newUnique = (GameObject) Instantiate(uniqueBossPrefabs[i], new Vector3(0f,-5f,-5f),Quaternion.Euler(-90,-125.2f, UnityEngine.Random.value*360f));
		newUnique.GetComponent<House>().health = 0;
		enemyDescriptionText.text = enemyAdjectives[((level-1)/10)%20] +" "+ uniqueNouns[i];
		newUnique.GetComponent<House>().delay();
		startBossTime();
	}

	public void completeRegion() {
		upgradeController.enableMapButton();
		// upgradeController.mapTab();
		completedRegions[region] = true;
		regionCompleteText.SetActive(true);
		if (regionButtons.Length > region+1)
			regionButtons[region+1].SetActive(true);
		achievementController.checkAchievement("region",region);
	}

	public void checkBossReward(Vector3 pos) {
		if (uniqueBoss) {
			GameObject chest = (GameObject) Instantiate(chestPrefab,pos+new Vector3(0,5f,-10f),Quaternion.Euler(-90, 152, 0));
			chest.GetComponentInChildren<chest>().SetItem(itemController.getCurrentBossItem());
			for (int i = 0;i< uniqueBossButtons.Length;i++) {
				if (level == uniqueBossLevels[i])
					uniqueBossButtons[i].interactable = false;
			}
		} else {
			if(unlockUniqueBoss()){
				GameObject blueprint = (GameObject) Instantiate(blueprintPrefab,pos+new Vector3(0,2f,-3f),Quaternion.Euler(0, 0, 0));
			}
		}
		upgradeController.enableMultiLevelUpButton();
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
			upgradeController.enableMapButton();
			// upgradeController.mapTab();
		}
		return dropBlueprint;
	}

	public void prestige() {
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
		changeRegion(0);
		Start();
		upgradeController.restart();
		upgradeController.goldTab();
		upgradeController.resetScroll();
		achievementController.checkAchievement("prestige",1);
	}

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
}