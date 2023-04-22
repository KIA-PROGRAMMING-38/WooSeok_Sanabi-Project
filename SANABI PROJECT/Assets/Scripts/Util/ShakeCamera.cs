using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [Header("WireShoot State")]
    [Range(0.05f, 1f)] public float shootShakeTime = 0.1f;
    [Range(0.1f, 1f)] public float shootShakeIntensity = 0.1f;

    [Header("Damaged State")]
    [Range(0.1f, 1f)] public float damagedShakeTime = 0.5f;
    [Range(0.1f, 1f)] public float damagedShakeIntensity = 0.45f;


    private float shakeTime;
    private float shakeIntensity;

    //private bool ShakeOn;
    private float saveTime; // 시간을 저장하기 위한 변수
    //private readonly string SHAKECAMERAPOSITION = "ShakeCameraPosition"; // 오타 방지용
    public Vector3 shakeMovePosition;
    private IEnumerator _ShakeCameraPosition;

    private void Start()
    {
        _ShakeCameraPosition = ShakeCameraPosition();
    }
    private void Update()
    {
        //if (ShakeOn)
        //{
        //    OnShakeCamera();
        //}
    }

    public void OnShakeCamera()
    {
        //SwitchBackOff();
        _ShakeCameraPosition = ShakeCameraPosition();
        StartCoroutine(_ShakeCameraPosition);
    }

    public void TurnOnShake(float shakeTime, float shakeIntensity)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity * GameManager.Instance.ScreenShakeIntensity;
        //ShakeOn = true;
        OnShakeCamera();
    }

    //private void SwitchBackOff()
    //{
    //    //ShakeOn = false;
    //}
    private IEnumerator ShakeCameraPosition()
    {
        // 흔들리기 직전의 시작 위치(흔들림 종료 후 돌아오기 위함)
        Vector3 startPosition = transform.position;
        saveTime = shakeTime;
        while (0f < saveTime)
        {
            // 초기 위치로부터 구 범위 * Intensity 의 범위 안에서 위치 변동
            //transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
            
            shakeMovePosition = Random.insideUnitSphere * shakeIntensity;

            saveTime -= Time.deltaTime;
            yield return null; // 매 프레임 흔들어줌
        }

        shakeMovePosition = Vector3.zero; // shake가 긑나면 shakemoveposition을 0으로 초기화

        transform.position = startPosition; // 다 흔들었으면 제자리로 돌아옴(프레임 밖으로 벗어나는걸 방지시키기 위함)
    }
}
