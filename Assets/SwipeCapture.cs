using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeCapture : MonoBehaviour {
	public bool tap, swipeUp, swipeDown;
	private Vector2 startTouch, swipeDelta;
	public bool isDragging = false;

	private float startScrollVal;

	public RectTransform scrollRect;

	public bool snappedOpen = false;
	public controller controller;
	public bool mapSwipeable = false;

	public ScrollRect navScrollRect;
	
	void Start() {
	}

	void Update() {
		tap = swipeUp = swipeDown = false;

		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			tap = true;
			startTouch = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			startScrollVal = scrollRect.GetComponentInChildren<Scrollbar>().value;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			Reset();
			FinishDrag();
		}

	
		if ( !mapSwipeable || controller.modalOpen ||
			((!(startTouch.y < 0.95f && startTouch.y > .90f) && !snappedOpen) || (!(startTouch.y < 0.6f && startTouch.y > 0.4f) && snappedOpen)))
			Reset();
		else {
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
					scrollRect.GetComponent<ScrollRect>().horizontal = false;
					navScrollRect.horizontal = false;
				}
			}
			else {
				if (y > 0.05f)
				{
					// Debug.Log("yup");				
					float height = Mathf.Min(585,y*1200f);
					scrollRect.offsetMin = new Vector2(scrollRect.offsetMin.x, height); 
					scrollRect.GetComponent<ScrollRect>().horizontal = false;
					navScrollRect.horizontal = false;
				}
			}
		}

	}

	public void Reset(){
		startTouch = Vector2.zero;
	}

	public void FinishDrag() {
		if (scrollRect.transform.parent.gameObject.activeSelf) {
			scrollRect.GetComponent<ScrollRect>().horizontal = true;
			navScrollRect.horizontal = true;
			if (scrollRect.offsetMin.y > 400f){
				SnapClose();
			}
			else {
				SnapOpen();
			}
		}
	}

	public void SnapOpen() {
		if (!controller.modalOpen){
			scrollRect.transform.parent.gameObject.SetActive(true);
			scrollRect.offsetMin = new Vector2(scrollRect.offsetMin.x, 0);
			snappedOpen = true;
		}
	}

	public void SnapClose() {
		scrollRect.transform.parent.gameObject.SetActive(false);
		snappedOpen = false;
	}
}
