using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    [ShotPower(1000f)]
    public float maxScore = 500f;
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text scoreText;

    private int totalScore = 0;
    private int consecutiveBalls = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnShotFired()
    {
        consecutiveBalls = 0;
    }

    public void BallPocketed()
    {
        consecutiveBalls++;
        int multiplier = consecutiveBalls * (consecutiveBalls + 1) / 2;
        totalScore += 33 * multiplier;

        Debug.Log($"Puntaje: {totalScore} | Consecutivas: {consecutiveBalls} | Multiplicador: {multiplier}x");

        scoreText?.SetText($"Puntaje: {totalScore}\nMultiplicador: {multiplier}x");
    }

    public void WhiteBallPocketed()
    {
        totalScore -= 100;
        consecutiveBalls = 0;

        Debug.Log("¡Bola blanca en el hoyo!");

        scoreText?.SetText($"Puntaje: {totalScore}\nMultiplicador: 0x");
    }

    public void ResetScore()
    {
        totalScore = 0;
        consecutiveBalls = 0;

        Debug.Log("¡Bola negra en el hoyo! Puntaje reiniciado.");

        scoreText?.SetText($"Puntaje: {totalScore}\nMultiplicador: 0x");
    }
}
