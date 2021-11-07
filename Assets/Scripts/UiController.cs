using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    public static UiController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    [SerializeField] GameObject storePopup;
    public void OpenStore()
    {
        DOTween.Kill(storePopup);
        storePopup.SetActive(!storePopup.activeSelf);
        if (storePopup.activeSelf)
        {
            storePopup.transform.localScale = Vector3.one * .3f;
            storePopup.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);
        }
    }
}
