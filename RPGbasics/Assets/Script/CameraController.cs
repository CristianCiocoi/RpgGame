using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CameraController : MonoBehaviour
{

    public Transform target;

    public Tilemap theMap;
    private Vector3 botomLeftLimit;
    private Vector3 topRightLimit;


    private float halfHeight;
    private float halfWidth;


    public int musicToPlay;
    private bool musicStarted;

    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerController.instance.transform;

        target = FindObjectOfType<PlayerController>().transform;
        halfHeight = Camera.main.orthographicSize;//what is the current size of the camera
        halfWidth = halfHeight * Camera.main.aspect; //we get the half height and half width


        botomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth,halfHeight,0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        FindObjectOfType<PlayerController>().SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // LateUpdate is called once per frame after Update// now the camera is a lot smoother
    void LateUpdate()
    {

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        //keep the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, botomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, botomLeftLimit.y, topRightLimit.y), transform.position.z);
        if (!musicStarted)
        {
            musicStarted = true;
            AudioManager.instance.PlayBGM(musicToPlay);
        }
    }
}
