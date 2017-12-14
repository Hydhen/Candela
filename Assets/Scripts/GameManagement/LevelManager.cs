using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance = null;

    public GameObject[] Levels = null;

    public string WallsParentObjectName = null;


    private int CurrentLevel = 0;


    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    public void SetCurrentLevel(int level)
    {     
        foreach (Transform trans in Levels[CurrentLevel].transform)
        {
            trans.gameObject.layer = LayerMask.NameToLayer("Walls Disabled");
        }

        CurrentLevel = level - 1;

        foreach (Transform trans in Levels[CurrentLevel].transform)
        {
            trans.gameObject.layer = LayerMask.NameToLayer("Walls Enabled");
        }
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        if (WallsParentObjectName == null)
        {
            Debug.LogError("<color='Red'>No Walls Parent Object Name given</color>");
        }

        GameObject levelProps = GameObject.Find(WallsParentObjectName);
        Transform[] children = levelProps.GetComponentsInChildren<Transform>();

        int numberOfChildren = 0;

        foreach (Transform child in children)
        {
            if (child.parent == levelProps.transform)
            {
                numberOfChildren++;
            }
        }

        Levels = new GameObject[numberOfChildren];

        foreach (Transform child in children)
        {
            if (child.parent == levelProps.transform)
            {
                Levels[int.Parse(child.name) - 1] = child.gameObject;
            }
        }
    }

}
