using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    // Launch collision event
    private void OnTriggerEnter2D(Collider2D element)
    {
        if (element.tag == "Player") {
            LevelGenerator.sharedInstance.AddNewBlock();
            LevelGenerator.sharedInstance.RemoveOldBlock();
        }
    }
}
