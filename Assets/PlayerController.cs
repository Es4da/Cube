using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移のために必要

public class PlayerController : MonoBehaviour
{
    // public（インスペクターで調整可能）な移動速度
    public float moveSpeed = 5f;

    // ジャンプの力
    public float jumpForce = 5f;

    // Rigidbodyコンポーネントを保持するための変数
    private Rigidbody rb;

    // ScoreManagerへの参照を追加
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        // 最初に一度だけ、自分にアタッチされているRigidbodyコンポーネントを取得しておく
        rb = GetComponent<Rigidbody>();

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプの入力受付（GetKeyDownはUpdateで検知するのが最も確実）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Rigidbodyコンポーネントに、上向きの力を加える
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        // 左右のキー入力を取得 (-1.0f から 1.0f の間の値)
        float moveHorizontal = Input.GetAxis("Horizontal"); // 右矢印で1, 左矢印で-1

        // Rigidbodyの速度を直接変更して移動させる
        // Y軸の速度(rb.velocity.y)はそのままにしないと、ジャンプや重力に影響が出る
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, 0);
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手のオブジェクトに "Obstacle" というタグが付いているかチェック
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 被弾したら、一連のゲームオーバー演出を開始する
            StartCoroutine(GameOverSequence());
        }
    }
    
    private IEnumerator GameOverSequence()
    {
        // --- 1. スコアを保存 ---
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            float finalScore = scoreManager.survivalTime;
            PlayerPrefs.SetFloat("LastScore", finalScore);
            float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
            if (finalScore > highScore)
            {
                PlayerPrefs.SetFloat("HighScore", finalScore);
            }
            PlayerPrefs.Save();
        }

        // --- 2. 画面を揺らす ---
        // Main CameraにアタッチされたCameraShakeスクリプトを取得
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake != null)
        {
            // Shakeメソッドを呼び出し、0.3秒間、強さ0.2で揺らす
            StartCoroutine(cameraShake.Shake(0.3f, 0.2f));
        }
        
        // 自分の物理的な動きや入力を止める
        rb.isKinematic = true; // 物理演算を停止
        GetComponent<PlayerController>().enabled = false; // このスクリプトを無効化

        // --- 3. 0.5秒待つ ---
        // Time.timeScaleの影響を受けない待機命令
        yield return new WaitForSecondsRealtime(0.5f);

        // --- 4. 時間の流れを止めて、シーンを遷移する ---
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver");
    }
}