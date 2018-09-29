using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

 public ItemController itemController;
 public Light halo;

 public Item item;

	public void SetItem(Item item) {
		this.item = item;
		switch (item.rarity) {
			case 0:
				halo.enabled = false;
				break;
			case 1:
				halo.color = Color.cyan;
				break;
			case 2:
				halo.color = Color.yellow;
				break;
		}
	}
	// Use this for initialization
	void Start () {
		itemController = GameObject.Find("ItemController").GetComponent<ItemController>();
		itemController.itemDrop = true;
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
				// StartCoroutine(startDying());
				GetItem();
			}
        }
	}

	// IEnumerator startDying() {
    //     yield return new WaitForSeconds(2f);
	// 	// itemController.itemDrop = false;
	// 	itemController.showItem(item);
    //     Destroy(gameObject.transform.parent.gameObject);
    // }

	public void GetItem(){
		itemController.showItem(item);
        Destroy(gameObject);
	}
}
