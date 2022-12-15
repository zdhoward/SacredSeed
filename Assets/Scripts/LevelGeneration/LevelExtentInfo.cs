using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ExtentConnection
{
    Left,
    Center,
    Right,
}

public class LevelExtentInfo : MonoBehaviour
{
    public int height;

    public ExtentConnection entrance;
    public ExtentConnection exit;
}
