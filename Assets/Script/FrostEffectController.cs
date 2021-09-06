using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostEffectController : MonoBehaviour
{
    private FrostEffect frostEffect;

    private float currentFrostValue;

    void Start()
    {
        frostEffect = GetComponent<FrostEffect>();

        InitialFrostAmount();
    }

    void Update()
    {
        if (currentFrostValue > 0)
        {
            currentFrostValue -= Time.deltaTime / 20;

            UpdateFrostAmount(0);

            if (currentFrostValue <= 0)
            {
                InitialFrostAmount();
            }
        }
    }
    /// <summary>
    /// frostEffectのFrostAmountの値を更新
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateFrostAmount(float amount)
    {
        currentFrostValue += amount;
        frostEffect.FrostAmount = currentFrostValue;
    }
    /// <summary>
    /// FrostEffectのFrostAmountの値の初期化
    /// </summary>
    public void InitialFrostAmount()
    {
        currentFrostValue = 0;
        frostEffect.FrostAmount = currentFrostValue;
    }
}
