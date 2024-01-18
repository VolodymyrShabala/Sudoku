using Board;
using Data;
using Tile;
using Tile.Pencil;
using UnityEngine;

public class GameCreator : MonoBehaviour{
    [SerializeField] private TileView tilePrefab;
    [SerializeField] private PencilView pencilPrefab;
    [SerializeField] private RectTransform boardRectTransform;
    
    public TileController[,] CreateGame(LevelData levelData) {
        int seed = levelData.GetRandomSeed();
        Vector2Int boardSize = levelData.GetBoardSize();
        int numberOfHiddenValues = levelData.GetNumberOfHiddenValues();
        SudokuGenerator sudokuGenerator = new(boardSize.y, boardSize.x, numberOfHiddenValues, seed);
        sudokuGenerator.Generate();

        BoardCreator boardCreator = new(tilePrefab, boardRectTransform, levelData);
        TileController[,] board = boardCreator.CreateBoard(sudokuGenerator.GetSudoku(), sudokuGenerator.GetHiddenNumbers());

        PencilCreator pencilCreator = new(pencilPrefab, board);
        pencilCreator.Generate(levelData);
        return board;
    }
}