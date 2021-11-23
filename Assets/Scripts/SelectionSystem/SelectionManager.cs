using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.SelectionSystem
{
    public class SelectableItemEventArgs<TSelectableItem> : EventArgs
    {
        public TSelectableItem SelectableItem { get;  }

        public SelectableItemEventArgs(TSelectableItem selectableItem)
        {
            SelectableItem = selectableItem;
        }
    }

    public class SelectionManager<TSelectableItem>
    {

        public event EventHandler<SelectableItemEventArgs<TSelectableItem>> Selected;
        public event EventHandler<SelectableItemEventArgs<TSelectableItem>> DeSelected;

        private HashSet<TSelectableItem> _selectableItems = new HashSet<TSelectableItem>();
        public IReadOnlyCollection<TSelectableItem> SelectableItems => _selectableItems;

        public bool IsSelected( TSelectableItem selectableItem)
            => _selectableItems.Contains(selectableItem);

        public bool Deselect(TSelectableItem selectableItem)
        {
            if (_selectableItems.Remove(selectableItem))
            {
                OnDeSelected(new SelectableItemEventArgs<TSelectableItem>(selectableItem));
                return true;
            }
            return false;         
        }

        public bool Select(TSelectableItem selectableItem)
        {
            if(_selectableItems.Add(selectableItem))
            {
                OnSelected(new SelectableItemEventArgs<TSelectableItem>(selectableItem));
                return true;
            }
            return false;
        }

        //toggle false or true both times if it works
        public bool Toggle(TSelectableItem selectableItem)
        {
            if (_selectableItems.Contains(selectableItem))
                return !Deselect(selectableItem);
            else
                return Select(selectableItem);          
        }

        public void DeselectAll()
        {
            foreach (var selectableItem in _selectableItems.ToList())
            {
                Deselect(selectableItem);
            }
        }

        protected virtual void OnSelected(SelectableItemEventArgs<TSelectableItem> eventArgs)
        {
            var handler = Selected;
;            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnDeSelected(SelectableItemEventArgs<TSelectableItem> eventArgs)
        {
            var handler = DeSelected;
            handler?.Invoke(this, eventArgs);
        }
    }
}
