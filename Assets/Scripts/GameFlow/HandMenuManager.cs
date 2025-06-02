using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandMenuManager : MonoBehaviour
{
    public class TaskInfo
    {
        public bool completed = false;
        public string text = "";
        public string color = "#500000";

        public TaskInfo(bool completed = false, string text = "", string color = "#500000")
        {
            this.completed = completed;
            this.text = text;
            this.color = color;
        }
    }
    public static HandMenuManager Instance = null;


    public float palmFacingThreshold;

    [SerializeField] private GameObject _menuGameObject;
    [SerializeField] private TMP_Text textHolder;
    [SerializeField] private Transform _leftHandTr;
    [SerializeField] private Transform _cameraTr;

    Dictionary<int, TaskInfo> textLines;
    private bool openMenu;
    private bool canOpenMenu = true;

    public void DisableMenu()
    {
        canOpenMenu = false;
        _menuGameObject.SetActive(false);
    }

    public void ReenableMenu()
    {
        canOpenMenu = true;
    }
    public void AddTask(int id, string text)
    {
        textLines.Add(id, new TaskInfo(false, text));
        UpdateText();
    }
    public void UpdateTask(int id, string newText)
    {
        textLines[id].text = newText;
        UpdateText();
        EventsManager.Instance.HandMenuUpdated();
    }
    public void DoneTask(int id)
    {
        textLines[id].completed = true;
        UpdateText();
    }
    public void ClearTasks()
    {
        textLines.Clear();
        UpdateText();
    }
 
    private void UpdateText()
    {
        textHolder.text = "";
        foreach (var entry in textLines)
        {
            string entryText = entry.Value.text;
            if (entry.Value.completed)
                entryText = "<s>" + entryText + "</s>";

            entryText = "<color=" + entry.Value.color + ">" + entryText + "</color>";

            textHolder.text += entryText;

            textHolder.text += "\n";
        }
    }

    private void OpenMenu()
    {
        _menuGameObject.SetActive(true);
        openMenu = true;
    }
    private void CloseMenu()
    {
        _menuGameObject.SetActive(false);
        openMenu = false;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnHandMenuOpen -= OpenMenu;
        EventsManager.Instance.OnHandMenuClose -= CloseMenu;
    }

    private void Awake()
    {
        textLines = new Dictionary<int, TaskInfo>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        EventsManager.Instance.OnHandMenuOpen += OpenMenu;
        EventsManager.Instance.OnHandMenuClose += CloseMenu;
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(_cameraTr.forward, _leftHandTr.right) < palmFacingThreshold && !openMenu && canOpenMenu)
            EventsManager.Instance.HandMenuOpen();
        else if (Vector3.Dot(_cameraTr.forward, _leftHandTr.right) > palmFacingThreshold && openMenu && canOpenMenu)
            EventsManager.Instance.HandMenuClose();

    }
}
