using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninjaneer : MonoBehaviour
{
    public Canvas Canvas;
    public static Vector3 position;
    public static Color playerColor;
    public Text RedText;
    public Text GreenText;
    public Text BlueText;
    public Slider MovingHealthBar;
    public int gunAmmo;
    public int swordAmmo;

    LayerMask Environment;

    public static bool isAttacking;
    public bool startSlashAnim;
    public static bool AttackColorNinja1;
    public static bool AttackColorCop1;

    Transform[] transforms;
    public GameObject Bullet;
    float shootTime;
    bool shooting;
    public GameObject Sword;
    public GameObject Gun;
    int currentWeaponIndex;
    public Text CurrentWeaponText;
    public List<string> equipment;
    public bool hasSword;
    public bool hasGun;
    public int ammoCount = 0;
    Ray gravityRay;
    public float maxSpeed;
    public float currentSpeed;
    public static int DamageTaken;
    public Slider Health;
    public Vector3 oldPosition;
    public static bool acquireColor;
    public Animator anim;
    public bool CanWallJump;
    public float doubleJumpMaxHeight;
    public float moveSpeed;
    float oldDistanceToDestination;
    float distanceToDestination;
    public Vector3 moveLockDirection;
    public bool movementLocked;
    public bool jumpLocked;
    public Vector3 targetDestination;
    public Material mainMaterial;
    public Material maskMaterial;
    public GameObject checkWallCollisions;
    public GameObject character;
    // Start is called before the first frame update
    public float jumpForce;
    public bool CanJump;
    public bool EndJump;
    public float maxJump;
    public float maxJumpHeight;
    public float startJumpHeight;
    public bool startMouseDrag;
    public Vector3 mouseInputStart;
    public Vector3 mouseInputFinish;
    public Vector2 Direction;
    public Vector2 touchInputStart;
    public Vector2 touchInputFinish;
    public bool mouseDrag;
    public float lockOut;
    public float yVelocity;
    public float yAcceleration;
    public bool Button0Down;
    Vector3 moveDirection;
    public bool startJump;
    public Text statusText;
    public Rigidbody RB;
    public CharacterController CC;
    public static float boost;
    public bool whileJump;
    public float rotateSpeed;
    public static float R;
    public static float G;
    public static float B;

    public Slider Red;
    public Slider Green;
    public Slider Blue;
    Ray camRay;
    LayerMask interactable;

    public bool isAtk;
    public bool isKick;
    public bool isHurt;
    public bool isShoot;
    public bool isSlash;
    public Vector3 moveDelta;
    // Start is called before the first frame update
    void Start()
    {

        //Cursor.lockState = CursorLockMode.Locked;

        playerColor = Color.black;
        R = 0;
        G = 0;
        B = 0;

        startSlashAnim = false;

        gunAmmo = 0;
        swordAmmo = 0;
        isAttacking = false;
        AttackColorCop1 = false;
        AttackColorNinja1 = false;
        transforms = GetComponentsInChildren<Transform>();
        Environment = LayerMask.GetMask("Environment");
        shooting = false;
        shootTime = 0;
        anim = GetComponent<Animator>();
        currentWeaponIndex = 0;
        equipment = new List<string>();
        equipment.Add("Unarmed");
        //equipment.Add("Gun");
        // equipment.Add("Sword");
        moveDelta = Vector3.zero;
        maxSpeed = 20;
        DamageTaken = 0;
        oldPosition = transform.position;
        acquireColor = false;
        isAtk = false;
        isKick = false;
        isHurt = false;
        // anim = GetComponent<Animator>();
        doubleJumpMaxHeight = 5;
        moveSpeed = 5;
        moveLockDirection = Vector3.zero;
        interactable = LayerMask.GetMask("Interactable");
        rotateSpeed = 5.0f;
        jumpForce = 0;
        whileJump = false;
        RB = GetComponent<Rigidbody>();
        CC = GetComponent<CharacterController>();
        startMouseDrag = false;
        yAcceleration = 0;
        moveDirection = new Vector3(0, 0, 0);
        maxJump = 3;
        startJumpHeight = 0;
        EndJump = false;
        startJump = false;
        CanJump = true;
        Button0Down = false;
        mouseDrag = false;
        boost = 0;

        CanWallJump = false;
    }

    public void updateStaticPosition()
    {

        position = transform.position;
    }
    public bool checkAnimStart(string animClip)
    {


        return anim.GetCurrentAnimatorStateInfo(0).IsName(animClip);


    }

    public void OnCollisionEnter(Collision collision)
    {


        Debug.Log("Collision");

        if (collision.collider.gameObject.name == "Ramp")
        {
            Color rampColor = collision.collider.gameObject.GetComponent<Renderer>().material.color;
            if (R < rampColor.r || B < rampColor.b || G < rampColor.g)
            {
                transform.position = oldPosition;
            }
        }
        if (collision.collider.gameObject.name == "LeftWall")
        {
            Debug.Log("LeftWall");

        }
        if (collision.collider.gameObject.name == "RightWall")
        {
            Debug.Log("LeftWall");

        }
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Sphere"))
        {
            //     Debug.Log("Sphere");
            //    character.GetComponent<Renderer>().material = maskMaterial;
            //    character.GetComponent<Renderer>().material.color += other.gameObject.GetComponent<Renderer>().material.color;
        }

    }

    public void ChangeCurrentWeapon(int change)
    {


        currentWeaponIndex += change;


        if (currentWeaponIndex < 0)
            currentWeaponIndex = equipment.Count + (currentWeaponIndex % equipment.Count);
        else
            currentWeaponIndex %= equipment.Count;



        if (equipment[currentWeaponIndex] == "Sword")
        {
            CurrentWeaponText.text = equipment[currentWeaponIndex] + " " + ammoCount;
            UnEquipGun();
            EquipSword();
        }
        if (equipment[currentWeaponIndex] == "Gun")
        {
            CurrentWeaponText.text = equipment[currentWeaponIndex] + " " + ammoCount;
            UnEquipSword();
            EquipGun();
        }
        if (equipment[currentWeaponIndex] == "Unarmed")
        {
            CurrentWeaponText.text = equipment[currentWeaponIndex];
            UnEquipSword();
            UnEquipGun();

        }


    }

    public void EquipGun()
    {
        Gun.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }
    public void UnEquipGun()
    {
        Gun.GetComponent<SkinnedMeshRenderer>().enabled = false;

    }
    public void EquipSword()
    {
        Sword.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }

    public void UnEquipSword()
    {
        Sword.GetComponent<SkinnedMeshRenderer>().enabled = false;

    }

    public bool CheckAnimPlaying(string animClip)
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            Debug.Log("Idle");

        return anim.GetCurrentAnimatorStateInfo(0).IsName(animClip);

    }

    public void setAtkTrigger()
    {
        isAtk = true;
        isSlash = false;
        isShoot = false;
        anim.SetTrigger("IsAtk");
        anim.ResetTrigger("IsSlash");
        anim.ResetTrigger("IsShoot");

    }
    public void setShootTrigger()
    {
        isShoot = true;
        isAtk = false;
        isSlash = false;
        anim.ResetTrigger("IsAtk");
        anim.ResetTrigger("IsSlash");
        anim.SetTrigger("IsShoot");

    }
    public void setSlashTrigger()
    {
        Debug.Log("Setting slash to true");
        isAttacking = true;
        isSlash = true;
        isShoot = false;
        isAtk = false;
        anim.ResetTrigger("IsAtk");
        anim.SetTrigger("IsSlash");
        anim.ResetTrigger("IsShoot");

    }
    public void processAttack()
    {

        if (!isAtk & !isSlash & !isShoot)
        {
            if (equipment[currentWeaponIndex] == "Unarmed")
            {

                setAtkTrigger();

            }
            if (equipment[currentWeaponIndex] == "Gun")
            {
                setShootTrigger();
                shooting = true;

            }
            if (equipment[currentWeaponIndex] == "Sword")
            {
                setSlashTrigger();
            }

        }
    }
    public void checkAllAnims()
    {
        if (isAtk)
        {
            if (!CheckAnimPlaying("Atk"))
            {
                isAtk = false;
            }
        }
        if (isShoot)
        {
            if (!CheckAnimPlaying("Shoot"))
            {
                isShoot = false;
            }

        }
        if (isSlash)
        {
            if (!startSlashAnim)
                startSlashAnim = checkAnimStart("Slash");

            if (startSlashAnim)
                if (!CheckAnimPlaying("Slash"))
                {
                    PopUpAttack.spawnButton = true;
                    Debug.Log("IsAttacking = false");
                    isAttacking = false;
                    isSlash = false;
                    AttackColorNinja1 = false;
                    AttackColorCop1 = false;
                    startSlashAnim = false;
                }
        }



    }

    public void updateShootingTimer()
    {

        if (shooting)
        {

            int transCount = transforms.Length;
            shootTime += Time.deltaTime;
            Debug.Log("In Update Shooting Timer");
            if (shootTime > (8.0f / 30.0f))
            {
                Debug.Log(transforms[transCount - 5].name);
                Instantiate(Bullet, transforms[transCount - 5].position, transforms[transCount - 5].rotation);

                ChooseColor chooseColor = Bullet.GetComponentsInChildren<ChooseColor>()[0];

                chooseColor.setColor(R, G, B);
                shooting = false;
                shootTime = 0;
            }


        }
    }

    public void updateColorTextHud()
    {
        RedText.text = R.ToString();
        BlueText.text = B.ToString();
        GreenText.text = G.ToString();
    }

    public void getPlayerDirection()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit EnvironmentHit;
        if (Physics.Raycast(camRay, out EnvironmentHit, 10000, Environment))
        {
            //   movingState = MovingStates.Move;

            targetDestination = new Vector3(EnvironmentHit.point.x, transform.position.y, EnvironmentHit.point.z);
            transform.LookAt(targetDestination);

        }

    }

    public void updateHealth()
    {
        Health.value += DamageTaken;
        DamageTaken = 0;

    }
    public void updatePlayerColor()
    {

        playerColor.r = R;
        playerColor.g = G;
        playerColor.b = B;

    }

    public void updateScrewable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit EnvironmentHit;
            if (Physics.Raycast(camRay, out EnvironmentHit, 10000, Environment))
            {
                //   movingState = MovingStates.Move;

                if (EnvironmentHit.collider.gameObject.name.Contains("Screw"))
                    EnvironmentHit.collider.gameObject.GetComponent<Screwable>().Slider.value -= 1;


                targetDestination = new Vector3(EnvironmentHit.point.x, transform.position.y, EnvironmentHit.point.z);
                transform.LookAt(targetDestination);

            }
        }


    }

    public void updateMovingHealth()
    {


        //this is the ui element
        RectTransform UI_Element;

        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = Canvas.GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        MovingHealthBar.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;




    }
    void Update()
    {
        updateScrewable();
        updateMovingHealth();
        updateStaticPosition();

        updatePlayerColor();

        updateHealth();

        updateColorTextHud();

        getPlayerDirection();


        if (shooting)
            updateShootingTimer();


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ChangeCurrentWeapon(1);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ChangeCurrentWeapon(-1);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            ChangeCurrentWeapon(1);

        }



        //{ Debug}
        // if (startJump)
        {

            //  if (CheckWallCollisions.LW || CheckWallCollisions.RW)
            {

                //   
            }


        }
        if (movementLocked)
        {
            moveDirection = moveLockDirection;
            //if (!atDestination())
            {
                //  moveDirection = moveLockDirection;

                //                moveDirection.y = jumpForce;

            }
            //          else
            {
                //            movementLocked = false;
            }

            moveDirection.y = jumpForce;

        }
        else
        {

            if (Input.GetAxis("Vertical") >= 1.0f)
            {
                //    Debug.Log(currentSpeed);
                currentSpeed += Time.deltaTime;

                if (currentSpeed > maxSpeed)
                    currentSpeed = maxSpeed;
            }
            else
                if (Input.GetAxis("Vertical") <= 0.0f)
            {
                currentSpeed = 5;
            }

            //  Debug.Log(transform.forward + " is forward");
            moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

            if (Input.GetAxis("Vertical") >= 1.0f || Input.GetAxis("Horizontal") >= 1.0f)
                anim.SetBool("IsRun", true);
            else
                anim.SetBool("IsRun", false);
            //   moveDirection.y = jumpForce;
        }


        moveDelta = moveDirection * currentSpeed;
        moveDelta.y = jumpForce;
        // if (!jumpLocked)
        //  RB.position+= (moveDirection * Time.deltaTime * moveSpeed);
        CC.Move(moveDelta * Time.deltaTime);
        //   else
        //    RB.position += (moveDirection * Time.deltaTime * (moveSpeed / 2));
        //        CC.Move(moveDirection * Time.deltaTime * (moveSpeed / 2));


        /*    if (jumpLocked)
            {

                if(!Input.GetMouseButton(0) &! Input.GetKey(KeyCode.Space))
                {
                    EndJump = true;
                }
                if (!EndJump)
                {
                    jumpForce = 20;
                    //RB.velocity += new Vector3(0.0f,, 0.0f);
                    mouseDrag = false;

                    if (transform.position.y > maxJumpHeight)
                    {
                        EndJump = true;

                    }


                }
                else
                {
                    jumpForce = -2.0f;

                }




            }


            gravityRay = new Ray(transform.position, Vector3.down);
            // if(Physics.Raycast(gravityRay, 1.10f, interactable))
            if ((CC.collisionFlags & CollisionFlags.Below) != 0)
            {
                movementLocked = false;
            //    Debug.Log("On Ground");
                CanJump = true;
                EndJump = false;
                whileJump = false;
                startJump = false;
                if (jumpLocked)
                {
                    //  anim.SetBool("IsJump", false);
                    movementLocked = false;
                    jumpLocked = false;
                    anim.SetBool("IsJump", false);
                }

                if (!movementLocked)
                    if (Input.GetAxis("Vertical") < 1.0)
                        anim.SetBool("IsRun", false);

                jumpForce = 0;

            }
            else
                jumpForce += -5;

            */

        /*   if ((CC.collisionFlags & CollisionFlags.Below) != 0)
           {
               CanJump = true;
               EndJump = false;
               whileJump = false;
               startJump = false;
               if(jumpLocked)
               {
                 //  anim.SetBool("IsJump", false);
                   movementLocked = false;
                   jumpLocked = false;
                   anim.SetBool("IsJump", false);
               }

               if (!movementLocked)
                   if (Input.GetAxis("Vertical") < 1.0)
                       anim.SetBool("IsRun", false);

           }
           */
        // transform.up = Vector3.up;
        //    checkWallCollisions.transform.position = new Vector3(transform.position.x - 0.6497991f, transform.position.y, transform.position.z + 1.1f);

        //  lockOut += Time.deltaTime;
        Vector3 rot = transform.rotation.eulerAngles;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            //  Debug.Log("got Left arrow");
            transform.Rotate(0.0f, Input.GetAxis("Horizontal") * rotateSpeed, 0.0f);
            CC.transform.rotation = transform.rotation;
            // transform.rotation= Quaternion.Euler(new Vector3(rot.x - Time.deltaTime, 0.0f, 0.0f));
            //   transform.position += new Vector3(-Time.deltaTime * 3, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = Vector3.zero;
            CanJump = true;
            startJump = false;
            EndJump = false;
            movementLocked = false;
            jumpLocked = false;


        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //   transform.rotation = Quaternion.Euler(new Vector3(rot.x + Time.deltaTime, 0.0f, 0.0f));
            // CC.transform.rotation = transform.rotation;
            //transform.position += new Vector3(Time.deltaTime * 3, 0.0f, 0.0f);
        }

        checkAllAnims();




        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanJump)
            {
                if (!startJump & !EndJump)
                {
                    movementLocked = true;
                    moveLockDirection = moveDirection;
                    CanJump = false;
                    anim.SetBool("IsJump", true);
                    anim.SetBool("IsRun", false);
                    startJumpHeight = transform.position.y;
                    maxJumpHeight = startJumpHeight + maxJump;
                    startJump = true;
                    jumpLocked = true;

                    jumpForce = 5;
                    //RB.velocity += new Vector3(0.0f,, 0.0f);
                    mouseDrag = false;
                }
                else if (!EndJump)
                {
                    //jumpLocked = true;

                    jumpForce = 5;
                    //RB.velocity += new Vector3(0.0f,, 0.0f);
                    mouseDrag = false;

                    if (transform.position.y > maxJumpHeight)
                        EndJump = true;


                }
                else
                {
                    jumpForce = 0;
                }
            }







        }

        #region Button1 jump

        if (Input.GetMouseButtonDown(1))
        {
            processAttack();




            #region Button 1 old jump
            if (startJump)
            {


                //if startjump is here you can double jump if you are on a wall


                if (CheckWallCollisions.LW || CheckWallCollisions.RW)
                {

                    camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit wallHit;
                    if (Physics.Raycast(camRay, out wallHit, 10000, interactable))
                    {

                        movementLocked = true;

                        moveLockDirection = wallHit.point - transform.position;
                        moveLockDirection.y = 0;
                        jumpLocked = true;



                    }

                }

            }
            else
            {
                camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit environmentHit;
                if (Physics.Raycast(camRay, out environmentHit, 10000, interactable))
                {

                    if (environmentHit.collider.gameObject.name.Contains("LeftWall"))
                        targetDestination = environmentHit.point;


                    if (environmentHit.collider.gameObject.name.Contains("RightWall"))
                        targetDestination = environmentHit.point;


                    if (environmentHit.collider.gameObject.name.Contains("LeftWall"))
                        targetDestination = environmentHit.point;

                    float yDiff = transform.position.y - targetDestination.y;

                    if (yDiff < -2)
                    {
                        if (CanJump)
                        {

                            jumpLocked = true;
                            startJumpHeight = transform.position.y;
                            maxJumpHeight = startJumpHeight + maxJump;
                            startJump = true;

                            jumpForce = 5;
                            //RB.velocity += new Vector3(0.0f,, 0.0f);
                            mouseDrag = false;



                        }
                        movementLocked = true;

                        moveLockDirection = targetDestination - transform.position;
                        moveLockDirection.y = 0;
                    }

                    //   targetDestination = floorHit.point + new Vector3(0.0f, 2.0f, 0.0f);
                    //    transform.LookAt(targetDestination);

                    //    Direction = transform.forward;
                    //    oldDistanceToDestination = Vector3.Distance(targetDestination, transform.position);
                    //    distanceToDestination = oldDistanceToDestination;

                }
            }
            #endregion
        }
        #endregion

        /*
        if (Input.GetMouseButton(0))
        {
          //  transform.position = transform.forward * Time.deltaTime;
            // transform.position += new Vector3(-Time.deltaTime, 0.0f, 0.0f);
            if (CanJump)
            {
                if (!startJump & !EndJump)
                {

                    //  CanJump = false;
                    movementLocked = true;
                    moveLockDirection = moveDirection;
                    anim.SetBool("IsJump", true);
                    anim.SetBool("IsRun", false);
                    startJumpHeight = transform.position.y;
                    maxJumpHeight = startJumpHeight + maxJump;
                    startJump = true;

                    jumpForce = 5;
                    //RB.velocity += new Vector3(0.0f,, 0.0f);
                    mouseDrag = false;
                }
                else if (!EndJump)
                {
                    jumpLocked = true;
                   
                    jumpForce = 5;
                    //RB.velocity += new Vector3(0.0f,, 0.0f);
                    mouseDrag = false;

                    if (transform.position.y > maxJumpHeight)
                        EndJump = true;


                }
                else
                {
                    jumpForce = -2;

                }
            }

        }
        else
        {
         //   if(startJump)
           // { 
         //   EndJump = true;
          //  jumpForce = 0;
          //  jumpForce = 0;

          //  EndJump = true;
          //  jumpForce = Physics.gravity.y;

        }
        */
        #region mouseMovement Drags
        /*     if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)

             {
                 mouseDrag = false;
                 mouseInputStart = Input.mousePosition;
                 startMouseDrag = true;
             }
             if (Input.GetMouseButton(0))
             {
                 if (Input.GetMouseButton(0) && startMouseDrag)
                 {

                     moveDirection = Vector3.zero;
                     // Button0Down = true;
     //
              
                     Vector3 mouseChange = Input.mousePosition;
                     Vector3 mouseDiff = mouseChange - mouseInputStart;

                     if (Mathf.Abs(mouseDiff.x) > Mathf.Abs(mouseDiff.y))
                         changeX = true;

                     if (changeX)
                     {

                         if ((mouseChange.x - mouseInputStart.x) > 3.0f)
                         {
                   

                             //   player.transform.right = Vector3.down;
                             // player.transform.up = Vector3.right;
                             //player.transform.forward = Vector3.forward;
                             mouseDrag = false; lockOut = 0;


                             moveDirection = new Vector3(-10, 0.0f, 0.0f);

                             //    RB.velocity += new Vector3(-Time.deltaTime, 0.0f, 0.0f);
                             //          player.transform.position -= new Vector3(Time.deltaTime, 0.0f, 0.0f);
                         }
                         else
                         if ((mouseChange.x - mouseInputStart.x) < -3.0f)
                         {
            


                             // player.transform.right = Vector3.up;
                             // player.transform.up = Vector3.left;
                             //player.transform.forward = Vector3.forward;
                             mouseDrag = false;
                             lockOut = 0;
                             moveDirection = new Vector3(10, 0.0f, 0.0f);
                             // RB.velocity += new Vector3(Time.deltaTime, 0.0f, 0.0f);
                             //player.transform.position += new Vector3(Time.deltaTime, 0.0f, 0.0f);
                         }
                         else
                         {

                             moveDirection = Vector3.zero;
                         }
                     }
                     else
                     {

                             if ((mouseChange.y - mouseInputStart.y) < -3.0f)
                             {

                       
                             if (CanJump)
                             {
                                 if (!startJump & !EndJump)
                                 {

                                     startJumpHeight = transform.position.y;
                                     maxJumpHeight = startJumpHeight + maxJump;
                                     startJump = true;

                                     moveDirection.y += 50 + Physics.gravity.y;
                                     //RB.velocity += new Vector3(0.0f,, 0.0f);
                                     mouseDrag = false;
                                 }
                                 else if (!EndJump)
                                 {
                                     moveDirection.y += 50 + Physics.gravity.y;
                                     //RB.velocity += new Vector3(0.0f,, 0.0f);
                                     mouseDrag = false;

                                     if (transform.position.y > maxJumpHeight)
                                         EndJump = true;


                                 }
                                 else
                                 {
                                     moveDirection.y = Physics.gravity.y;

                                 }


                                 lockOut = 0;

                             }
                             else
                             {

                                moveDirection.y = Physics.gravity.y;
                             }
                         }



                     }




                 }
             }
             else
             {
                 moveDirection = new Vector3(0.0f,Physics.gravity.y, 0.0f);

             }
             //   if (lockOut > 1)

        */

        #endregion
        #region Touches
        /*
        if (Input.touchCount > 0)
                 {
                     float positiveX, positiveY;
                     statusText.text = "GOT A TOUCH";
                     Touch touch = Input.GetTouch(0);

                     // Handle finger movements based on TouchPhase
                     switch (touch.phase)
                     {
                         //When a touch has first been detected, change the message and record the starting position
                         case TouchPhase.Began:
                             // Record initial touch position.
                             touchInputStart = touch.position;
                             break;

                         //Determine if the touch is a moving touch
                         case TouchPhase.Moved:
                             // Determine direction by comparing the current touch position with the initial one
                             Direction = touch.position - touchInputStart;

                             Direction = touch.position - touchInputStart;

                             positiveX = Mathf.Abs(Direction.x);
                             positiveY = Mathf.Abs(Direction.y);

                             if (transform.position.x < -2.5f)
                             {
                                 transform.position = new Vector3(-2.5f, 1.0f, transform.position.z);

                                 transform.up = Vector3.right; ;


                             }
                             if (positiveX > positiveY)
                             {
                                 if (Direction.x > 0)
                                 {
                                     //      DraggedDirection.Right;

                                     //  transform.right = Vector3.up;
                                     //  transform.up = Vector3.left;
                                     //transform.forward = Vector3.forward;

                                     transform.position -= new Vector3(Time.deltaTime, 0.0f, 0.0f);

                                 }
                                 else //DraggedDirection.Left;
                                 {


                                     //  transform.right = Vector3.down;
                                     //  transform.up = Vector3.right;
                                     // transform.forward = Vector3.forward;

                                     transform.position += new Vector3(Time.deltaTime, 0.0f, 0.0f);
                                 }
                             }
                             else
                             {
                                 if (Direction.y > 0)
                                 {
                                     // transform.up = Vector3.up;
                                     // transform.right = new Vector3(-1, 0, 0);
                                     //  transform.forward = Vector3.forward;

                                     yAcceleration = 1.0f;
                                     // transform.position += new Vector3(0.0f, yAcceleration * Time.deltaTime, 0.0f);
                                 }

                                 else
                                 {


                                     //transform.up = Vector3.down;
                                     //transform.right = Vector3.right;
                                     //    transform.forward = Vector3.forward;

                                 }

                             }

                             break;

                         case TouchPhase.Ended:
                             // Report that the touch has ended when it ends
                             touchInputFinish = touch.position;

                             Direction = touch.position - touchInputStart;

                             positiveX = Mathf.Abs(Direction.x);
                             positiveY = Mathf.Abs(Direction.y);

                             if (positiveX > positiveY)
                             {
                                 if (Direction.x > 0)
                                 {
                                     //      DraggedDirection.Right;

                                     transform.right = Vector3.up;
                                     transform.up = Vector3.left;
                                     //transform.forward = Vector3.forward;


                                     transform.position -= new Vector3(Time.deltaTime * 10, 0.0f, 0.0f);

                                 }
                                 else //DraggedDirection.Left;
                                 {


                                     transform.right = Vector3.down;
                                     transform.up = Vector3.right;
                                     // transform.forward = Vector3.forward;
                                     transform.position += new Vector3(Time.deltaTime * 10, 0.0f, 0.0f);
                                 }
                             }
                             else
                             {
                                 if (Direction.y > 0)
                                 {
                                     transform.up = Vector3.up;
                                     transform.right = new Vector3(-1, 0, 0);
                                     //  transform.forward = Vector3.forward;
                                 }

                                 else
                                 {


                                     transform.up = Vector3.down;
                                     transform.right = Vector3.right;
                                     //    transform.forward = Vector3.forward;

                                 }

                             }

                             break;
                     }
                 }*/
        #endregion

        //    Debug.Log(transform.rotation);
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
}
