using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField] private float alphaset = 0.7f;
    [SerializeField] private float alphaMultiplier = 0.95f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;
    public ObjectPool<PlayerAfterImage> WireDashPool { private get; set; }

    private Color spriteOriginalColor = new Color(1f, 1f, 1f);
    private Color spriteTempColor;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaset;
        SR.sprite = playerSR.sprite;

        spriteOriginalColor.a = alpha;
        spriteTempColor = spriteOriginalColor;
        SR.color = spriteTempColor;

        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;

        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;

        spriteTempColor.a = alpha;
        SR.color = spriteTempColor;

        //spriteOriginalColor = new Color(1f, 1f, 1f, alpha);
        //SR.color = spriteOriginalColor;


        if (timeActivated + activeTime <= Time.time)
        {
            WireDashPool.ReturnToPool(this);
        }
    }
}
