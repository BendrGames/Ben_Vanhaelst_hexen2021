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
        }

        public override void ExecuteAction(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            foreach (var hex in TotalValidPositions(board, grid, position, piece, card))
            {
                if (board.TryGetPieceAt(hex, out var enemy))
                {
                    board.Take(enemy);

                    // calculate next hex player poss - enemy pos?

                    board.Place(enemy, position);
                }
            }
        }

        public override List<Position> TotalValidPositions(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelper.Direction0(1)
                        .Direction1(1)
                        .Direction2(1);

           

            ActionHelper<TCard, TPiece> actionHelperPartual = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelperPartual.TargettedDirection0(1)
                        .TargettedDirection1(1)
                        .TargettedDirection2(1);
                        

            if (!DisplayFullSelection)
                return actionHelper.Collect();

            else
                return actionHelperPartual.Collect();

        }
    }
}