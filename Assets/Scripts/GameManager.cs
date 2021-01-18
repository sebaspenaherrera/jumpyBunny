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
                break;
            case GameState.GameOver:
                break;
            case GameState.Menu:
                break;
            default:
                break;
        }
        this.currentGameState = newGameState;
    }

    // Start our game
    public void StartGame()
    {
        PlayerController.getInstance().StartGame();
        ChangeGameState(GameState.InGame);
    }

    // Called when player dies
    public void GameOver()
    {
        ChangeGameState(GameState.GameOver);
    }

    //Resume game, menu
    public void BackToMainMenu()
    {
        ChangeGameState(GameState.Menu);
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
    }

    // Update the game in every frame update
    private void Update()
    {
        if ((currentGameState != GameState.InGame) && (Input.GetButtonDown("s"))) {
            StartGame();
        }
    }




}
