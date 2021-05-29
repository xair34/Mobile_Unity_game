using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreTxt;
    static int score = 0;
    public  void SetScore(int value)
    {
        score += value;
    }
    public static int GetScore()
    {
        return score;
    }
    private void Start()
    {
        scoreTxt.text = "0";
        score = 0;
    }
    private void Update()
    {
        scoreTxt.text = GetScore().ToString();
    }
}
