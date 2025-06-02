using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject locomotionSystem;
    public int sceneId;
    public void LoadScene()
    {
        locomotionSystem.SetActive(true);
        xrOrigin.transform.position = new Vector3(-15.68f, 0, 10.553f);
        xrOrigin.transform.rotation = Quaternion.Euler(0, -90, 0);
        GameManager.Instance.InitTasks();
        SceneManager.LoadScene(sceneId);
    }
}
