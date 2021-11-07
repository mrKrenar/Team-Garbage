using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LakeController : MonoBehaviour
{
    public Transform cameraPosition;

    [SerializeField] Transform[] lakeChannelsStart, lakeChannelsEnd;
    [SerializeField] Transform lakeCenter;

    [SerializeField] LakeChannel[] litterableTransforms;

    [SerializeField] GameObject humanPrefab, factoryPrefab, truckPrefab;

    [SerializeField] Transform allHumansParent, allFactoriesParent, allTrucksParent;

    [SerializeField] float garbageAttractionForceToLakeCenter = 10;

    List<Rigidbody> garbagesToMoveToLakeCenter;

    int spawnedIndex = 1;

    private void Awake()
    {
        garbagesToMoveToLakeCenter = new List<Rigidbody>();
        indexesOfDestroyedRBs = new List<int>();
    }

    List<int> indexesOfDestroyedRBs;
    private void FixedUpdate()
    {
        for (int i = 0; i < garbagesToMoveToLakeCenter.Count; i++)
        {
            try
            {
                garbagesToMoveToLakeCenter[i].AddForce((lakeCenter.position - garbagesToMoveToLakeCenter[i].position).normalized * garbageAttractionForceToLakeCenter, ForceMode.VelocityChange);
            }
            catch (MissingReferenceException)
            {
                indexesOfDestroyedRBs.Add(i);
            }
        }

        if (indexesOfDestroyedRBs.Count > 0)
        {
            for (int i = indexesOfDestroyedRBs.Count - 1; i >= 0; i--)
            {
                garbagesToMoveToLakeCenter.RemoveAt(indexesOfDestroyedRBs[i]);
            }
            indexesOfDestroyedRBs.Clear();
        }
    }

    public void AddLitterable(LitterType litterType)
    {
        int litterableIndexesCount = 0;
        int channelIndex = 0;
        int newLitterableIndex = 0;

        //check all lakes channels if theres room to add a litterable at any of them
        for (; channelIndex < litterableTransforms.Length; channelIndex++)
        {
            if (spawnedIndex < litterableTransforms[channelIndex].litterableTransforms.Length - 1 + litterableIndexesCount )
            {
                newLitterableIndex = litterableTransforms[channelIndex].litterableTransforms.Length - 1 + litterableIndexesCount - spawnedIndex;
                break;
            }

            litterableIndexesCount += litterableTransforms[channelIndex].litterableTransforms.Length;

            if (channelIndex == litterableTransforms.Length - 1)
            {
                Debug.LogWarning("max number of litterables already has been reached! Can't add new ones");
                return;
            }
        }

        GameObject litterSpawned;
        switch (litterType)
        {
            case LitterType.human:
                litterSpawned = Instantiate(humanPrefab, litterableTransforms[channelIndex].litterableTransforms[newLitterableIndex].position, litterableTransforms[channelIndex].litterableTransforms[newLitterableIndex].rotation);
                litterSpawned.GetComponent<HumanController>().StartLittering();
                litterSpawned.transform.parent = allHumansParent;
                CoinsSystem.RemoveCoins(10);
                break;
            case LitterType.factory:
                litterSpawned = Instantiate(factoryPrefab, litterableTransforms[channelIndex].litterableTransforms[newLitterableIndex].position, litterableTransforms[channelIndex].litterableTransforms[newLitterableIndex].rotation);
                litterSpawned.GetComponent<FactoryController>().StartLittering();
                litterSpawned.transform.parent = allFactoriesParent;
                break;
            case LitterType.truck:
                litterSpawned = Instantiate(truckPrefab, litterableTransforms[channelIndex].litterableTransforms[newLitterableIndex].position, litterableTransforms[channelIndex].litterableTransforms[newLitterableIndex].rotation);
                litterSpawned.GetComponent<TruckController>().StartLittering(litterableTransforms[channelIndex].litterableTransforms[newLitterableIndex].position);
                litterSpawned.transform.parent = allTrucksParent;
                break;
        }
        spawnedIndex++;
    }
    [SerializeField] GameObject garbagePrefab;
    public void ThrowGarbage(Transform human)
    {
        var garbage = Instantiate(garbagePrefab, human.position, human.rotation);
        MoveGarbageThroughChanelToLake(garbage, 0);
    }

    public void MoveGarbageThroughChanelToLake(GameObject garbage, int channelIndex, bool startFromLakeStart = false)
    {
        if (channelIndex >= lakeChannelsStart.Length)
        {
            Debug.LogWarning($"Channel with index {channelIndex} doesn't exist. Setting channelIndex to 0", gameObject);
            channelIndex = 0;
        }

        if (startFromLakeStart)
        {
            garbage.transform.position = lakeChannelsStart[channelIndex].position;
        }

        garbage.transform.DOMove(lakeChannelsEnd[channelIndex].position, 2).OnComplete(()=> {
            AddGarbageToListForMovingToLakeCenter(garbage);
        });
    }

    void AddGarbageToListForMovingToLakeCenter(GameObject garbage)
    {
        var rb = garbage.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        garbagesToMoveToLakeCenter.Add(rb);
    }
}

[System.Serializable]
class LakeChannel
{
    [SerializeField] public Transform[] litterableTransforms;
}

public enum LitterType { human, factory, truck}
