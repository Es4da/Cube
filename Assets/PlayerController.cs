using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.1f, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手のオブジェクトに "Obstacle" というタグが付いているかチェック
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // もし付いていれば、コンソールにメッセージを表示
            Debug.Log("障害物に当たった！ゲームオーバー！");

            // ここで自分のオブジェクトを破壊して、ゲームオーバーを表現
            Destroy(gameObject);
        }
    }
}
