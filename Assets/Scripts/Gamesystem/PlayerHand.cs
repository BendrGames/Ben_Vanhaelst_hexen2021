using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.HexSystem;
using DAE.HexSystem;

namespace DAE.Gamesystem
{
    class PlayerHand: MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IHand
{
        public int Handsize => throw new NotImplementedException();
        List<ICard> IHand.PlayerHand => throw new NotImplementedException();

        public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("OnPointerEnter");
		if (eventData.pointerDrag == null)
			return;

		Card d = eventData.pointerDrag.GetComponent<Card>();
		if (d != null)
		{
			d.placeholderParent = this.transform;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("OnPointerExit");
		if (eventData.pointerDrag == null)
			return;

			Card d = eventData.pointerDrag.GetComponent<Card>();
		if (d != null && d.placeholderParent == this.transform)
		{
			d.placeholderParent = d.parentToReturnTo;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

			Card d = eventData.pointerDrag.GetComponent<Card>();
		if (d != null)
		{
			d.parentToReturnTo = this.transform;
		}

	}

        public List<ICard> Drawcard()
        {
            throw new NotImplementedException();
        }

        public void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
