using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // この関数は、他のColliderがこのオブジェクトのTriggerに入ってきた時に自動で呼び出される
    private void OnTriggerEnter(Collider other)
    {
        // このゾーンに入ってきたオブジェクト（other）を、シーンから破壊（削除）する
        Destroy(other.gameObject);
    }
}