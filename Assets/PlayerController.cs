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
    // --- 1. まず時間を止める！ ---
    Time.timeScale = 0f;

    // --- 2. スコアを保存 ---
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

    // --- 3. 画面を揺らす ---
    CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
    if (cameraShake != null)
    {
        // 時間が止まっていても、この揺れは正しく実行される
        StartCoroutine(cameraShake.Shake(0.3f, 0.2f));
    }
    
    // プレイヤーの動きを止める
    rb.isKinematic = true; 
    GetComponent<PlayerController>().enabled = false;

    // --- 4. 0.5秒待つ ---
    yield return new WaitForSecondsRealtime(0.5f);

    // --- 5. ゲームオーバー画面へ ---
    SceneManager.LoadScene("GameOver");
}
}