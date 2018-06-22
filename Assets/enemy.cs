using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour {
    public Animator _animator = null;
    public controller controller = null;
    public SimpleHealthBar healthBar;
    public Canvas canvas;
    public GameObject damageTextPrefab;

    public GameObject diamondPrefab;

    public int health = 1;
    public int maxHealth = 2;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        controller = GameObject.Find("controller").GetComponent<controller>();
        healthBar = GameObject.Find("healthBar").GetComponent<SimpleHealthBar>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (health > 0) {
            bool hit = checkClick() && !checkDead();
            _animator.SetBool("hit", hit);
        }
    }

    bool checkClick() {
        bool hit = false;

        if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            BoxCollider2D coll = GetComponent<BoxCollider2D>();

            if (coll.OverlapPoint(wp)) {
                hit = true;
                health -= controller.clickDamage;
                healthBar.UpdateBar( health, maxHealth );
                createFloatText(Input.mousePosition,controller.clickDamage, Color.red);
                // GameObject damageText = (GameObject) Instantiate(damageTextPrefab,Input.mousePosition,Quaternion.Euler(0, 0, 0));
                // damageText.GetComponent<Text>().text = ""+controller.clickDamage;
                // damageText.transform.parent = canvas.transform;
            }
        }

        return hit;
    }

    bool checkDead() {
        if (health <= 0) {
            _animator.SetBool("death", true);
            StartCoroutine(startDying());
            return true;
        }
        return false;
    }

    IEnumerator startDying() {
        if(Random.value <= controller.diamondChance) {
            GameObject diamond = (GameObject) Instantiate(diamondPrefab,transform.position+new Vector3(0,2f,-3f),Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(0.6f);
        int goldIncrement = controller.enemyDied();
        createFloatText(new Vector3(950f,80f,0f), goldIncrement, Color.yellow);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    void createFloatText(Vector3 pos, int number, Color color) {
        GameObject floatText = (GameObject) Instantiate(damageTextPrefab,pos,Quaternion.Euler(0, 0, 0));
        floatText.GetComponent<Text>().text = ""+number;
        floatText.GetComponent<Text>().color = color;
        floatText.transform.parent = canvas.transform;
        floatText.transform.position = pos;
    }
}
