using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ez;

public class EzTweenDebug : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.gameObject.name = "EzTween: ("+EzTween.TweenCount + ")";
    }
}
