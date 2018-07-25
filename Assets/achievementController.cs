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
					uController.enableAchievementsButton();
					refreshAchievementsUI();
					controller.RecalculateAchievementMultipliers();
				}
			}
		}
	}

	public void refreshAchievementsUI() {
		foreach (Transform child in achievementContent.transform) {
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < achievements.Count; i++) {
			float x = i % 2 > 0 ? 160f : -160f;
			float y = 425 - (i / 2) * 200f;
			Vector3 pos = new Vector3(x,y,0f);
			GameObject achievementIcon = (GameObject) Instantiate(achievementPrefab,pos,Quaternion.Euler(0, 0, 0));
			achievementIcon.GetComponent<RectTransform>().anchoredPosition = pos;
			achievementIcon.transform.SetParent(achievementContent.transform, false);
			setItemIcon(achievementIcon,achievements[i]);
		}
	}

	public void setItemIcon(GameObject achievementIcon, achievement achievement) {
		if (achievement.completed) {
			achievementIcon.GetComponent<Image>().color = Color.yellow;
		}
		else {
			achievementIcon.GetComponent<Image>().color = new Color(152,152,152,255);
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
