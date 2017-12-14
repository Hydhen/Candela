using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour {

    public Text CurrentLevelText = null;

    public TopCamera TC = null;

    public void OnLevelButtonPressed()
    {
        // Set new Level Value
        int lastLevel = LevelManager.Instance.GetCurrentLevel();
        LevelManager.Instance.SetCurrentLevel(int.Parse(EventSystem.current.currentSelectedGameObject.name));

        // Set UI Text Accordingly
        CurrentLevelText.text = "Current Level: " + EventSystem.current.currentSelectedGameObject.name;

        // Raise the TopCamera
        Vector3 newOffset = new Vector3(0, (LevelManager.Instance.GetCurrentLevel() - lastLevel) * 5, 0);
        TC.SetTargetOffset(newOffset);
    }


    private void Awake()
    {
        if (CurrentLevelText == null)
        {
            Debug.LogError("<color='Red'>No Current Level Text given</color>");
        }

        if (TC == null)
        {
            Debug.LogError("<color='Red'>No Camera given</color>");
        }
    }

    private void Start()
    {
        if (CurrentLevelText)
        {
            CurrentLevelText.text = "Current Level: " + LevelManager.Instance.GetCurrentLevel().ToString();
        }
    }

}
