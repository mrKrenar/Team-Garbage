using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour, ILitterable
{
    [SerializeField] Animator animator;
    [SerializeField] float timeBetweenDrops = 3, minTimeBetweenDrops = .1f;
    [SerializeField] float timeDecreasePerUpgrade = .1f;
    [SerializeField] int upgradeCost = 10, earningsPerGarbageThrown = 1;

    public void StartLittering()
    {
        StopCoroutine(nameof(StartThrowingGarbage));
        StartCoroutine(nameof(StartThrowingGarbage));
    }
    IEnumerator StartThrowingGarbage()
    {
        yield return new WaitForSeconds(timeBetweenDrops * Random.Range(.8f, 1.2f));
        while (true)
        {
            ThrowGarbage();
            yield return new WaitForSeconds(timeBetweenDrops);
        }
    }

    public void Upgrade()
    {
        if (timeBetweenDrops - timeDecreasePerUpgrade > minTimeBetweenDrops && CoinsSystem.RemoveCoins(upgradeCost))
        {
            timeBetweenDrops -= timeDecreasePerUpgrade;
            StartLittering();
        }
    }

    private void ThrowGarbage()
    {
        animator.SetTrigger("tr_ThrowGarbage");
        CoinsSystem.AddCoins(earningsPerGarbageThrown);
        //TODO: continue implementation
    }
}
