using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour {
    public controller controller = null;
    public ItemController itemController;
    public SimpleHealthBar healthBar;
    public Canvas canvas;
    public GameObject damageTextPrefab;
    public GameObject diamondPrefab;
    public GameObject chestPrefab;
    public int health = 0;
    public int maxHealth = 2;

    private float[] nextActionTime = new float[] {0.0f,0.0f,0.0f,0.0f,0.0f,0.0f,0.0f};
	public float p1Period = 0.1f;
	private BoxCollider2D coll;

	public Shader unfinished;
	public Shader finished;
	MeshRenderer rend;

	// Use this for initialization
	void Start () {
        controller = GameObject.Find("controller").GetComponent<controller>();
        itemController = GameObject.Find("ItemController").GetComponent<ItemController>();
        healthBar = GameObject.Find("healthBar").GetComponent<SimpleHealthBar>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		coll = GameObject.Find("Click Area").GetComponent<BoxCollider2D>();
		rend = GetComponent<MeshRenderer>();
		rend.material.shader = unfinished;
        maxHealth = controller.calculateHealth();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, Time.deltaTime+0.15f, 0);
        if (health < maxHealth) {
            if (!itemController.itemDrop){
                partnerDamage();
                bool hit = checkClick();
                hit = !checkDead() && hit;
            }
        }
    }

	void partnerDamage() {
        for (int i = 0; i < 7; i++){
            if (Time.time > nextActionTime[i] ) {
                nextActionTime[i] = Time.time + controller.periods[i];
                health += controller.units[i+1];
            }
        }
        healthBar.UpdateBar( health, maxHealth );
	}

    bool checkClick() {
        bool hit = false;

        if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (coll.OverlapPoint(wp)) {
                hit = true;
                health += controller.units[0];
                healthBar.UpdateBar( health, maxHealth );
                createFloatText(Input.mousePosition,controller.units[0].ToString(), Color.red);
            }
        }

        return hit;
    }

    bool checkDead() {
        if (health >= maxHealth) {
            // _animator.SetBool("death", true);
            StartCoroutine(startDying());
            return true;
        }
        return false;
    }

    IEnumerator startDying() {
        if (controller.boss) {
            GameObject chest = (GameObject) Instantiate(chestPrefab,transform.position+new Vector3(0,5f,-10f),Quaternion.Euler(-90, 152, 0));
            chest.GetComponentInChildren<chest>().SetItem(itemController.getCurrentBossItem());
        }
        else if (controller.level == 5 && controller.levelCount == 1) {
            //Guarantee first diamond
            GameObject diamond = (GameObject) Instantiate(diamondPrefab,transform.position+new Vector3(0,2f,-3f),Quaternion.Euler(0, 0, 0));
        }
        else if(controller.level >= 6 && Random.value <= controller.diamondChance) {
            GameObject diamond = (GameObject) Instantiate(diamondPrefab,transform.position+new Vector3(0,2f,-3f),Quaternion.Euler(0, 0, 0));
        }
		rend.material.shader = finished;
        yield return new WaitForSeconds(1f);
        while (itemController.itemDrop)
            yield return new WaitForSeconds(1f);
        int goldIncrement = controller.enemyDied();
        createFloatText(new Vector3(1300f,450f,0f), "+"+goldIncrement+"g", Color.yellow);
        // yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    void createFloatText(Vector3 pos, string text, Color color) {
        GameObject floatText = (GameObject) Instantiate(damageTextPrefab,pos,Quaternion.Euler(0, 0, 0));
        floatText.GetComponent<Text>().text = text;
        floatText.GetComponent<Text>().color = color;
        floatText.transform.SetParent(canvas.transform);
        // floatText.transform.parent = canvas.transform;
        floatText.transform.position = pos;
    }
}
