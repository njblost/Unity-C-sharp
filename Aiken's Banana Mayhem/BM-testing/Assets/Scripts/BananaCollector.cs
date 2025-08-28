using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BananaCollector : MonoBehaviour
{
    public static BananaCollector Instance;

    [Header("Score")]
    public int score = 0;
    public Text scoreText;

    [Header("Audio")]
    [Tooltip("Assign Banana Collect .wav file here")]
    public AudioClip bananaCollectClip;

    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private AudioSource _audio;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        _audio = GetComponent<AudioSource>();
        _audio.playOnAwake = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Banana") || other.GetComponent<Banana>() != null)
        {
            if (bananaCollectClip != null)
                _audio.PlayOneShot(bananaCollectClip, sfxVolume);

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

            // Destroy(other.gameObject);
        }
    }
}