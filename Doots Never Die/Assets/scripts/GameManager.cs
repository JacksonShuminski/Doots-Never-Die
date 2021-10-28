using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Play,
    Pause,
    End
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameState state;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        state = GameState.Menu;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == GameState.Menu && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
            state = GameState.Play;
        }

        if (state == GameState.Play &&  Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("SaumilScene");
            state = GameState.Pause;
        }

        if(state == GameState.Pause && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Game");
            state = GameState.Play;
        }
    }
}
