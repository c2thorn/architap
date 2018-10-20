using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour {

	public Transform panelTr;
	public controller controller;

	[HideInInspector]
	public List<string> keys = new List<string>() {
		"autoClick","clickBoost","partnerBoost","goldBoost",
		"criticalClickChanceBoost","goldHouseChanceBoost","doubleNextSkill","buildingReduction",
		"cooldownReduction"
	};

	public Dictionary<string,bool> skillsBought = new Dictionary<string,bool>(){
		{"autoClick",false},
		{"clickBoost",false},
		{"partnerBoost",false},
		{"goldBoost",false},
		{"criticalClickChanceBoost",false},
		{"goldHouseChanceBoost",false},
		{"doubleNextSkill",false},
		{"buildingReduction",false},
		{"cooldownReduction",false}
	};
	
	public Dictionary<string,double> skillEffect = new Dictionary<string,double>(){
		{"autoClick",0.1},
		{"clickBoost",2},
		{"partnerBoost",2},
		{"goldBoost",2},
		{"criticalClickChanceBoost",0.5},
		{"goldHouseChanceBoost",0.5},
		{"doubleNextSkill",2},
		{"buildingReduction",1},
		{"cooldownReduction",300}
	};

	public Dictionary<string,float> skillDuration = new Dictionary<string,float>(){
		{"autoClick",8f},
		{"clickBoost",10f},
		{"partnerBoost",10f},
		{"goldBoost",20f},
		{"criticalClickChanceBoost",20},
		{"goldHouseChanceBoost",20},
		{"buildingReduction",60}
	};

	public Dictionary<string,int> skillCooldown = new Dictionary<string,int>(){
		{"autoClick",0},
		{"clickBoost",0},
		{"partnerBoost",0},
		{"goldBoost",0},
		{"criticalClickChanceBoost",0},
		{"goldHouseChanceBoost",0},
		{"doubleNextSkill",0},
		{"buildingReduction",0},
		{"cooldownReduction",0}
	};
	public Dictionary<string,int> skillCooldownStartTime = new Dictionary<string,int>(){
		{"autoClick",480},
		{"clickBoost",600},
		{"partnerBoost",600},
		{"goldBoost",600},
		{"criticalClickChanceBoost",600},
		{"goldHouseChanceBoost",900},
		{"doubleNextSkill",600},
		{"buildingReduction",600},
		{"cooldownReduction",600}
	};
	public Dictionary<string,bool> skillFlag = new Dictionary<string,bool>(){
		{"autoClick",false},
		{"clickBoost",false},
		{"partnerBoost",false},
		{"goldBoost",false},
		{"criticalClickChanceBoost",false},
		{"goldHouseChanceBoost",false},
		{"doubleNextSkill",false},
		{"buildingReduction",false},
		{"cooldownReduction",false}
	};

	public Dictionary<string,Button> skillButtons = new Dictionary<string,Button>() {
		{"autoClick",null},
		{"clickBoost",null},
		{"partnerBoost",null},
		{"goldBoost",null},
		{"criticalClickChanceBoost",null},
		{"goldHouseChanceBoost",null},
		{"doubleNextSkill",null},
		{"buildingReduction",null},
		{"cooldownReduction",null}
	};
	public Dictionary<string,Text> skillText = new Dictionary<string,Text>() {
		{"autoClick",null},
		{"clickBoost",null},
		{"partnerBoost",null},
		{"goldBoost",null},
		{"criticalClickChanceBoost",null},
		{"goldHouseChanceBoost",null},
		{"doubleNextSkill",null},
		{"buildingReduction",null},
		{"cooldownReduction",null}
	};

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitSetUp());
	}

	IEnumerator WaitSetUp() {
		yield return new WaitForEndOfFrame();
		InvokeRepeating("DecrementCooldowns", Time.time, 1f);
		foreach (string key in keys) {
			Button button = panelTr.Find(key+"Button").GetComponent<Button>();
			skillButtons[key] = button;
			skillText[key] = button.transform.Find(key+"CooldownText").GetComponent<Text>();
			skillText[key].text = NumberFormat.format(skillCooldown[key]);
		}
		//TODO calculate skills cooldown from time

		CheckIfSkillsBought();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckIfSkillsBought(){
		foreach (string key in keys){
			skillButtons[key].gameObject.SetActive(skillsBought[key]);
			skillButtons[key].interactable = skillCooldown[key] == 0;
			skillText[key].gameObject.SetActive(skillsBought[key]);
		}
	}

	public void BuySkill(string key) {
		skillsBought[key] = true;
		skillButtons[key].gameObject.SetActive(true);
		skillButtons[key].interactable = true;
		skillText[key].gameObject.SetActive(true);
		skillCooldown[key] = 0;
	}

	public void activateSkill(string key) {
		if (!skillFlag[key]) {
			skillCooldown[key] = skillCooldownStartTime[key];
			skillButtons[key].interactable = false;
			switch (key) {
				case "autoClick":
					StartCoroutine(AutoClickForDuration());
					break;
				case "clickBoost":
					StartCoroutine(BoostClicksForDuration());
					break;
				case "partnerBoost":
					StartCoroutine(BoostPartnersForDuration());
					break;
			}
		}
	}

	protected IEnumerator AutoClickForDuration() {
		float autoClickRate = (float)skillEffect["autoClick"];
		int numberClicks = Mathf.FloorToInt(skillDuration["autoClick"]/autoClickRate);
		skillFlag["autoClick"] = true;
		for (int i = 0; i < numberClicks; i++){
			GameObject.FindGameObjectWithTag("enemy").GetComponent<House>().autoClick();
			yield return new WaitForSeconds(autoClickRate);
		}
		skillFlag["autoClick"] = false;
    }

	protected IEnumerator BoostClicksForDuration() {
		skillFlag["clickBoost"] = true;
		controller.RecalculateUnit(0);
		yield return new WaitForSeconds(skillDuration["clickBoost"]);
		skillFlag["clickBoost"] = false;
		controller.RecalculateUnit(0);
	}

	protected IEnumerator BoostPartnersForDuration() {
		skillFlag["partnerBoost"] = true;
		controller.RecalculateAllUnits();
		yield return new WaitForSeconds(skillDuration["partnerBoost"]);
		skillFlag["partnerBoost"] = false;
		controller.RecalculateAllUnits();
	}

	public void DecrementCooldowns() {
		foreach (string key in keys) {
			if (skillCooldown[key] > 0) {
				skillCooldown[key]--;
				skillText[key].text = NumberFormat.format(skillCooldown[key]);
				if (skillCooldown[key] <= 0) {
					skillButtons[key].interactable = true;
					skillCooldown[key] = 0;
				}
			}
		}
	}

	public void ResetSkillCooldowns() {
		foreach (string key in keys) {
			skillCooldown[key] = 0;
			skillText[key].text = NumberFormat.format(skillCooldown[key]);
			skillButtons[key].interactable = true;
		}
	}
}
