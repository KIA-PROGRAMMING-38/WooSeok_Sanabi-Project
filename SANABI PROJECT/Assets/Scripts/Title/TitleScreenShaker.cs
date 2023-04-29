using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenShaker : MonoBehaviour
{
    private float mouseX;
    [SerializeField][Range(0.05f, 0.2f)] private float shakeTime = 0.1f;
    [SerializeField][Range(0.01f, 0.5f)] private float shakeIntensity = 0.1f;
    private float saveTime; // �ð��� �����ϱ� ���� ����

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
        // ��鸮�� ������ ���� ��ġ(��鸲 ���� �� ���ƿ��� ����)
        Vector3 startPosition = transform.position;
        saveTime = shakeTime;
        while (0f < saveTime)
        {
            // �ʱ� ��ġ�κ��� �� ���� * Intensity �� ���� �ȿ��� ��ġ ����
            Camera.main.transform.position = startPosition + Random.insideUnitSphere * shakeIntensity * PlayerPrefs.GetFloat("shakeIntensity");
            saveTime -= Time.deltaTime;
            yield return null; // �� ������ ������
        }

        transform.position = startPosition; // �� �������� ���ڸ��� ���ƿ�(������ ������ ����°� ������Ű�� ����)
    }
}
