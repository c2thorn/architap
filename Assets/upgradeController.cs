using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeController : MonoBehaviour {
	public controller controller;
	private bool clickBoost1Bought = false;
	private bool clickBoost2Bought = false;
	private bool clickBoost3Bought = false;
	public Button clickBoost1;
	public Button clickBoost2;
	public Button clickBoost3;
	public int clickBoost1Price = 100;
	public int clickBoost2Price = 1000;
	public int clickBoost3Price = 10000;
	private bool p1Boost1Bought = false;
	private bool p1Boost2Bought = false;
	private bool p1Boost3Bought = false;
	public Button p1Boost1;
	public Button p1Boost2;
	public Button p1Boost3;
	public int p1Boost1Price = 250;
	public int p1Boost2Price = 2500;
	public int p1Boost3Price = 25000;
	public Button diamondButton;
	public GameObject goldPanel;
	public GameObject diamondPanel;

	// Use this for initialization
	void Start () {
		clickBoost1.gameObject.SetActive(false);
		clickBoost2.gameObject.SetActive(false);
		clickBoost3.gameObject.SetActive(false);
		p1Boost1.gameObject.SetActive(false);
		p1Boost2.gameObject.SetActive(false);
		p1Boost3.gameObject.SetActive(false);
		diamondButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (clickBoost1.gameObject.active && controller.gold >= clickBoost1Price && !clickBoost1Bought)
			clickBoost1.interactable = true;
		if (clickBoost2.gameObject.active && controller.gold >= clickBoost2Price && !clickBoost2Bought)
			clickBoost2.interactable = true;
		if (clickBoost3.gameObject.active && controller.gold >= clickBoost3Price && !clickBoost3Bought)
			clickBoost3.interactable = true;
		if (p1Boost1.gameObject.active && controller.gold >= p1Boost1Price && !p1Boost1Bought)
			p1Boost1.interactable = true;
		if (p1Boost2.gameObject.active && controller.gold >= p1Boost2Price && !p1Boost2Bought)
			p1Boost2.interactable = true;
		if (p1Boost3.gameObject.active && controller.gold >= p1Boost3Price && !p1Boost3Bought)
			p1Boost3.interactable = true;
		if (controller.diamonds > 0){ 
			if (!diamondButton.gameObject.active)
				diamondButton.gameObject.SetActive(true);
		}
	}

	public void enableClickBoost1() {
		clickBoost1.gameObject.SetActive(true);
		clickBoost1.interactable = false;
	}
	public void enableClickBoost2() {
		clickBoost2.gameObject.SetActive(true);
		clickBoost2.interactable = false;
	}
	public void enableClickBoost3() {
		clickBoost3.gameObject.SetActive(true);
		clickBoost3.interactable = false;
	}

	public void buyClickBoost1() {
		clickBoost1Bought = true;
		controller.gold -= clickBoost1Price;
		controller.clickMultiplier1 += .25f;
		controller.RecalculateClickDamage();
		clickBoost1.interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			clickBoost1.GetComponent<Image>().color = newCol;
	}
	public void buyClickBoost2() {
		clickBoost2Bought = true;
		controller.gold -= clickBoost2Price;
		controller.clickMultiplier1 += .5f;
		controller.RecalculateClickDamage();
		clickBoost2.interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			clickBoost2.GetComponent<Image>().color = newCol;
	}
	public void buyClickBoost3() {
		clickBoost3Bought = true;
		controller.gold -= clickBoost3Price;
		controller.clickMultiplier1 += 1f;
		controller.RecalculateClickDamage();
		clickBoost3.interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			clickBoost3.GetComponent<Image>().color = newCol;
	}

	public void enablep1Boost1() {
		p1Boost1.gameObject.SetActive(true);
		p1Boost1.interactable = false;
	}
	public void enablep1Boost2() {
		p1Boost2.gameObject.SetActive(true);
		p1Boost2.interactable = false;
	}
	public void enablep1Boost3() {
		p1Boost3.gameObject.SetActive(true);
		p1Boost3.interactable = false;
	}

	public void buyp1Boost1() {
		p1Boost1Bought = true;
		controller.gold -= p1Boost1Price;
		controller.p1Multiplier += .25f;
		controller.RecalculateP1Damage();
		p1Boost1.interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			p1Boost1.GetComponent<Image>().color = newCol;
	}
	public void buyp1Boost2() {
		p1Boost2Bought = true;
		controller.gold -= p1Boost2Price;
		controller.p1Multiplier += .5f;
		controller.RecalculateP1Damage();
		p1Boost2.interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			p1Boost2.GetComponent<Image>().color = newCol;
	}
	public void buyp1Boost3() {
		p1Boost3Bought = true;
		controller.gold -= p1Boost3Price;
		controller.p1Multiplier += 1f;
		controller.RecalculateP1Damage();
		p1Boost3.interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			p1Boost3.GetComponent<Image>().color = newCol;
	}

	public void goldTab() {
		goldPanel.SetActive(true);
		diamondPanel.SetActive(false);
	}
	public void diamondTab() {
		goldPanel.SetActive(false);
		diamondPanel.SetActive(true);
	}
}
