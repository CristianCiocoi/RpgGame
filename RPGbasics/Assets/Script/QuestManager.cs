using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{//so a quest is when you activate and deactivate certain game objects to be able to move to the next scene, to do different things in that area

    public string[] questMarkerName;
    public bool[] questMarkersComplete;

    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;


        // so the questMarkersComplete need to be the same length as questMarkerName
        questMarkersComplete = new bool[questMarkerName.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckIfComplete("quest test"));
            MarkQuestComplete("quest test");

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveQuestData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadQuestData();
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questMarkerName.Length; i++)
        {
            if (questMarkerName[i] == questToFind)
            {
                return i;
            }
        }
        Debug.LogError("Quest: " + questToFind + " does not exist");
        return 0; //we are gonna asume that 0 means there is no quest, thats why i left blank at element 0
    }


    //check is the q is complete

    //all teh methods will chech through the questMarkerName and basically will check and find the position     of that q by its name and in the q complete set the value true or false
    public bool CheckIfComplete(string questToCheck)
    {//if its not equal to zero than we can check the quest
        if (GetQuestNumber(questToCheck) != 0)
        {
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }

        return false;
    }


    public void MarkQuestComplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>(); //FindObjectsOfType<QuestObjectActivator>() will find all the objects in the scene that have the QuestObjectActivator script attached to it
        if(questObjects.Length > 0)
        {
            for(int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckComplete();
            }
        }
    
    
    }




    //looping through all of the questMarkers and set it should be stored as true or false
    public void SaveQuestData()
    {
        //when we wanna save we will write some infos to the player prefs
        for (int i = 0; i < questMarkerName.Length; i++)
        {
            if (questMarkersComplete[i])
            {
                //set int 0 means false and 1 is true
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerName[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerName[i], 0);
            }
        }

    }

    public void LoadQuestData()
    {
        //and when we load we read some infos from player prefs
        for(int i = 0; i < questMarkerName.Length; i++)
        {
            int valueToSet = 0;
            if (PlayerPrefs.HasKey("QuestMarker_" + questMarkerName[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerName[i]);
            }

            if(valueToSet == 0)
            {
                questMarkersComplete[i] = false;
            }
            else
            {
                questMarkersComplete[i] = true;
            }
        }
    }

}
