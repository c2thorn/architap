using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAppearance : MonoBehaviour {
    public controller controller = null;
	public GameObject[] pelvisPrefabs;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
		int level = controller.level;
	//	changeBody((level%6)+1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeBody(int type) {
		//changeFace(type);
		 foreach (Transform child in transform) {
    		 GameObject.Destroy(child.gameObject);
 		}
		GameObject pelvis = (GameObject) Instantiate(pelvisPrefabs[type],new Vector3(0.01f,2.41f,0f),Quaternion.Euler(0, 0, 0),transform);
		//pelvis.transform.parent = transform;
		//pelvis.transform.position = new Vector3(0.01f,2.41f,0f);
	}

	void changeFace(int type) {
		GameObject faceObject = transform.Find("Rogue_pelvis_01/Rogue_torso_01/Rogue_head_01/Face").gameObject;
		SpriteRenderer sprite = faceObject.GetComponent<SpriteRenderer>();
		sprite.sprite = Resources.Load<Sprite>("Heroes/Rogue/Rogue_face_0"+type);
	}
}
