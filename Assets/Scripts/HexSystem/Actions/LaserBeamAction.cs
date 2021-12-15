﻿using DAE.BoardSystem;
using DAE.HexSystem;
using DAE.HexSystem.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Actions
{

    class LaserBeamAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        public bool DisplayFullSelection;

        public override bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            if (TotalValidPositions(board, grid, position, piece, card).Contains(position))
            {
                DisplayFullSelection = true;
                return true;
            }

            else
            {
                DisplayFullSelection = false;
                return true;
            }

            return false;
        }

        public override void ExecuteAction(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            foreach (var hex in TotalValidPositions(board, grid, position, piece, card))
            {
                if (board.TryGetPieceAt(hex, out var enemy))
                {
                    board.Take(enemy);
                }
            }
        }

        public override List<Position> TotalValidPositions(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelper.Direction0(10)
                        .Direction1(10)
                        .Direction2(10)
                        .Direction3(10)
                        .Direction4(10)
                        .Direction5(10);

            ActionHelper<TCard, TPiece> actionHelperPartual = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelperPartual.TargettedDirection0(10)
                        .TargettedDirection1(10)
                        .TargettedDirection2(10)
                        .TargettedDirection3(10)
                        .TargettedDirection4(10)
                        .TargettedDirection5(10);

            if (!DisplayFullSelection)
                return actionHelper.Collect();

            else
                return actionHelperPartual.Collect();


        }
    }
}
