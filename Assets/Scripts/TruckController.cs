using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TruckController : MonoBehaviour
{
    [SerializeField] int garbageCount = 10;
    [SerializeField] int moveSpeed = 10;


    public void StartLittering(Vector3 moveTo)
    {
        transform.DOMove(moveTo, moveSpeed).OnComplete(() => {
            ThrowGarbage();
        });
    }
    
    void ThrowGarbage()
    {
        //throw <garbageCount> garbages
    }
}
