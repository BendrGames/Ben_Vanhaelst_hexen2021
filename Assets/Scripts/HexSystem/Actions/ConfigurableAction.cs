using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Actions
{
    class ConfigurableAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {
        public delegate List<Position> PositionCollector(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card);
        

        private PositionCollector _positionCollector;

        public ConfigurableAction(PositionCollector positionCollector)
        {
            _positionCollector = positionCollector;
        }

        //public override List<Position> Positions(Board<Position, ICard> board, Grid<Position> grid, ICard piece)
        //    => _positionCollector(board, grid, piece);

        

        public override List<Position> TotalValidPositions(Board<Position, TPiece> board, Grid<Position> grid, Position position, TPiece piece, CardType card)
        => _positionCollector(board, grid, position, piece, card);
        
    }
}
