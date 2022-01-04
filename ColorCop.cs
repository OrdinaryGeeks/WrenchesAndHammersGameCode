using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ColorCop : MonoBehaviour
{
    public static int Health;
    public GameObject bullet;
    public SkinnedMeshRenderer skin;
    Ray yRay;
    NavMeshAgent navMeshAgent;
    float oldDistanceToDestination;
    float distanceToDestination;
    Vector3 targetDestination;
    public Transform runnerTransform;
    public LayerMask interactable;
    Animator anim;

    float shootTime;
    bool shooting;
    public enum AIStates
    {
        patrol, chase, attack, none, aim
    }

    public enum AnimState
    {

        run, shoot, hurt, atk, idle, none

    }

    AnimState animState = AnimState.none;

    AIStates aiState = AIStates.patrol;
    // Start is called before the first frame update
    void Start()
    {
        Health = 3;
        shootTime = 0;
        shooting = false;
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();
        interactable = LayerMask.GetMask("Interactable");
        navMeshAgent = GetComponent<NavMeshAgent>();

        
        targetDestination = new Vector3(10, 0.0f, 3.0f);

        navMeshAgent.SetDestination(targetDestination);
        StartRun();
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

    public void StartShoot()
    {
        StopRun();

        shooting = true;
        animState = AnimState.shoot;
        anim.SetTrigger("IsShoot");
    }

    public void StopShoot()
    {
        animState = AnimState.idle;
        //anim.SetBool("IsShoot", false);

    }
    public void OnTriggerStay(Collider other)
    {

        if(aiState == AIStates.patrol)
        if(other.gameObject.tag == "Player")
        {
       //     Debug.Log("Player");
          //  if (Runner.acquireColor)
            {
                aiState = AIStates.chase;
      //          Debug.Log("Acuire");
            }

        }




    }
    // Update is called once per frame
    void Update()
    {
     //   Debug.Log(aiState);
      //  Debug.Log(navMeshAgent.isStopped);
        if (Health < 0)
            Destroy(gameObject);
        Transform[] transforms = GetComponentsInChildren<Transform>();
        int transCount = transforms.Length;
        //    Debug.Log(navMeshAgent.isStopped);
        if (shooting)
        { 
            shootTime += Time.deltaTime;
        
            if (shootTime > (8.0f / 30.0f))
            {
               // Debug.Log(transforms[transCount - 1].name);
                Instantiate(bullet, transforms[transCount-1].position, transforms[transCount-1].rotation);
                shooting = false;
                shootTime = 0;
            }


        }
      //  Debug.Log(skin);
        //Debug.Log(skin.localToWorldMatrix);
     //   Debug.Log(anim.GetBoneTransform(HumanBodyBones.RightHand).position);
        //Debug.Log()
  //      Debug.Log(skin.transform.TransformPoint(anim.GetBoneTransform(HumanBodyBones.RightHand).position));
        //Debug.Log(skin.worldToLocalMatrix.MultiplyPoint(anim.GetBoneTransform(HumanBodyBones.RightHand).position));
        if (aiState == AIStates.attack && Vector3.Distance(runnerTransform.position, transform.position) > 3.0f)
        {

            aiState = AIStates.chase;
        }

        if (animState == AnimState.run)
            if (aiState == AIStates.attack)
                animState = AnimState.shoot;
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
            //   Debug.Log("Chasing");

            navMeshAgent.SetDestination(runnerTransform.position);


            if (navMeshAgent.remainingDistance <= 3.0f)
            {

                aiState = AIStates.aim;
                animState = AnimState.idle;
                navMeshAgent.isStopped = true;
            }
            else if(navMeshAgent.isStopped)
            {
                navMeshAgent.isStopped = false;

            }

        }
        else if(aiState == AIStates.aim)
        {
            transform.LookAt(new Vector3(runnerTransform.position.x, transform.position.y, runnerTransform.position.z));
            aiState = AIStates.attack;

        }
        else if(aiState == AIStates.attack)
        {

            //  Debug.Log("Attacking");
            //    Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"));
            //      Debug.Log(animState);


            transform.LookAt(new Vector3(runnerTransform.position.x, transform.position.y, runnerTransform.position.z));

            if (animState == AnimState.idle)
            StartShoot();



            else  if(animState == AnimState.shoot &! anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
        //        Debug.Log("shooting");
                StartShoot();

            }


        }
        

        
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
           // Debug.Log(targetDestination);
        }

    }
}
