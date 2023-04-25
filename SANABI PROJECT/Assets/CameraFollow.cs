using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject mainGround;
    private ShakeCamera camShake;
    private Vector3 offSet;

    Volume volume;
    Bloom bloom;
    Color initialColor;
    Color damagedColor = Color.red;

    [SerializeField][Range(0f, 0.4f)] private float ColorChangeTime = 0.07f;
    private bool isColorChange;
    private WaitForSeconds colorChangeTime;
    private bool isPlayerDead;
    Vector2 referenceVelocity = Vector2.zero;   

    private void Awake()
    {
        camShake = GetComponent<ShakeCamera>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
    }
    private void Start()
    {
        playerHealth.OnDead -= WatchPlayerDie;
        playerHealth.OnDead += WatchPlayerDie;
        offSet = transform.position - playerTransform.position;
        initialColor = bloom.tint.value;
        colorChangeTime = new WaitForSeconds(ColorChangeTime);
        isPlayerDead = playerHealth.CheckIfDead();
    }

    private void Update()
    {

        if (isColorChange)
        {
            TryChangeColor();
        }
    }

    Vector2 playerDeathPlatformHitPosition;
    Vector2 fixedPosition;
    private void LateUpdate()
    {
        //if (!isPlayerDead)
        //{
        //    transform.position = playerTransform.position + offSet + camShake.shakeMovePosition; // 카메라가 흔들리는 만큼 추가로 이동해줌
        //}
        if (!isPlayerDead && !isPlayerNearDeathPlatform)
        {
            transform.position = playerTransform.position + offSet + camShake.shakeMovePosition; // 카메라가 흔들리는 만큼 추가로 이동해줌
        }
        else if (isPlayerNearDeathPlatform)
        {
            fixedPosition.Set(playerTransform.position.x, playerDeathPlatformHitPosition.y);
            transform.position = (Vector3)fixedPosition + offSet + camShake.shakeMovePosition;
        }
        
    }

    private bool isPlayerNearDeathPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathPlatform"))
        {
            playerDeathPlatformHitPosition = transform.position;
            isPlayerNearDeathPlatform = true;
        }
        
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("DeathPlatform"))
    //    {
    //        Debug.Log("deathplatform이랑 닿는중");
    //        isPlayerNearDeathPlatform = true;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathPlatform"))
        {
            isPlayerNearDeathPlatform = false;
        }
    }

    private void TryChangeColor()
    {
        StartCoroutine(ShowDamagedColor());
    }

    private IEnumerator ShowDamagedColor()
    {
        bloom.tint.value = damagedColor;
        yield return colorChangeTime;
        bloom.tint.value = initialColor;
        isColorChange = false;
    }

    public void ChangeColor()
    {
        isColorChange = true;
    }
    
    private void WatchPlayerDie()
    {
        SlowCameraMove();
        Camera.main.orthographicSize = 3f;
        if (backGround.gameObject != null)
        {
            backGround.gameObject.SetActive(false);
        }
        if (mainGround.gameObject != null)
        {
            mainGround.gameObject.SetActive(false);
        }
        Camera.main.backgroundColor = Color.black;
    }

    private void SlowCameraMove()
    {
        transform.position = Vector2.SmoothDamp(transform.position, playerTransform.position, ref referenceVelocity, 3f);
    }
}
