using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Si usas Text clásico, cambia a: using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Acceso global desde cualquier script

    [Header("Referencias UI")]
    public TMP_Text coinsText;     // Si usas Text clásico, cambia el tipo a Text
    public TMP_Text scoreText;
    public GameObject defeatMenu;  // Panel del menú de derrota

    [Header("Referencias")]
    public Transform player;

    private int coins = 0;
    private int score = 0;
    private bool isGameOver = false;
    private float startZ;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (player != null) startZ = player.position.z;
        if (defeatMenu != null) defeatMenu.SetActive(false);
        Time.timeScale = 1f; // Por si venimos de un reinicio
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver || player == null) return;

        // Puntaje según la distancia recorrida en Z
        score = Mathf.Max(score, Mathf.FloorToInt(player.position.z - startZ));
        UpdateUI();
    }

    public void AddCoin()
    {
        coins++;
        UpdateUI();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        if (defeatMenu != null) defeatMenu.SetActive(true);
        Time.timeScale = 0f; // Congela el juego
        // Aquí luego conectamos la música de derrota
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateUI()
    {
        if (coinsText != null) coinsText.text = "MONEDAS: " + coins.ToString("00");
        if (scoreText != null) scoreText.text = "PUNTUACIÓN: " + score.ToString("00000000");
    }
}