using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCapture : MonoBehaviour {
	public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
	private Vector2 startTouch, swipeDelta;
	public bool isDragging = false;

	void Update() {
		tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			tap = true;
			startTouch = Input.mousePosition;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			Reset();
		}

		if (Input.touchCount > 0){
			if (Input.touches[0].phase == TouchPhase.Began)
			{
				isDragging = true;
				tap = true;
				startTouch = Input.touches[0].position;
			} else if(Input.touches[0].phase==TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
			{
				isDragging = false;
				Reset();
			}
		}

		swipeDelta = Vector2.zero;
		if (isDragging)
		{
			if (Input.touches.Length > 0)
				swipeDelta = Input.touches[0].position - startTouch;
			else if (Input.GetMouseButton(0))
				swipeDelta = (Vector2)Input.mousePosition - startTouch;
		}

		if (swipeDelta.magnitude > 125)
		{
			float x = swipeDelta.x;
			float y = swipeDelta.y;
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x < 0)
					swipeLeft = true;
				else
					swipeRight = true;
			}
			else{
				if (y> 0)
					swipeUp = true;
				else
					swipeDown = true;
			}
			Reset();
		}
	}

	public void Reset(){
		startTouch = Vector2.zero;
	}
}
