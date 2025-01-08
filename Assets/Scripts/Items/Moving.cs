using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : VCNVMonoBehaviour
{
    [Header("Moving")]
    [SerializeField] protected GameObject PosA;
    [SerializeField] protected GameObject PosB;
    [SerializeField] protected float moveSpeed = 0.5f;
    [SerializeField] protected bool isMoveToB = true;
}
