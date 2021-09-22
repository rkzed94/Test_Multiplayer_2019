using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{

    public GameObject characterStat;

    private GameObject _attack;
    private GameObject _critical;
    private GameObject _range;

    void Start()
    {
        _attack = characterStat.transform.Find("AttackSlider").gameObject;
        _critical = characterStat.transform.Find("CriticalSlider").gameObject;
        _range = characterStat.transform.Find("RangeSlider").gameObject;

        UpdateStat(1, 1, 10);
    }

    public void UpdateStat(float attack, float critical, float range)
    {
        _attack.GetComponent<Slider>().value = attack / 10;
        _critical.GetComponent<Slider>().value = critical / 10;
        _range.GetComponent<Slider>().value = range / 10;
    }
}
