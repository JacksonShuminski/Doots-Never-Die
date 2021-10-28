using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Play,
    End
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameState state;
    bool test;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        state = GameState.Menu;
        test = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && state == GameState.Menu)
        {
            SceneManager.LoadScene("Game");
            state = GameState.Play;
        }

        if (Input.GetKey(KeyCode.P) && state == GameState.Play)
        {
            SceneManager.LoadScene("SaumilScene");
            state = GameState.Menu;
        }
    }
}
