using BoardActions;
using Data;
using Events;
using GameHandler;
using GameOver;
using SaveSystem;
using SelectableNumbers;
using Tile;
using UndoSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStarter : MonoBehaviour{
    [SerializeField] private GameCreator gameCreator;
    [SerializeField] private LevelDatasHolder levelDatasHolder;
    [FormerlySerializedAs("gameController")] [SerializeField] private BoardController _boardController;
    [SerializeField] private SelectableNumberController selectableNumberController;
    [SerializeField] private BoardActionsController boardActionsController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private MistakesController mistakesController;
    [SerializeField] private TimerController timerController;
    [SerializeField] private SettingsButton settingsButton;
    [SerializeField] private SettingsButton pauseButton;
    [SerializeField] private InputController inputController;
    [SerializeField] private LevelDifficultyNameDisplay levelDifficultyNameDisplay;
    [SerializeField] private PopupController popupController;
    private UndoController undoController;
    private GameEndController gameEndController;
    
    private void Start() {
        int levelIndex = LevelSave.GetSaveLevelIndex();
        LevelData levelData = levelDatasHolder.GetLevel(levelIndex);

        TileController[,] board = gameCreator.CreateGame(levelData);
        BoardModel boardModel = new(board, levelData);
        _boardController.Init(boardModel, levelData);
        selectableNumberController.Init(board, levelData);
        boardActionsController.Init();
        undoController = new UndoController();
        scoreController.Init(levelData);
        mistakesController.Init(levelData);
        timerController.Init();
        settingsButton.Init();
        pauseButton.Init();
        inputController.Init();
        levelDifficultyNameDisplay.Init(levelData.GetDifficultyName());
        popupController.Init(levelData.GetDifficultyName(), levelData.GetAllowedMistakesAmount(), levelIndex);
        gameEndController = new GameEndController(popupController, timerController);
        
        EventSystem.Subscribe(EventKey.ClearLevel, Clear);
    }

    private void Clear(BaseEvent baseEvent) {
        _boardController.Clear();
        selectableNumberController.Clear();
        boardActionsController.Clear();
        undoController.Clear();
        scoreController.Clear();
        mistakesController.Clear();
        timerController.Clear();
        settingsButton.Clear();
        pauseButton.Clear();
        inputController.Clear();
        popupController.Clear();
        gameEndController.Clear();
        
        EventSystem.Unsubscribe(EventKey.ClearLevel, Clear);
        EventSystem.Dispose();
    }
}