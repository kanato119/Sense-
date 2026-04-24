using UnityEngine;

public class rotateBlock : MonoBehaviour
{

    [SerializeField] public float maxAngle = 45.0f;
    private Rigidbody _rigidBody;



    [SerializeField] float ResetAngleTime;

    [SerializeField] private float _Time;

    bool ExitPlayer;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        _Time = ResetAngleTime;

        ExitPlayer = false;

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
        
        if(ExitPlayer)
        {
            ResetAngleTime -= Time.deltaTime;

            if (ResetAngleTime <= 0)
            {
                Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    0.3f * Time.deltaTime
                );

                float torque = -z * 2f;

                _rigidBody.AddTorque(Vector3.forward * torque);

                //_rigidBody.angularVelocity = Vector3.forward * torque;

                if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f)) < 1.0f)
                {
                    _rigidBody.angularVelocity = Vector3.zero;
                    ExitPlayer = false;
                    ResetAngleTime = _Time;
                }
            }
        }

    }




    private void OnCollisionExit(Collision collision)
    {

        //if(collision.gameObject.CompareTag("Player")) { 
        ExitPlayer = true;

        //}

    }
    private void OnCollisionStay(Collision collision)
    {
        ExitPlayer = false;
        ResetAngleTime = _Time;

    }


}
