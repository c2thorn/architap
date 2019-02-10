using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour {
	public controller controller;
	public float buildingDeathWaitTime = 1f;

	 	[System.Serializable]
	public class LevelBuildingList
	{
		public GameObject[] buildings;
	}
 	public LevelBuildingList[] levelBuildingLists;

	public GameObject previewList;

	public GameObject previewButtonPrefab;

    public float[] nextActionTime = new float[] {0.0f,0.0f,0.0f,0.0f,0.0f,0.0f,0.0f};

	public RectTransform contentRect;
	public ScrollRect scrollRect;
	public GameObject buildingButtonConnectorPrefab;
	public GameObject selectedPanel;
	public float buttonDistance = 200f;
	public RectTransform canvasRect;
	public int currentBuildingIndex = 0;
	public GameObject currentBuildingPrefab;


	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
		for (int i = 0; i < 7; i++){
            nextActionTime[i] = Time.time + controller.periods[i];
        } 
	}
	
	// Update is called once per frame
	void Update () {
		double sumDamage = 0;
		for (int i = 0; i < 7; i++){
			if (Time.time > nextActionTime[i] ) {
				nextActionTime[i] = Time.time + controller.periods[i];
				sumDamage += controller.units[i+1]*controller.periods[i];
			}
		}
		GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
		if (enemy && sumDamage > 0) {
			House house = enemy.GetComponent<House>();
			house.partnerDamage(sumDamage);
		}
	}

	public void CreateBuildingNavigation() {
		ShowNavigationScrollView();
		GameObject[] oldButtons = GameObject.FindGameObjectsWithTag("preview_button");
		for (int i = 0; i < oldButtons.Length; i++) {
			Destroy(oldButtons[i]);
		}

		int region = controller.region;
		int levelRange = controller.regionLevels[region,1]-controller.regionLevels[region,0]+1;
		int minLevel = controller.regionLevels[region,0];

		int highestLevelAttained = controller.highestRegionLevels[controller.region];

		for (int i = 0; i < levelRange; i++) {
			int buildingIndex = i%levelBuildingLists[region].buildings.Length;
			Sprite buildingSprite = levelBuildingLists[region].buildings[buildingIndex].transform.Find("Structure").GetComponent<SpriteRenderer>().sprite;
			GameObject previewButton = (GameObject) Instantiate(previewButtonPrefab,new Vector3(0,0,0),Quaternion.Euler(0, 0, 0),previewList.transform);
			previewButton.transform.Find("Structure").gameObject.GetComponent<SVGImage>().m_Sprite = buildingSprite;
			RectTransform rect = previewButton.GetComponent<RectTransform>();
			rect.anchoredPosition = new Vector2(i*buttonDistance + 75,0);
			previewButton.transform.GetComponentInChildren<Text>().text = ""+(i+minLevel);
			previewButton.GetComponent<BuildingPreview>().index = i+minLevel;

			DeterminePreviewColor(previewButton,highestLevelAttained);

			if( i+1 < levelRange ){
				GameObject connectorBar = (GameObject) Instantiate(buildingButtonConnectorPrefab,new Vector3(0,0,0),Quaternion.Euler(0, 0, 0),previewList.transform);
				connectorBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(i*buttonDistance + 175f,-5f);
			}

		}
		contentRect.sizeDelta = new Vector2(((levelRange+0)*buttonDistance+(75f)),contentRect.sizeDelta.y);
		LevelHasChanged();

	}

	public void centerOnButton() {
		int region = controller.region;
		int levelRange = controller.regionLevels[region,1]-controller.regionLevels[region,0]+1;

		float canvasRatio = (canvasRect.rect.width/675f);


		float scrollPosition = (((float)currentBuildingIndex-2)*canvasRatio)/((float)(levelRange-3)*canvasRatio);
		
		selectedPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2((currentBuildingIndex-1)*buttonDistance + 75,0);
		StopCoroutine(ScrollTowards(scrollPosition));
		StartCoroutine(ScrollTowards(scrollPosition));
	}

	IEnumerator ScrollTowards(float scrollPosition) {
		float timeLimit = .55f;
		float finalPosition = Mathf.Min(1,Mathf.Max(0,scrollPosition));

		float difference = finalPosition - scrollRect.horizontalNormalizedPosition;
		if (difference > 0.25f)
			scrollRect.horizontalNormalizedPosition += difference*0.90f;
		while (timeLimit > 0f){
			scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, 
				finalPosition, 0.10f);
			timeLimit -= 0.01f;
			yield return null;	
		}

	}

	public void UpdateCurrentBuilding(int level){
		int region = controller.region;
		currentBuildingIndex = level - controller.regionLevels[region,0]+1;
		int buildingNumber = (currentBuildingIndex-1)%levelBuildingLists[region].buildings.Length;
		currentBuildingPrefab = levelBuildingLists[region].buildings[buildingNumber];
	}

	public void ChangeLevel(int level) {
		UpdateCurrentBuilding(level);
		if (controller.levelJump(level)) {
			centerOnButton();
		}
	}

	public void LevelHasChanged() {
		UpdateCurrentBuilding(controller.level);
		centerOnButton();
	}

	public void RefreshBuildingPreviews() {
		GameObject[] previews = GameObject.FindGameObjectsWithTag("preview_button");

		int highestLevel = controller.highestRegionLevels[controller.region];
		foreach(GameObject preview in previews) {
			DeterminePreviewColor(preview, highestLevel);
		}
	}

	public void DeterminePreviewColor(GameObject preview, int highestLevel) {
		BuildingPreview buildingPreview = preview.GetComponent<BuildingPreview>();
		if (buildingPreview) {
			SVGImage svgImage1 = preview.transform.Find("Ground").GetComponent<SVGImage>();
			SVGImage svgImage2 = preview.transform.Find("Structure").GetComponent<SVGImage>();
			svgImage1.color = buildingPreview.index <= highestLevel ? Color.white : new Color(0,0,0,0.7f); 
			svgImage2.color = buildingPreview.index <= highestLevel ? Color.white : new Color(0,0,0,0.7f); 

		}
	}

	public void HideNavigationScrollView() {
		scrollRect.gameObject.SetActive(false);
	}

	public void ShowNavigationScrollView() {
		scrollRect.gameObject.SetActive(true);
	}
}
