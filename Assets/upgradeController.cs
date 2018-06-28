using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeController : MonoBehaviour {
	public controller controller;
	public Button diamondButton;
	public GameObject goldPanel;
	public GameObject diamondPanel;
	int characterAmount = 8;
	public GameObject[] characterBoards;
	public bool[] boostBought1 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought2 = new bool[] { false, false, false, false, false, false, false, false};
	public bool[] boostBought3 = new bool[] { false, false, false, false, false, false, false, false};

	public Button[] boost1;
	public Button[] boost2;
	public Button[] boost3;

	public int[] boost1Price = new int[] {100, 250, 25000, 2500000, 2500000, 2500000, 2500000, 2500000};
	public int[] boost2Price = new int[] {1000, 2500, 250000, 25000000, 25000000, 25000000, 25000000, 25000000};
	public int[] boost3Price = new int[] {10000, 25000, 2500000, 250000000, 250000000, 250000000, 250000000, 250000000};


	// Use this for initialization
	void Start () {
		for(int i = 0; i < characterAmount; i++) {
			boost1[i].gameObject.SetActive(false);
			boost2[i].gameObject.SetActive(false);
			boost3[i].gameObject.SetActive(false);
			if (i != 0)
				characterBoards[i].SetActive(false);
		}
		diamondButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < characterAmount; i++) {
			if (boost1[i].gameObject.active && controller.gold >= boost1Price[i] && !boostBought1[i])
				boost1[i].interactable = true;
			if (boost2[i].gameObject.active && controller.gold >= boost2Price[i] && !boostBought2[i])
				boost2[i].interactable = true;
			if (boost3[i].gameObject.active && controller.gold >= boost3Price[i] && !boostBought3[i])
				boost3[i].interactable = true;
		}
		if (controller.diamonds > 0){ 
			if (!diamondButton.gameObject.active)
				diamondButton.gameObject.SetActive(true);
		}
	}

	public void enableBoard(int i) {
		characterBoards[i].SetActive(true);
	}

	public void enableBoost1(int index) {
		boost1[index].gameObject.SetActive(true);
		boost1[index].interactable = false;
	}
	public void enableBoost2(int index) {
		boost2[index].gameObject.SetActive(true);
		boost2[index].interactable = false;
	}
	public void enableBoost3(int index) {
		boost3[index].gameObject.SetActive(true);
		boost3[index].interactable = false;
	}

	public void buyBoost1(int i) {
		boostBought1[i] = true;
		controller.gold -= boost1Price[i];
		controller.clickMultiplier1 += .25f;
		controller.RecalculateClickDamage();
		boost1[i].interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			boost1[i].GetComponent<Image>().color = newCol;
	}

	public void buyBoost2(int i) {
		boostBought2[i] = true;
		controller.gold -= boost2Price[i];
		controller.clickMultiplier1 += .25f;
		controller.RecalculateClickDamage();
		boost2[i].interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			boost2[i].GetComponent<Image>().color = newCol;
	}

	public void buyBoost3(int i) {
		boostBought3[i] = true;
		controller.gold -= boost3Price[i];
		controller.clickMultiplier1 += .25f;
		controller.RecalculateClickDamage();
		boost3[i].interactable = false;
		Color newCol;
		if (ColorUtility.TryParseHtmlString("#9F6752", out newCol))
			boost3[i].GetComponent<Image>().color = newCol;
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
