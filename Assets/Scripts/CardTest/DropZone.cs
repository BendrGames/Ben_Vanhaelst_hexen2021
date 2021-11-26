﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;

		//Card d = eventData.pointerDrag.GetComponent<Card>();
		//if(d != null) {
		//	d.placeholderParent = this.transform;
		//}


		//highlight tiles A groep
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		//Card d = eventData.pointerDrag.GetComponent<Card>();
		//if(d != null && d.placeholderParent==this.transform) {
		//	d.placeholderParent = d.parentToReturnTo;
		//}

		//highlight tiles  b goep
	}

	public void OnDrop(PointerEventData eventData) {
		Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

		Destroy(eventData.pointerDrag.gameObject);


		

	}
}