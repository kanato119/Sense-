using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [Header("左右の扉")]
    public Transform leftDoor;   // 左扉
    public Transform rightDoor;  // 右扉

    [Header("左右の扉の当たり判定")]
    public MeshCollider leftDoorMeshCollider;   // 左扉のMeshCollider
    public MeshCollider rightDoorMeshCollider;  // 右扉のMeshCollider

    [Header("開閉設定")]
    public float openAngle = 90f;     // 開く角度
    public float openSpeed = 3f;      // 開く速さ
    public KeyCode openKey = KeyCode.E;

    [Header("鍵設定")]
    public bool needKey = true;       // 鍵が必要か
    public bool consumeKey = false;   // 開けた時に鍵を消費するか
    public bool autoOpen = false;     // 範囲に入ったら自動で開くか

    [Header("プレイヤータグ")]
    public string playerTag = "Player";

    [SerializeField] GameObject Key;

    private bool isPlayerInside = false;
    private bool isOpen = false;
    private bool hasOpenedOnce = false;

    private KeyHold currentKeyHolder;

    private Quaternion leftCloseRotation;
    private Quaternion rightCloseRotation;
    private Quaternion leftOpenRotation;
    private Quaternion rightOpenRotation;

    private void Start()
    {
        if (leftDoor == null)
        {
            Debug.LogError("leftDoor が未設定です");
            enabled = false;
            return;
        }

        if (rightDoor == null)
        {
            Debug.LogError("rightDoor が未設定です");
            enabled = false;
            return;
        }

        leftCloseRotation = leftDoor.localRotation;
        rightCloseRotation = rightDoor.localRotation;

        leftOpenRotation = leftCloseRotation * Quaternion.Euler(0f, -openAngle, 0f);
        rightOpenRotation = rightCloseRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            if (autoOpen)
            {
                TryOpenDoor();
            }
            else
            {
                if (Input.GetKeyDown(openKey))
                {
                    TryOpenDoor();
                }
            }
        }

        if (isOpen)
        {
            leftDoor.localRotation = Quaternion.Slerp(
                leftDoor.localRotation,
                leftOpenRotation,
                Time.deltaTime * openSpeed
            );

            rightDoor.localRotation = Quaternion.Slerp(
                rightDoor.localRotation,
                rightOpenRotation,
                Time.deltaTime * openSpeed
            );
        }
        else
        {
            leftDoor.localRotation = Quaternion.Slerp(
                leftDoor.localRotation,
                leftCloseRotation,
                Time.deltaTime * openSpeed
            );

            rightDoor.localRotation = Quaternion.Slerp(
                rightDoor.localRotation,
                rightCloseRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    private void TryOpenDoor()
    {
        if (hasOpenedOnce)
        {
            return;
        }

        if (needKey)
        {
            if (currentKeyHolder == null)
            {
                return;
            }

            if (!currentKeyHolder.hasKey)
            {
                return;
            }
        }

        if (Key != null)
        {
            Key.SetActive(false);
        }

        isOpen = true;
        hasOpenedOnce = true;

        // 開いた瞬間に左右の扉のMeshColliderを消す
        if (leftDoorMeshCollider != null)
        {
            leftDoorMeshCollider.enabled = false;
        }

        if (rightDoorMeshCollider != null)
        {
            rightDoorMeshCollider.enabled = false;
        }

        if (consumeKey && currentKeyHolder != null)
        {
            currentKeyHolder.hasKey = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = true;
            currentKeyHolder = other.GetComponent<KeyHold>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = false;
            currentKeyHolder = null;
        }
    }
}