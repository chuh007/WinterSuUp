using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSO")]
public class StatSO : ScriptableObject, ICloneable
{
    public delegate void ValueChnageHandle(StatSO stat, float currentValue, float previousValue);
    public event ValueChnageHandle OnValueChnaged;

    public string statName;
    public string descr;
    public Sprite icon;

    private float _baseValue, _minValue, _maxValue;

    private Dictionary<string, float> _modifyiedValueByKey;

    private float _modifyiedValue = 0;

    public float MinValue
    {
        get => _minValue;
    }

    public float MaxValue
    {
        get => _maxValue;
    }

    public float BaseValue
    {
        get => _baseValue;
        set
        {
            float prevValue = value;
            _baseValue = Mathf.Clamp(value, MinValue, MaxValue);
            TryInvokeValueChangeEvent(value,prevValue);
        }
    }

    public float Value => Mathf.Clamp(_baseValue+_modifyiedValue, _minValue, _maxValue);
    public bool IsMax => Mathf.Approximately(Value, MaxValue);
    public bool IsMin => Mathf.Approximately(Value, MinValue);

    private void TryInvokeValueChangeEvent(float value, float prevValue)
    {
        if(Mathf.Approximately(value,prevValue) == false)
        {
            OnValueChnaged?.Invoke(this,value, prevValue);
        }
    }

    public void AddModifier(string key, float value)
    {
        if (_modifyiedValueByKey.ContainsKey(key)) return;

        float prevValue = Value;

        _modifyiedValue += Value;
        _modifyiedValueByKey.Add(key, value);

        TryInvokeValueChangeEvent(Value, prevValue);
    }

    public void Removemodifier(string key)
    {
        
    }

    public void Clear()
    {

    }

    public object Clone()
    {
        return Instantiate(this);
    }
}
