using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject hitFX;
    [SerializeField] GameObject explosionFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;

    GameObject parent;
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1) { KillEnemy(); }
        
    }

    void KillEnemy()
    {

        scoreBoard.AddPoints(scorePerHit);
        GameObject vfx = Instantiate(explosionFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        GameObject tempVFX = Instantiate(hitFX, transform.position, Quaternion.identity);
        tempVFX.transform.parent = parent.transform;
        hitPoints--;
    }
}
