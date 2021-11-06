using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FactoryController : MonoBehaviour, ILitterable
{
    [SerializeField] float timeBetweenGarbageSpawns = 3, minTimeBetweenGarbageSpawns = .1f;
    [SerializeField] float timeDecreasePerSpawnsUpgrade = .1f;
    [SerializeField] int spawnsDelayUpgradeCost = 10;

    [SerializeField] float timeOfTrack = 3, minTimeOfTrack = .1f;
    [SerializeField] float timeDecreasePerTrackSpeedUpgrade = .1f;
    [SerializeField] int trackSpeedUpgradeCost = 10;

    [SerializeField] int earningsPerGarbageThrown = 1;

    [SerializeField] Transform garbageStartSpawn, garbageEndSpawn;

    public void StartLittering()
    {
        StopCoroutine(nameof(StartThrowingGarbage));
        StartCoroutine(nameof(StartThrowingGarbage));
    }
    IEnumerator StartThrowingGarbage()
    {
        yield return new WaitForSeconds(timeBetweenGarbageSpawns * Random.Range(.8f, 1.2f));
        while (true)
        {
            ThrowGarbage();
            yield return new WaitForSeconds(timeBetweenGarbageSpawns);
        }
    }

    public void UpgradeThrowDelay()
    {
        if (timeBetweenGarbageSpawns - timeDecreasePerSpawnsUpgrade > minTimeBetweenGarbageSpawns && CoinsSystem.RemoveCoins(spawnsDelayUpgradeCost))
        {
            timeBetweenGarbageSpawns -= timeDecreasePerSpawnsUpgrade;
            StartLittering();
        }
    }

    public void UpgradeTrackSpeed()
    {
        if (timeOfTrack - timeDecreasePerTrackSpeedUpgrade > minTimeOfTrack && CoinsSystem.RemoveCoins(trackSpeedUpgradeCost))
        {
            timeOfTrack -= timeDecreasePerTrackSpeedUpgrade;
            StartLittering();
        }
    }

    private void ThrowGarbage()
    {
        CoinsSystem.AddCoins(earningsPerGarbageThrown);

        //var garbage = Instantiate(...)
        //garbage.transform.localScale *= Vector3.one * Random.Range(.8f, 1.2f);
        //garbage.transform.position = garbageStartSpawn.position;

        //garbage.transform.DOMove(garbageEndSpawn.position, timeOfTrack)
        //    .OnComplete(()=>
        //    {
        //        //garbage.transform.DOPath(new Vector3[] { new Vector3(garbage.transform.position.x, garbage.transform.position.y, garbage.transform.position.z) },)
        //    });


        //on finished, throw to canal
    }
}
