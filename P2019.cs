using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class P2019 : MonoBehaviour, IEndDragHandler
{
  
    public GameObject sphere;
    public GameObject cube;

    public GameObject root;
    public GameObject player;
    public GameObject hedge;
  
    public GameObject Left;
    public GameObject Right;
    public int currentTile = 0;
    public bool newTileMade;

    void Start()
    {

        newTileMade = false;
 
        
    }


    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    private DraggedDirection GetDragDirection(Vector2 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Press position + " + eventData.pressPosition);
        Debug.Log("End position + " + eventData.position);
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        Debug.Log("norm + " + dragVectorDirection);
        GetDragDirection(dragVectorDirection);
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
 

        currentTile =(int) player.transform.position.z / 5;
        if (player.transform.position.z % 5 < 1)
            newTileMade = false;
        if (player.transform.position.z % 5 > 2 & !newTileMade)
        {

            newTileMade = true;
            int index = Random.Range(0, 2);

            if (index == 0)
                Instantiate(Left, new Vector3(0.0f, 0.0f, currentTile * 5 + 5), Quaternion.identity);
            if (index == 1)
                Instantiate(Right, new Vector3(0.0f, 0.0f, currentTile * 5 + 5), Quaternion.identity);

            int hedgeXIndex = Random.Range(-4, 5);
            int hedgeYIndex = Random.Range(1, 5);

            Instantiate(hedge, new Vector3(hedgeXIndex, 0.0f, currentTile * 5 + 5 + hedgeYIndex), Quaternion.identity);


            int numSpheres = Random.Range(0, 4);
            int numCubes = Random.Range(0, 2);

            for (int i = 0; i < numSpheres; i++)

            {
                int x = Random.Range(-4, 5);
                int y = Random.Range(1, 5);

                Instantiate(sphere, new Vector3(x, 1.0f, currentTile * 5 + 5 + y), Quaternion.identity);



            }
            for (int i = 0; i < numCubes; i++)

            {
                int x = Random.Range(-4, 5);
                int y = Random.Range(1, 5);

                Instantiate(cube, new Vector3(x, 0.0f, currentTile * 5 + 5 + y), Quaternion.identity);



            }






        }

        // Debug.LogError("In Update");
       

       /*     if (Input.GetMouseButtonDown(0) || Input.touchCount>0)

        {
            mouseDrag = false;
            mouseInputStart = Input.mousePosition;
                startMouseDrag = true;
        }
            if(Input.GetMouseButton(0) && startMouseDrag)
            {

               // Button0Down = true;

                Debug.Log("IGMB0 SMD");
                Vector3 mouseChange = Input.mousePosition;
              Vector3 mouseDiff = mouseChange - mouseInputStart;

                if (Mathf.Abs(mouseDiff.x) > Mathf.Abs(mouseDiff.y))
                    changeX = true;

                if (changeX)
                {

                    if ((mouseChange.x - mouseInputStart.x) >0.0f)
                    {
                        Debug.Log("Left");


                        player.transform.right = Vector3.down;
                        player.transform.up = Vector3.right;
                        //player.transform.forward = Vector3.forward;
                        mouseDrag = false; lockOut = 0;
                    //    player.transform.position -= new Vector3(Time.deltaTime, 0.0f, 0.0f);
                    }
                    else
                    if ((mouseChange.x - mouseInputStart.x) < 0.0f)
                    {
                        Debug.Log("Right");




                        player.transform.right = Vector3.up;
                        player.transform.up = Vector3.left;
                        //player.transform.forward = Vector3.forward;
                        mouseDrag = false;
                        lockOut = 0;
                    //    player.transform.position += new Vector3(Time.deltaTime, 0.0f, 0.0f);
                    }
                }
                else
                    Debug.Log("Y CHANGE");



            }
            */
       /* if (Input.GetMouseButtonUp(0))
        {

            mouseDrag = true;
            mouseInputFinish = Input.mousePosition;

                Debug.Log(mouseInputStart + " " + mouseInputFinish);

                Vector3 mouseDiff = mouseInputFinish - mouseInputStart;

                if (Mathf.Abs(mouseDiff.x) > Mathf.Abs(mouseDiff.y))
                    changeX = true;
                
                  
        }


            if (mouseDrag)
            {
                if (!changeX)
                {
                    if ((mouseInputFinish.y - mouseInputStart.y) > 3.0f)
                    {


                        player.transform.up = Vector3.up;
                        player.transform.right = Vector3.right;
                        //player.transform.forward = Vector3.forward;
                        Debug.Log("Down");
                        mouseDrag = false; lockOut = 0;
                   //     yAcceleration = 1.5f;
                     //   yVelocity = 0.01f;
                        player.transform.position += new Vector3(0.0f, (yVelocity), 0.0f);
                    }
                    else
                    if ((mouseInputFinish.y - mouseInputStart.y) < -3.0f)
                    {


                        Debug.Log("Up");
                      
                        player.transform.up = Vector3.down;
                      //  player.transform.right = new Vector3(1, 0, 0);
                        //player.transform.forward = Vector3.forward;
                        mouseDrag = false; lockOut = 0;

                    }
                }
                else if (changeX)
                {

                    if ((mouseInputFinish.x - mouseInputStart.x) > 3.0f)
                    {
                        Debug.Log("Left");


                        player.transform.right = Vector3.down;
                        player.transform.up = Vector3.right;
                        //player.transform.forward = Vector3.forward;
                        mouseDrag = false; lockOut = 0;
                        player.transform.position -= new Vector3(Time.deltaTime * 10, 0.0f, 0.0f);
                    }
                    else
                    if ((mouseInputFinish.x - mouseInputStart.x) < -3.0f)
                    {
                        Debug.Log("Right");




                        player.transform.right = Vector3.up;
                        player.transform.up = Vector3.left;
                        //player.transform.forward = Vector3.forward;
                        mouseDrag = false;
                        lockOut = 0;
                        player.transform.position += new Vector3(Time.deltaTime * 10, 0.0f, 0.0f);
                    }
                }
            }*/
        }
    }

