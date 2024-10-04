using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class DisableHealthBarUnitlDamage : MonoBehaviour

{
    [SerializeField] private MMHealthBar  _mmHealthBar;
    void Start()
    {
        _mmHealthBar = GetComponent<MMHealthBar>();
    }

}
