using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyovertime : MonoBehaviour
{
    [SerializeField] private float timer;
    void Start()
    {
        Destroy(this, timer);
    }
}
