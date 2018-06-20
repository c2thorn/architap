using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	public int clickDamage = 1;
	public int gold = 0;

	public int level = 1;
	public int levelCount = 1;
	public int baseGoldDrop = 1;
	public int baseHealth = 2;
	public float goldMultiplier1 = 1f;
	public float goldMultiplier2 = 1f;
	public GameObject enemyPrefab = null;
    public SimpleHealthBar healthBar;
	public Text levelText = null;

	// Use this for initialization
	void Start () {
		int health = baseHealth*level;
		int maxHealth = baseHealth*level;

		healthBar.UpdateBar( health, maxHealth );
		levelText.text = "Level "+level+"\n"+levelCount+" / 10";

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enemyDied () {
		levelCount++;
		if (levelCount > 10) {
			level++;
			baseHealth++;
			levelCount = 1;
		}
		GameObject newEnemy = (GameObject) Instantiate(enemyPrefab, new Vector3(3.7245f,-5.06f,0f),Quaternion.Euler(0, 180, 0));
		int health = baseHealth*level;
		int maxHealth = baseHealth*level;
		newEnemy.GetComponent<enemy>().health = health;
		newEnemy.GetComponent<enemy>().maxHealth = maxHealth;

		healthBar.UpdateBar( health, maxHealth );
		levelText.text = "Level "+level+"\n"+levelCount+" / 10";

		gold += (int)(baseGoldDrop*level*goldMultiplier1*goldMultiplier2);
		
	}
}
