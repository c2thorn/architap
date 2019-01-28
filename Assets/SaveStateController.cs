using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Net;
using UnityEngine.Networking;

public class SaveStateController : MonoBehaviour {

	public controller controller;

	public ItemController itemController;

	public upgradeController upgradeController;

	public achievementController achievementController;
	public SoundController soundController;
	public SkillController skillController;

	public Text autoSaveText;

	public int countdownNumber;

	public GameObject idleRewardModal;
	public Text idleTimeText;
	public Text idleBuildingText;
	public Text idleGoldText;
	public Text idleCoalText;
	public bool canSave = true;

	public void LoadData() {
		controller.gold = LoadDouble("gold");
		controller.diamonds = LoadDouble("diamonds");
		controller.prestigeCurrency = LoadDouble("prestigeCurrency");
		controller.unconvertedPrestigeCurrency = LoadDouble("unconvertedPrestigeCurrency") != 0 ? LoadDouble("unconvertedPrestigeCurrency") : controller.unconvertedPrestigeCurrency;
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

		for (int i = 1; i < controller.characterEverBought.Length; i++)
			controller.characterEverBought[i] = LoadBool("characterEverBought"+i);

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
			Item item = itemController.createFromName(itemName);
			if (item != null)
				itemController.inventory.Add(item);
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

		for (int i = 0; i < controller.characterGilds.Length; i++) 
			controller.characterGilds[i] = LoadDouble("characterGild"+i);
		
		itemController.modern = LoadBool("modern");
		if(LoadBool("soundMute")){
			soundController.soundMute = true;
			soundController.soundToggle.Toggle();
		}
		if(LoadBool("musicMute")) {
			soundController.musicMute = true;
			soundController.musicAudioSource.volume = 0;
			soundController.musicToggle.Toggle();
		}
		foreach(String key in skillController.keys){
			skillController.skillsBought[key] = LoadBool(key+"Bought");
			skillController.skillCooldown[key] = LoadInt(key+"Cooldown");
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
		if (canSave){
			// Debug.Log("Saving");
			countdownNumber = 60;
			autoSaveText.text = "Autosave in " + countdownNumber;

			SaveDouble("gold", controller.gold);
			SaveDouble("totalBuildings", controller.totalBuildings);


			SaveDouble("diamonds", controller.diamonds);
			SaveDouble("prestigeCurrency",controller.prestigeCurrency);
			SaveDouble("unconvertedPrestigeCurrency",controller.unconvertedPrestigeCurrency);
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
			
			for (int i = 0; i < controller.characterEverBought.Length; i++)
				SaveBool("characterEverBought"+i, controller.characterEverBought[i]);

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
				SaveString("item"+i, item.name+"$"+item.count+"$"+item.rarity);
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

			for (int i = 0; i < controller.characterGilds.Length; i++) 
				SaveDouble("characterGild"+i,controller.characterGilds[i]);

			SaveBool("modern",itemController.modern);
			SaveBool("soundMute", soundController.soundMute);
			SaveBool("musicMute", soundController.musicMute);

			foreach(String key in skillController.keys){
				SaveBool(key+"Bought",skillController.skillsBought[key]);
				SaveInt(key+"Cooldown", skillController.skillCooldown[key]);
			}

			StartCoroutine(TrySaveDateTime());
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

	void Start() {
		InvokeRepeating("AutoSaveCountdown",Time.time,1.0f);
		countdownNumber = 60;
		idleRewardModal.SetActive(false);
	}

	private void AutoSaveCountdown() {
		countdownNumber--;
		if (countdownNumber <= 0) {
			SaveData();
			countdownNumber = 60;
		}
		autoSaveText.text = "Autosave in " + countdownNumber;
	}

	IEnumerator TrySaveDateTime()
	{
		//Make request
		UnityWebRequest uwr = UnityWebRequest.Get("http://architap.io");
		yield return uwr.SendWebRequest();
		try {
			if (uwr.isHttpError)
			{
				Debug.Log("Error While Sending: " + uwr.error);
			}
			else
			{
				// Debug.Log("Received: " + uwr.downloadHandler.text);
				string todaysDates = uwr.GetResponseHeader("date");
				DateTime nowTime =  DateTime.ParseExact(todaysDates, 
										"ddd, dd MMM yyyy HH:mm:ss 'GMT'", 
										CultureInfo.InvariantCulture.DateTimeFormat, 
										DateTimeStyles.AssumeUniversal);
				SaveString("lastSaveTime",nowTime.ToString());
				// Debug.Log("Saving date: " + nowTime.ToString());
			} 
		} catch (Exception e) {
			Debug.Log("Saving Current Date failed.");
			Debug.Log(e.StackTrace);
		}
	}

	IEnumerator TryGetCurrentTime()
	{
		Debug.Log("Making the Request");
		canSave = false;
		//Make request
		UnityWebRequest uwr = UnityWebRequest.Get("http://architap.io");
		yield return uwr.SendWebRequest();
		try {
			if (uwr.isHttpError)
			{
				Debug.Log("Error While Sending: " + uwr.error);
			}
			else
			{
				// Debug.Log("Received: " + uwr.downloadHandler.text);
				string todaysDates = uwr.GetResponseHeader("date");
				DateTime nowTime =  DateTime.ParseExact(todaysDates, 
										"ddd, dd MMM yyyy HH:mm:ss 'GMT'", 
										CultureInfo.InvariantCulture.DateTimeFormat, 
										DateTimeStyles.AssumeUniversal);

				string previousTimeString = LoadString("lastSaveTime");
				TimeSpan idleTimeSpan = GetTimeDifference(previousTimeString, nowTime);
				double idleSeconds = idleTimeSpan.TotalSeconds;

				if (idleSeconds > 0) {
					Debug.Log("Time Difference: "+idleTimeSpan);
					Debug.Log("In Seconds: " + idleSeconds);
					CalculateIdleReward(idleSeconds, idleTimeSpan);
					foreach (string key in skillController.keys) {
						skillController.DecreaseCooldownBySeconds(key, (int)idleSeconds);
					}
				}
			} 
		} catch (Exception e) {
			Debug.Log("Loading Current Date failed.");
			Debug.Log(e.StackTrace);
		}
		canSave = true;
	}

	public TimeSpan GetTimeDifference(string previousTimeString, DateTime nowTime) {
		try {
			// Debug.Log("Loaded previous date string: "+previousTimeString);
			DateTime previousTime = DateTime.Parse(previousTimeString);
			Debug.Log("Previous date time: "+previousTime);
			Debug.Log("Current date time: "+nowTime);
			if (previousTime.Equals(DateTime.MinValue) || nowTime.Equals(DateTime.MinValue)) {
				//Could not connect
				Debug.Log("date time returned as min value. Probably could not connect to server.");
			} else {
				TimeSpan diff = nowTime.Subtract(previousTime);
				return diff;
			}
		} catch (Exception e) {
			// Debug.Log(e.StackTrace);
			//Could not connect or parse
			Debug.Log("could not connect to server, or previous time is null");
		}
		return TimeSpan.MinValue;
	}

	public void CheckIdleTime() {
		StartCoroutine(TryGetCurrentTime());
	}

	public void CalculateIdleReward(double idleSeconds, TimeSpan idleTimeSpan) {
		double sumUnitsPerSecond = 0;
		for (int i = 1; i < controller.units.Length; i ++) {
			sumUnitsPerSecond += controller.units[i];
		}

		double idleHouseHealth = controller.calculateHealth();
		// Debug.Log("Idle House Health: " + idleHouseHealth);

		double secondsToBuildSingleHouse = (idleHouseHealth/sumUnitsPerSecond) + 1;
		// Debug.Log("Seconds to build one house: " + secondsToBuildSingleHouse);

		double housesBuilt = Math.Floor(idleSeconds/secondsToBuildSingleHouse);
		// Debug.Log("Houses Built: " + housesBuilt);

		double goldEarned = housesBuilt*controller.calculateGold();
		// Debug.Log("Gold Earned: " + goldEarned);
		controller.IncrementGold(goldEarned);

		double coalFound = Math.Min(Math.Floor(housesBuilt*controller.coalChance), 20);
		if (controller.level < 10 || controller.totalPrestiges > 0)
			coalFound = 0;
		else {
			controller.IncrementCoal(coalFound);
		}
		// Debug.Log("Coal Found: " + coalFound);

		idleTimeText.text = idleTimeSpan.Days > 0 ? idleTimeSpan.Days + "d " : ""
							+ (idleTimeSpan.Hours > 0 ? idleTimeSpan.Hours + "h " : "")
							+ (idleTimeSpan.Minutes > 0 ? idleTimeSpan.Minutes + "m " : "")
							+ (idleTimeSpan.Seconds > 0 ? idleTimeSpan.Seconds + "s " : "");
		idleBuildingText.text = 
								//"You built "+
								NumberFormat.format(housesBuilt)+" buildings";
		idleGoldText.text = NumberFormat.format(goldEarned);
		idleGoldText.gameObject.SetActive(goldEarned > 0);
		idleCoalText.text = NumberFormat.format(coalFound);
		idleCoalText.gameObject.SetActive(coalFound > 0);

		OpenIdleModal();
		SaveData();
	}

	public void OpenIdleModal() {
		idleRewardModal.SetActive(true);
		controller.activateModal();
	}

	public void CloseIdleModal() {
		idleRewardModal.SetActive(false);
		controller.closeModal();
	}
}
