using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : WholeSceneManager
{
    [SerializeField] [Range (0.05f, 0.2f)]private float shakeTime = 0.1f;
    public float shakeIntensity { private get; set; }

    private float mouseX; // ���콺 �Է��� �ޱ� ���� ����(Y������ 1�� �׸��� ���� �� ����)
    private float saveTime; // �ð��� �����ϱ� ���� ����
    private readonly string SHAKECAMERAPOSITION = "ShakeCameraPosition"; // ��Ÿ ������

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
