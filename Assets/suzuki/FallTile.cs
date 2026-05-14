using UnityEngine;

public class FallTile : MonoBehaviour
{
    Rigidbody rb;
    bool hoge = false;
    [SerializeField] float timer = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }
    void Update()
    {
            //何かが触れたらタイマーが作動する　
        if (hoge)
        {
            timer -= Time.deltaTime;

            //0秒になったら
            if (timer < 0)
            {
                //RigidBody追加
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        hoge = true;
    }
}
