using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFO : MonoBehaviour {

    public controller controller = null;
    public double value = 1;
    public GameObject damageTextPrefab;
    public Canvas canvas;
    public CurrencyAudioSource currencyAudioSource;
	CircleCollider2D coll;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        currencyAudioSource = GameObject.Find("Currency Audio Source").GetComponent<CurrencyAudioSource>();
		FlyInDirection();
		
		coll = GetComponent<CircleCollider2D>();
		Physics2D.IgnoreLayerCollision(4,0,true);
		Physics2D.IgnoreLayerCollision(4,2,true);
		Physics2D.IgnoreLayerCollision(4,1,true);
	}

	void FlyInDirection() {
		var y = Random.Range(-.2f, .2f);
		Vector2 direction;
		bool side;
		if (Random.Range(0f, 1f) < 0.5f) {
			transform.position = new Vector3(-4f,2f+(y*10f),-6f);
			direction = new Vector2(150f,0);
			side = false;
		} else {
			transform.position = new Vector3(4f,2f+(y*10f),-6f);
			direction = new Vector2(-150f,0);
			side = true;
		}
		Rigidbody2D rigid = GetComponent<Rigidbody2D>();
		rigid.AddForce(direction);
		rigid.gravityScale = y;
		StartCoroutine(FlyInOtherDirection(side));
	}

	IEnumerator FlyInOtherDirection(bool side) {
		yield return new WaitForSeconds(3.5f);
		var y = Random.Range(-.2f, .2f);
		Vector2 direction;
		if (side) {
			transform.position = new Vector3(-4f,2f+(y*10f),-6f);
			direction = new Vector2(150f,0);
		} else {
			transform.position = new Vector3(4f,2f+(y*10f),-6f);
			direction = new Vector2(-150f,0);
		}
		Rigidbody2D rigid = GetComponent<Rigidbody2D>();
		rigid.velocity = Vector3.zero;
    	rigid.angularVelocity = 0f; 
		rigid.AddForce(direction);
		rigid.gravityScale = y;
		StartCoroutine(startDying());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (coll.OverlapPoint(wp)) {
				CashOut();
            }
        }
	}

	IEnumerator startDying() {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

	public void CashOut() {
        StopAllCoroutines();
        createFloatText(Camera.main.WorldToScreenPoint(transform.position), "+"+value+"g", Color.yellow, true);
        controller.IncrementGold(value);
        currencyAudioSource.PlayCoinSound();
        Destroy(gameObject);
    }

    protected void createFloatText(Vector3 pos, string text, Color color, bool goldPos) {
        GameObject floatText = (GameObject) Instantiate(damageTextPrefab,pos,Quaternion.Euler(0, 0, 0),canvas.transform);
        floatText.GetComponent<Text>().text = text;
        floatText.GetComponent<Text>().color = color;
        floatText.transform.SetAsFirstSibling();
    }
}
