using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        activeLake = allLakes[PlayerPrefs.GetInt("ActiveLake")].GetComponent<LakeController>();
    }

    [SerializeField] TextMeshProUGUI coinsText;

    public GameObject[] allLakes;
    public LakeController activeLake;
    public GameObject allHumans, allFactories, allTrucks;

    [SerializeField] GameObject garbagePrefab;

    private void Start()
    {
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        coinsText.text = CoinsSystem.GetCoinsAmount().ToString();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Lake"))
                {
                    //make garbage
                    CoinsSystem.AddCoins(1);
                    activeLake.MoveGarbageThroughChanelToLake(Instantiate(garbagePrefab), 0, true);
                }
            }
        }
    }

    public void ActivateNextLake()
    {
        for (int i = 0; i < allLakes.Length; i++)
        {
            allLakes[i].gameObject.SetActive(false);
        }

        PlayerPrefs.SetInt("ActiveLake", PlayerPrefs.GetInt("ActiveLake") + 1);

        activeLake = allLakes[PlayerPrefs.GetInt("ActiveLake")].GetComponent<LakeController>();
        activeLake.gameObject.SetActive(true);
    }

    public void DonateTeamSeas()
    {
        Application.OpenURL("https://teamseas.org/");
    }

    public void BuyCharacter()
    {
        activeLake.AddLitterable(LitterType.human);
    }

    public void BuyFactories()
    {
        activeLake.AddLitterable(LitterType.factory);
    }

    public void BuyTruck()
    {
        activeLake.AddLitterable(LitterType.truck);
    }

    public void UpgradeAllCharacters()
    {
        print("BUy CHARACTER");
        if (CoinsSystem.RemoveCoins(50))
        {
            allHumans.SendMessage("Upgrade", SendMessageOptions.DontRequireReceiver);

        }
    }

    public void UpgradeAllFactoriesSpeed()
    {
        allFactories.SendMessage("UpgradeTrackSpeed", SendMessageOptions.DontRequireReceiver);
    }

    public void UpgradeAllFactoriesDelay()
    {
        allFactories.SendMessage("UpgradeThrowDelay", SendMessageOptions.DontRequireReceiver);
    }
}
