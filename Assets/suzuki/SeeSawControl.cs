using UnityEngine;

public class rotateBlock : MonoBehaviour
{

    [SerializeField] public float maxAngle = 45.0f;
    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        // Z軸の角度を取得
        float z = transform.localEulerAngles.z;

        // 角度を正規化 -180.0～180.0の範囲内に収まるように
        if (z > 180f) z -= 360f;

        // 現在の角度をでグリー角度で取得
        Vector3 current = transform.localEulerAngles;

        // 角度を比較
        if (z > maxAngle)
        {

            // 角度を固定
            _rigidBody.MoveRotation(Quaternion.Euler(current.x, current.y, maxAngle));

            // RigidBodyコンポーネントの回転速度を0に固定
            _rigidBody.angularVelocity = Vector3.zero;
        }
        else if (z < -maxAngle)
        {

            // 角度を固定
            _rigidBody.MoveRotation(Quaternion.Euler(current.x, current.y, -maxAngle));

            // RigidBodyコンポーネントの回転速度を0に固定
            _rigidBody.angularVelocity = Vector3.zero;
        }
    }
}
