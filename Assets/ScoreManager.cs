using UnityEngine;
using TMPro; // TextMeshProを使うために必要

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UIテキストを紐づけるための変数
    public float survivalTime; // 生存時間

    void Start()
    {
        survivalTime = 0f;
    }

    void Update()
    {
        // Time.timeScaleが0（ゲーム停止中）でなければ時間を加算
        if (Time.timeScale > 0)
        {
            survivalTime += Time.deltaTime;
            
            // UIテキストを更新（小数点以下1桁まで表示）
            scoreText.text = "Time: " + survivalTime.ToString("F1");
        }
    }
}