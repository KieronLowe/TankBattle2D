using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text PlayerScoreUI;
    public Text EnemyPointsUI;
    public static int PlayerPoints = 0;
    public static int EnemyPoints = 0;

    void Update()
    {
        PlayerScoreUI.text = PlayerPoints.ToString();
        EnemyPointsUI.text = EnemyPoints.ToString();
    }

    public static void ResetScores()
    {
        PlayerPoints = 0;
        EnemyPoints = 0;
    }
}
