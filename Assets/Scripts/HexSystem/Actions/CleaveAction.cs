using DAE.BoardSystem;
using DAE.HexSystem;
using DAE.HexSystem.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Actions
{
    class CleaveAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        public override bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            return base.CanExecute(board, grid, position, piece, card);
        }

        public override void ExecuteAction(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            foreach (var hex in Positions(board, grid, position, piece, card))
            {
                if (board.TryGetPieceAt(hex, out var enemy))
                {
                    board.Take(enemy);

                    // calculate next hex player poss - enemy pos?

                    board.Place(enemy, position);
                }
            }
        }

        public override List<Position> Positions(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelper.Direction0(1)
                        .Direction1(1)
                        .Direction2(1);

            //how to get in right direction?
                        


            return actionHelper.Collect();
        }
    }
}