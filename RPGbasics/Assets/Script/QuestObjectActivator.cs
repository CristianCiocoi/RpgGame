using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestObjectActivator : MonoBehaviour
{
    public GameObject ojectToActivate;

    public string questToCheck;

    public bool activeIfComplete;

    private bool initialCheckDone;

       // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialCheckDone)
        {
            initialCheckDone = true;

            CheckComplete();
        }
    }

    public void CheckComplete()
    {
        if (QuestManager.instance.CheckIfComplete(questToCheck))
        {
            ojectToActivate.SetActive(activeIfComplete);
        }
    }

}
