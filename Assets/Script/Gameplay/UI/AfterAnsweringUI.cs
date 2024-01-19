using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterAnsweringUI : MonoBehaviour
{
    public void OnTrueAnswerAnimationEnd()
    {
        StaticAction.NextQuestion?.Invoke();
        gameObject.SetActive(false);
    }

    public void OnFalseAnswerAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
