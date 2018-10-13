using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour {

	public bool[] abilitiesBought = {false,false,false,false,false,false,false,false}; 
	public double cooldownCheckRate = 1;
	public float autoClickRate = .1f;
	public float autoClickDuration = 8;
	public int autoClickCooldown = 0;
	public int autoClickCooldownStartTime = 480;
	public Button autoClickButton;
	public Text autoClickCooldownText;
	public bool autoClickFlag = false;
	public double clickSkillMultiplier = 2;
	public float clickSkillDuration = 10;
	public float clickSkillCooldown = 600;
	public float clickSkillCooldownStartTime = 600;
	public Button clickSkillButton;
	public Text clickSkillCooldownText;
	public double partnerSkillMultiplier = 2;
	public float partnerSkillDuration = 10;
	public float partnerSkillCooldown = 600;
	public float clickSkillCooldownStartTime = 600;
	public Button clickSkillButton;
	public Text clickSkillCooldownText;
	public double goldSkillMultiplier = 2;
	public float goldSkillDuration = 20;
	public float goldSkillCooldown = 600;
	public float clickSkillCooldownStartTime = 600;
	public Button clickSkillButton;
	public Text clickSkillCooldownText;
	public float criticalClickChanceAddition = 0.50f;
	public float criticalClickSkillDuration = 20;
	public float criticalClickSkillCooldown = 600;
	public float clickSkillCooldownStartTime = 600;
	public Button clickSkillButton;
	public Text clickSkillCooldownText;
	public float goldHouseChanceAddition = 0.5f;
	public float goldHouseDuration = 20;
	public float goldHouseCooldown = 900;
	public float clickSkillCooldownStartTime = 600;
	public Button clickSkillButton;
	public Text clickSkillCooldownText;
	public int buildingReductionAmount = 1;
	public float buildingReductionDuration = 60;
	public float buildingReductionCooldown = 600;
	public float clickSkillCooldownStartTime = 600;
	public Button clickSkillButton;
	public Text clickSkillCooldownText;
	public float cooldownReduction = 300;
	public float cooldownReductionCoolDown = 600;
	public float clickSkillCooldownStartTime = 600;
	public Button clickSkillButton;
	public Text clickSkillCooldownText;
	// Use this for initialization
	void Start () {
		InvokeRepeating("DecrementCooldowns", Time.time, 1f);
		autoClickCooldownText.text = NumberFormat.format(autoClickCooldown);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void autoClickSkill() {
		if (!autoClickFlag) {
			StartCoroutine(AutoClickForDuration());
			autoClickCooldown = autoClickCooldownStartTime;
			autoClickButton.interactable = false;
		}
	}

	protected IEnumerator AutoClickForDuration() {
		int numberClicks = Mathf.FloorToInt(autoClickDuration/autoClickRate);
		autoClickFlag = true;

		for (int i = 0; i < numberClicks; i++){
			GameObject.FindGameObjectWithTag("enemy").GetComponent<House>().autoClick();
			yield return new WaitForSeconds(autoClickRate);
		}
		autoClickFlag = false;
		 
    }

	public void DecrementCooldowns() {
		if (autoClickCooldown > 0) {
			// autoClickCooldown -= cooldownCheckRate;
			autoClickCooldown--;
			autoClickCooldownText.text = NumberFormat.format(autoClickCooldown);
			if (autoClickCooldown <= 0 ) {
				autoClickButton.interactable = true;
				autoClickCooldown = 0;
			} 
		}
	}


}
