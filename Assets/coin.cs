using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin : MonoBehaviour {
    public controller controller = null;
    public double value = 1;
    public GameObject damageTextPrefab;
    public Canvas canvas;
    public CurrencyAudioSource currencyAudioSource;

    public BuildingAudioSource buildingAudioSource;

    


	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        currencyAudioSource = GameObject.Find("Currency Audio Source").GetComponent<CurrencyAudioSource>();

		var x = Random.Range(-1f, 1f);
		var y = Random.Range(-1f, 1f);
		Vector2 direction = new Vector2(x, y);
		direction = direction.normalized * -150f;
		GetComponent<Rigidbody2D>().AddForce(direction);
		StartCoroutine(startDying());
		Physics2D.IgnoreLayerCollision(0,1,true);
        Physics2D.IgnoreLayerCollision(1,1,true);

	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PolygonCollider2D coll = GetComponent<PolygonCollider2D>();

            if (coll.OverlapPoint(wp)) {
                CashOut();
            }
        // }
	}

	IEnumerator startDying() {
        float x;
        for (int i = 0; i < 3; i++){
            x = Random.Range(-0.25f, 0.25f);
            yield return new WaitForSeconds(.5f+x);
            if (GameObject.FindGameObjectsWithTag("currency_drop").Length > 8)
                CashOut();
        }

        x = Random.Range(-0.25f, 0.25f);
        yield return new WaitForSeconds(.5f+x);
        CashOut();
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
