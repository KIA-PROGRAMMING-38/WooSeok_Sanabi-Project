using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideDust : MonoBehaviour
{
    public ObjectPool<WallSlideDust> dustPool { private get; set; }
    

    private void OnEnable()
    {
        transform.position = GameManager.Instance.playerController.WallSlideEffectorTransform.position;
        transform.localScale = GameManager.Instance.playerController.WallSlideEffectorTransform.localScale;
    }

    private void Start()
    {
        
    }

    private void DustReturnToPool()
    {
        dustPool.ReturnToPool(this);
    }
}
