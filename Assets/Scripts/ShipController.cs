using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipController : MonoBehaviour
{
    public float moveDuration;


    DOTweenPath path;
    private void Awake()
    {
        path = GetComponent<DOTweenPath>();
        path.duration = moveDuration;
        path.DOPlay();
    }
}
