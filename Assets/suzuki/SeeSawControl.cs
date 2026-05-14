using UnityEngine;

public class rotateBlock : MonoBehaviour
{

    float maxAngle = 25.0f;

    private Rigidbody _rigidBody;

    //初期位置
    private Vector3 initPos;

    //タイマー
    [SerializeField] float ResetAngleTime;
    [SerializeField] private float _Time;

    bool ExitPlayer;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        //設定した時間を入れる
        _Time = ResetAngleTime;

        ExitPlayer = false;
        initPos=transform.position;
    }

    void FixedUpdate()
    {
        //ｚ軸以外を固定する
        Vector3 rotation = gameObject.transform.localEulerAngles;
        rotation.y = 0;
        rotation.x = 0;
      
        _rigidBody.MoveRotation(Quaternion.Euler(rotation.x, rotation.y, rotation.z));

        // Z軸の角度を取得
        float z = transform.localEulerAngles.z;
        _rigidBody.MovePosition(initPos);

        
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
                //傾きをもとに戻す
                Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    0.3f * Time.deltaTime
                );

                ////角速度 += -角度 * バネ強さ
                ////角速度 *= 減衰率
                ////角度 += 角速度

                //傾いていた逆の方向に力を入れる
                float torque = -z * 2f;
                _rigidBody.AddTorque(Vector3.forward * torque);

                //傾いていなかった場合は何もしない
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
        ExitPlayer = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        ExitPlayer = false;

        //のっかっていたら時間は減らさない
        ResetAngleTime = _Time;

    }


}
