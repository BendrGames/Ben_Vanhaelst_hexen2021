﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DAE.BoardSystem;

namespace DAE.HexSystem.Moves
{
    class ActionHelper
    {
        private Board<Position, ICard> _board;
        private Grid<Position> _grid;
        private ICard _piece;

        private List<Position> _validPositions = new List<Position>();

        public ActionHelper(Board<Position, ICard> board, Grid<Position> grid, ICard piece)
        {
            _board = board;
            this._piece = piece;
            this._grid = grid;
        }

        public delegate bool Validator(Board<Position, ICard> board, Grid<Position> grid, ICard piece, Position position);

        public ActionHelper ReturnAllAction(int xCenter, int yCenter, int numTiles = int.MaxValue, params Validator[] validators)
        {
            // Do the rangecheck thing, add all to list that dont have a piece.

            //for each - N ≤ q ≤ +N:
            //for each max(-N, -q - N) ≤ r ≤ min(+N, -q + N):
            //results.append(axial_add(center, Hex(q, r)))

            //for (int i = xCenter; -numTiles<= i <= numTiles; i++)
            //{

            //}
            

            //for (int dx = -range; dx <= range; dx++)
            //{
            //    for (int dy = Mathf.Max(-range, -dx - range); dy <= Mathf.Min(range, -dx + range); dy++)
            //    {
            //        o = new CubeIndex(dx, dy, -dx - dy) + center.index;
            //        if (grid.ContainsKey(o.ToString()))
            //            ret.Add(grid[o.ToString()]);
            //    }
            //}

            var nextXCoordinate = 0;
            var nextYCoordinate = 0;
            var nextposition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out var nextPosition);

            return this;
        }

        public ActionHelper StraightAction(int xOffset, int yOffset, int numTiles = int.MaxValue, params Validator[] validators)
        {
            if (!_board.TryGetPositionOf(_piece, out var position))
                return this;

            if (_grid.TryGetCoordinateOf(position, out var coordinate))
                return this;

            var nextXCoordinate = coordinate.x = xOffset;
            var nextYCoordinate = coordinate.y = yOffset;

            var hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out var nextPosition);
            int step = 0;

            while (hasNextPosition && step < numTiles)
            {
                var isOk = validators.All((v) => v(_board, _grid, _piece, nextPosition));
                if (!isOk)
                    return this;

                var hasPiece = _board.TryGetPieceAt(nextPosition, out var nextPiece);
                if (hasPiece)
                {
                    _validPositions.Add(nextPosition);
                }
                else
                {
                    if (nextPiece.player == _piece.player)
                        return this;

                    _validPositions.Add(nextPosition);
                    return this;
                }

                nextXCoordinate = coordinate.x = xOffset;
                nextYCoordinate = coordinate.y = yOffset;

                hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out nextPosition);

                step++;
            }

            return this;
        }



        //internal ActionHelper North(int numTiles = int.MaxValue, params Validator[] validators)
        //   => Move(0, 1, numTiles, validators);

        //internal ActionHelper North(int numTiles = int.MaxValue, params Validator[] validators)
        //   => Move(0, 1, numTiles, validators);
        //internal ActionHelper NorthEast(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(1, 1, numTiles, validators);
        //internal ActionHelper East(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(1, 0, numTiles, validators);
        //internal ActionHelper SouthEast(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(1, -1, numTiles, validators);
        //internal ActionHelper South(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(0, -1, numTiles, validators);
        //internal ActionHelper SouthWest(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(-1, -1, numTiles, validators);
        //internal ActionHelper West(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(-1, 0, numTiles, validators);
        //internal ActionHelper NorthWest(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(-1, 1, numTiles, validators);

        

        internal ActionHelper Direction0(int numTiles = int.MaxValue, params Validator[] validators)
         => StraightAction((int)_directions[0].x, (int)_directions[0].y, numTiles, validators);
        internal ActionHelper Direction1(int numTiles = int.MaxValue, params Validator[] validators)
         => StraightAction((int)_directions[0].x, (int)_directions[0].y, numTiles, validators);
        internal ActionHelper Direction2(int numTiles = int.MaxValue, params Validator[] validators)
         => StraightAction((int)_directions[0].x, (int)_directions[0].y, numTiles, validators);
        internal ActionHelper Direction3(int numTiles = int.MaxValue, params Validator[] validators)
         => StraightAction((int)_directions[0].x, (int)_directions[0].y, numTiles, validators);
        internal ActionHelper Direction4(int numTiles = int.MaxValue, params Validator[] validators)
         => StraightAction((int)_directions[0].x, (int)_directions[0].y, numTiles, validators);
        internal ActionHelper Direction5(int numTiles = int.MaxValue, params Validator[] validators)
         => StraightAction((int)_directions[0].x, (int)_directions[0].y, numTiles, validators);



        private Vector2[] _directions =
            new Vector2[6]{new Vector2(1,0), new Vector2(1,-1), new Vector2(0,-1),
            new Vector2(-1,0), new Vector2(-1,1), new Vector2(0,1)};

        private Vector2[] _diagonalDirections =
            new Vector2[6] { new Vector2(+2, -1), new Vector2(+1, -2), new Vector2(-1, -1),
            new Vector2(-2, +1), new Vector2(-1, +2), new Vector2(+1, +1) };


        public Vector2 HexAdd(Vector2 hexA, Vector2 HexB)
        {
            return new Vector2(hexA.x + HexB.x, hexA.y + HexB.y);
        }

        public Vector2 HexSubstract(Vector2 hexA, Vector2 HexB)
        {
            return new Vector2(hexA.x - HexB.x, hexA.y - HexB.y);
        }

        public Vector2 HexMultiply(Vector2 hexA, Vector2 HexB)
        {
            return new Vector2(hexA.x * HexB.x, hexA.y * HexB.y);
        }

        public int HexLenght(Vector2 hex)
        {
            return (int)((Mathf.Abs(hex.x) + (Mathf.Abs(hex.y)) + (-(Mathf.Abs(hex.x) - (Mathf.Abs(hex.x))))));
        }

        public int HexDistance(Vector2 hexA, Vector2 hexB)
        {
            return HexLenght(HexSubstract(hexA, hexB));
        }

        public Vector2 HexDirection(int direction)
        {
            return _directions[direction];
        }

        public Vector2 HexDiagonalDirection(int diagonalDirection)
        {
            return _diagonalDirections[diagonalDirection];
        }

        public Vector2 HexNeighbour(Vector3 hex, int direction)
        {
            return HexAdd(hex, HexDirection(direction));
        }

        

        internal List<Position> Collect()
        {
            return _validPositions;
        }
    }
}