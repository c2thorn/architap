using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour {

	public bool[] abilitiesBought = {false,false,false,false,false,false,false,false}; 
	public float autoClickRate = .1f;
	public float autoClickDuration = 8;
	public float autoClickCooldown = 480;
	public double clickSkillMultiplier = 2;
	public float clickSkillDuration = 10;
	public float clickSkillCooldown = 600;
	public double partnerSkillMultiplier = 2;
	public float partnerSkillDuration = 10;
	public float partnerSkillCooldown = 600;
	public double goldSkillMultiplier = 2;
	public float goldSkillDuration = 20;
	public float goldSkillCooldown = 600;
	public float criticalClickChanceAddition = 0.50f;
	public float criticalClickSkillDuration = 20;
	public float criticalClickSkillCooldown = 600;
	public float goldHouseChanceAddition = 0.5f;
	public float goldHouseDuration = 20;
	public float goldHouseCooldown = 900;
	public int buildingReductionAmount = 1;
	public float buildingReductionDuration = 60;
	public float buildingReductionCooldown = 600;
	public float cooldownReduction = 300;
	public float cooldownReductionCoolDown = 600;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
