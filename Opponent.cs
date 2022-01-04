using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Opponent : MonoBehaviour
{
    // Start is called before the first frame update

    float turnTime;
    public int Health = 10;
  
    NavMeshAgent nav;
    float radius;
    LineRenderer lineR;
    public Vector3 dest;
    public GameObject Bullet;

    int shots;
    float shootTime;

    enum AIStates {ChoosePosition, ChooseBox, ControlBoxes, Attack}
    AIStates aiState;

    enum AttackStates { Aim, Range, Attack}
    AttackStates attackState;
    void Start()
    {
        shots = 1;
        turnTime = 3;
        attackState = AttackStates.Aim;
        aiState = AIStates.ChoosePosition;
        nav = GetComponent<NavMeshAgent>();
        radius = 5;
        lineR = GetComponent<LineRenderer>();
        lineR = GetComponent<LineRenderer>();
        lineR.startWidth = (0.05f);
        lineR.endWidth = (0.05f);
        lineR.positionCount = (21);
        lineR.useWorldSpace = false;
        //randomDest();
        CreatePoints();
    }

    void CreatePoints()
    {

        float x;
        float y;
        float z;

        float angle = 0.0f;

        for (int i = 0; i < 21; i++)
        {

            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineR.SetPosition(i, new Vector3(x, .5f, z));

            angle += (360.0f / 20);

        }


    }

    void ChoosePosition()
    {

        randomDest();
    }
    void randomDest()
    {

         dest = Vector3.zero + new Vector3(Random.Range(-7.0f, 7.0f), 0.0f, Random.Range(-7.0f, 7.0f));
      //  print("New dest is " + dest);
        nav.SetDestination(dest);
    }

    public void ChooseABox()
    {

        if(WrenchesAndHammers.robots != null)
        if (WrenchesAndHammers.robots.Count > 0)
            foreach (GameObject robot in WrenchesAndHammers.robots)
                if (Vector3.Distance(robot.transform.position, transform.position) < radius)
        {
                    robot.GetComponent<Robot>().selected = true;
                  //  robot.GetComponent<Renderer>().material.color = Color.yellow;

                    ChooseNewState();
                    break;

        }


    }
    public void ControlBoxes()
    {
        turnTime -= Time.deltaTime * 3;
        if(WrenchesAndHammers.robots.Count > 0)
        foreach(GameObject robot in WrenchesAndHammers.robots)
        {

            if (Vector3.Distance(robot.transform.position, transform.position) < radius)
            {

                robot.GetComponent<Renderer>().material.color = Color.red;
                robot.transform.rotation *= Quaternion.Euler(0.0f, -1.0f, 0.0f);
            }

        }

        if (turnTime <= 0)
            ChooseNewState();

    }

    public void ChooseNewState()
    {
        System.Random random = new System.Random();
        int decision = random.Next(0, 8);

        if (decision == 0)
            aiState = AIStates.ChoosePosition;
        else if (decision == 1)
            aiState = AIStates.ChooseBox;
        else if (decision == 2)
            aiState = AIStates.ControlBoxes;
        else
        {
            aiState = AIStates.Attack;
            shots = random.Next(1, 4);
        }

       // print(aiState);

    }


    public void Attack()
    {

        if (aiState == AIStates.Attack)
        {
            shootTime += Time.deltaTime;
            if (shootTime > 2)
            {
                if (attackState == AttackStates.Aim)
                {

                    nav.enabled = false;

                    transform.LookAt(WrenchAndHammer.currentPosition);
                    attackState = AttackStates.Attack;
                }
                else if (attackState == AttackStates.Attack)
                {

                    Instantiate(Bullet, transform.position, transform.rotation);
                    nav.enabled = true;

                    shots--;
                    if (shots == 0)
                    {
                        attackState = AttackStates.Aim;
                        ChooseNewState();
                        shootTime = 0;
                    }
                    else
                    {
                        shootTime = 0;
                        attackState = AttackStates.Aim;
                    }
                }

                
            }
        }


    }

  
    public void CheckChoosePosition()
    {
        if(nav.enabled)
        if (nav.isStopped || nav.path.status == NavMeshPathStatus.PathComplete )
        {
            //Random random = new Random();
            ChooseNewState();


        }
    }
    public void CheckControlBoxes()
    {



    }
    public void CheckSelectBox()
    {



    }

    // Update is called once per frame
    void Update()
    {
    //    print(Health);
        if (Health < 0)
        {

            Destroy(this.gameObject);
        }
        else
        {
            CheckChoosePosition();
            turnTime += Time.deltaTime;
            CreatePoints();

            //  print(aiState);
            if (aiState == AIStates.ChoosePosition)
            {
                randomDest();

            }
            else if (aiState == AIStates.ControlBoxes)
                ControlBoxes();
            else if (aiState == AIStates.ChooseBox)
                ChooseABox();
            else if (aiState == AIStates.Attack)
            {

                Attack();
            }
        }
      //  print(nav.isStopped);
     //   print(dest);
   

        
    }
}
