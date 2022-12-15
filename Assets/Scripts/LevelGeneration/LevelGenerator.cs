using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    public static event EventHandler<OnExtentGeneratedArgs> OnExtentGenerated;

    public class OnExtentGeneratedArgs : EventArgs
    {
        public int extentsGenerated;
        public int extentsBeforeEnd;
    }

    public Transform grid;

    public LevelExtentInfo firstExtent;

    public GameObject[] levelExtentPrefabs;

    private List<GameObject> levelExtentLeftEntrance = new List<GameObject>();
    private List<GameObject> levelExtentCenterEntrance = new List<GameObject>();
    private List<GameObject> levelExtentRightEntrance = new List<GameObject>();

    private int spawnY = 10;

    private int numOfExtents = 0;
    [SerializeField] private int numOfExtentsBeforeEnd = 10;
    [SerializeField] private GameObject endExtent;

    private ExtentConnection lastExtentExit;

    // MONOBEHAVIOUR //
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance of LevelGenerator in the scene! " + transform.position + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        lastExtentExit = firstExtent.exit;
        SortLevelExtantsByEntrance();
        GenerateNextExtent();
    }

    // CORE //
    private void SortLevelExtantsByEntrance()
    {
        for (int i = 0; i < levelExtentPrefabs.Length; i++)
        {
            LevelExtentInfo info = levelExtentPrefabs[i].GetComponent<LevelExtentInfo>();

            switch (info.entrance)
            {
                case ExtentConnection.Left:
                    levelExtentLeftEntrance.Add(levelExtentPrefabs[i]);
                    break;
                case ExtentConnection.Center:
                    levelExtentCenterEntrance.Add(levelExtentPrefabs[i]);
                    break;
                case ExtentConnection.Right:
                    levelExtentRightEntrance.Add(levelExtentPrefabs[i]);
                    break;
            }
        }
    }

    private void AddLevelExtent(GameObject extentPrefab)
    {
        Vector3 spawnPosition = new Vector2(0, spawnY);
        GameObject extent = Instantiate(extentPrefab, spawnPosition, Quaternion.identity, grid);
        LevelExtentInfo info = extent.GetComponent<LevelExtentInfo>();
        lastExtentExit = info.exit;
        spawnY += info.height;
        numOfExtents++;
        OnExtentGenerated?.Invoke(this, new OnExtentGeneratedArgs
        {
            extentsGenerated = numOfExtents,
            extentsBeforeEnd = numOfExtentsBeforeEnd - numOfExtents
        });
    }

    public void GenerateNextExtent()
    {
        if (numOfExtents >= numOfExtentsBeforeEnd)
        {
            AddLevelExtent(endExtent);
            return;
        }

        // Find all prefabs where the Entrance/Exits are adjacent
        //// Left -> Center
        //// Center -> Left || Center -> Right
        //// Right -> Center
        int randomIndex = 0;
        switch (lastExtentExit)
        {
            case ExtentConnection.Left:
                randomIndex = UnityEngine.Random.Range(0, levelExtentCenterEntrance.Count);
                AddLevelExtent(levelExtentCenterEntrance[randomIndex]);
                //Debug.Log("Left->Center");
                break;
            case ExtentConnection.Center:
                randomIndex = UnityEngine.Random.Range(0, 1);
                if (randomIndex == 1)
                {
                    //Debug.Log("Center->Left");
                    randomIndex = UnityEngine.Random.Range(0, levelExtentCenterEntrance.Count);
                    AddLevelExtent(levelExtentCenterEntrance[randomIndex]);
                }
                else
                {
                    //Debug.Log("Center->Right");
                    randomIndex = UnityEngine.Random.Range(0, levelExtentCenterEntrance.Count);
                    AddLevelExtent(levelExtentCenterEntrance[randomIndex]);
                }
                break;
            case ExtentConnection.Right:
                //Debug.Log("Right->Center");
                randomIndex = UnityEngine.Random.Range(0, levelExtentCenterEntrance.Count);
                AddLevelExtent(levelExtentCenterEntrance[randomIndex]);
                break;
        }


    }
}
