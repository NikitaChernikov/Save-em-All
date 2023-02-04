using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAd : MonoBehaviour
{
    [SerializeField] private AdsInit ads;

    // Start is called before the first frame update
    void Start()
    {
        ads.ShowAd();
    }
}
