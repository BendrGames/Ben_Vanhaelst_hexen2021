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

    class TeleportAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        public bool DisplayFullSelection;
        public override bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            //if (TotalValidPositions(board, grid, position, piece, card).Contains(position))
            //{
            //    DisplayFullSelection = true;
            //    return true;
            //}

            //else
            //{
            //    DisplayFullSelection = false;
            //    return true;
            //}

            if (board.TryGetPieceAt(position, out var toPiece))
                return false;

            else return true;
        }

        public override void ExecuteAction(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {

            board.Move(piece, position);
        }

        public override List<Position> TotalValidPositions(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        {
            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelper.SelectSIngle();

            return actionHelper.Collect();


        }
    }
}