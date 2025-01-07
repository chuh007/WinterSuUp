using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSO")]
public class StatSO : ScriptableObject, ICloneable
{
    public delegate void ValueChangeHandle(StatSO stat, float currentValue, float previousValue);
    public event ValueChangeHandle OnValueChanged;

    public string statName;
    public string description;
    public string displayName;
    public Sprite icon;

    private float _baseValue, _minValue, _maxValue;

    private Dictionary<string, float> _modifyValueKey = new Dictionary<string, float>();

    [field: SerializeField] public bool IsPrecent { get; private set; }

    private float _modifiedValue = 0;

    public float MaxValue
    {
        get => _maxValue;
        set => _maxValue = value;
    }

    public float MinValue
    {
        get => _minValue;
        set => _minValue = value;
    }

    public float Value => Mathf.Clamp(_baseValue + _modifiedValue, MaxValue, MaxValue);
    public bool IsMax => Mathf.Approximately(Value, MaxValue);
    public bool IsMin => Mathf.Approximately(Value, MinValue);

    public float BaseValue
    {
        get => _baseValue;
        set
        {
            float prevValue = Value;
            _baseValue = Mathf.Clamp(value, MinValue, MaxValue);
            TryInvokeValueChangeEvent(Value, prevValue);
        }
    }

    private void TryInvokeValueChangeEvent(float value, float prevValue)
    {
        if (Mathf.Approximately(value, prevValue) == false)
        {
            OnValueChanged?.Invoke(this, value, prevValue);
        }
    }

    public void AddModifier(string key, float value)
    {
        if (_modifyValueKey.ContainsKey(key)) return;

        float prevValue = Value;

        _modifiedValue += value;
        _modifyValueKey.Add(key, value);

        TryInvokeValueChangeEvent(Value, prevValue);
    }

    public void RemoveModifier(string key)
    {
        if (_modifyValueKey.TryGetValue(key, out float value))
        {
            float prevValue = Value;

            _modifiedValue -= value;
            _modifyValueKey.Remove(key);

            TryInvokeValueChangeEvent(Value, prevValue);
        }
    }

    public void ClearModifiers()
    {
        float prevValue = Value;
        _modifyValueKey.Clear();
        _modifiedValue = 0;
        TryInvokeValueChangeEvent(Value, prevValue);
    }

    public object Clone()
    {
        return Instantiate(this);
    }
}
