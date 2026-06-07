using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Escenas")]
    public string gameSceneName = "Game"; 

    public void Jugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void Salir()
    {
        Application.Quit(); 
    }
}