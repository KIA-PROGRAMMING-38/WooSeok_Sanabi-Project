using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private ShakeCamera camShake;
    private Vector3 offSet;

    Volume volume;
    Bloom bloom;
    Color initialColor;
    Color damagedColor = Color.red;

    [SerializeField][Range(0f, 0.4f)] private float ColorChangeTime = 0.07f;
    private bool isColorChange;
    private WaitForSeconds colorChangeTime;

    private void Awake()
    {
        camShake = GetComponent<ShakeCamera>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
    }
    private void Start()
    {
        offSet = transform.position - playerTransform.position;
        initialColor = bloom.tint.value;
        colorChangeTime = new WaitForSeconds(ColorChangeTime);
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
        transform.position = playerTransform.position + offSet + camShake.shakeMovePosition; // 카메라가 흔들리는 만큼 추가로 이동해줌
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
    

}
