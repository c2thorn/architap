using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitBreakdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public int characterIndex;
	public upgradeController upgradeController;
	// Use this for initialization
	public void OnPointerEnter(PointerEventData eventData)
	{
		// if (gameObject.transform.parent.gameObject.active 
		// 	&& gameObject.GetComponent<Text>().text.Length > 1
		// 	&& Application.platform != RuntimePlatform.Android
		// 	&& Application.platform != RuntimePlatform.IPhonePlayer)
		// 	upgradeController.showToolTip(characterIndex);
     }

	public void OnPointerExit(PointerEventData eventData)
	{
		// if (Application.platform != RuntimePlatform.Android
		// 	&& Application.platform != RuntimePlatform.IPhonePlayer)
		// 	upgradeController.hideToolTip();
     }

}
