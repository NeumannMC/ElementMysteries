using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  [SerializeField] private GameObject PlayButton;
  [SerializeField] private GameObject QuittButton;

   public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            quitGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayGame();
        }
    }
}
