// FinalScoreDisplay.cs
using UnityEngine;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText; // ハイスコア表示用のUIテキストを追加

    void Start()
    {
        // 今回のスコアを表示 (既存の処理)
        float lastScore = PlayerPrefs.GetFloat("LastScore", 0f);
        finalScoreText.text = "あなたの記録: " + lastScore.ToString("F1") + " 秒";

        // ハイスコアを表示 (ここから追加)
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        highScoreText.text = "ハイスコア: " + highScore.ToString("F1") + " 秒";
    }
}