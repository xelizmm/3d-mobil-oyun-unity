using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public int minScore, maxScore;
    private GameManager gameManager;
    public ParticleSystem collectEffect;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Rotate(180f * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddScore(Random.Range(minScore, maxScore));
            collectEffect.Play();
            Destroy(this.gameObject, 0.5f);
        }
        
    }
    
}
