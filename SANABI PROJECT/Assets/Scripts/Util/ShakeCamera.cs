using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : WholeSceneManager
{
    [SerializeField] [Range (0.05f, 0.2f)]private float shakeTime = 0.1f;
    public float shakeIntensity { private get; set; }

    private float mouseX; // 마우스 입력을 받기 위한 변수(Y축으로 1자 그릴수 없을 것 같음)
    private float saveTime; // 시간을 저장하기 위한 변수
    private readonly string SHAKECAMERAPOSITION = "ShakeCameraPosition"; // 오타 방지용

    private void Awake()
    {
        shakeIntensity = WholeSceneManager.shakeIntensity;
    }
    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        if (mouseX != 0f)
        {
            OnShakeCamera(shakeTime, shakeIntensity);
        }
    }

    public void OnShakeCamera(float shakeTime, float shakeIntensity)
    {
        StartCoroutine(SHAKECAMERAPOSITION);
    }

    private IEnumerator ShakeCameraPosition()
    {
        // 흔들리기 직전의 시작 위치(흔들림 종료 후 돌아오기 위함)
        Vector3 startPosition = transform.position;
        saveTime = shakeTime;
        while (0f < saveTime)
        {
            // 초기 위치로부터 구 범위 * Intensity 의 범위 안에서 위치 변동
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            saveTime -= Time.deltaTime;
            yield return null; // 매 프레임 흔들어줌
        }

        transform.position = startPosition; // 다 흔들었으면 제자리로 돌아옴(프레임 밖으로 벗어나는걸 방지시키기 위함)
    }
}
