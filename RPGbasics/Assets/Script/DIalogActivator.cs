using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogActivator : MonoBehaviour
{

    public string[] lines;
    private bool canActivate;

    public bool isPlayer = true;

    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;

    void Start()
    {
        
    }

    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines, isPlayer);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canActivate = false;
        }
    }

}
