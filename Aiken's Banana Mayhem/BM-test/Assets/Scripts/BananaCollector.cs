using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaCollector : MonoBehaviour
{
    public static BananaCollector Instance;
    public int score = 0;
    public Text scoreText;

    [Header("Audio")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip bananaCollectSfx;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Collect()
    {
        score++;
        Debug.Log("Score Updated: " + score);

        if (scoreText != null)
        {
            scoreText.text = "Bananas: " + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is NULL!");
        }

        if (bananaCollectSfx != null)
        {
            if (sfxSource != null)
                sfxSource.PlayOneShot(bananaCollectSfx);
            else
                AudioSource.PlayClipAtPoint(bananaCollectSfx, Camera.main.transform.position);
        }
    }
}