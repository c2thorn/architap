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

		controller.level = LoadInt("level") != 0 ? LoadInt("level") : controller.level;
		controller.highestLevel = LoadInt("highestLevel") != 0 ? LoadInt("highestLevel") : controller.highestLevel;
		controller.levelCount = LoadInt("levelCount") != 0 ? LoadInt("levelCount") : controller.levelCount;
		// controller.levelMaxCount = LoadInt("levelMaxCount") != 0 ? LoadInt("levelMaxCount") : 1;

		if (LoadDouble("goldMultiplier1") != 0)
			controller.goldMultiplier1 = LoadDouble("goldMultiplier1");

		if (LoadDouble("goldMultiplier2") != 0)
		controller.goldMultiplier2 = LoadDouble("goldMultiplier2");

		for (int i = 0; i < controller.baseLevelUnits.Length; i++) {
			if (LoadDouble("baseLevelUnits"+i) != 0)
				controller.baseLevelUnits[i] = LoadDouble("baseLevelUnits"+i);
		}
		
		for (int i = 0; i < controller.unitM1.Length; i++){
			if (LoadDouble("unitM1"+i) != 0)
				controller.unitM1[i] = LoadDouble("unitM1"+i);
		} 
		for (int i = 0; i < controller.m1Level.Length; i++){
			if (LoadDouble("m1Level"+i) != 0)
				controller.m1Level[i] = LoadInt("m1Level"+i);
		}

		// for (int i = 0; i < controller.unitItemM2.Length; i++) 
		// 	controller.unitItemM2[i] = LoadDouble("unitItemM2"+i);

		// for (int i = 0; i < controller.unitAchievementM3.Length; i++) 
		// 	controller.unitAchievementM3[i] = LoadDouble("unitAchievementM3"+i);

		for (int i = 0; i < controller.characterLevel.Length; i++) {
			if (LoadInt("characterLevel"+i) != 0)
				controller.characterLevel[i] = LoadInt("characterLevel"+i);
		}

		controller.region = LoadInt("region");
		for (int i = 0; i < controller.completedRegions.Length; i++) 
			controller.completedRegions[i] = LoadBool("completedRegions"+i);
		for (int i = 0; i < controller.highestRegionLevels.Length; i++) {
			if (LoadInt("highestRegionLevels"+i) != 0)
				controller.highestRegionLevels[i] = LoadInt("highestRegionLevels"+i);
		}
		//TODO completedBossLevels?
		for (int i = 0; i < controller.uniqueBossCompleted.Length; i++) 
			controller.uniqueBossCompleted[i] = LoadBool("uniqueBossCompleted"+i);

		if (LoadInt("bossStartTime") != 0)
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
			if (itemController.createFromName(itemName) != null)
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


		SaveDouble("diamonds", controller.diamonds);
		SaveDouble("prestigeCurrency",controller.prestigeCurrency);
		SaveDouble("coal", controller.coal);

		if (controller.uniqueBoss) {
			SaveInt("level", controller.highestRegionLevels[controller.region]);
		}
		else{
			SaveInt("level", controller.level);
		}
		SaveInt("highestLevel", controller.highestLevel);
		SaveInt("levelCount", controller.levelCount);
		SaveInt("levelMaxCount", controller.levelMaxCount);

		SaveDouble("goldMultiplier1", controller.goldMultiplier1);
		SaveDouble("goldMultiplier2", controller.goldMultiplier2);

		for (int i = 0; i < controller.baseLevelUnits.Length; i++) 
			SaveDouble("baseLevelUnits"+i, controller.baseLevelUnits[i]);
		
		for (int i = 0; i < controller.unitM1.Length; i++) 
			SaveDouble("unitM1"+i, controller.unitM1[i]);
		for (int i = 0; i < controller.m1Level.Length; i++) 
			SaveDouble("m1Level"+i, controller.m1Level[i]);

		// for (int i = 0; i < controller.unitItemM2.Length; i++) 
		// 	controller.unitItemM2[i] = LoadDouble("unitItemM2"+i);

		// for (int i = 0; i < controller.unitAchievementM3.Length; i++) 
		// 	controller.unitAchievementM3[i] = LoadDouble("unitAchievementM3"+i);

		for (int i = 0; i < controller.characterLevel.Length; i++) 
			SaveInt("characterLevel"+i, controller.characterLevel[i]);

		SaveInt("region", controller.region);
		for (int i = 0; i < controller.completedRegions.Length; i++) 
			SaveBool("completedRegions"+i,controller.completedRegions[i]);
		for (int i = 0; i < controller.highestRegionLevels.Length; i++) 
			SaveInt("highestRegionLevels"+i, controller.highestRegionLevels[i]);
		//TODO completedBossLevels?
		for (int i = 0; i < controller.uniqueBossCompleted.Length; i++) 
			SaveBool("uniqueBossCompleted"+i,controller.uniqueBossCompleted[i]);

		SaveInt("bossStartTime", controller.bossStartTime);

		SaveDouble("totalBuildings", controller.totalBuildings);
		SaveDouble("totalPrestiges", controller.totalPrestiges);
		SaveDouble("totalClicks", controller.totalClicks);
		SaveDouble("totalGold", controller.totalGold);
		SaveDouble("totalUnits", controller.totalUnits);
		SaveDouble("totalRegionsCompleted", controller.totalRegionsCompleted);

		SaveInt("itemSize", itemController.inventory.Count);
		for (int i = 0; i < itemController.inventory.Count; i++){
			Item item = itemController.inventory[i];
			SaveString("item"+i, item.name);
		} 

		for (int i = 0; i < upgradeController.boostBought1.Length; i++) 
			SaveBool("boostBought1"+i,upgradeController.boostBought1[i]);
		for (int i = 0; i < upgradeController.boostBought2.Length; i++) 
			SaveBool("boostBought2"+i,upgradeController.boostBought2[i]);
		for (int i = 0; i < upgradeController.boostBought3.Length; i++) 
			SaveBool("boostBought3"+i,upgradeController.boostBought3[i]);
		//TODO set boost buttons to non-interactable;

		for (int i = 0; i < achievementController.achievements.Count; i++){
			achievement achievement = achievementController.achievements[i];
			SaveBool("achievement "+achievementController.achievements[i].name, achievement.completed);
		}
	}

	public void SaveDouble(string name, double value) {
		PlayerPrefs.SetString(name, value.ToString());
	}

	public void SaveInt(string name, int value) {
		PlayerPrefs.SetString(name, value.ToString());
	}

	public void SaveBool(string name, bool value) {
		PlayerPrefs.SetString(name, value.ToString());
	}

	public void SaveString(string name, string value) {
		PlayerPrefs.SetString(name, value);
	}

	public void DeleteAll() {
		PlayerPrefs.DeleteAll();
	}
}
