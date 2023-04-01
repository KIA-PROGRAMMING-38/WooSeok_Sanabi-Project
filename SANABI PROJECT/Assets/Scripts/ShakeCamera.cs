using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] [Range (0.05f, 0.2f)]private float shakeTime = 0.1f;
    [SerializeField] [Range(0.01f, 0.1f)]private float shakeIntensity = 0.05f;

    private float mouseX; // ���콺 �Է��� �ޱ� ���� ����(Y������ 1�� �׸��� ���� �� ����)
    private float saveTime; // �ð��� �����ϱ� ���� ����
    private readonly string SHAKECAMERAPOSITION = "ShakeCameraPosition"; // ��Ÿ ������

    void Update()
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
        // ��鸮�� ������ ���� ��ġ(��鸲 ���� �� ���ƿ��� ����)
        Vector3 startPosition = transform.position;
        saveTime = shakeTime;
        while (0f < saveTime)
        {
            // �ʱ� ��ġ�κ��� �� ���� * Intensity �� ���� �ȿ��� ��ġ ����
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            saveTime -= Time.deltaTime;
            yield return null; // �� ������ ������
        }

        transform.position = startPosition; // �� �������� ���ڸ��� ���ƿ�(������ ������ ����°� ������Ű�� ����)
    }
}
