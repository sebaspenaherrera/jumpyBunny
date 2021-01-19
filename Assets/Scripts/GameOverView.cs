using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverView : MonoBehaviour
{
    // ATTRIBUTES
    public TMP_Text coinsLabel;
    public TMP_Text scoreLabel;
    public static GameOverView sharedInstance;

    
    private void Awake()
    {
        sharedInstance = this;
    }

    // METHODS
    public static GameOverView GetInstance() {
        return sharedInstance;
    }

    // Update is called once per frame
    public void UpdateGUI()
    {
        if (GameManager.getInstance().currentGameState == GameState.GameOver) {
            coinsLabel.text = GameManager.getInstance().GetCollectedCoins().ToString("00000");
            scoreLabel.text = PlayerController.getInstance().GetDistance().ToString("000000");
        }
    }
}
