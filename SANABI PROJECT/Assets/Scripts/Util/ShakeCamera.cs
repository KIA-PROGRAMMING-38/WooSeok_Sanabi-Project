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
    private float saveTime; // �ð��� �����ϱ� ���� ����
    //private readonly string SHAKECAMERAPOSITION = "ShakeCameraPosition"; // ��Ÿ ������
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
        // ��鸮�� ������ ���� ��ġ(��鸲 ���� �� ���ƿ��� ����)
        Vector3 startPosition = transform.position;
        saveTime = shakeTime;
        while (0f < saveTime)
        {
            // �ʱ� ��ġ�κ��� �� ���� * Intensity �� ���� �ȿ��� ��ġ ����
            //transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
            
            shakeMovePosition = Random.insideUnitSphere * shakeIntensity;

            saveTime -= Time.deltaTime;
            yield return null; // �� ������ ������
        }

        shakeMovePosition = Vector3.zero; // shake�� �P���� shakemoveposition�� 0���� �ʱ�ȭ

        transform.position = startPosition; // �� �������� ���ڸ��� ���ƿ�(������ ������ ����°� ������Ű�� ����)
    }
}
