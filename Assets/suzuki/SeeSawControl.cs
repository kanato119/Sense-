using UnityEngine;

public class rotateBlock : MonoBehaviour
{

    [SerializeField] public float maxAngle = 45.0f;
    private Rigidbody _rigidBody;

    [SerializeField] bool SeeSowX;
    [SerializeField] bool SeeSowY;
    [SerializeField] bool SeeSowZ;

    [SerializeField] float ResetAngleTime;

    private float _Time;

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
        float x = transform.localEulerAngles.x;
        float y = transform.localEulerAngles.y;
        float z = transform.localEulerAngles.z;



        if (SeeSowX)
        {

            // 角度を正規化 -180.0～180.0の範囲内に収まるように
            if (x > 180f) x -= 360f;

            // 現在の角度をでグリー角度で取得
            Vector3 current = transform.localEulerAngles;

            // 角度を比較
            if (x > maxAngle)
            {

                // 角度を固定
                _rigidBody.MoveRotation(Quaternion.Euler(maxAngle, current.y, current.z));

                // RigidBodyコンポーネントの回転速度を0に固定
                _rigidBody.angularVelocity = Vector3.zero;
            }
            else if (x < -maxAngle)
            {

                // 角度を固定
                _rigidBody.MoveRotation(Quaternion.Euler(-maxAngle, current.y, current.z));

                // RigidBodyコンポーネントの回転速度を0に固定
                _rigidBody.angularVelocity = Vector3.zero;
            }
        }

        if (SeeSowY)
        {

            // 角度を正規化 -180.0～180.0の範囲内に収まるように
            if (y > 180f) y -= 360f;

            // 現在の角度をでグリー角度で取得
            Vector3 current = transform.localEulerAngles;

            // 角度を比較
            if (y > maxAngle)
            {

                // 角度を固定
                _rigidBody.MoveRotation(Quaternion.Euler(current.x, maxAngle, current.z));

                // RigidBodyコンポーネントの回転速度を0に固定
                _rigidBody.angularVelocity = Vector3.zero;
            }
            else if (y < -maxAngle)
            {

                // 角度を固定
                _rigidBody.MoveRotation(Quaternion.Euler(current.x, -maxAngle, current.z));

                // RigidBodyコンポーネントの回転速度を0に固定
                _rigidBody.angularVelocity = Vector3.zero;
            }
        }

        if (SeeSowZ)
        {

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


        if (ExitPlayer)
        {



            ResetAngleTime -= Time.deltaTime;

            if (ResetAngleTime <= 0)
            {
                _Time = ResetAngleTime;

                ExitPlayer = false;
            }
            else
            {

             
               

            }

        }


    }

    private void OnCollisionExit(Collision collision)
    {

        //if(collision.gameObject.CompareTag("Player")) { 
        ExitPlayer = true;

        //}

    }
   

}
