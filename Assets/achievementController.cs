using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class achievementController : MonoBehaviour {

	public controller controller;
	public upgradeController uController;
	public List<achievement> achievements = new List<achievement>();
	public GameObject achievementPanel;
	public GameObject unlockedScrollView;
	public GameObject unlockedContent;
	public GameObject lockedScrollView;
	public GameObject lockedContent;
	public GameObject achievementPrefab;
	public SaveStateController saveStateController;
	public GameObject lockedButton;
	public GameObject unlockedButton;

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
		foreach (Transform child in unlockedContent.transform) {
			GameObject.Destroy(child.gameObject);
		}
		foreach (Transform child in lockedContent.transform) {
			GameObject.Destroy(child.gameObject);
		}

		int unlockedCount = 0;
		int lockedCount = 0;

		for (int i = 0; i < achievements.Count; i++) {
			GameObject achievementIcon = (GameObject) Instantiate(achievementPrefab,Vector3.zero,Quaternion.Euler(0, 0, 0));
			float x;
			float y;
			if (achievements[i].completed){
				achievementIcon.transform.SetParent(unlockedContent.transform, false);
				y = unlockedCount * -120F - 60;
				x = lockedContent.GetComponent<RectTransform>().rect.width/2;
				unlockedCount++;
			}
			else{
				achievementIcon.transform.SetParent(lockedContent.transform, false);
				y = lockedCount * -120F - 60;
				x = lockedContent.GetComponent<RectTransform>().rect.width/2;
				lockedCount++;
			}
			Vector3 pos = new Vector3(x,y,0f);
			achievementIcon.GetComponent<RectTransform>().position = pos;
			achievementIcon.GetComponent<RectTransform>().localPosition = pos;
			// achievementIcon.GetComponent<RectTransform>().offsetMin = new Vector2(0,achievementIcon.GetComponent<RectTransform>().offsetMin.y);
			setItemIcon(achievementIcon,achievements[i]);
		}
	}

	public void setItemIcon(GameObject achievementIcon, achievement achievement) {
		Color newCol;
		if (achievement.completed) {
			// if (ColorUtility.TryParseHtmlString("#FFFD69", out newCol))
			// 	achievementIcon.GetComponent<Image>().color = newCol;
			
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

	public void openUnlocked() {
		lockedScrollView.SetActive(false);
		unlockedScrollView.SetActive(true);
		lockedButton.transform.Find("Indicator").gameObject.SetActive(false);
		unlockedButton.transform.Find("Indicator").gameObject.SetActive(true);
	}

	public void openLocked() {
		lockedScrollView.SetActive(true);
		unlockedScrollView.SetActive(false);
		lockedButton.transform.Find("Indicator").gameObject.SetActive(true);
		unlockedButton.transform.Find("Indicator").gameObject.SetActive(false);
	}
}
