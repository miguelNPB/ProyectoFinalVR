using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameObject locomotion;

    int numManchasPared;
    int numManchasSuelo;
    int numSillasPorColocar;

    private bool droppedCartaMadre;
    private bool donePared, doneSuelo, doneSillas, doneLlave;
    
    private void CleanManchaPared()
    {
        if (!donePared)
        {
            numManchasPared--;
            if (numManchasPared == 0)
            {
                donePared = true;
                HandMenuManager.Instance.DoneTask(2);
            }
            HandMenuManager.Instance.UpdateTask(2, 3 - numManchasPared + "/3 Limpia las manchas de las paredes, rellena el cubo de agua y frotalas con la esponja mojada en el cubo");
        }
    }
    private void CleanManchaSuelo()
    {
        if (!doneSuelo)
        {
            numManchasSuelo--;
            if (numManchasSuelo == 0)
            {
                doneSuelo = true;
                HandMenuManager.Instance.DoneTask(3);
            }
            HandMenuManager.Instance.UpdateTask(3, 5 - numManchasSuelo + "/5 Limpia los escombros de tierra del suelo con la escoba");
        }
    }
    private void PositionChair()
    {
        if (!doneSillas)
        {
            numSillasPorColocar--;
            if (numSillasPorColocar == 0)
            {
                doneSillas = true;
                HandMenuManager.Instance.DoneTask(4);
            }
            HandMenuManager.Instance.UpdateTask(4, 4 - numSillasPorColocar + "/4 Recoloca las sillas en sus mesas");
        }
    }
    private void UnpositionChair()
    {
        if (!doneSillas)
        {
            numSillasPorColocar++;
            HandMenuManager.Instance.UpdateTask(4, 4 - numSillasPorColocar + "/4 Recoloca las sillas en sus mesas");
        }
    }

    private void ReadCartaMadre()
    {
        if (!droppedCartaMadre)
        {
            droppedCartaMadre = true;
            HandMenuManager.Instance.ClearTasks();
            HandMenuManager.Instance.AddTask(1, "Coge la llave de la barra y cierra la taberna.");
            HandMenuManager.Instance.AddTask(2, 3 - numManchasPared + "/3 Limpia las manchas de las paredes, rellena el cubo de agua y frotalas con la esponja mojada en el cubo");
            HandMenuManager.Instance.AddTask(3, 5 - numManchasSuelo + "/5 Limpia los escombros de tierra del suelo con la escoba");
            HandMenuManager.Instance.AddTask(4, 4 - numSillasPorColocar + "/4 Recoloca las sillas en sus mesas");

        }
    }

    private void LockDoor()
    {
        if (!doneLlave)
        {
            doneLlave = true;
            HandMenuManager.Instance.DoneTask(1);
        }
    }

    public void InitTasks()
    {
        EventsManager.Instance.OnReadTutorialLetter += ReadCartaMadre;
        EventsManager.Instance.OnChairPositioned += PositionChair;
        EventsManager.Instance.OnChairPositioned += CheckAllTasksDone;
        EventsManager.Instance.OnChairUnPositioned += UnpositionChair;
        EventsManager.Instance.OnWallManchaCleared += CleanManchaPared;
        EventsManager.Instance.OnWallManchaCleared += CheckAllTasksDone;
        EventsManager.Instance.OnFloorManchaCleared += CleanManchaSuelo;
        EventsManager.Instance.OnFloorManchaCleared += CheckAllTasksDone;
        EventsManager.Instance.OnDoorLocked += LockDoor;
        EventsManager.Instance.OnDoorLocked += CheckAllTasksDone;

        HandMenuManager.Instance.AddTask(0, "Lee la carta de tu madre");
    }

    private void CheckAllTasksDone()
    {
        if (doneSuelo && donePared && doneLlave && doneSillas)
        {
            HandMenuManager.Instance.ClearTasks();
            HandMenuManager.Instance.AddTask(10, "Ve a dormir");
            EventsManager.Instance.AllTasksDone();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        numManchasPared = 3;
        numManchasSuelo = 5;
        numSillasPorColocar = 4;
        droppedCartaMadre = false;
        donePared = false;
        doneSuelo = false;
        doneLlave = false;
        doneSillas = false;
        //InitTasks();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
