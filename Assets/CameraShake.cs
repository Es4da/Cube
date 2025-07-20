using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 揺らす処理を外部から呼び出すためのメソッド
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition; // 元の位置を保存
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // XとY方向に、揺れの強さ(magnitude)の範囲でランダムな値を加える
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null; // 1フレーム待つ
        }

        transform.localPosition = originalPos; // 揺れが終わったら元の位置に戻す
    }
}