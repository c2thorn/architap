using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	public int clickDamage = 1;
	public int gold = 0;
	public int diamonds = 0;
	public float diamondChance = 0.05f;
	public int level = 1;
	public int levelCount = 1;
	public int levelMax = 10;
	public int baseGoldDrop = 1;
	public int baseHealth = 2;
	public float goldMultiplier1 = 1.1f;
	public float goldMultiplier2 = 1f;
	public float clickMultiplier1 = 1.1f;
	public int clickM1Level = 1;
	public int clickM1UpgradeCost = 5;
	public int clickM1UpgradeBaseCost = 5;
	public float clickM1UpgradeCostMultiplier = 1.08f;
	public float clickMultiplier2 = 1.1f;
	public int heroLevel = 1;
	public int heroUpgradeCost = 10;
	public int baseHeroUpgradeCost = 10;
	public float heroUpgradeCostMultiplier = 1.07f;
	public GameObject[] enemyPrefabs;
    public SimpleHealthBar healthBar;
	public Text levelText = null;
	public Text goldText = null;
	public Text diamondText = null;
	public Text heroLevelText = null;
	public Button heroLevelUpButton = null;
	public Text clickDamageText;
	public Text clickDamageMultiplierText;
	public Button clickDamageM1Button;
	public Text p1DamageM1Text;
	public Button p1DamageM1Button;

	public int p1Damage = 0;
	public int p1BaseDamage = 1;
	public float p1Multiplier = 1.00f;
	public int p1Level = 0;
	public int p1UpgradeCost = 100;
	public int basep1UpgradeCost = 100;
	public float p1UpgradeCostMultiplier = 1.1f;
	public int p1M1Level = 1;
	public int p1M1UpgradeCost = 8;
	public int p1M1UpgradeBaseCost = 8;
	public float p1M1UpgradeCostMultiplier = 1.09f;
	public Text p1LevelText;
	public Button p1LevelUpButton;
	public Text p1DamageText;
	public bool boss = false;
	public Text enemyDescriptionText;
	public string[] enemyNouns;
	public string[] enemyAdjectives;

	public upgradeController upgradeController;

	public GameObject goldPanel;
	public GameObject diamondPanel;
	public GameObject p1Board;
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		int health = 0;
		int maxHealth = baseHealth*level;
		healthBar.UpdateBar( health, maxHealth );
		levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMax;
		heroLevelText.text = "Hero Level: "+heroLevel;
		heroLevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+heroUpgradeCost+"g";
		clickDamageText.text = "Units: "+clickDamage;
		p1LevelText.text = "Partner";
		p1LevelUpButton.GetComponentInChildren<Text>().text = "Hire: "+p1UpgradeCost+"g";
		p1DamageText.text = "";
		if (diamonds < 1)
			diamondPanel.SetActive(false);
		p1Board.SetActive(false);
		goldPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		heroLevelUpButton.interactable = gold >= heroUpgradeCost;
		p1LevelUpButton.interactable = gold >= p1UpgradeCost;
		clickDamageText.text = "Units: "+clickDamage;
		if (p1Level > 0)
			p1DamageText.text = "Units: "+p1Damage;

		diamondText.text = "Diamonds: "+diamonds;
		clickDamageM1Button.interactable = diamonds >= clickM1UpgradeCost;
		p1DamageM1Button.interactable = diamonds >= p1M1UpgradeCost;
		clickDamageMultiplierText.text = "+ "+(clickMultiplier1-1.0f)*100+"%";
		p1DamageM1Text.text = "+ "+(p1Multiplier-1.0f)*100+"%";
		goldText.text = "Gold: "+gold;
	}

	private void enemyLevelUp() {
		level++;
		baseHealth++;
		levelCount = 1;
	}

	private int calculateGold() {
		return (int)(baseGoldDrop*level*goldMultiplier1*goldMultiplier2);
	}
	public void RecalculateClickDamage() {
		clickDamage = (int)(heroLevel*clickMultiplier1*clickMultiplier2);
	}
	public void RecalculateP1Damage() {
		p1Damage = (int)(p1BaseDamage*p1Multiplier*p1Level);
	}
	public int enemyDied () {
		int goldIncrement = boss ? calculateGold()*10 : calculateGold();
		if (boss) {
			boss = false;
			enemyLevelUp();
		}
		else {
			levelCount++;
			if (levelCount > levelMax) {
				if (level%10 == 0) 
					boss = true;
				else
					enemyLevelUp();
			}
		}

		spawnNewEnemy();
		if (!goldPanel.active)
			goldPanel.SetActive(true);
		gold += goldIncrement;
		return goldIncrement;
	}

	private void spawnNewEnemy() {
		int health, maxHealth, enemySelector;
		if (boss) {
			health = 0;
			maxHealth = baseHealth*level*5;
			levelText.text = "Level "+level+"\nBOSS FIGHT!";
			enemySelector = 5;
		}
		else {
			health = 0;
			maxHealth = baseHealth*level;
			levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMax;
			enemySelector = ((level-1)/2)%5;
		}

		GameObject newEnemy = (GameObject) Instantiate(enemyPrefabs[enemySelector], new Vector3(0f,-5f,-5f),Quaternion.Euler(0, Random.value*360f, 0));
		newEnemy.GetComponent<House>().health = 0;
		newEnemy.GetComponent<House>().maxHealth = maxHealth;
		healthBar.UpdateBar( health, maxHealth );
		enemyDescriptionText.text = enemyAdjectives[((level-1)/10)%20] +" "+ enemyNouns[enemySelector];
	}

	public void heroLevelUp() {
		heroLevel++;
		gold -= heroUpgradeCost;
		heroLevelText.text = "Hero Level: "+heroLevel;
		heroUpgradeCost = (int)(baseHeroUpgradeCost*Mathf.Pow(heroUpgradeCostMultiplier,heroLevel));
		heroLevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+heroUpgradeCost+"g";
		RecalculateClickDamage();
		if (heroLevel >= 5) 
			upgradeController.enableClickBoost1();
		if (heroLevel >= 10) 
			upgradeController.enableClickBoost2();
		if (heroLevel >= 20) 
			upgradeController.enableClickBoost3();
		if (!p1Board.active)
			p1Board.SetActive(true);
	}

	public void p1LevelUp() {
		p1Level++;
		gold -= p1UpgradeCost;
		p1LevelText.text = "Partner Level: "+p1Level;
		p1UpgradeCost = (int)(basep1UpgradeCost*Mathf.Pow(p1UpgradeCostMultiplier,p1Level));
		p1LevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+p1UpgradeCost+"g";
		p1Damage = (int)(p1BaseDamage*p1Multiplier*p1Level);
		if (p1Level >= 5) 
			upgradeController.enablep1Boost1();
		if (p1Level >= 10) 
			upgradeController.enablep1Boost2();
		if (p1Level >= 20) 
			upgradeController.enablep1Boost3();
	}
	public void getDiamond() {
		diamonds++;
	}

	public void buyClickM1Up() {
		clickM1Level++;
		diamonds -= clickM1UpgradeCost;
		diamondText.text = "Diamonds: "+diamonds;
		clickM1UpgradeCost = (int)(clickM1UpgradeBaseCost*Mathf.Pow(clickM1UpgradeCostMultiplier,clickM1Level));
		clickDamageM1Button.GetComponentInChildren<Text>().text = clickM1UpgradeCost+" diamonds";
		clickMultiplier1 += .25f;
		clickDamage = (int)(heroLevel*clickMultiplier1*clickMultiplier2);
	}

	public void buyP1M1Up() {
		p1M1Level++;
		diamonds -= p1M1UpgradeCost;
		diamondText.text = "Diamonds: "+diamonds;
		p1M1UpgradeCost = (int)(p1M1UpgradeBaseCost*Mathf.Pow(p1M1UpgradeCostMultiplier,p1M1Level));
		p1DamageM1Button.GetComponentInChildren<Text>().text = p1M1UpgradeCost+" diamonds";
		p1Multiplier += .25f;
		p1Damage = (int)(p1BaseDamage*p1Multiplier*p1Level);
	}


}
