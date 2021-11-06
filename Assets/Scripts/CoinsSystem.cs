using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class CoinsSystem
{
    public static string CoinKey { get { return "SavedCoins"; } }
    public static CoinsValueChanged onValueChanged;

    public static int GetCoinsAmount()
    {
        return PlayerPrefs.GetInt(CoinKey, 0);
    }
    public static void SetCoinsAmount(int amount)
    {
        PlayerPrefs.SetInt(CoinKey, amount);
        onValueChanged?.Invoke(GetCoinsAmount());
    }
    public static void AddCoins(int amount)
    {
        PlayerPrefs.SetInt(CoinKey, GetCoinsAmount() + amount);
        onValueChanged?.Invoke(GetCoinsAmount());
    }
    public static bool RemoveCoins(int amount)
    {
        if (GetCoinsAmount() - amount >= 0)
        {
            SetCoinsAmount(GetCoinsAmount() - amount);
            onValueChanged?.Invoke(GetCoinsAmount());
            return true;
        }
        return false;
    }

}

public class CoinsValueChanged : UnityEvent<int>
{
}