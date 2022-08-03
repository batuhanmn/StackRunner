using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int score;

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        if (this.score + score < 0) this.score = 0;
        else this.score += score;
    }

    public void ResetScore()
    {
        score = 0;
    }

}
