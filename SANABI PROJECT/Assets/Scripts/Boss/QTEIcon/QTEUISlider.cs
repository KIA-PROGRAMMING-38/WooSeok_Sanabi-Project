using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEUISlider : MonoBehaviour
{
    private Slider QTESlider;

    [SerializeField] private float increaseAmount = 0.05f;
    [SerializeField] private float decreaseAmount = 0.05f;

    private Vector2 offset = new Vector2(0f, 3f);
    public Transform bossTransform { private get; set; }

    private void Awake()
    {
        QTESlider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (bossTransform != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(bossTransform.position + (Vector3)offset);
        }
    }

    
}
