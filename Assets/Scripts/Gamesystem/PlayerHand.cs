
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
    class PlayerHand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IHand<Card>
    {
        public Deck PlayerDeck;
        public GameObject HandView;

        private int _handsize;
        public List<Card> _playerHandCardList;
        public int Handsize => _handsize;
        public List<Card> PlayerHandCardList => _playerHandCardList;

        public void InitializePlayerHand(Deck playerDeck, int handsize)
        {
            PlayerDeck = playerDeck;
            PlayerDeck.GenerateDeck();

            _handsize = handsize;

            
        }

        public Card Drawcard()
        {
            int randomnum = Random.Range(0, PlayerDeck.CurrentDeckList.Count);
            _playerHandCardList.Add(PlayerDeck.CurrentDeckList[randomnum]);
            PlayerDeck.CurrentDeckList.RemoveAt(randomnum);
            var card = Instantiate(_playerHandCardList[_playerHandCardList.Count - 1], HandView.transform);

            return card;

        }

        public void PlayCard()
        {

        }

        public List<Card> DiscardCard()
        {
            return null;
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
