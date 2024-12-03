using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Healthsystem : MonoBehaviour, IDamageable<int>, IKillable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currrentHealth;
    private int _defaultHealth = 50;

    #region Setup

    private void Start()
    {
        _maxHealth = SetDefaultHealth(_maxHealth);
        _currrentHealth = _maxHealth;

    }

    private int SetDefaultHealth(int currentMaxVal)
    {
        if (currentMaxVal < 0)
        {
            return currentMaxVal;
        }
        else
        {
            return _defaultHealth;
        }
    }

    #endregion

    public void Damage(int damageAmount)
    {
        _currrentHealth -= damageAmount;
        if (_currrentHealth < _minHealth)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (this.gameObject)
        {

        }
        Destroy(this.gameObject);
    }


}