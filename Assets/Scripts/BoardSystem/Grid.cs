using DAE.Commons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.BoardSystem
{
    public class Grid <TPosition>
    {
        public int rows { get; }
        public int columns { get; }

        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;          
        }

        private BidirectionalDictionary<(int x, int y), TPosition> _positions = new BidirectionalDictionary<(int x, int y), TPosition>();

        public bool TryGetPositionAt(int x, int y, out TPosition position) => _positions.TryGetValue((x, y), out position);

        public bool TryGetCoordinateOf(TPosition position, out (int x, int y) coordinate)
            => _positions.TryGetKey(position, out coordinate);

        public void Register(int x, int y, TPosition position)
        {           
            _positions.Add((x, y), position);
        }
    }


}