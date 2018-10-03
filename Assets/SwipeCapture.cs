using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCapture : MonoBehaviour {
	public bool tap, swipeUp, swipeDown;
	private Vector2 startTouch, swipeDelta;
	public bool isDragging = false;

	public RectTransform scrollRect;

	public bool snappedOpen = false;
	
	void Start() {
	}

	void Update() {
		tap = swipeUp = swipeDown = false;

		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			tap = true;
			startTouch = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			Reset();
			FinishDrag();
		}

		if (!(startTouch.y < 0.95f && startTouch.y > .90f) 
				// || (!(startTouch.y < 0.4f && startTouch.y > 0.35f) && snappedOpen)
				)
			Reset();

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
			else if (Input.GetMouseButton(0)){
				swipeDelta = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition) - startTouch;
			}
		}

		float y = swipeDelta.y;
		if (!snappedOpen) {
			if (y < -0.05f)
			{				
				float height = Mathf.Max(0,y*1200f+600f);
				scrollRect.offsetMin = new Vector2(scrollRect.offsetMin.x, height); 
				if (!scrollRect.transform.parent.gameObject.activeSelf)
					scrollRect.transform.parent.gameObject.SetActive(true);
			}
		}
		else {
			if (y > 0.05f)
			{
				Debug.Log("yup");				
				// float height = Mathf.Max(0,y*1200f+600f);
				// scrollRect.offsetMin = new Vector2(scrollRect.offsetMin.x, height); 
				// if (!scrollRect.transform.parent.gameObject.activeSelf)
				// 	scrollRect.transform.parent.gameObject.SetActive(true);
			}
		}

	}

	public void Reset(){
		startTouch = Vector2.zero;
	}

	public void FinishDrag() {
		if (scrollRect.transform.parent.gameObject.activeSelf) {
			if (scrollRect.offsetMin.y > 400f){
				scrollRect.transform.parent.gameObject.SetActive(false);
				snappedOpen = false;
			}
			else {
				scrollRect.offsetMin = new Vector2(scrollRect.offsetMin.x, 0);
				snappedOpen = true;
			}
		}
	}
}
