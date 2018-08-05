using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
	public controller controller;
	public GameObject settingsPanel;
	public GameObject statisticsPanel;
	public GameObject settingsButton;
	public Text totalBuildingsText;
	public Text totalUnitsText;
	public Text totalClicksText;
	public Text totalGoldText;
	public Text totalRegionsText;
	public Text totalPrestigesText;
	// Use this for initialization
	void Start () {
		settingsPanel.SetActive(false);
		statisticsPanel.SetActive(false);
		settingsButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enableSettings() {
		settingsButton.SetActive(true);
	}

	public void OpenSettings() {
		controller.modalOpen = true;
		settingsPanel.SetActive(true);
		statisticsPanel.SetActive(false);
	}

	public void CloseSettings() {
		controller.modalOpen = false;
		settingsPanel.SetActive(false);
		statisticsPanel.SetActive(false);
	}

	public void OpenStatistics() {
		controller.modalOpen = true;
		settingsPanel.SetActive(false);
		statisticsPanel.SetActive(true);
		updateStatisticsTexts();
	}

	public void updateStatisticsTexts() {
		totalBuildingsText.text = "Buildings Built: " + NumberFormat.format(controller.totalBuildings);
		totalUnitsText.text = "Units Worked: " + NumberFormat.format(controller.totalUnits);
		totalClicksText.text = "Total Clicks: " + NumberFormat.format(controller.totalClicks);
		totalGoldText.text = "Gold Earned: " + NumberFormat.format(controller.totalGold);	
		totalRegionsText.text = "Regions Completed: " + NumberFormat.format(controller.totalRegionsCompleted);
		totalPrestigesText.text = "Prestiges: " + NumberFormat.format(controller.totalPrestiges);

	}
}
