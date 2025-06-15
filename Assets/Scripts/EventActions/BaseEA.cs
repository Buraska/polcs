using System;
using System.Collections;
using UnityEngine;

namespace EventActions
{


    public class BaseEA : MonoBehaviour

    {
    public virtual IEnumerator ActionCoroutine()
    {
        yield break;
    }
    }
}