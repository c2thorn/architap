using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveStateController : MonoBehaviour {

	public controller controller;

	public ItemController itemController;

	public upgradeController upgradeController;

	public achievementController achievementController;

	public void LoadData() {
		controller.gold = LoadDouble("gold");
		controller.diamonds = LoadDouble("diamonds");
		controller.prestigeCurrency = LoadDouble("prestigeCurrency");
		controller.coal = LoadDouble("coal");

		controller.level = LoadInt("level");
		controller.highestLevel = LoadInt("highestLevel");
		controller.levelCount = LoadInt("levelCount");
		controller.levelMaxCount = LoadInt("levelMaxCount");

		controller.goldMultiplier1 = LoadDouble("goldMultiplier1");
		controller.goldMultiplier2 = LoadDouble("goldMultiplier2");

		for (int i = 0; i < controller.baseLevelUnits.Length; i++) 
			controller.baseLevelUnits[i] = LoadDouble("baseLevelUnits"+i);
		
		for (int i = 0; i < controller.unitM1.Length; i++) 
			controller.unitM1[i] = LoadDouble("unitM1"+i);
		for (int i = 0; i < controller.m1Level.Length; i++) 
			controller.m1Level[i] = LoadInt("m1Level"+i);

		// for (int i = 0; i < controller.unitItemM2.Length; i++) 
		// 	controller.unitItemM2[i] = LoadDouble("unitItemM2"+i);

		// for (int i = 0; i < controller.unitAchievementM3.Length; i++) 
		// 	controller.unitAchievementM3[i] = LoadDouble("unitAchievementM3"+i);

		for (int i = 0; i < controller.characterLevel.Length; i++) 
			controller.characterLevel[i] = LoadInt("characterLevel"+i);

		controller.region = LoadInt("region");
		for (int i = 0; i < controller.completedRegions.Length; i++) 
			controller.completedRegions[i] = LoadBool("completedRegions"+i);
		for (int i = 0; i < controller.highestRegionLevels.Length; i++) 
			controller.highestRegionLevels[i] = LoadInt("highestRegionLevels"+i);
		//TODO completedBossLevels?

		controller.bossStartTime = LoadInt("bossStartTime");

		controller.totalBuildings = LoadDouble("totalBuildings");
		controller.totalPrestiges = LoadDouble("totalPrestiges");
		controller.totalClicks = LoadDouble("totalClicks");
		controller.totalGold = LoadDouble("totalGold");
		controller.totalUnits = LoadDouble("totalUnits");
		controller.totalRegionsCompleted = LoadDouble("totalRegionsCompleted");

		int itemSize = LoadInt("itemSize");
		for (int i = 0; i < itemSize; i++){
			string itemName = LoadString("item"+i);
			itemController.inventory.Add(itemController.createFromName(itemName));
		} 

		for (int i = 0; i < upgradeController.boostBought1.Length; i++) 
			upgradeController.boostBought1[i] = LoadBool("boostBought1"+i);
		for (int i = 0; i < upgradeController.boostBought2.Length; i++) 
			upgradeController.boostBought2[i] = LoadBool("boostBought2"+i);
		for (int i = 0; i < upgradeController.boostBought3.Length; i++) 
			upgradeController.boostBought3[i] = LoadBool("boostBought3"+i);
		//TODO set boost buttons to non-interactable;

		for (int i = 0; i < achievementController.achievements.Count; i++){
			bool completed = LoadBool("achievement "+achievementController.achievements[i].name);
			achievementController.achievements[i].completed = completed;
		}

		controller.RecalculateAchievementMultipliers();
		for (int i = 0; i < controller.baseCharacterUpgradeCost.Length; i++)
			controller.RecalculateCharacterUpgradeCost(i);
		controller.RecalculateItemMultipliers();
	}

	public double LoadDouble(string name) {
		return  PlayerPrefs.GetString(name).Length > 0 ? double.Parse(PlayerPrefs.GetString(name)) : 0;
	}

	public int LoadInt(string name) {
		return  PlayerPrefs.GetString(name).Length > 0 ? int.Parse(PlayerPrefs.GetString(name)) : 0;
	}

	public string LoadString(string name) {
		return  PlayerPrefs.GetString(name);
	}

	public bool LoadBool(string name) {
		return  PlayerPrefs.GetString(name).Length > 0 ? bool.Parse(PlayerPrefs.GetString(name)) : false;
	}

	public void SaveData() {
		SaveDouble("gold", controller.gold);
		SaveDouble("totalBuildings", controller.totalBuildings);
	}

	public void SaveDouble(string name, double value) {
		PlayerPrefs.SetString(name, value.ToString());
	}

	public void DeleteAll() {
		PlayerPrefs.DeleteAll();
	}
}
