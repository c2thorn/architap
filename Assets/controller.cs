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
	public GameObject enemyPrefab = null;
    public SimpleHealthBar healthBar;
	public Text levelText = null;
	public Text goldText = null;
	public Text heroLevelText = null;
	public Button heroLevelUpButton = null;

	// Use this for initialization
	void Start () {
		int health = baseHealth*level;
		int maxHealth = baseHealth*level;

		healthBar.UpdateBar( health, maxHealth );
		levelText.text = "Level "+level+"\n"+levelCount+" / "+levelMax;
		heroLevelText.text = "Hero Level: "+heroLevel;
		heroLevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+heroUpgradeCost+"g";
	}
	
	// Update is called once per frame
	void Update () {
		heroLevelUpButton.interactable = gold >= heroUpgradeCost;
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
		Debug.Log("click");
		heroLevel++;
		gold -= heroUpgradeCost;
		heroLevelText.text = "Hero Level: "+heroLevel;
		heroUpgradeCost = baseHeroUpgradeCost*heroLevel*2;
		heroLevelUpButton.GetComponentInChildren<Text>().text = "Level Up: "+heroUpgradeCost+"g";
		clickDamage = (int)(heroLevel*clickMultiplier1*clickMultiplier2);
	}
}
