using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;
    [SerializeField] int switchToScene = -1;
    public int SwitchSceneId { get { return switchToScene; } } 

    public List<string> Lines {get { return lines; }}
}
