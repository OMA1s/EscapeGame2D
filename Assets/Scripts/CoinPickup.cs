using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    AudioSource myAudio;
    AudioClip myClip;

    [SerializeField] int coinPoint;
    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myClip = myAudio.clip;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(myClip, Camera.main.transform.position);
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.processScore(coinPoint);
        Destroy(gameObject);
    }
}
