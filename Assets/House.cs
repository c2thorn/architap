using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class House : MonoBehaviour {
    public controller controller = null;
    public ItemController itemController;
    public SimpleHealthBar healthBar;
    public Canvas canvas;
    public GameObject damageTextPrefab;
    public GameObject diamondPrefab;
    public GameObject coalPrefab;
    public GameObject coinPrefab;
    public double health = 0;
    public double maxHealth = 2;

    public GameObject dustParticle;

	public float p1Period = 0.1f;
	private BoxCollider2D coll;
    public bool invulnerable = false;

    public BuildingController buildingController;
    public GameObject halo;

    BuildingAudioSource buildingAudioSource;

    public TutorialController tutorialController;

    public GameObject maskObject;

    private float rectHeight;

    public bool startedDying = false;
    public bool finishingDying = false;


	// Use this for initialization
	void Start () {
        controller = GameObject.Find("controller").GetComponent<controller>();
        itemController = GameObject.Find("ItemController").GetComponent<ItemController>();
        buildingController = GameObject.Find("Building Controller").GetComponent<BuildingController>();
        healthBar = GameObject.Find("healthBar").GetComponent<SimpleHealthBar>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		coll = GameObject.Find("Click Area").GetComponent<BoxCollider2D>();
        tutorialController = GameObject.Find("Tutorial Controller").GetComponent<TutorialController>();

        maxHealth = controller.calculateHealth();
        healthBar.UpdateBar( health, maxHealth );
        if (!controller.uniqueBoss)
            halo.SetActive(controller.bonusEnemy);
        buildingAudioSource = GameObject.Find("Building Audio Source").GetComponent<BuildingAudioSource>();
        maskObject = transform.Find("Sprite Mask").gameObject;
        // rectHeight = GetComponent<RectTransform>().rect.height;
	}
	
	// Update is called once per frame
	void Update () {
        if (health < maxHealth && !invulnerable) {
            if (!itemController.itemDrop && !controller.modalOpen){
                bool hit = checkClick();
                hit = !checkDead() && hit;
            }
        } else {
            if (startedDying)
                CheckClickSkip();
        }
    }

	public void partnerDamage(double sumDamage) {
        if (health < maxHealth && !invulnerable) {
            if (!itemController.itemDrop && !controller.modalOpen){
                health += sumDamage;
                updateTotalUnits(sumDamage);
                healthBar.UpdateBar( health, maxHealth );
                checkDead();
            }
        }
	}

    bool checkClick() {
        bool hit = false;

        if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (coll.OverlapPoint(wp)) {
                hit = true;
                
                ClickAction(Input.mousePosition);

            }
        }

        return hit;
    }

    public void autoClick() {
        Vector3 wp = new Vector3(0.5f,0.5f,-25f);
        if (health < maxHealth && !invulnerable) {
            if (!itemController.itemDrop && !controller.modalOpen){
                ClickAction(Camera.main.WorldToScreenPoint(wp));
                checkDead();
            }
        } else {
            if (startedDying)
                StartCoroutine(FinishDying());
            createDust(wp);
        }
    }

    public void CheckClickSkip() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (coll.OverlapPoint(wp)) {
                StartCoroutine(FinishDying());
            }
        } 
    }



    public void ClickAction(Vector3 mousePosition) {
        Vector3 wp = Camera.main.ScreenToWorldPoint(mousePosition);

        buildingAudioSource.clickSound();
        controller.StopIdling();

        double clickDamage = controller.units[0];
        bool critical = UnityEngine.Random.value <= controller.criticalClickChance;
        if (critical){
            clickDamage *= controller.criticalClickMultiplier;
        }
        
        health += clickDamage;
        updateTotalUnits(clickDamage);
        healthBar.UpdateBar( health, maxHealth );
        createFloatText(mousePosition,NumberFormat.format(clickDamage), Color.red, critical);
        createDust(wp);
        controller.totalClicks++;
        //TODO best way?
        tutorialController.RemovePointer();
    }


    private void updateTotalUnits(double amount) {
        double amountToIncrement = Math.Min(amount, maxHealth - health);
        controller.totalUnits += amountToIncrement;
        updateMaskPercentage();
    }

    public void updateMaskPercentage() {
        double percentage = Math.Min(1,((.92f)*health/maxHealth)+0.08f);
        rectHeight = 1f;
        maskObject.transform.localPosition = new Vector3(0,(float)percentage*rectHeight,-.16f);
    }



    bool checkDead() {
        if (health >= maxHealth) {
            // _animator.SetBool("death", true);
            StartCoroutine(startDying());
            return true;
        }
        return false;
    }

    protected virtual IEnumerator startDying() {
        startedDying = true;
        buildingAudioSource.PlayBuildingComplete(); 
        bool uniqueBoss = controller.uniqueBoss;
        if (controller.boss || controller.uniqueBoss) {
            controller.checkBossReward(transform.position);
        }
        else if (controller.level == 5 && controller.levelCount == 1) { 
            //TODO: Can trigger multiple times
            //Guarantee first diamond
            GameObject diamond = (GameObject) Instantiate(diamondPrefab,transform.position+new Vector3(0,0f,-3f),Quaternion.Euler(0, 0, 0));
        }
        else if((controller.level >= 6 || controller.totalPrestiges > 0) && UnityEngine.Random.value <= controller.diamondChance) {
            GameObject diamond = (GameObject) Instantiate(diamondPrefab,transform.position+new Vector3(0,0f,-3f),Quaternion.Euler(0, 0, 0));
        } else if ((controller.level >= 10  || controller.totalPrestiges > 0) && UnityEngine.Random.value <= controller.coalChance) {
            GameObject coal = (GameObject) Instantiate(coalPrefab,transform.position+new Vector3(0,0f,-3f),Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(buildingController.buildingDeathWaitTime*(0.7f));
        if (!finishingDying)
            StartCoroutine(FinishDying());
    }

    protected IEnumerator FinishDying() {
        finishingDying = true;
        bool uniqueBoss = controller.uniqueBoss;

        yield return new WaitForSeconds(buildingController.buildingDeathWaitTime*(0.3f));
        while (itemController.itemDrop || controller.modalOpen)
            yield return new WaitForSeconds(1f);
        double goldIncrement = controller.enemyDied(true, true);

        DropCoins(goldIncrement);

        if (!uniqueBoss)
            Destroy(gameObject);
    }

    public void DropCoins(double goldIncrement) {
        int coins = (int)Math.Min(7,Math.Ceiling(goldIncrement/50));
        var x = UnityEngine.Random.Range(-0.5f, 0.5f);
        var y = UnityEngine.Random.Range(-0.5f, 0.5f);
        double coinValue = Math.Max(1,Math.Ceiling(goldIncrement/coins));
        GameObject coin = (GameObject) Instantiate(coinPrefab,transform.position+new Vector3(0+x,0f+y,-3f),Quaternion.Euler(0, 0, 0));
        coin.GetComponent<coin>().value = coinValue;
        
        double remaining = goldIncrement - coinValue;

        for (int i = 1; i < coins;i++){
            var x2 = UnityEngine.Random.Range(-0.5f, 0.5f);
            var y2 = UnityEngine.Random.Range(-0.5f, 0.5f);
            double extraCoinValue = Math.Min(remaining,Math.Ceiling(goldIncrement/coins));
            GameObject extraCoin = (GameObject) Instantiate(coinPrefab,transform.position+new Vector3(0+x2,0f+y2,-3f),Quaternion.Euler(0, 0, 0));
            extraCoin.GetComponent<coin>().value = extraCoinValue;
            remaining -= extraCoinValue;
        }
    }

    IEnumerator delayDamage() {
        invulnerable = true;
        yield return new WaitForSeconds(.5f);
        invulnerable = false;
    }

    public void delay() {
        StartCoroutine(delayDamage());
    }

    protected void createFloatText(Vector3 pos, string text, Color color, bool critical) {
        GameObject floatText = (GameObject) Instantiate(damageTextPrefab,pos,Quaternion.Euler(0, 0, 0),canvas.transform);
        floatText.transform.SetAsFirstSibling();

        if (critical){
            floatText.transform.localScale = new Vector3(2f,2f,1f);
            text = text + "!";
            color = Color.magenta;
        }
        floatText.GetComponent<Text>().text = text;
        floatText.GetComponent<Text>().color = color;
    }

    protected void createDust(Vector3 pos){
        Vector3 posi = pos+new Vector3(UnityEngine.Random.Range(-2f,1.5f),UnityEngine.Random.Range(-2f,2f),5);
        GameObject dust = (GameObject) Instantiate(dustParticle,posi,Quaternion.Euler(-90, 0, 0));
    }
    public void stopDamage(){
        invulnerable = true;
    }

    public void startDamage() {
        invulnerable = false;
    }
}
