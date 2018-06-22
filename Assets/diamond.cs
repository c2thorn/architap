using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamond : MonoBehaviour {
    public controller controller = null;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
		var x = Random.Range(-1f, 1f);
		var y = Random.Range(-1f, 1f);
		Vector2 direction = new Vector2(x, y);
		direction = direction.normalized * -100f;
		Debug.Log(direction);
		GetComponent<Rigidbody2D>().AddForce(direction);
		StartCoroutine(startDying());
		Physics2D.IgnoreLayerCollision(0,1,true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator startDying() {
        yield return new WaitForSeconds(1.5f);
        controller.getDiamond();
        //createFloatText(new Vector3(950f,80f,0f), goldIncrement, Color.yellow);
        Destroy(gameObject);
    }
}
