using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeScenes(string scene)
    {
        Core_Game_Manager.Instance.ChangeScene(scene);
    }

    public void ResumeTimeScale()
    {
        Core_Game_Manager.Instance.ResumeTime();
    }
}
