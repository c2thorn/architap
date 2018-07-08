﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

 public controller controller = null;
 public Animator animator;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
		controller.itemDrop = true;
		var x = Random.Range(-.2f, .2f);
		var y = Random.Range(-.2f, .2f);
		Vector2 direction = new Vector2(x, y);
		direction = direction.normalized * -60f;
		GetComponent<Rigidbody2D>().AddForce(direction);
		Physics2D.IgnoreLayerCollision(0,1,true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            BoxCollider2D coll = GetComponent<BoxCollider2D>();

            if (coll.OverlapPoint(wp)) {
				animator.SetBool("open",true);
				StartCoroutine(startDying());
			}
        }
	}

	IEnumerator startDying() {
        yield return new WaitForSeconds(2f);
		controller.itemDrop = false;
        controller.getDiamond();
        Destroy(gameObject);
    }
}
