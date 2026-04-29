using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI bestTimeText;

    [Header("Tiempo")]
    public float timeLimit = 30f;

    private float currentTime;
    private bool gameActive = true;
    private float startTime;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = timeLimit;
        startTime = timeLimit;
        messageText.text = "";
        Time.timeScale = 1f;
        UpdateBestTimeUI();
    }

    void Update()
    {
        if (!gameActive)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        int seconds = Mathf.CeilToInt(currentTime);
        timerText.text = seconds + "s";

        if (currentTime <= 10f)
            timerText.color = Color.red;

        if (currentTime <= 0f)
            Lose("¡Se acabo el tiempo!");
    }

    public void Win()
    {
        gameActive = false;
        float timeUsed = startTime - currentTime;
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (timeUsed < bestTime)
        {
            bestTime = timeUsed;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            messageText.text = "¡GANASTE! Record: " + bestTime.ToString("F1") + "s\n[R] Reiniciar";
        }
        else
        {
            messageText.text = "¡GANASTE! Tiempo: " + timeUsed.ToString("F1") + "s\n[R] Reiniciar";
        }

        messageText.color = Color.yellow;
        UpdateBestTimeUI();
        Time.timeScale = 0f;
    }

    public void Lose(string reason)
    {
        gameActive = false;
        messageText.text = reason + "\n[R] Reiniciar";
        messageText.color = Color.red;
        Time.timeScale = 0f;
    }

    void UpdateBestTimeUI()
    {
        if (bestTimeText == null) return;
        float best = PlayerPrefs.GetFloat("BestTime", -1f);
        bestTimeText.text = best < 0 ? "Mejor: --" : "Mejor: " + best.ToString("F1") + "s";
    }

    public bool IsGameActive() => gameActive;
}