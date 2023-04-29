using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenShaker : MonoBehaviour
{
    private float mouseX;
    [SerializeField][Range(0.05f, 0.2f)] private float shakeTime = 0.1f;
    [SerializeField][Range(0.01f, 0.5f)] private float shakeIntensity = 0.1f;
    private float saveTime; // 시간을 저장하기 위한 변수

    private IEnumerator _ShakeCameraPosition;
    private void Start()
    {
        //_ShakeCameraPosition = ShakeCameraPosition();
    }


    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        if (mouseX != 0)
        {
            OnShakeCamera(shakeTime, shakeIntensity);
        }
    }

    public void OnShakeCamera(float shakeTime, float shakeIntensity)
    {
        //if (_ShakeCameraPosition == null)
        //{
        //    _ShakeCameraPosition = ShakeCameraPosition();
        //}
        //StartCoroutine(_ShakeCameraPosition);
        //this.shakeIntensity = shakeIntensity * PlayerPrefs.GetFloat("shakeIntensity");
        StartCoroutine(ShakeCameraPosition());
    }

    public void StopShakeCameraCoroutine()
    {
        StopCoroutine(ShakeCameraPosition());
    }

    private IEnumerator ShakeCameraPosition()
    {
        // 흔들리기 직전의 시작 위치(흔들림 종료 후 돌아오기 위함)
        Vector3 startPosition = transform.position;
        saveTime = shakeTime;
        while (0f < saveTime)
        {
            // 초기 위치로부터 구 범위 * Intensity 의 범위 안에서 위치 변동
            Camera.main.transform.position = startPosition + Random.insideUnitSphere * shakeIntensity * PlayerPrefs.GetFloat("shakeIntensity");
            saveTime -= Time.deltaTime;
            yield return null; // 매 프레임 흔들어줌
        }

        transform.position = startPosition; // 다 흔들었으면 제자리로 돌아옴(프레임 밖으로 벗어나는걸 방지시키기 위함)
    }
}
