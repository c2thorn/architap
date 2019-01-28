using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {
	public controller controller;
	public upgradeController upgradeController;
	public GameObject settingsPanel;
	public GameObject statisticsPanel;
	public GameObject debugPanel;
	public GameObject settingsButton;
	public Text totalBuildingsText;
	public Text totalUnitsText;
	public Text totalClicksText;
	public Text totalGoldText;
	public Text totalRegionsText;
	public Text totalPrestigesText;
	public BuildingController buildingController;
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
		controller.activateModal();
		settingsPanel.SetActive(true);
		statisticsPanel.SetActive(false);
		debugPanel.SetActive(false);
	}

	public void CloseSettings() {
		controller.closeModal();
		settingsPanel.SetActive(false);
		statisticsPanel.SetActive(false);
		debugPanel.SetActive(false);
	}

	public void OpenStatistics() {
		controller.activateModal();
		settingsPanel.SetActive(false);
		statisticsPanel.SetActive(true);
		debugPanel.SetActive(false);
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

	public void OpenDebug() {
		controller.activateModal();
		settingsPanel.SetActive(false);
		statisticsPanel.SetActive(false);
		debugPanel.SetActive(true);
	}

	public void GoldDebug() {
		controller.IncrementGold(1E100);
	}

	public void DiamondDebug() {
		upgradeController.enableDiamondButton(true);
		controller.IncrementDiamonds(1E50);
	}

	public void LevelCountDebug() {
		controller.levelMaxCount = 1;
	}

	public void PrestigeButtonDebug() {
		upgradeController.enableMapButton(true);
		controller.prestigeButton.gameObject.SetActive(true);
	}

	public void BuildingDeathWaitTimeDebug() {
		buildingController.buildingDeathWaitTime = .1f;
	}

	public void restartGameDebug() {
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
	}
}
