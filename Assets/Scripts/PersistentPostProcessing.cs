using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentPostProcessing : MonoBehaviour
{
    public static PersistentPostProcessing Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PersistentPostProcessing in this scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }
}
