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
    Vector2 referenceVelocity = Vector2.zero;

    //private bool isPlayerDead;
    private bool isPlayerHit;

    [Header("Camera Zoom-In")]
    private float initialZoomAmount;
    [SerializeField] private float zoomInAmount = 3f;
    [SerializeField] private float zoomInTime = 1f;
    [SerializeField] [Range(0.01f, 0.05f)] private float zoomInControl = 0.04f;

    private void Awake()
    {
        camShake = GetComponent<ShakeCamera>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        initialZoomAmount = Camera.main.orthographicSize;
    }
    private void Start()
    {
        playerHealth.OnDead -= WatchPlayerDie;
        playerHealth.OnDead += WatchPlayerDie;
        offSet = transform.position - playerTransform.position;
        initialColor = bloom.tint.value;
        colorChangeTime = new WaitForSeconds(ColorChangeTime);
        //isPlayerDead = playerHealth.CheckIfDead();
    }

    private void Update()
    {

        if (isColorChange)
        {
            TryChangeColor();
        }

    }


    private void LateUpdate()
    {

        FollowPlayer();
        //transform.position = playerTransform.position + offSet + camShake.shakeMovePosition; // 카메라가 흔들리는 만큼 추가로 이동해줌

        

    }


    private void FollowPlayer()
    {
        transform.position = playerTransform.position + offSet + camShake.shakeMovePosition; // 카메라가 흔들리는 만큼 추가로 이동해줌
    }

    public void StartTemporaryZoomInPlayer()
    {
        StartCoroutine(TemporaryZoomInPlayer());
        
    }

    private IEnumerator TemporaryZoomInPlayer()
    {
        while (true)
        {
            Camera.main.orthographicSize -= zoomInControl;
            yield return null;
            if (Camera.main.orthographicSize <= zoomInAmount)
            {
                break;
            }
        }

        yield return new WaitForSeconds(zoomInTime);
        
        while (true)
        {
            Camera.main.orthographicSize += zoomInControl;
            yield return null;
            if (initialZoomAmount <= Camera.main.orthographicSize)
            {
                break;
            }
        }
        Camera.main.orthographicSize = initialZoomAmount;
    }

    public void StartEternalZoomInPlayer()
    {
        StartCoroutine(EternalZoomInPlayer());
    }

    public void StopEternalZoomOutPlayer()
    {
        StartCoroutine(EternalZoomOutPlayer());
    }
    private IEnumerator EternalZoomInPlayer()
    {
        while (true)
        {
            Camera.main.orthographicSize -= zoomInControl;
            yield return null;
            if (Camera.main.orthographicSize <= zoomInAmount)
            {
                break;
            }
        }
    }

    private IEnumerator EternalZoomOutPlayer()
    {
        while (true)
        {
            Camera.main.orthographicSize += zoomInControl;
            yield return null;
            if (initialZoomAmount <= Camera.main.orthographicSize)
            {
                break;
            }
        }
        Camera.main.orthographicSize = initialZoomAmount;
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
        Camera.main.orthographicSize = zoomInAmount;
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
