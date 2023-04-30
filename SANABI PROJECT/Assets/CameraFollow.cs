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
    //private bool isColorChange;
    private WaitForSeconds colorChangeTime;
    Vector2 referenceVelocity = Vector2.zero;

    private Transform bossFilmSpot;
    //private bool isPlayerDead;
    private bool inAnimation;
    private Vector3 refVel = Vector3.zero;
    private IEnumerator _FilmBossAppear;
    private IEnumerator _ShowDamagedColor;
    private IEnumerator _TemporaryZoomInPlayer;
    private IEnumerator _EternalZoomInPlayer;
    private IEnumerator _EternalZoomOutPlayer;
    private IEnumerator _PlayerDeadZoomIn;

    [Header("Camera Zoom-In")]
    private float initialZoomAmount;
    [SerializeField] private float zoomInAmount = 3f;
    [SerializeField] private float zoomInTime = 1f;
    [SerializeField][Range(0.01f, 0.05f)] private float zoomInControl = 0.04f;

    private void Awake()
    {
        camShake = GetComponent<ShakeCamera>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        initialZoomAmount = Camera.main.orthographicSize;
        
    }

    

    private void Start()
    {
        _FilmBossAppear = FilmBossAppear();
        _ShowDamagedColor = ShowDamagedColor();
        _TemporaryZoomInPlayer = TemporaryZoomInPlayer();
        _EternalZoomInPlayer = EternalZoomInPlayer();
        _EternalZoomOutPlayer = EternalZoomOutPlayer();
        _PlayerDeadZoomIn = PlayerDeadZoomIn();

        playerHealth.OnDead -= WatchPlayerDie;
        playerHealth.OnDead += WatchPlayerDie;
        GameManager.Instance.bossEnterance.OnBossEnterance -= ChangeBloomState;
        GameManager.Instance.bossEnterance.OnBossEnterance += ChangeBloomState;
        offSet = transform.position - playerTransform.position;
        initialColor = bloom.tint.value;
        colorChangeTime = new WaitForSeconds(ColorChangeTime);
        //isPlayerDead = playerHealth.CheckIfDead();
    }

    private void Update()
    {
        //if (isColorChange)
        //{
        //    TryChangeColor();
        //}

    }


    private void LateUpdate()
    {
        if (!inAnimation)
        {
            FollowPlayer();
        }
        //FollowPlayer();
    }



    public void StartFilmBossAppear(Transform spot)
    {
        inAnimation = true;
        bossFilmSpot = spot;
        StartCoroutine(_FilmBossAppear);
    }

    private void ChangeBloomState()
    {
        volume.weight = 0.12f;
        bloom.intensity.value = 6f;
        bloom.scatter.value = 1f;
    }

    private IEnumerator FilmBossAppear()
    {
        float elapsedTime = 0f;
        Vector3 initialCamPos = transform.position;
        GameManager.Instance.playerController.ChangeToPausedState();
        while (true)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, bossFilmSpot.position, ref refVel, 1f);
            yield return null;
            if (Vector3.Distance(transform.position, bossFilmSpot.position) <= 0.01f)
            {
                break;
            }
        }

        transform.position = bossFilmSpot.position;
        yield return new WaitForSeconds(GameManager.Instance.bossData.idleWaitTime - elapsedTime);
        //GameManager.Instance.bossCanvasController.TurnOffAppearText();

        while (true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, initialCamPos, ref refVel, 0.5f);
            yield return null;
            if (Vector3.Distance(transform.position, initialCamPos) <= 0.01f)
            {
                break;
            }
        }

        transform.position = initialCamPos;
        inAnimation = false;
        GameManager.Instance.playerController.ChangeToIdleState();
        //GameManager.Instance.audioManager.Play("BossBGM");
        GameManager.Instance.audioManager.GradualVolumePlay("BossBGM");
    }



    private void FollowPlayer()
    {
        transform.position = playerTransform.position + offSet + camShake.shakeMovePosition; // 카메라가 흔들리는 만큼 추가로 이동해줌
    }


    public void StartTemporaryZoomInPlayer()
    {

        _TemporaryZoomInPlayer = TemporaryZoomInPlayer();

        StartCoroutine(_TemporaryZoomInPlayer);
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

        _EternalZoomInPlayer = EternalZoomInPlayer();

        //StartCoroutine(EternalZoomInPlayer());
        StartCoroutine(_EternalZoomInPlayer);
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



    public void StopEternalZoomOutPlayer()
    {

        _EternalZoomOutPlayer = EternalZoomOutPlayer();

        //StartCoroutine(EternalZoomOutPlayer());
        StartCoroutine(_EternalZoomOutPlayer);
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



    public void StartChangeColor()
    {

        _ShowDamagedColor = ShowDamagedColor();

        //StartCoroutine(ShowDamagedColor());
        StartCoroutine(_ShowDamagedColor);
    }

    private IEnumerator ShowDamagedColor()
    {
        bloom.tint.value = damagedColor;
        yield return colorChangeTime;
        bloom.tint.value = initialColor;
        //isColorChange = false;
    }

    //public void ChangeColor()
    //{
    //    isColorChange = true;
    //}

    private void WatchPlayerDie()
    {
        //Camera.main.orthographicSize = zoomInAmount;
        //StartCoroutine(PlayerDeadZoomIn());
        StartCoroutine(_PlayerDeadZoomIn);
        //StartCoroutine(BlinkEverything());
        TurnOffEverythingExceptPlayerAndCamera();
        Camera.main.backgroundColor = Color.black;
    }

    private void TurnOffEverythingExceptPlayerAndCamera()
    {
        foreach (GameObject obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.CompareTag("WholePlayer") || obj.CompareTag("MainCamera"))
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }


    //private void TurnOnEverything()
    //{
    //    foreach (GameObject obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
    //    {
    //        if (obj.layer == LayerMask.NameToLayer("UI"))
    //        {
    //            continue;
    //        }
    //        obj.SetActive(true);
    //    }
    //}

    //private IEnumerator BlinkEverything()
    //{
    //    int count = 0;
    //    while (true)
    //    {
    //        TurnOffEverythingExceptPlayerAndCamera();
    //        ++count;
    //        yield return new WaitForSeconds(0.05f);
    //        TurnOnEverything();
    //        yield return new WaitForSeconds(0.05f);
    //        if (10 <= count)
    //        {
    //            break;
    //        }
    //    }
    //    TurnOffEverythingExceptPlayerAndCamera();
    //}



    private IEnumerator PlayerDeadZoomIn()
    {
        while (true)
        {
            Camera.main.orthographicSize -= 0.05f;
            yield return null;
            if (Camera.main.orthographicSize <= zoomInAmount - 1f)
            {
                break;
            }
        }
        
    }
}
