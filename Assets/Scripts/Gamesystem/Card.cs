using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DAE.Gamesystem
{
    class CardEventArgs : EventArgs
    {
        public Card Card { get; }
        public CardEventArgs(Card piece) => Card = Card;
    }

    class Card : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public event EventHandler<CardEventArgs> Clicked;
        public event EventHandler<CardEventArgs> BeginDrag;
        public event EventHandler<CardEventArgs> Dragging;
        public event EventHandler<CardEventArgs> EndDrag;
        public event EventHandler<CardEventArgs> IDrop;

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragging(this, new CardEventArgs(this));
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragging(this, new CardEventArgs(this));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragging(this, new CardEventArgs(this));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked(this, new CardEventArgs(this));
        }
        public void OnDrop(PointerEventData eventData)
        {
            OnIDrop(this, new CardEventArgs(this));
        }

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

        protected virtual void OnIDrop(object source, CardEventArgs e)
        {
            var handler = IDrop;
            handler?.Invoke(this, e);
        }

    }
}

