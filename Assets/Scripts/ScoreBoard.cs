using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreTxt;

    void Start()
    {
        scoreTxt = GetComponent<TMP_Text>();
        scoreTxt.text = "Score: 0";
    }

    public void AddPoints(int earnedPoints)
    {
        score += earnedPoints;
        scoreTxt.text = "Score: " + score.ToString();
    }
}
