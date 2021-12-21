using DAE.BoardSystem;
using DAE.SelectionSystem;
using DAE.CardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DAE.HexSystem;
using DAE.StateSystem;
using DAE.GameSystem.GameStates;
using DAE.ReplaySystem;
//using DAE.HexSystem.Cards;
//using DAE.HexSystem;

namespace DAE.Gamesystem
{
    //testcommit

    class GameLoop : MonoBehaviour
    {
        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Transform _boardParent;
               
        private Grid<Position> _grid;
        private Board<Position, Piece> _board;

        public Deck _deckview;
        public PlayerHand _playerhand;
        private ActionManager<Card, Piece> _actionManager;

        public Piece Player;
       
        public Card CurrentCard;

        private StateMachine<GameStateBase> _gameStateMachine;

        
        //[SerializeField]
        //private DeckObject _mydeckObject;

        public void Start()
        {
            _grid = new Grid<Position>(3, 3);
            ConnectGrid(_grid);
            _board = new Board<Position, Piece>();           
            ConnectPiece(_grid, _board);         
            
            var replayManager = new ReplayManager();

            _actionManager = new ActionManager<Card,Piece>(_board, _grid, replayManager);


            _gameStateMachine = new StateMachine<GameStateBase>();
            _gameStateMachine.Register(GameState.GamePlayState, new GamePlayState(_gameStateMachine, _board, _actionManager, _playerhand, _deckview));
            _gameStateMachine.Register(GameState.ReplayState, new ReplayState(_gameStateMachine, replayManager));
            
            _gameStateMachine.InitialState = GameState.GamePlayState;


            _board.moved += (s, e) =>
            {             
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_grid, _boardParent, toCoordinate.x, toCoordinate.y);

                    e.Piece.MoveTo(worldPosition);
                }
            };

            _board.placed += (s, e) =>
            {

                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_grid, _boardParent, toCoordinate.x, toCoordinate.y);


                    e.Piece.Place(worldPosition);
                }

            };

            _board.taken += (s, e) =>
            {
                e.Piece.Taken();
            };
        }      

        private void ConnectGrid(Grid<Position> grid)
        {
            var views = FindObjectsOfType<PositionView>();
            foreach (var view in views)
            {
                var position = new Position();
                view.Model = position;

                view.Dropped += (s, e) => _gameStateMachine.CurrentState.OnDrop(Player, position);
                view.Entered += (s, e) => _gameStateMachine.CurrentState.HighLightNew(Player, position);
                view.Exitted += (s, e) => _gameStateMachine.CurrentState.UnHighlightOld(Player, position);

                
                var (x, y) = _positionHelper.ToGridPosition(_grid, _boardParent, view.transform.position);
                Debug.Log($"value of tile { view.name} is X: {x} and y: {y}");

                grid.Register(x, y, position);

                view.gameObject.name = $"tile {x},{y}";
            }
        }
        private void ConnectPiece(Grid<Position> grid, Board<Position, Piece> board)
        {
            var pieces = FindObjectsOfType<Piece>();
            foreach (var piece in pieces)
            {
                var (x, y) = _positionHelper.ToGridPosition(_grid, _boardParent, piece.transform.position);
                if (grid.TryGetPositionAt(x, y, out var position))
                {
                    Debug.Log("registered");                                      

                    board.Place(piece, position);
                }
            }
        }

        public void Forward()
                 => _gameStateMachine.CurrentState.Forward();
        public void Backward()
                 => _gameStateMachine.CurrentState.Backward();



      

    }
}

