using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	public int[] units = new int[] {1, 0, 0, 0, 0, 0, 0, 0};
	public int gold = 0;
	public int diamonds = 0;
	public float diamondChance = 0.05f;
	public int level = 1;
	public int highestLevel = 1;
	public int levelCount = 1;
	public int levelMaxCount = 10;
	public int baseGoldDrop = 1;
	public float baseGoldMultiplier = 1.07f;
	public int baseHealth = 5;
	public float healthMultiplier = 1.12f;
	public float goldMultiplier1 = 1f;
	public float goldMultiplier2 = 1f;
	public float baseUnitMultiplier = 1.04f;
	public int[] baseUnits = new int[] {1, 1, 10, 100, 1000, 10000, 100000, 1000000};
	public float[] periods = new float[] {0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f};
	public float[] unitM1 = new float[] {1.1f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f};
	public int[] m1Level = new int[] {1, 1, 1, 1, 1, 1, 1, 1};
	public int[] m1UpgradeCost = new int[] {5, 8};
	public int[] m1UpgradeBaseCost = new int[] {5, 8};
	public float[] m1UpgradeCostMultiplier = new float[] {1.08f, 1.09f};
	public float[] unitM2 = new float[] {1.1f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f};
	public int[] characterLevel = new int[] {1, 0, 0, 0, 0, 0, 0, 0};
	public int[] characterUpgradeCost = new int[] {10, 60, 700, 8000, 90000, 100000, 1100000, 12000000};
	public int[] baseCharacterUpgradeCost = new int[] {10, 60, 700, 8000, 90000, 100000, 1100000, 12000000};
	public float[] characterUpgradeCostMultiplier = new float[] {1.07f, 1.1f, 1.1f, 1.1f, 1.1f, 1.1f, 1.1f, 1.1f};
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
	public Text enemyDescriptionText;
	public string[] enemyNouns;
	public string[] enemyAdjectives;

	public upgradeController upgradeController;

	public GameObject tabsScrollView;
	public GameObject goldPanel;
	public GameObject diamondPanel;

	public Button instaGoldButton;
	public Text instaGoldText;
	public int instaGoldPrice = 20;
	public int instaGoldMultiplier = 200;

	void Start () {
		Application.runInBackground = true;
		int health = 0;
		int maxHealth = calculateHealth();
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
		if (diamonds < 1)
			diamondPanel.SetActive(false);
		instaGoldButton.GetComponentInChildren<Text>().text = instaGoldPrice+" diamonds";
		instaGoldText.text = calculateMaxGold()+" gold!";
		goldPanel.SetActive(false);
		tabsScrollView.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < upgradeController.characterAmount; i++) {
			levelUpButton[i].interactable = gold >= characterUpgradeCost[i];
			if (characterLevel[i] > 0) 
				unitText[i].text = "Units: "+units[i];
			if (i < unitM1Button.Length)
				unitM1Button[i].interactable = diamonds >= m1UpgradeCost[i];
		}
		goldText.text = "Gold: "+gold;
		diamondText.text = "Diamonds: "+diamonds;
		instaGoldButton.interactable = diamonds >= instaGoldPrice;
		// p1DamageM1Button.interactable = diamonds >= p1M1UpgradeCost;
		// clickDamageMultiplierText.text = "+ "+(clickMultiplier1-1.0f)*100+"%";
		// p1DamageM1Text.text = "+ "+(p1Multiplier-1.0f)*100+"%";
	}

	private void enemyLevelUp() {
		level++;
		baseHealth++;
		levelCount = 1;
		highestLevel++;
		instaGoldText.text = calculateMaxGold()+" gold!";
	}

	private int calculateGold() {
		return Mathf.RoundToInt(baseGoldDrop*Mathf.Pow(baseGoldMultiplier,level)*goldMultiplier1*goldMultiplier2);
	}

	private int calculateMaxGold() {
		return Mathf.RoundToInt((baseGoldDrop*Mathf.Pow(baseGoldMultiplier,highestLevel)*goldMultiplier1*goldMultiplier2))*instaGoldMultiplier;
	}
	public void RecalculateUnits(int i) {
		units[i] = characterLevel[i] == 0 ? 0 : units[i]+(int)(baseUnits[i]*unitM1[i]*Mathf.Pow(baseUnitMultiplier,characterLevel[i]));
	}

	public int calculateHealth() {
		return (int)(baseHealth*Mathf.Pow(healthMultiplier,level));
	}
	public int enemyDied () {
		int goldIncrement = boss ? calculateGold()*10 : calculateGold();
		if (boss) {
			boss = false;
			enemyLevelUp();
		}
		else {
			levelCount++;
			if (levelCount > levelMaxCount) {
				if (level%10 == 0) 
					boss = true;
				else
					enemyLevelUp();
			}
		}
		spawnNewEnemy();
		if (!goldPanel.active){
			goldPanel.SetActive(true);
			tabsScrollView.SetActive(true);
		}
		gold += goldIncrement;
		return goldIncrement;
	}

	private void spawnNewEnemy() {
		int health, maxHealth, enemySelector;
		if (boss) {
			health = 0;
			maxHealth = calculateHealth()*5;
			levelText.text = "Level "+level+"\nBOSS FIGHT!";
			enemySelector = 5;
		}
		else {
			health = 0;
			maxHealth = calculateHealth();
			levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMaxCount;
			enemySelector = ((level-1)/2)%5;
		}

		GameObject newEnemy = (GameObject) Instantiate(enemyPrefabs[enemySelector], new Vector3(0f,-5f,-5f),Quaternion.Euler(0, Random.value*360f, 0));
		newEnemy.GetComponent<House>().health = 0;
		newEnemy.GetComponent<House>().maxHealth = maxHealth; //redundant?
		healthBar.UpdateBar( health, maxHealth );
		enemyDescriptionText.text = enemyAdjectives[((level-1)/10)%20] +" "+ enemyNouns[enemySelector];
	}

	public void levelUp(int i) {
		characterLevel[i]++;
		gold -= characterUpgradeCost[i];
		string preText = i == 0 ? "Hero Level: " : "Partner "+i+" Level: ";
		characterLevelText[i].text = preText+characterLevel[i];
		characterUpgradeCost[i] = (int)(baseCharacterUpgradeCost[i]*Mathf.Pow(characterUpgradeCostMultiplier[i],characterLevel[i]));
		levelUpButton[i].GetComponentInChildren<Text>().text = "Level Up: "+characterUpgradeCost[i]+"g";
		RecalculateUnits(i);
		if (characterLevel[i] >= 5) 
			upgradeController.enableBoost1(i);
		if (characterLevel[i] >= 10) 
			upgradeController.enableBoost2(i);
		if (characterLevel[i] >= 20) 
			upgradeController.enableBoost3(i);
		upgradeController.enableBoard(i);
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
		m1UpgradeCost[i] = (int)(m1UpgradeBaseCost[i]*Mathf.Pow(m1UpgradeCostMultiplier[i],m1Level[i]));
		unitM1Button[i].GetComponentInChildren<Text>().text = m1UpgradeCost[i]+" diamonds";
		unitM1[i] += .25f;
		unitMultiplierText[i].text = " + "+(m1Level[i]-1)*25+"%"; 
		RecalculateUnits(i);
	}

	public void buyAllPartnersM1Up() {
		for (int i = 1; i < upgradeController.characterAmount; i++) {
			m1Level[i]++;
			unitM1[i] += .25f;
			RecalculateUnits(i);
		}
		diamonds -= m1UpgradeCost[1];
		diamondText.text = "Diamonds: "+diamonds;
		m1UpgradeCost[1] = (int)(m1UpgradeBaseCost[1]*Mathf.Pow(m1UpgradeCostMultiplier[1],m1Level[1]));
		unitM1Button[1].GetComponentInChildren<Text>().text = m1UpgradeCost[1]+" diamonds";
		unitMultiplierText[1].text = " + "+(m1Level[1]-1)*25+"%"; 
	}

	// public void buyM1Up(int i) {
	// 	m1Level[i]++;
	// 	diamonds -= m1UpgradeCost[i];
	// 	diamondText.text = "Diamonds: "+diamonds;
	// 	m1UpgradeCost[i] = (int)(m1UpgradeBaseCost[i]*Mathf.Pow(m1UpgradeCostMultiplier[i],m1Level[i]));
	// 	unitM1Button[i].GetComponentInChildren<Text>().text = m1UpgradeCost[i]+" diamonds";
	// 	unitM1[i] += .25f;
	// 	unitMultiplierText[i].text = " + "+(m1Level[i]-1)*25+"%"; 
	// 	RecalculateUnits(i);
	// }

	public void buyInstaGold() {
		gold += calculateMaxGold();
		diamonds -= instaGoldPrice;
	}
}
