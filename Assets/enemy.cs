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

    private float nextActionTime = 0.0f;
	public float p1Period = 0.1f;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        controller = GameObject.Find("controller").GetComponent<controller>();
        healthBar = GameObject.Find("healthBar").GetComponent<SimpleHealthBar>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Colorize(controller.level);
	}
	
	// Update is called once per frame
	void Update () {
        if (health > 0) {
            partner1Damage();
            bool hit = checkClick();
            hit = !checkDead() && hit;
            _animator.SetBool("hit", hit);
        }
        
    }

	bool partner1Damage() {
		if (Time.time > nextActionTime ) {
			nextActionTime = Time.time + p1Period;
			// execute block of code here
            health -= controller.p1Damage;
            healthBar.UpdateBar( health, maxHealth );
            return true;
		}
        return false;
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
                createFloatText(Input.mousePosition,controller.clickDamage.ToString(), Color.red);
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
        if(controller.level >= 5 && Random.value <= controller.diamondChance) {
            GameObject diamond = (GameObject) Instantiate(diamondPrefab,transform.position+new Vector3(0,2f,-3f),Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(0.6f);
        int goldIncrement = controller.enemyDied();
        createFloatText(new Vector3(1300f,450f,0f), "+"+goldIncrement+"g", Color.yellow);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    void createFloatText(Vector3 pos, string text, Color color) {
        GameObject floatText = (GameObject) Instantiate(damageTextPrefab,pos,Quaternion.Euler(0, 0, 0));
        floatText.GetComponent<Text>().text = text;
        floatText.GetComponent<Text>().color = color;
        floatText.transform.parent = canvas.transform;
        floatText.transform.position = pos;
    }

    void Colorize(int level) {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        int levelMul = (((level-1)/10)%10)*10;
        // Debug.Log(levelMul);
       // Debug.Log(((.5f*100+levelMul)%101f)/100f);
        foreach (SpriteRenderer sprite in sprites) {
            Color oldColor = sprite.color;
            float newR = ((level-1)/10)%3 == 0 ? ((oldColor.r*100-levelMul)%101f)/100f : oldColor.r;
            float newG = ((level-1)/10)%3 == 1 ? ((oldColor.g*100-levelMul)%101f)/100f : oldColor.g;
            float newB = ((level-1)/10)%3 == 2 ? ((oldColor.b*100-levelMul)%101f)/100f : oldColor.b;
           //Debug.Log(oldColor);
            sprite.color = new Color(newR,newG,newB, 1);
          // Debug.Log(sprite.color);
        }

    }
}
