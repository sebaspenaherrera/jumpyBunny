using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewInGame : MonoBehaviour
{
    // ATTRIBUTES
    public TMP_Text coinsLabel;
    public TMP_Text scoreLabel;
    public TMP_Text maxScoreLabel;
    public static ViewInGame sharedInstance;


    // Initilize object
    private void Awake()
    {
        sharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //if ((GameManager.getInstance().currentGameState == GameState.Menu) || (GameManager.getInstance().currentGameState == GameState.GameOver)) {
        //    ShowHighestScore();
        //}

        if (GameManager.getInstance().currentGameState == GameState.InGame)
        { 
            scoreLabel.text = PlayerController.getInstance().GetDistance().ToString("000000");
        }
    }

    // METHODS
    public static ViewInGame GetInstance() {
        return sharedInstance;
    }

    public void ShowHighestScore() {
        maxScoreLabel.text = PlayerController.getInstance().GetMaxScore().ToString("000000");
    }

    public void UpdateCoins() {
        if (GameManager.getInstance().currentGameState == GameState.InGame) {
            coinsLabel.text = GameManager.getInstance().GetCollectedCoins().ToString("00000");
        }
    }
}
