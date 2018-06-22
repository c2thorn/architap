using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	public int clickDamage = 1;
	public int gold = 0;
	public int level = 1;
	public int levelCount = 1;
	public int levelMax = 10;
	public int baseGoldDrop = 1;
	public int baseHealth = 2;
	public float goldMultiplier1 = 1.1f;
	public float goldMultiplier2 = 1f;
	public float clickMultiplier1 = 1.1f;
	public float clickMultiplier2 = 1.1f;
	public int heroLevel = 1;
	public int heroUpgradeCost = 10;
	public int baseHeroUpgradeCost = 10;
	public float heroUpgradeCostMultiplier = 1.07f;
	public GameObject enemyPrefab = null;
    public SimpleHealthBar healthBar;
	public Text levelText = null;
	public Text goldText = null;
	public Text heroLevelText = null;
	public Button heroLevelUpButton = null;
	public Text clickDamageText;

	public int p1Damage = 0;
	public int p1BaseDamage = 1;
	public float p1Multiplier = 1.00f;
	public int p1Level = 0;
	public int p1UpgradeCost = 100;
	public int basep1UpgradeCost = 100;
	public float p1UpgradeCostMultiplier = 1.1f;
	public Text p1LevelText;
	public Button p1LevelUpButton;
	public Text p1DamageText;
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
	}
	
	// Update is called once per frame
	void Update () {
		heroLevelUpButton.interactable = gold >= heroUpgradeCost;
		p1LevelUpButton.interactable = gold >= p1UpgradeCost;
		clickDamageText.text = "Damage: "+clickDamage;
		if (p1Level > 0)
			p1DamageText.text = "Damage: "+p1Damage;
	}

	public int enemyDied () {
		levelCount++;
		if (levelCount > levelMax) {
			level++;
			baseHealth++;
			levelCount = 1;
		}
		GameObject newEnemy = (GameObject) Instantiate(enemyPrefab, new Vector3(3.7245f,-5.06f,-2f),Quaternion.Euler(0, 180, 0));
		int health = baseHealth*level;
		int maxHealth = baseHealth*level;
		newEnemy.GetComponent<enemy>().health = health;
		newEnemy.GetComponent<enemy>().maxHealth = maxHealth;

		healthBar.UpdateBar( health, maxHealth );
		levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMax;

		int goldIncrement = (int)(baseGoldDrop*level*goldMultiplier1*goldMultiplier2);
		gold += goldIncrement;
		goldText.text = "Gold: "+gold;
		
		return goldIncrement;
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
		p1LevelText.text = "Hero Level: "+p1Level;
		p1UpgradeCost = (int)(basep1UpgradeCost*Mathf.Pow(p1UpgradeCostMultiplier,p1Level));
		p1LevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+p1UpgradeCost+"g";
		p1Damage = (int)(p1BaseDamage*p1Multiplier*p1Level);
	}
}
