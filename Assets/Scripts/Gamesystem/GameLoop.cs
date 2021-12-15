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

        private SelectionManager<Piece> _selectionmanagerPiece;
        //private SelectionManager<Tile> _selectionmanagerTile;
        private Grid<Position> _grid;
        private Board<Position, Piece> _board;

        public Deck _deckview;
        public PlayerHand _playerhand;
        private ActionManager<Piece> _actionManager;

        public Piece Player;

        public Card CurrentCard;

        //[SerializeField]
        //private DeckObject _mydeckObject;

        public void Start()
        {

            _playerhand.InitializePlayerHand(_deckview, 5);
            InitializeHand();

            _grid = new Grid<Position>(3, 3);
            ConnectGrid(_grid);
            _board = new Board<Position, Piece>();


            //_selectionmanagerPiece = new SelectionManager<Piece>();
            ConnectPiece(_selectionmanagerPiece, _grid, _board);
            //InitializePieceSelection();

            _actionManager = new ActionManager<Piece>(_board, _grid);

            //_selectionmanagerTile = new SelectionManager<Tile>();
            //ConnectTile(_selectionmanagerTile, _grid, _board);
            //InitializeTileSelection();

            _board.moved += (s, e) =>
            {
                //_movemanager.ValidPisitionsFor(e.Piece);

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

        private void InitializeHand()
        {
            var cards = FindObjectsOfType<Card>();
            foreach (var card in cards)
            {                

                card.BeginDrag += (s, e) =>
                {
                    CurrentCard = e.Card;                                       
                    
                    Debug.Log($"draggingEvent {CurrentCard}");

                };                             
            }
        }



        //private void InitializeTileSelection()
        //{
        //    _selectionmanagerTile.Selected += (s, e) =>
        //    {
        //        //if (_board.TryGetPieceAt(e.SelectableItem, out var piece))
        //        //{
        //        //    Debug.Log($"Piece {e.SelectableItem} on tile {piece.gameObject.name}");
        //        //}
        //        Debug.Log($"tile {e.SelectableItem.name}");

        //        e.SelectableItem.Highlight = true;
        //    };

        //    _selectionmanagerTile.DeSelected += (s, e) =>
        //    {
        //        // dehighlight
        //        e.SelectableItem.Highlight = false;
        //    };
        //}

        //private void InitializePieceSelection()
        //{
        //    _selectionmanagerPiece.Selected += (s, e) =>
        //    {
        //        var positions = _actionManager.ValidPisitionsFor(e.SelectableItem);

        //        foreach (var position in positions)
        //        {
        //            position.Activate();
        //        }
        //    };

        //    _selectionmanagerPiece.DeSelected += (s, e) =>
        //    {
        //        var positions = _actionManager.ValidPisitionsFor(e.SelectableItem);

        //        foreach (var position in positions)
        //        {
        //            position.Deactivate();
        //        }
        //    };
        //}

        public void DeselectAll()
        {
            _selectionmanagerPiece.DeselectAll();
            //_selectionmanagerTile.DeselectAll();
        }

        private void ConnectGrid(Grid<Position> grid)
        {
            var views = FindObjectsOfType<PositionView>();
            foreach (var view in views)
            {
                var position = new Position();
                view.Model = position;


                view.Dropped += (s, e) =>
                {
                    ////if (!_selectionmanagerPiece.HasSelection)
                    ////    return;
                    //var SelectedItem = _selectionmanagerPiece.SelectedItem;
                    var validpositions = _actionManager.ValidPisitionsFor(Player, /*e.Position,*/ CurrentCard._cardType);

                    if (validpositions.Contains(e.Position))
                    {
                        _actionManager.Move(Player, e.Position, CurrentCard._cardType);
                        foreach(var position in validpositions)
                        {
                            position.Deactivate();
                        }
                        

                        //_selectionmanagerPiece.DeselectAll();
                        //_selectionmanagerPiece.Toggle(s as Piece);
                    }

                };

                view.Entered += (s, e) =>
                {
                    var positions = _actionManager.ValidPisitionsFor(Player, /*e.Position,*/ CurrentCard._cardType);

                    foreach (var position in positions)
                    {
                        position.Activate();
                    }
                };

                view.Exitted += (s, e) =>
                {
                    var positions = _actionManager.ValidPisitionsFor(Player, /*e.Position,*/ CurrentCard._cardType);

                    foreach (var position in positions)
                    {
                        position.Deactivate();
                    }
                };




                var (x, y) = _positionHelper.ToGridPosition(_grid, _boardParent, view.transform.position);
                Debug.Log($"value of tile { view.name} is X: {x} and y: {y}");

                grid.Register(x, y, position);

                view.gameObject.name = $"tile {x},{y}";
            }
        }

        private void ConnectPiece(SelectionManager<Piece> selectionmanager, Grid<Position> grid, Board<Position, Piece> board)
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

        //private void ConnectTile(SelectionManager<Tile> selectionmanager, Grid<Tile> grid, Board<Tile, Piece> board)
        //{
        //    var Tiles = FindObjectsOfType<Tile>();
        //    foreach (var found in Tiles)
        //    {
        //        Debug.Log("found");

        //        var (x, y) = _positionHelper.ToGridPosition(_grid, _boardParent, found.transform.position);
        //        if (grid.TryGetPositionAt(x, y, out var tile))
        //        {
        //            Debug.Log("registered");
        //            found.Clicked += (s, e) => selectionmanager.Toggle(s as Tile);
        //        }


        //        //if (grid.TryGetPositionAt(x, y, out var tile))
        //        //{
        //        //    tile.Clicked += (s, e) => selectionmanager.Toggle(s as Tile);
        //        //    //board.Place(tile, tile);
        //        //}
        //    }
        //}

    }
}

