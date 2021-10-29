using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public GameObject pauseOverlay;
    GameState gameState;

    // Update is called once per frame
    void Update()
    {
        gameState = player.GetState;


        if (gameState == GameState.Pause)
        {
            pauseOverlay.SetActive(true);
        }
        else
        {
            pauseOverlay.SetActive(false);
        }
    }
}
