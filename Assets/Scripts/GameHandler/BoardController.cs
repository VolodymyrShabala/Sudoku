using System.Collections.Generic;
using Data;
using Events;
using GameHandler;
using Tile;
using UndoSystem;
using UnityEngine;

public class BoardController : MonoBehaviour{
    private LevelData levelData;
    private TileController highlightedTile;
    private BoardModel model;
    private bool isPencilSelected;

    public void Init(BoardModel boardModel, LevelData data) {
        levelData = data;
        model = boardModel;

        EventSystem.Subscribe(EventKey.PlayerInput, ProcessInput);
        EventSystem.Subscribe(EventKey.SelectTile, SimpleSelectTile);
        EventSystem.Subscribe(EventKey.NumberSelected, NumberSelected);
        EventSystem.Subscribe(EventKey.PencilSelected, PencilSelected);
        EventSystem.Subscribe(EventKey.Erase, Erase);
    }

    private void ProcessInput(BaseEvent baseEvent) {
        PlayerInputEvent inputEvent = (PlayerInputEvent)baseEvent;
        if (model.IsInsideOfBoard(inputEvent.inputPosition) == false) {
            return;
        }

        TileController tile = model.GetTile(inputEvent.inputPosition);
        if (tile == null || highlightedTile == tile) {
            return;
        }

        SelectTile(tile);
    }

    // V: Used y undo system to reselect tiles and to skip the select animation
    private void SimpleSelectTile(BaseEvent baseEvent) {
        SelectTileEvent selectTileEvent = (SelectTileEvent)baseEvent;
        SelectTile(selectTileEvent.tile, true);
    }

    private void SelectTile(TileController tile, bool simpleHighlight = false) {
        highlightedTile?.RemoveHighlight();
        model.RemoveAllHighlight();
        model.SelectTile(tile, simpleHighlight);
        highlightedTile = tile;
    }

    private void NumberSelected(BaseEvent baseEvent) {
        if (highlightedTile == null) {
            return;
        }

        NumberSelectedEvent numberSelectedEvent = (NumberSelectedEvent)baseEvent;
        int numberIndex = numberSelectedEvent.numberIndex;
        if (isPencilSelected) {
            HandlePencilNumberSelected(numberIndex);
        }
        else {
            HandleTileNumberSelected(numberIndex, numberSelectedEvent.isUndo);
        }
    }

    private void HandlePencilNumberSelected(int numberIndex) {
        if (highlightedTile.IsCorrectNumberShown() == false &&
            highlightedTile.pencilController.CanShow(numberIndex) == false) {
            Vector2Int boardPosition = highlightedTile.GetBoardPosition();
            model.HighlightConflictingNumbers(numberIndex, boardPosition, isPencilSelected);
        }
        else {
            if (highlightedTile.pencilController.IsShowing(numberIndex)) {
                highlightedTile.pencilController.Hide(numberIndex);
            }
            else {
                highlightedTile.pencilController.Show(numberIndex);
            }
            
            EventSystem.Trigger(new UndoRecordEvent(new UndoPencilNumberPlacedCommand(highlightedTile, numberIndex)));
        }
    }

    private void HandleTileNumberSelected(int numberIndex, bool isUndo) {
        if (highlightedTile.IsCorrectNumberShown()) {
            return;
        }

        int previousNumberIndex = highlightedTile.GetShowingNumberIndex();
        if (previousNumberIndex == numberIndex) {
            return;
        }

        if (isUndo == false) {
            SaveNumberPlacedToUndo(previousNumberIndex);
        }
        
        string visualNumber = levelData.GetVisualNumber(numberIndex);
        int showingNumberIndex = highlightedTile.GetShowingNumberIndex(); 
        HandleNumberPlaced(numberIndex, visualNumber, showingNumberIndex);
        
        Vector2Int boardPosition = highlightedTile.GetBoardPosition();
        if (highlightedTile.IsCorrectNumberShown() == false) { 
            HandleWrongNumberPlaced(numberIndex, boardPosition, isUndo);
        }
        else { 
            HandleCorrectNumberPlaced(numberIndex, boardPosition, isUndo);
        }
    }

    private void SaveNumberPlacedToUndo(int previousNumberIndex) {
        List<int> showingPencilIndices = highlightedTile.pencilController.GetAllShowingNumberIndices();
        EventSystem.Trigger(new UndoRecordEvent(new UndoNumberPlacedCommand(highlightedTile, previousNumberIndex, showingPencilIndices)));
    }

    private void HandleNumberPlaced(int numberIndex, string visualNumber, int showingNumberIndex) {
        highlightedTile.PlaceNumber(numberIndex, visualNumber);
        highlightedTile.pencilController.HideAll();
        // V: In case we try to place a different number without re-selecting a tile
        if (showingNumberIndex != -1 && showingNumberIndex != numberIndex) {
            model.RemoveAllHighlight();
            model.SelectTile(highlightedTile);
        }
    }

    private void HandleWrongNumberPlaced(int numberIndex, Vector2Int boardPosition, bool isUndo) {
        model.HighlightAllOfNumberSimple(numberIndex, boardPosition);
        model.HighlightConflictingNumbers(numberIndex, boardPosition, isPencilSelected);
        highlightedTile.HighlightWrongNumber();
        model.HighlightAllOfPencilNumbers(numberIndex, boardPosition);
        if (isUndo == false) {
            EventSystem.Trigger(new WrongNumberPlacedEvent());
        }
    }

    private void HandleCorrectNumberPlaced(int numberIndex, Vector2Int boardPosition, bool isUndo) {
        List<TileController> tilesWithPencilIndexShowing = model.GetTilesWithPencilIndexShowing(numberIndex, boardPosition);
        foreach (TileController tile in tilesWithPencilIndexShowing) {
            tile.pencilController.Hide(numberIndex);
            tile.pencilController.RemoveAllHighlight();
        }
            
        EventSystem.Trigger(new UndoRecordEvent(new UndoRemovePencilFromNeighbouringTilesCommand(tilesWithPencilIndexShowing, numberIndex)));
        SelectTile(highlightedTile, true);
            
        if (isUndo == false) {
            EventSystem.Trigger(new CorrectNumberPlacedEvent(numberIndex));
        }
            
        CheckLevelWonCondition();
    }

    private void PencilSelected(BaseEvent baseEvent) {
        PencilSelectedEvent pencilSelectedEvent = (PencilSelectedEvent)baseEvent;
        isPencilSelected = pencilSelectedEvent.isSelected;
    }

    private void Erase(BaseEvent baseEvent) {
        if (highlightedTile == null) {
            return;
        }

        List<int> showingPencilIndices = highlightedTile.pencilController.GetAllShowingNumberIndices();
        EventSystem.Trigger(new UndoRecordEvent(new UndoEraseCommand(highlightedTile, showingPencilIndices)));
        highlightedTile.pencilController.HideAll();
    }

    private void CheckLevelWonCondition() {
        bool isBoardFilled = model.IsBoardFilled();
        if (isBoardFilled) { 
            EventSystem.Trigger(new LevelWonEvent());
        }
    }

    public void Clear() {
        model.Clear();
        model = null;
        levelData = null;
        highlightedTile = null;
        EventSystem.Unsubscribe(EventKey.PlayerInput, ProcessInput);
        EventSystem.Unsubscribe(EventKey.SelectTile, SimpleSelectTile);
        EventSystem.Unsubscribe(EventKey.NumberSelected, NumberSelected);
        EventSystem.Unsubscribe(EventKey.PencilSelected, PencilSelected);
        EventSystem.Unsubscribe(EventKey.Erase, Erase);
    }
}