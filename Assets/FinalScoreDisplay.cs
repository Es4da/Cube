using UnityEngine;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        // "LastScore"というキー名で保存されたスコアを読み込む
        float lastScore = PlayerPrefs.GetFloat("LastScore", 0f);

        // UIテキストに表示する（小数点以下1桁まで）
        finalScoreText.text = "あなたの記録: " + lastScore.ToString("F1") + " 秒";
    }
}