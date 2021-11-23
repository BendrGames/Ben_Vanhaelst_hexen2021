using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Moves
{
    class ConfigurableAction : ActionBase
    {
        public delegate List<Position> PositionCollector(Board<Position, ICard> board, Grid<Position> grid, ICard piece);

        private PositionCollector _positionCollector;

        public ConfigurableAction(PositionCollector positionCollector)
        {
            _positionCollector = positionCollector;
        }

        public override List<Position> Positions(Board<Position, ICard> board, Grid<Position> grid, ICard piece)
            => _positionCollector(board, grid, piece);


    }
}
