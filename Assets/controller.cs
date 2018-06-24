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

	public GameObject diamondPanel;
	// Use this for initialization
	void Start () {
		int health = baseHealth*level;
		int maxHealth = baseHealth*level;
		healthBar.UpdateBar( health, maxHealth );
		levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMax;
		heroLevelText.text = "Hero Level: "+heroLevel;
		heroLevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+heroUpgradeCost+"g";
		clickDamageText.text = "Damage: "+clickDamage;
		p1LevelText.text = "Partner";
		p1LevelUpButton.GetComponentInChildren<Text>().text = "Hire: "+p1UpgradeCost+"g";
		p1DamageText.text = "";
		if (diamonds < 1)
			diamondPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		heroLevelUpButton.interactable = gold >= heroUpgradeCost;
		p1LevelUpButton.interactable = gold >= p1UpgradeCost;
		clickDamageText.text = "Damage: "+clickDamage;
		if (p1Level > 0)
			p1DamageText.text = "Damage: "+p1Damage;
		if (diamonds > 0){ 
			if (!diamondPanel.active)
				diamondPanel.SetActive(true);
		}
		diamondText.text = "Diamonds: "+diamonds;
		clickDamageM1Button.interactable = diamonds >= clickM1UpgradeCost;
		p1DamageM1Button.interactable = diamonds >= p1M1UpgradeCost;
		clickDamageMultiplierText.text = "+ "+(clickMultiplier1-1.0f)*100+"%";
		p1DamageM1Text.text = "+ "+(p1Multiplier-1.0f)*100+"%";
	}

	private void enemyLevelUp() {
		level++;
		baseHealth++;
		levelCount = 1;
	}

	private int calculateGold() {
		return (int)(baseGoldDrop*level*goldMultiplier1*goldMultiplier2);
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

		gold += goldIncrement;
		goldText.text = "Gold: "+gold;
		return goldIncrement;
	}

	private void spawnNewEnemy() {
		int health, maxHealth, enemySelector;
		if (boss) {
			health = baseHealth*level*5;
			maxHealth = baseHealth*level*5;
			levelText.text = "Level "+level+"\nBOSS FIGHT!";
			enemySelector = 5;
		}
		else {
			health = baseHealth*level;
			maxHealth = baseHealth*level;
			levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMax;
			enemySelector = ((level-1)/2)%5;
		}

		GameObject newEnemy = (GameObject) Instantiate(enemyPrefabs[enemySelector], new Vector3(-.09f,-4.94f,-2f),Quaternion.Euler(0, 180, 0));
		newEnemy.GetComponent<enemy>().health = health;
		newEnemy.GetComponent<enemy>().maxHealth = maxHealth;
		healthBar.UpdateBar( health, maxHealth );
		enemyDescriptionText.text = enemyAdjectives[((level-1)/10)%20] +" "+ enemyNouns[enemySelector];
	}

	public void heroLevelUp() {
		heroLevel++;
		gold -= heroUpgradeCost;
		goldText.text = "Gold: "+gold;
		heroLevelText.text = "Hero Level: "+heroLevel;
		heroUpgradeCost = (int)(baseHeroUpgradeCost*Mathf.Pow(heroUpgradeCostMultiplier,heroLevel));
		heroLevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+heroUpgradeCost+"g";
		clickDamage = (int)(heroLevel*clickMultiplier1*clickMultiplier2);
	}

	public void p1LevelUp() {
		p1Level++;
		gold -= p1UpgradeCost;
		goldText.text = "Gold: "+gold;
		p1LevelText.text = "Partner Level: "+p1Level;
		p1UpgradeCost = (int)(basep1UpgradeCost*Mathf.Pow(p1UpgradeCostMultiplier,p1Level));
		p1LevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+p1UpgradeCost+"g";
		p1Damage = (int)(p1BaseDamage*p1Multiplier*p1Level);
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
