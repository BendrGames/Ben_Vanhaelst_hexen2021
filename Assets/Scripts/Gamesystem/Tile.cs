using DAE.Gamesystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DAE.Gamesystem
{
    //[Serializable]
    //public class HighLightEvent : UnityEvent<bool> { }
    public class TileEventArgs : EventArgs
    {
        public Tile Tile { get; }

        public TileEventArgs(Tile tile) => Tile = tile;
    }
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private HighLightEvent OnHighlight;

        public bool Highlight
        {
            set
            {
                OnHighlight.Invoke(value);
            }
        }

        public event EventHandler<TileEventArgs> Clicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked(this, new TileEventArgs(this));
        }

        protected virtual void OnClicked(object source, TileEventArgs e)
        {
            var handler = Clicked;
            handler?.Invoke(this, e);
        }

        public override string ToString()
        {
            return gameObject.name;
        }

        }
}
