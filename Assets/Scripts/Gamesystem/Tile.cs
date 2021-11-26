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
    public class Tile : MonoBehaviour, IPointerClickHandler
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

        public void OnPointerClick(PointerEventData eventData)
        {

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
