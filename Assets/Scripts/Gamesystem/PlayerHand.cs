using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 
using DAE.HexSystem;

namespace DAE.Gamesystem
{
    class PlayerHand: MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IHand<Card>
	{
		public Deck PlayerDeck;
		public GameObject HandView;

		private int _handsize;
		private List<Card> _playerHandCardList;
        public int Handsize => _handsize;
		public List<Card> PlayerHandCardList => _playerHandCardList;

        public PlayerHand(Deck playerDeck, int handsize, List<Card> playerHandCardList)
        {
            PlayerDeck = playerDeck;
            _handsize = handsize;

            for (int i = 0; i < _handsize-1; i++)
            {
				Drawcard();
			}
        }	

        public void Drawcard()
        {
            _playerHandCardList.Add(PlayerDeck.CurrentDeckList.Pop());
        }

        public void PlayCard()
        {
			//get this into the tile when drop
            throw new NotImplementedException();
        }

        public List<Card> DiscardCard()
        {
            throw new NotImplementedException();
        }

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

    }
}
