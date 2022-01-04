using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ColorNinja : MonoBehaviour
{

    public static bool isAttacking;
    //public int DamageTaken;
    public static int Health;
    public static Vector3 healthViewPos;
    float jumpTime;
    float elapsedJumpTime;
    bool startJump;
    bool endJump;
    float yVelocity;
    float xZVelocity;
    public float jumpXZDistance;
    public float jumpYDistance;
    public bool jumping;
    public Rigidbody RB;
    public Vector3 jumpDestination;
    public static bool AttackPlayer;
    Ray yRay;
    NavMeshAgent navMeshAgent;
    float oldDistanceToDestination;
    float distanceToDestination;
    Vector3 targetDestination;
    public Transform runnerTransform;
    public LayerMask interactable;
    Animator anim;


    //public enum 
    public enum GoToC2
    {
        chooseC2, goToC2, none, arrived, chooseDescend, decend

    }
    public enum GoToC1
    {
        chooseB1, goToB1, chooseC1, goToC1, arrived, none, chooseDescend, descend

    }
    public enum AIStates
    {
        patrol, chase, attack, none, aim
    }

    public enum AnimState
    {

        run, slash, hurt, atk, idle, none

    }


    public bool isGoToC2;
    public GoToC2 goToC2;
    public bool isGoToC1;
    public GoToC1 goToC1;

    AnimState animState = AnimState.none;

    AIStates aiState = AIStates.patrol;
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        Health = 3;
        goToC2 = GoToC2.none;
        elapsedJumpTime = 0;
        yVelocity = 0;
        xZVelocity = 0;
        jumpXZDistance = 0;
        jumpYDistance = 0;
        jumping = false;
        RB = GetComponent<Rigidbody>();
        jumpDestination = Vector3.zero;
        goToC1 = GoToC1.chooseB1;
        isGoToC1 = true;
        anim = GetComponent<Animator>();
        interactable = LayerMask.GetMask("Interactable");
        navMeshAgent = GetComponent<NavMeshAgent>();

        targetDestination = new Vector3(10, 0.0f, 3.0f);

        navMeshAgent.SetDestination(targetDestination);
        StartRun();

        AttackPlayer = false;
    }

    public void StartRun()
    {
        animState = AnimState.run;
        anim.SetBool("IsRun", true);

    }

    public void StopRun()
    {

        anim.SetBool("IsRun", false);
        animState = AnimState.idle;

    }

    public void StartSlash()
    {
        StopRun();
        isAttacking = true;
        animState = AnimState.slash;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
            anim.SetTrigger("IsSlash");
    }

    public void StopSlash()
    {
        animState = AnimState.idle;
        isAttacking = false;
        //anim.SetBool("IsShoot", false);

    }
    public void OnTriggerStay(Collider other)
    {

        if (aiState == AIStates.patrol)
            if (other.gameObject.tag == "Player")
            {
                //     Debug.Log("Player");
                //  if (Runner.acquireColor)
                {
                    aiState = AIStates.chase;
                    //          Debug.Log("Acuire");
                }

            }

        if (aiState == AIStates.attack && Vector3.Distance(runnerTransform.position, transform.position) > .7f)
        {

            aiState = AIStates.chase;
        }




    }
    // Update is called once per frame
    void Update()
    {

        if (Health < 0)
            Destroy(gameObject);
        healthViewPos = Camera.main.WorldToViewportPoint(transform.position);

/*
        if (goToC2 == GoToC2.chooseC2)
        {

            chooseC2();
            jumping = false;
            startJump = false;

        }
        else if (goToC2 == GoToC2.goToC2)
        {
            Debug.Log(" Go To c2");

            if (!jumping)
            {


                navMeshAgent.isStopped = true;
                jump();
            }
            else
                jump();



        }
        else if (goToC1 == GoToC1.chooseB1)
        {

            chooseB1();
        }

        else if (goToC1 == GoToC1.goToB1)
        {
            //   Debug.Log(targetDestination);

            //if (!jumping)
            {
                //       Debug.Log(navMeshAgent.remainingDistance);
                if (navMeshAgent.remainingDistance <= 3.0f)
                {

                    goToC1 = GoToC1.chooseC1;




                }
                //  if (jumping)
                //    jump();
                //    else
                //       jump();
            }
            // else
            //    jump();
        }

        else if (goToC1 == GoToC1.chooseC1)
        {
            Debug.Log("Choose C1");

            chooseC1();



        }
        else if (goToC1 == GoToC1.goToC1)
        {
            Debug.Log(" Go To c1");
            if (!jumping)
            {


                navMeshAgent.isStopped = true;
                jump();
            }
            else
                jump();


        }
        else if (goToC1 == GoToC1.arrived)
        {
            //goToC2 = GoToC2.chooseC2;
            goToC1 = GoToC1.descend;

        }
        else if (goToC1 == GoToC1.chooseDescend)
        {

            jumping = false;
            startJump = false;
            getRandomDescend();
            goToC1 = GoToC1.descend;


        }
        else if (goToC1 == GoToC1.descend)
        {
            Debug.Log(" Go To descend");
            if (!jumping)
            {


                navMeshAgent.isStopped = true;
                jump();
            }
            else
                jump();
        }

        */

         {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
            {
                AttackPlayer = false;
                isAttacking = false;
            }
            //    Debug.Log(navMeshAgent.isStopped);


            if (animState == AnimState.run)
                if (aiState == AIStates.attack)
                    animState = AnimState.slash;
            if (aiState == AIStates.patrol)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)

                {

                    getRandomDestination();
                    navMeshAgent.SetDestination(targetDestination);


                }
            }
            else if (aiState == AIStates.chase)
            {
                //  Debug.Log("Chasing");

                if (navMeshAgent.remainingDistance <= 0.70f)
                {

                    aiState = AIStates.attack;
                    animState = AnimState.idle;
                }

                navMeshAgent.SetDestination(runnerTransform.position);
            }
            else if (aiState == AIStates.attack)
            {

                //Debug.Log("Attacking");
                // Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Slash"));
                //   Debug.Log(animState);

                if (animState == AnimState.idle)
                {
                    StartSlash();

                }

                else if (animState == AnimState.slash & !anim.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                {
                    //Debug.Log("shooting");

                    //  AttackPlayer = false;
                    StartSlash();

                }


            }
            }


    }

    // public void 

    public void chooseB1()
    {

        float x = Random.Range(-1.0f, 1.0f);
        float z = Random.Range(-1.0f, 1.0f);

        x += -1.78f;
        float y = 3.0f;
        z += 7.30f;

        targetDestination = new Vector3(x, y, z);
        navMeshAgent.SetDestination(targetDestination);

        goToC1 = GoToC1.goToB1;


    }

    public void chooseC1()
    {

        float x;
        float y;
        float z;

        x = 1.75f;
        z = 7.3f;
        y = 4.5f;

        jumpDestination = new Vector3(x, y, z);

        goToC1 = GoToC1.goToC1;
    }

    public void chooseC2()
    {
        float x;
        float y;
        float z;

        x = 4.8f;
        z = 7.3f;
        y = 4.5f;

        jumpDestination = new Vector3(x, y, z);

        goToC2 = GoToC2.goToC2;

    }
    void jump()
    {
        //var rigid = GetComponent<Rigidbody>();
        if (navMeshAgent.enabled)
        {
            if (!navMeshAgent.isStopped)
                navMeshAgent.isStopped = true;

            navMeshAgent.enabled = false;
        }
       // Debug.Log(startJump);
        if (!startJump)
        {
         //   Debug.Log("startJump");
            startJump = true;
            Vector3 trans = transform.position;
            //Vector3 jumpVec = jumpDestination;
            jumpXZDistance = Vector3.Distance(new Vector3(trans.x, 0.0f, trans.z), new Vector3(jumpDestination.x, 0.0f, jumpDestination.z));
            jumpYDistance = Vector3.Distance(new Vector3(0.0f, trans.y, 0.0f), new Vector3(0.0f, jumpDestination.y, 0.0f));

            jumpYDistance = trans.y - jumpDestination.y;
            //which one decides the time
            float yTime;

            yTime = jumpYDistance / 5.0f;
            /*
            if (jumpYDistance > 0)
            {

                yTime = Mathf.Abs(jumpYDistance / 5.0f);


            }
            else
            {
                yTime = Mathf.Abs(jumpYDistance / 5.0f);

            }
            */
            float xzTime;

            xzTime = (jumpXZDistance / 5.0f);

            if (xzTime > yTime)
            {

                yVelocity = jumpYDistance / xzTime;
                xZVelocity = 5.0f;

                jumpTime = xzTime;

            }

            else
            {
                yVelocity = 20.0f;
                xZVelocity = jumpXZDistance / yTime;
                jumpTime = yTime;
            }
            
        }

        
       // Debug.Log(yVelocity + " " + xZVelocity);
        //Debug.Log(jumpDestination);
        
        transform.LookAt(new Vector3(jumpDestination.x,transform.position.y, jumpDestination.z));
        //transform.up = Vector3.up;
        //Debug.Log(transform.forward);
        //if(!atJumpDestination())
        transform.position += new Vector3(transform.forward.x * xZVelocity * Time.deltaTime, Vector3.up.y * -yVelocity * Time.deltaTime, transform.forward.z * xZVelocity * Time.deltaTime);

        elapsedJumpTime += Time.deltaTime;
        if (elapsedJumpTime > jumpTime)
        {
            jumping = false;
            if(goToC1 == GoToC1.goToC1)
            goToC1 = GoToC1.arrived;
            if(goToC1 == GoToC1.descend)
            {
                navMeshAgent.enabled = true;
                navMeshAgent.isStopped = false;
                goToC1= GoToC1.none;
            }
        }

       /* else
        {

            jumping = false;
            startJump = false;
        }*/
        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }

 
    bool atJumpDestination()
    {
        oldDistanceToDestination = distanceToDestination;
        distanceToDestination = Vector3.Distance(transform.position, jumpDestination);
        //  if (Vector3.Distance(transform.position, targetDestination) < 1.0f)
        //   Debug.Log("Distancer");
        //if (oldDistanceToDestination < distanceToDestination + 0.10f)
        //    Debug.Log("Old");


        if (Vector3.Distance(transform.position, jumpDestination) < 0.3f)                 //|| oldDistanceToDestination < distanceToDestination)
        {
            //  movingState = MovingStates.Open;
            oldDistanceToDestination = 0;
            return true;
        }
        return false;

    }

    bool atDestination()
    {
        //        Vector3 oldDistanceTodestination;
        oldDistanceToDestination = distanceToDestination;
        distanceToDestination = Vector3.Distance(transform.position, targetDestination);
        //  if (Vector3.Distance(transform.position, targetDestination) < 1.0f)
        //   Debug.Log("Distancer");
        //if (oldDistanceToDestination < distanceToDestination + 0.10f)
        //    Debug.Log("Old");
        if (Vector3.Distance(transform.position, targetDestination) < 1.0f)                 //|| oldDistanceToDestination < distanceToDestination)
        {
            //  movingState = MovingStates.Open;
            oldDistanceToDestination = 0;
            return true;
        }
        return false;
    }

    public void getRandomDescend()
    {

        float x = Random.Range(-3, 3);
        float z = Random.Range(-3, 3);


        if (x < 1.5f)
            x = 1.5f;
        if (z < 1.5f)
            z = 1.5f;

        x = 0;
        z = 7.5f;

        x = -6;
        z = 6;
        yRay = new Ray(new Vector3(x, 6, z), Vector3.down);
        RaycastHit environmentHit;
        //Debug.Log("Casting Ray");
        if (Physics.Raycast(yRay, out environmentHit, 10000, interactable))
        {


            jumpDestination = environmentHit.point;
         //   Debug.Log(targetDestination);
        }

    }
    public void getRandomDestination()
    {


        float x = Random.Range(-14, 15);
        float z = Random.Range(-14, 15);

        x = 0;
        z = 7.5f;

        x = -6;
        z = 6;
        yRay = new Ray(new Vector3(x, 6, z), Vector3.down);
        RaycastHit environmentHit;
       // Debug.Log("Casting Ray");
        if (Physics.Raycast(yRay, out environmentHit, 10000, interactable))
        {


            targetDestination = environmentHit.point;
            Debug.Log(targetDestination);
        }

    }
}
