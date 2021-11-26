using DAE.HexSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DAE.Gamesystem
{
    class CardEventArgs : EventArgs
    {
        public Card Card { get; }
        public CardEventArgs(Card piece) => Card = Card;
    }

    class Card : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, ICard
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private bool _played;
        [SerializeField] private Texture2D _cardTexture;
        [SerializeField] private Color _CardColor;

        [SerializeField] public GameObject CardImage;
        [SerializeField] public GameObject Cardcolour;
        [SerializeField] public GameObject TitleText;
        [SerializeField] public GameObject DiscriptionText;



        [SerializeField] private CardType _cardType;

        public Transform parentToReturnTo = null;
        public Transform placeholderParent = null;

        GameObject placeholder = null;

        public bool Played => _played;
        public string Name => _name;
        public CardType CardType => _cardType;
        public Texture2D CardTexture => _cardTexture;
        public string Description => _description;
        public Color Color => _CardColor;

        public event EventHandler<CardEventArgs> Clicked;
        public event EventHandler<CardEventArgs> BeginDrag;
        public event EventHandler<CardEventArgs> Dragging;
        public event EventHandler<CardEventArgs> EndDrag;
        public event EventHandler<CardEventArgs> IDrop;

        private void Start()
        {
            //Cardcolour.GetComponent<RawImage>().color = _CardColor;
            CardImage.GetComponent<RawImage>().texture = CardTexture;
            TitleText.GetComponent<Text>().text = Name;
            DiscriptionText.GetComponent<Text>().text = _description;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");

            placeholder = new GameObject();
            placeholder.transform.SetParent(this.transform.parent);
            LayoutElement le = placeholder.AddComponent<LayoutElement>();
            le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
            le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

            parentToReturnTo = this.transform.parent;
            placeholderParent = parentToReturnTo;
            this.transform.SetParent(this.transform.parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;

            OnBeginDragging(this, new CardEventArgs(this));
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");

            this.transform.position = eventData.position;

            if (placeholder.transform.parent != placeholderParent)
                placeholder.transform.SetParent(placeholderParent);

            int newSiblingIndex = placeholderParent.childCount;

            for (int i = 0; i < placeholderParent.childCount; i++)
            {
                if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
                {

                    newSiblingIndex = i;

                    if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;

                    break;
                }
            }

            placeholder.transform.SetSiblingIndex(newSiblingIndex);

            OnDragging(this, new CardEventArgs(this));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            Destroy(placeholder);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //zoom in on card

            OnClicked(this, new CardEventArgs(this));
        }
        //public void OnDrop(PointerEventData eventData)
        //{
        //    OnIDrop(this, new CardEventArgs(this));
        //}

        protected virtual void OnClicked(object source, CardEventArgs e)
        {
            var handler = Clicked;
            handler?.Invoke(this, e);
        }
        protected virtual void OnBeginDragging(object source, CardEventArgs e)
        {
            var handler = BeginDrag;
            handler?.Invoke(this, e);
        }
        protected virtual void OnDragging(object source, CardEventArgs e)
        {
            var handler = Dragging;
            handler?.Invoke(this, e);
        }
        protected virtual void OnEndDragging(object source, CardEventArgs e)
        {
            var handler = EndDrag;
            handler?.Invoke(this, e);
        }

        //protected virtual void OnIDrop(object source, CardEventArgs e)
        //{
        //    var handler = IDrop;
        //    handler?.Invoke(this, e);
        //}

    }
}

