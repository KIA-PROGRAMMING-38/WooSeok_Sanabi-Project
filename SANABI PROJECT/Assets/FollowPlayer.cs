using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    public Material material;
    
    [SerializeField] private float followSpeed = 0.2f;
    private Vector2 velocity = Vector2.zero; // reference speed
    
    [SerializeField] private float glowCoolTime = 0.25f;
    private WaitForSeconds glowCooltime;
    private Color originalColor;

    [SerializeField]private float multiplyFactor = 20f;
    private Color glowOffColor;
    private Color glowOnColor;

    
    private void Start()
    {
        glowCooltime = new WaitForSeconds(glowCoolTime);
        originalColor = material.color;

        glowOffColor = originalColor;
        glowOnColor = new Color(glowOffColor.r * multiplyFactor, glowOffColor.g * multiplyFactor, glowOffColor.b * multiplyFactor);
        Debug.Break();
        StartCoroutine(StartGlowing());
    }

    private void LateUpdate()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPos.position, ref velocity, followSpeed);
    }


    private IEnumerator StartGlowing()
    {
        while (true)
        {
            material.color = glowOffColor;
            yield return glowCooltime;
            material.color = glowOnColor;
            yield return glowCooltime;
        }
    }

    private void OnDisable()
    {
        material.color = originalColor;
    }
}
