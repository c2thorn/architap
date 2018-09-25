using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceSpriteMask : MonoBehaviour {
	public SpriteMask spriteMask;

	public float height;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Texture2D text = spriteMask.sprite.texture;
		// Debug.Log(spriteMask.sprite);
		// Debug.Log(spriteMask.sprite.border);
		// Debug.Log(spriteMask.sprite.bounds);
		// Debug.Log(spriteMask.sprite.rect);
		// Debug.Log(spriteMask.sprite.textureRectOffset);
		// spriteMask.sprite.textureRectOffset = new 

		// Debug.Log(text);
		// Sprite tempSprite = Sprite.Create(text, 
		// new Rect(0,0,text.width,text.height-height), 
		// Vector2.zero);
		// spriteMask.sprite = tempSprite;

		// Vector4 border = spriteMask.sprite.border;
		// Debug.Log(border);
		// spriteMask.sprite.border = .Set(border.x,border.y-height,border.z,border.w);
	}
}
