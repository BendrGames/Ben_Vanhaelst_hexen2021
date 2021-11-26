using DAE.Gamesystem;
using DAE.HexSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DAE.Gamesystem
{

    //public class TileEventArgs : EventArgs
    //{
    //    public Tile Tile { get; }

    //    public TileEventArgs(Tile tile) => Tile = tile;
    //}
    public class Tile : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        
        [SerializeField] private UnityEvent OnActivate;
        [SerializeField] private UnityEvent Ondeactivate;

        private Position _model;

        public Position Model
        {
            set
            {
                if (_model != null)
                {
                    _model.Activated -= PositionActivated;
                    _model.Deactivated -= PositionDeactivated;
                }

                _model = value;

                if (_model != null)
                {
                    _model.Activated += PositionActivated;
                    _model.Deactivated += PositionDeactivated;
                }
            }
            get { return _model; }
        }

        private void PositionDeactivated(object sender, EventArgs e)
        => Ondeactivate.Invoke();

        private void PositionActivated(object sender, EventArgs e)
        => OnActivate.Invoke();

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter");
            if (eventData.pointerDrag == null)
                return;

            //Card d = eventData.pointerDrag.GetComponent<Card>();
            //if(d != null) {
            //	d.placeholderParent = this.transform;
            //}


            //highlight tiles A groep
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
            if (eventData.pointerDrag == null)
                return;

            //Card d = eventData.pointerDrag.GetComponent<Card>();
            //if(d != null && d.placeholderParent==this.transform) {
            //	d.placeholderParent = d.parentToReturnTo;
            //}

            //highlight tiles  b goep
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

            Destroy(eventData.pointerDrag.gameObject);
        }
    

        

        //public bool Highlight
        //{
        //    set
        //    {
        //        OnHighlight.Invoke(value);
        //    }
        //}

        //public event EventHandler<TileEventArgs> Clicked;

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    OnClicked(this, new TileEventArgs(this));
        //}

        //protected virtual void OnClicked(object source, TileEventArgs e)
        //{
        //    var handler = Clicked;
        //    handler?.Invoke(this, e);
        //}

        //public override string ToString()
        //{
        //    return gameObject.name;
        //}

        }
}
