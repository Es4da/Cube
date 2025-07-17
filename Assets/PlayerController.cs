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
            Debug.Log("障害物に当たった！ゲームオーバー！");

            // ゲーム内の時間を止める
            Time.timeScale = 0f;

            // ゲームオーバー時にスコアを保存する (ここから追加)
            if (scoreManager != null)
            {
                float finalScore = scoreManager.survivalTime;
                // "LastScore"というキー名で、今回のスコア(finalScore)を保存する
                PlayerPrefs.SetFloat("LastScore", finalScore);
                // 今までのハイスコアを "HighScore" というキーで読み込む（なければ0）
            float highScore = PlayerPrefs.GetFloat("HighScore", 0f);

            // もし今回のスコアがハイスコアより高ければ、ハイスコアを更新する
            if (finalScore > highScore)
            {
                PlayerPrefs.SetFloat("HighScore", finalScore);
                Debug.Log("ハイスコア更新！"); // 確認用のログ
            }
            
            // --- ↑↑↑ ここまでハイスコア処理を追加 ↑↑↑ ---

                PlayerPrefs.Save(); // すぐに保存を確定させる
            }

            // GameOverシーンを読み込む
            SceneManager.LoadScene("GameOver");
        }
    }
}