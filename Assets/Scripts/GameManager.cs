using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Menu,
    InGame,
    GameOver,
    Resume
}

public class GameManager : MonoBehaviour
{
    // ATTRIBUTES
    public GameState currentGameState = GameState.Menu;
    private static GameManager sharedInstance;
    public Canvas mainMenu;
    public Canvas gameMenu;
    public Canvas gameOverMenu;
    private int collectedCoins = 0;

    // METHODS

    // Access to object
    public static GameManager getInstance() {
        return sharedInstance;
    }

    //Change game state
    void ChangeGameState(GameState newGameState)
    {

        switch (newGameState)
        {
            case GameState.InGame:
                mainMenu.enabled = false;
                gameMenu.enabled = true;
                gameOverMenu.enabled = false;
                break;
            case GameState.GameOver:
                mainMenu.enabled = false;
                gameMenu.enabled = false;
                gameOverMenu.enabled = true;
                break;
            case GameState.Menu:
                mainMenu.enabled = true;
                gameMenu.enabled = false;
                gameOverMenu.enabled = false;
                break;
            default:
                break;
        }
        this.currentGameState = newGameState;
    }

    // Start our game
    public void StartGame()
    {
        LevelGenerator.sharedInstance.CreateInitialBlocks();
        PlayerController.getInstance().StartGame();
        ChangeGameState(GameState.InGame);
        ViewInGame.GetInstance().ShowHighestScore();
    }

    // Called when player dies
    public void GameOver()
    {
        LevelGenerator.sharedInstance.RemoveAllBlocks();
        ChangeGameState(GameState.GameOver);
        GameOverView.GetInstance().UpdateGUI();
    }

    //Resume game, menu
    public void BackToMainMenu()
    {
        ChangeGameState(GameState.Menu);
    }

    // Coins count
    public void CollectCoin() {
        collectedCoins++;
        ViewInGame.GetInstance().UpdateCoins();
    }

    public int GetCollectedCoins() {
        return collectedCoins;
    }

    // Reset player preferences (highest score)
    public void ResetScore() {
        if (currentGameState == GameState.Menu) {
            PlayerController.getInstance().ResetHighestScore();
        }
    }

    // Initialize the game
    private void Awake()
    {
        sharedInstance = this;   
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentGameState = GameState.Menu;
        mainMenu.enabled = true;
        gameMenu.enabled = false;
        gameOverMenu.enabled = false;
    }

    // Update the game in every frame update
    private void Update()
    {
        if ((currentGameState != GameState.InGame) && (Input.GetButtonDown("s"))) {
            StartGame();
        }
    }




}
