// TitleManager.cs
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        // ゲーム開始時に、時間の流れを通常に戻す
        Time.timeScale = 1f;
    }
}