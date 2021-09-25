using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] Dialog dialog;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting soon");
        Timings.DelayAction(1f);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }

}
