using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour {

	public Transform contentTr;
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
		{"cooldownReduction",true}
	};
	
	public Dictionary<string,double> skillEffect = new Dictionary<string,double>(){
		{"autoClick",0.1},
		{"clickBoost",2},
		{"partnerBoost",2},
		{"goldBoost",2},
		{"criticalClickChanceBoost",0.5},
		{"goldHouseChanceBoost",1},
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
	public Dictionary<string,Image> skillRadial = new Dictionary<string,Image>() {
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
	public Dictionary<string,bool> skillDoubled = new Dictionary<string,bool>(){
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

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitSetUp());
	}

	IEnumerator WaitSetUp() {
		yield return new WaitForEndOfFrame();
		InvokeRepeating("DecrementCooldowns", Time.time, 1f);
		foreach (string key in keys) {
			Button button = contentTr.Find(key+"Button").GetComponent<Button>();
			button.interactable = true;
			skillButtons[key] = button;
			skillRadial[key] = button.transform.Find("Fill").GetComponent<Image>();
			skillRadial[key].fillAmount = 1 - ((float)skillCooldown[key]/(float)skillCooldownStartTime[key]);
			skillText[key] = button.transform.Find("Cooldown").GetComponent<Text>();
			skillText[key].text = FormatSeconds(skillCooldown[key]);
		}
		RectTransform rect = contentTr.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(rect.sizeDelta.x, 170f);
		//TODO calculate skills cooldown from time

		CheckIfSkillsBought();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (string key in keys){
			SetRadialColor(key);
		}
	}

	public void SetRadialColor(string key) {
		if (skillFlag[key]) {
			Color newCol;
			if (skillDoubled[key]) {
				if (ColorUtility.TryParseHtmlString("#FF00C8", out newCol))
					skillRadial[key].color = newCol;
			}
			else if (key == "doubleNextSkill" || key == "cooldownReduction"){
				if (ColorUtility.TryParseHtmlString("#FFAF02", out newCol))
					skillRadial[key].color = newCol;
			}
			else {
				if (ColorUtility.TryParseHtmlString("#A8DA61", out newCol))
					skillRadial[key].color = newCol;
			}

		}
	}


	public void CheckIfSkillsBought(){
		foreach (string key in keys){
			skillButtons[key].gameObject.SetActive(skillsBought[key]);
			skillRadial[key].gameObject.SetActive(skillsBought[key]);
			skillText[key].gameObject.SetActive(skillsBought[key]);
			if (skillsBought[key])
				AdjustContentRect(key);
		}
	}

	public void AdjustContentRect(string key) {
		int rows = (keys.IndexOf(key)/3)+1;
		if (rows > 1){
			float heightNeeded = rows*170f;
			RectTransform rect = contentTr.GetComponent<RectTransform>();
			if (rect.sizeDelta.y < heightNeeded)
				rect.sizeDelta = new Vector2(rect.sizeDelta.x, heightNeeded);
		}
	}

	public void BuySkill(string key) {
		skillsBought[key] = true;
		skillButtons[key].gameObject.SetActive(true);
		skillRadial[key].gameObject.SetActive(true);
		skillText[key].gameObject.SetActive(true);
		skillCooldown[key] = 0;
		AdjustContentRect(key);
		if (key == "buildingReduction")
			BuySkill("cooldownReduction");
	}

	public void activateSkill(string key) {
		if (!skillFlag[key]) {
			if (skillCooldown[key] > 0){
				// Debug.Log("cooldown for "+key+" not ready");
				if (skillFlag["cooldownReduction"]) {
					skillCooldown[key] = 0;
					skillRadial[key].fillAmount = 1;
					skillText[key].text = FormatSeconds(0);
					SkillFinished("cooldownReduction");
				}
			} else {
				// Debug.Log(key+" ready");
				skillCooldown[key] = skillCooldownStartTime[key];
				if (skillFlag["doubleNextSkill"] && key != "doubleNextSkill" && key != "cooldownReduction") {
					skillDoubled[key] = true;
					SkillFinished("doubleNextSkill");
				}
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
					case "goldBoost":
						StartCoroutine(BoostGoldForDuration());
						break;
					case "criticalClickChanceBoost":
						StartCoroutine(BoostCriticalClicksForDuration());
						break;
					case "goldHouseChanceBoost":
						StartCoroutine(BoostGoldHouseChanceForDuration());
						break;
					case "doubleNextSkill":
						SetDoubleNextSkill();
						break;
					case "buildingReduction":
						StartCoroutine(ReduceBuildingRequirementForDuration());
						break;
					case "cooldownReduction":
						ResetCooldownForNextSkill();
						break;
				}
			}
		}
	}

	protected IEnumerator AutoClickForDuration() {
		float autoClickRate = (float)GetSkillEffect("autoClick");
		int numberClicks = Mathf.FloorToInt(skillDuration["autoClick"]/autoClickRate);
		float timeLeft = skillDuration["autoClick"];
		skillFlag["autoClick"] = true;
		for (int i = 0; i < numberClicks; i++){
			GameObject.FindGameObjectWithTag("enemy").GetComponent<House>().autoClick();
			skillRadial["autoClick"].fillAmount = 1 - ((float)i/(float)numberClicks);
			timeLeft -= autoClickRate;
			skillText["autoClick"].text = FormatSeconds(Mathf.Ceil(timeLeft));
			yield return new WaitForSeconds(autoClickRate);
		}
		SkillFinished("autoClick");
    }

	protected IEnumerator BoostClicksForDuration() {
		skillFlag["clickBoost"] = true;
		controller.RecalculateUnit(0);
		yield return StartCoroutine(DurationWait("clickBoost"));
		controller.RecalculateUnit(0);
	}

	protected IEnumerator BoostPartnersForDuration() {
		skillFlag["partnerBoost"] = true;
		controller.RecalculateAllUnits();
		yield return StartCoroutine(DurationWait("partnerBoost"));
		controller.RecalculateAllUnits();
	}
	protected IEnumerator BoostGoldForDuration() {
		yield return StartCoroutine(DurationWait("goldBoost"));
	}

	protected IEnumerator BoostCriticalClicksForDuration() {
		skillFlag["criticalClickChanceBoost"] = true;
		controller.criticalClickChance += (float)GetSkillEffect("criticalClickChanceBoost");
		yield return StartCoroutine(DurationWait("criticalClickChanceBoost"));
	}

	protected IEnumerator BoostGoldHouseChanceForDuration() {
		skillFlag["goldHouseChanceBoost"] = true;
		controller.bonusEnemyChance += (float)GetSkillEffect("goldHouseChanceBoost");
		yield return StartCoroutine(DurationWait("goldHouseChanceBoost"));
		controller.RecalculateItemMultipliers();
	}

	public void SetDoubleNextSkill() {
		skillFlag["doubleNextSkill"] = true;
	}

	protected IEnumerator ReduceBuildingRequirementForDuration() {
		skillFlag["buildingReduction"] = true;
		int effect = (int)GetSkillEffect("buildingReduction");
		controller.levelMaxCount = Math.Max(1,controller.levelMaxCount - effect);
		yield return StartCoroutine(DurationWait("buildingReduction"));
		controller.levelMaxCount = Math.Max(1,controller.levelMaxCount + effect);
	}

	public void ResetCooldownForNextSkill() {
		skillFlag["cooldownReduction"] = true;
	}
	
	public IEnumerator DurationWait(string key) {
		skillFlag[key] = true;
		float rate = 50;
		float duration = skillDuration[key];
		float timeLeft = duration;
		for (int i = 0; i < duration*rate; i++){
			skillRadial[key].fillAmount = 1 - ((float)i/(duration*rate));
			timeLeft -= (1/rate);
			skillText[key].text = FormatSeconds(Mathf.Ceil(timeLeft));
			yield return new WaitForSeconds(1/rate);
		}
		SkillFinished(key);
	}

	public void DecrementCooldowns() {
		foreach (string key in keys) {
			if (skillCooldown[key] > 0 && !skillFlag[key]) {
				skillCooldown[key]--;
				if (skillCooldown[key] <= 0) {
					skillCooldown[key] = 0;
				}
				skillRadial[key].fillAmount = 1 - ((float)skillCooldown[key]/(float)skillCooldownStartTime[key]);
				skillText[key].text = FormatSeconds(skillCooldown[key]);
			}
		}
	}

	public void ResetSkillCooldowns() {
		foreach (string key in keys) {
			skillCooldown[key] = 0;
			if (!skillFlag[key]){
				skillRadial[key].fillAmount = 1 - ((float)skillCooldown[key]/(float)skillCooldownStartTime[key]);
			}
			skillText[key].text = FormatSeconds(skillCooldown[key]);
		}
	}

	public string FormatSeconds(float seconds) {
		int min = Mathf.FloorToInt(seconds/60f);
		int remainder = (int)seconds%60;
		string returnString = min > 0 ? min+"m "+remainder+"s" : remainder+"s";
		return returnString;
	}

	public double GetSkillEffect(string key) {
		if (skillDoubled[key]){
			double effect = skillEffect[key];
			if (key == "autoClick")
				effect /= skillEffect["doubleNextSkill"];
			else
				effect *= skillEffect["doubleNextSkill"];
			return effect;
		}
		else {
			return skillEffect[key];
		}
	}
	public void SkillFinished(string key) {
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#A8DA61", out newCol))
			skillRadial[key].color = newCol;
		skillFlag[key] = false;
		skillRadial[key].fillAmount = 0;
		skillDoubled[key] = false;
	}
}
