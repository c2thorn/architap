using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(startFade(1f, GetComponent<Text>()));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator startFade(float t, Text i) {
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		Transform tr = gameObject.transform;
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			//tr.position = Vector3.Lerp(tr.position, new Vector3(tr.position.x, tr.position.y+10, tr.position.z), 0.1f);
			tr.position = Vector3.Lerp(tr.position, new Vector3(tr.position.x, tr.position.y+10, tr.position.z), 0.2f);
			yield return null;
		}
		Destroy(gameObject);
    }
}
