using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Unityエディタから障害物のPrefabをセットするための変数
    public GameObject obstaclePrefab;

    void Start()
    {
        // SpawnObstaclesという処理を呼び出す
        StartCoroutine(SpawnObstacles());
    }

    // 障害物を生成し続けるコルーチン
    IEnumerator SpawnObstacles()
    {
        // このループはゲーム中ずっと続く
        while (true)
        {
            // 1秒待つ（この数値を小さくすると難易度が上がる）
            yield return new WaitForSeconds(0.075f);

            // 障害物を生成する位置を計算
            // X座標は-10から10のランダムな値、Y座標は10（画面の上の方）
            Vector3 spawnPos = new Vector3(Random.Range(-10f, 10f), 10f, 0);

            // 障害物のPrefabを、計算した位置に生成する
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}