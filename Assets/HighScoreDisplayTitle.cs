// HighScoreDisplay.cs
using UnityEngine;
using TMPro;

public class HighScoreDisplayTitle : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        // "HighScore"というキーで保存されたスコアを読み込む
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);

        // UIテキストに表示する
        highScoreText.text = "ハイスコア: " + highScore.ToString("F1") + " 秒";
    }
}