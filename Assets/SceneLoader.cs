using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ↓↓↓ このメソッドを追記 ↓↓↓
    public void QuitGame()
    {
        // Unityエディタで実行している場合
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // ビルドしたゲームで実行している場合
#else
        Application.Quit();
#endif
    }
}