using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Firebase;
using Firebase.Analytics;

public class diamond : MonoBehaviour {
    public controller controller = null;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
		var x = Random.Range(-1f, 1f);
		var y = Random.Range(-1f, 1f);
		Vector2 direction = new Vector2(x, y);
		direction = direction.normalized * -150f;
		GetComponent<Rigidbody2D>().AddForce(direction);
		StartCoroutine(startDying());
		Physics2D.IgnoreLayerCollision(0,1,true);
		controller.getDiamond();

		//get diamond event
			Debug.Log("get diamond event");
			FirebaseAnalytics.LogEvent("diamond_found");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PolygonCollider2D coll = GetComponent<PolygonCollider2D>();

            if (coll.OverlapPoint(wp)) {
				var x = Random.Range(-1f, 1f);
				var y = Random.Range(-1f, 1f);
				Vector2 direction = new Vector2(x, y);
				direction = direction.normalized * -200f;
				GetComponent<Rigidbody2D>().AddForce(direction);
            }
        }
	}

	IEnumerator startDying() {
        yield return new WaitForSeconds(1.5f);
        //createFloatText(new Vector3(950f,80f,0f), goldIncrement, Color.yellow);
        Destroy(gameObject);
    }
}
