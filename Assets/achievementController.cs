using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class achievementController : MonoBehaviour {

	public controller controller;
	public upgradeController uController;
	public List<achievement> achievements = new List<achievement>();
	public GameObject achievementPanel;
	public GameObject achievementContent;
	public GameObject achievementPrefab;
	public SaveStateController saveStateController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void checkAchievement(string requirement, double requirementValue) {
		for (int i = 0; i < achievements.Count; i++) {
			if (achievements[i].requirement == requirement && !achievements[i].completed) {
				if (requirementValue >= achievements[i].requirementValue) {
					achievements[i].completed = true;
					uController.enableAchievementsButton(true);
					refreshAchievementsUI();
					controller.RecalculateAchievementMultipliers();
					saveStateController.SaveData();
				}
			}
		}
	}

	public void refreshAchievementsUI() {
		foreach (Transform child in achievementContent.transform) {
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < achievements.Count; i++) {
			float y = i * -120F - 60;
			Vector3 pos = new Vector3(0,y,0f);
			GameObject achievementIcon = (GameObject) Instantiate(achievementPrefab,pos,Quaternion.Euler(0, 0, 0));
			achievementIcon.GetComponent<RectTransform>().localPosition = pos;
			achievementIcon.transform.SetParent(achievementContent.transform, false);
			setItemIcon(achievementIcon,achievements[i]);
		}
	}

	public void setItemIcon(GameObject achievementIcon, achievement achievement) {
		Color newCol;
		if (achievement.completed) {
			if (ColorUtility.TryParseHtmlString("#FFFD69", out newCol))
				achievementIcon.GetComponent<Image>().color = newCol;
			
		}
		else {
			if (ColorUtility.TryParseHtmlString("#989898", out newCol))
				achievementIcon.GetComponent<Image>().color = newCol;
		}

		foreach (Transform child in achievementIcon.transform) {
			GameObject obj = child.gameObject;
			if(obj.name == "Badge Name") {
				obj.GetComponent<Text>().text = achievement.name;
			}else if (obj.name == "Badge Description") {
				obj.GetComponent<Text>().text = achievement.effect + " + " + achievement.effectValue*100 + "%";
			}
		}
	}
}
