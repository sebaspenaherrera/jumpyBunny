using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            CollectCoin();
        }
    }

    void CollectCoin() {
        isCollected = true;
        HideCoin();
        GameManager gm = GameManager.getInstance();
        gm.CollectCoin();
        print("Coins collected = " + gm.GetCollectedCoins());
    }

    void HideCoin() {

        // Hide coin sprite
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) {
            sprite.enabled = false;
        }

        // Hide collider
        var collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
