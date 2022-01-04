using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

delegate bool InRange(Vector3 position);
public static class ZeroHeight
{

    public static Vector3 zeroHeight(this Vector3 nonZeroHeight)
    {

        return new Vector3(nonZeroHeight.x, 0.0f, nonZeroHeight.z);
    }

}
public class WrenchAndHammer : MonoBehaviour
{

    public float oldRotateDifference;
    public float rotateDifference;
    public bool Axis2TurnClockwise;
    float oldStayAngle;
    float oldCAngle;
    float CAngle;
    Ray selectionRay;
    bool wasShortMag;
    public float oldHorizontal;
    public float horizontal;
    public float oldVertical;
    public float vertical;
    public Text RedText;
    public Text GreenText;
    public Text BlueText;
    public bool LTrigDown;
    public bool RTrigDown;
    public GameObject options;
    public static int damageTaken;
    public Slider health;
    public static Vector3 currentPosition;
    public Image Attack;
    public Image Build;
    public Image Combine;
    public Image Hack;
    public Image ArcWrench;
    public Image Select;
    public Camera cineCamera;
    Transform[] transforms;
    public GameObject Bullet;
    float shootTime;
    public Image aim;
    public Canvas canvas;
    float radius;
    float lastAngle;
    float thisAngle;
    Ray camRay;
    LayerMask floor;
    LayerMask selected;
    Vector3 targetDestination;
    float distanceToDestination;
    float oldDistanceToDestination;
    public GameObject Blueprint;
    public GameObject sphere;
    public GameObject cube;
    NavMeshAgent navMeshAgent;
    
   
    public int currentWeaponIndex;
    public Animator anim;
    public static float R;
    public static float G;
    public static float B;

    public GameObject Sword;
    public GameObject Gun;

    public bool isAtk;
    public bool isKick;
    public bool isHurt;
    public bool isShoot;
    public bool isSlash;

    public bool shooting;
    public int ammoCount;
    public static bool isAttacking;
    public bool startSlashAnim; 
    public Text CurrentWeaponText;
    public List<string> equipment;
    public bool hasSword;
    public bool hasGun;
    public int menuIndex;
    public List<GameObject> cubes;
    public List<GameObject> selectedCubes;
    public static bool combine;
    enum InputStates { ArcWrench, MatterHammer, Synthesizer, Hack, Open, Jetpack, Attack, Combine, Select }
    InputStates inputState;
    enum MovingStates { Open, Aim, Move }
    MovingStates movingState;
    LineRenderer lineR;

    public delegate void AffectCube(int index, int change);

    public delegate void ChangeCubes(float change);
    public void rotateCubePos(float change)
    {

        foreach(GameObject cube in selectedCubes)
        cube.transform.Rotate(new Vector3(0.0f, change * Time.deltaTime, 0.0f));

    }
    
    public void changeCubes(ChangeCubes change, float delta)
    {

            change(delta);

     
    }
    public void affectCube(AffectCube AffCub, int index, int change) {


        AffCub(index, change);

    }

    public void updateColorTextHud()
    {
        RedText.text = R.ToString();
        BlueText.text = B.ToString();
        GreenText.text = G.ToString();
    }
    InRange checkDistance;
    // Start is called before the first frame update
    void Start()
    {


        oldStayAngle = 720;
        oldRotateDifference = 0;
        rotateDifference = 0;
        transforms = GetComponentsInChildren<Transform>();
        shootTime = 0;
        damageTaken = 0;

        Axis2TurnClockwise = false;

        wasShortMag = false;
        oldHorizontal = 0;
        horizontal = 0;
        vertical = 0;
        oldVertical = 0;


        LTrigDown = false;
        RTrigDown = false;

        startSlashAnim = false;

        isAttacking = false;
        R = 0;
        G = 0;
        B = 0;

        anim = GetComponent<Animator>();
        // Cursor.lockState = CursorLockMode.Locked;
        radius = 1;

        lineR = GetComponent<LineRenderer>();
        lineR.startWidth = (0.05f);
        lineR.endWidth = (0.05f);
        lineR.positionCount = (21);
        lineR.useWorldSpace = false;
        lastAngle = 0;
        thisAngle = 0;

        inputState = InputStates.Open;
        selected = LayerMask.GetMask("Selected");
        selectedCubes = new List<GameObject>();
        cubes = new List<GameObject>();
        floor = LayerMask.GetMask("Environment");
        navMeshAgent = GetComponent<NavMeshAgent>();
        checkDistance = CubeCheck;

        currentPosition = transform.position;

        //July 1st


        currentWeaponIndex = 0;
        equipment = new List<string>();
        equipment.Add("Unarmed");
        equipment.Add("Gun");
         equipment.Add("Sword");

      //  aim.canvasRenderer.
    }

    /*   public void EquipGun()
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

       }  */

    bool CubeCheck(Vector3 position)
    {
        if (Vector3.Distance(transform.position.zeroHeight(), position.zeroHeight()) < radius)
            return true;

        return false;
    }
  
    #region ises
    public bool isHack()
    {
        if (inputState == InputStates.Hack)
            return true;

        else
            return false;

    }
    public bool checkAnimStart(string animClip)
    {


        return anim.GetCurrentAnimatorStateInfo(0).IsName(animClip);


    }



    public bool isAttack()
    {
        if (inputState == InputStates.Attack)
            return true;

        else
            return false;

    }
    public bool isSynth()
    {
        if (inputState == InputStates.Synthesizer)
            return true;

        else
            return false;
    }
    public bool isArcWrench()
    {
        if (inputState == InputStates.ArcWrench)

            return true;

        else
            return false;
    }

    public bool isJetpack()
    {
        if (inputState == InputStates.Jetpack)
            return true;

        else
            return false;
    }

    #endregion
    void CreatePoints()
    {

        float x;
        float y;
        float z;

        float angle = 0.0f;
        if(inputState == InputStates.Select)
        {

            for(int i = 0; i<21; i++)
            {

                Vector3 fakePosition = new Vector3(0.0f, 0.3f, (30.0f / 21.0f) * i);
                lineR.SetPosition(i, fakePosition);
            }


        }
        else
        for (int i = 0; i < 21; i++)
        {

            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineR.SetPosition(i, new Vector3(x, .5f, z));

            angle += (360.0f / 20);

        }


    }


    #region enable/disables

    public void EnableSelect()
    {
        DisableWrench();
        DisableHack();
        DisableSynth();
        DisableAttack();
        DisableCombine();

        radius = 5.0f;
        inputState = InputStates.Select;
        Select.GetComponent<Image>().color = Color.black;
    }

    public void DisableSelect()
    {
        radius = 1;
        inputState = InputStates.Open;
        Select.GetComponent<Image>().color = Color.white;
    }
    public void EnableCombine()
    {
        DisableWrench();
        DisableHack();
        DisableSynth();
        DisableAttack();
        DisableSelect();
        radius = 5.0f;
        inputState = InputStates.Combine;
        Combine.GetComponent<Image>().color = Color.black;

    }
    public void DisableCombine()
    {
        radius = 1;
        inputState = InputStates.Open;
        Combine.GetComponent<Image>().color = Color.white;

    }
    public void EnableAttack()
    {
        DisableCombine();
        DisableWrench();
        DisableHack();
        DisableSynth();
        DisableSelect();
        inputState = InputStates.Attack;
        Attack.GetComponent<Image>().color = Color.black;

    }
    public void DisableAttack()
    {
        radius = 1;
        inputState = InputStates.Open;
        Attack.GetComponent<Image>().color = Color.white;
    }
    public void EnableSynth()
    {
        DisableCombine();
        DisableAttack();
        DisableWrench();
        DisableHack();
        DisableSelect();
        inputState = InputStates.Synthesizer;
        radius = 10.0f;
   
        Build.GetComponent<Image>().color = Color.black;
    }

    public void DisableSynth()
    {
     
        inputState = InputStates.Open;
        radius = 1;
        Build.GetComponent<Image>().color = Color.white;
    }

    public void EnableHack()
    {
        DisableCombine();
        DisableAttack();
        DisableSynth();
        DisableWrench();
        DisableSelect();
        inputState = InputStates.Hack;
        radius = 10.0f;
        Hack.GetComponent<Image>().color = Color.black;
   
    }

    public void DisableHack()
    {
        inputState = InputStates.Open;
        radius = 1.0f;
    
        Hack.GetComponent<Image>().color = Color.white;
    }

    public void EnableWrench()
    {
        DisableCombine();
        DisableAttack();
        DisableHack();
        DisableSynth();
        DisableSelect();
        inputState = InputStates.ArcWrench;
        radius = 5.0f;
    
        ArcWrench.GetComponent<Image>().color = Color.black;
    }

    public void DisableWrench()
    {
        inputState = InputStates.Open;
        radius = 1.0f;
     
        ArcWrench.GetComponent<Image>().color = Color.white;
    }

    #endregion

    #region attacks form NINJANEER

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
                  // AttackColorNinja1 = false;
                  //  AttackColorCop1 = false;
                    startSlashAnim = false;
                }
        }



    }
    #endregion

    #region Toggles
    public void ToggleAttack()
    {
        if (!isAttack())
        {
            EnableAttack();
        }
        else
            DisableAttack();


    }
    public void ToggleSynthesizer()
    {


        if (!isSynth())
        {
            EnableSynth();
        }
        else 
        {
            DisableSynth();
        }

    }
    public void ToggleJetpack()
    {
        if (inputState == InputStates.Open)
        {
          //  print("Jetpack on");
            inputState = InputStates.Jetpack;
        }
        else if (inputState == InputStates.Jetpack)
        {
         //  print("Jetpack off");
            inputState = InputStates.Open;
        }
    }
    public void ToggleHack()
    {
        if (!isHack())
        {
            EnableHack();
        }
        else
        {
            DisableHack();
        }
    }
    public void ToggleArcWrench()
    {


        if (!isArcWrench())
        {
            EnableWrench();
        }
        else 
        {
            DisableWrench();
        }

    }
    #endregion


    public void RotateCubes()
    {


    }
    public void LineRender()
    {


     }
    // Update is called once per frame
    void Update()
    {

        health.value -= damageTaken;
        damageTaken = 0;
        currentPosition = transform.position;
        CreatePoints();

        checkAllAnims();

        // updateColorTextHud();


        if (shooting)
            updateShootingTimer();
        //  print(Input.GetAxis("Mouse X"));
        //   print(Input.GetAxis("Mouse Y"));





        //  Camera.main.transform.position = transform.position;
        // Camera.main.transform.Rotate(new Vector3(1, 0, 0), -Input.GetAxis("Mouse Y"), Space.World);

        //Camera.main.transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Mouse X"), Space.World);

        //Camera.main.transform.Translate(new Vector3(0.0f, 2.0f, -2.0f));



        //aim.GetComponent<RectTransform>().position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f) * 5.0f;

        // print(Input.GetAxis("Mouse ScrollWheel"));

        #region mouse
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            menuIndex++;
            if (menuIndex > 5)
                menuIndex = 0;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            menuIndex--;
            if (menuIndex < 0)
                menuIndex = 5;
        }

        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, 10000, floor))
        {
            // movingState = MovingStates.Move;


            //      gameObject.transform.LookAt(floorHit.point);

            // else


            /* ADD T HIS BACK IF YOU GO BACK TO MOUSE
             * 
             * ADD THIS BACK IF YOU GO BACK TO MOUSE
             * 
             * ADD T HIS BAFCK IF YOU GO BACK TO MOUSE
             */
            //  
        }

        if (Input.GetMouseButtonDown(0))
        {
            {

                Vector3 halfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

                //  thisAngle = Mathf.Rad2Deg * Mathf.Atan2(Input.mousePosition.y - halfScreen.y, Input.mousePosition.x - halfScreen.x);


                GameObject spawnedButton = Instantiate(options, canvas.GetComponent<Transform>());

                spawnedButton.GetComponent<Transform>().SetParent(canvas.GetComponent<Transform>(), false);
                // spawnButton = false;

                Vector2 pos = new Vector2(Input.mousePosition.x - halfScreen.x, Input.mousePosition.y - halfScreen.y);  // get the game object position

                // Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

                // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
                options.GetComponent<RectTransform>().anchorMin = pos;
                options.GetComponent<RectTransform>().anchorMax = pos;


            }

            //  camRay = Camera.main.ScreenPointToRay(aim.GetComponent<RectTransform>().position);


            if (Physics.Raycast(camRay, out floorHit, 10000, selected))
            {
                GameObject selectedObject = floorHit.collider.gameObject;



                if (selectedObject.GetComponent<Renderer>().material.color == Color.green)
                    selectedObject.GetComponent<Renderer>().material.color = Color.red;
            }

            if (inputState == InputStates.Synthesizer)
            {
                if (Physics.Raycast(camRay, out floorHit, 10000, floor))
                {
                    cubes.Add(Instantiate(cube, floorHit.point + new Vector3(0.0f, 1.0f, 0.0f), transform.rotation)); ;// + new Vector3(0.0f, 2.0f, 0.0f);

                }


            }
            if (inputState == InputStates.Select)
            {
                if (Physics.Raycast(camRay, out floorHit, 10000, floor))
                {
                    // movingState = MovingStates.Move;


                    if (floorHit.collider.gameObject.name.Contains("Robot"))
                    {
                        GameObject selectedObject = floorHit.collider.gameObject;

                        if (selectedObject.GetComponent<Renderer>().material.color == Color.green)
                            selectedObject.GetComponent<Renderer>().material.color = Color.red;
                        selectedObject.GetComponent<Renderer>().material.color = Color.green;

                        selectedObject.GetComponent<Robot>().selected = true;
                        selectedCubes.Add(selectedObject);
                        selectedObject.layer = 14;
                        selectedCubes.Add(selectedObject);
                    }
                    // else



                    //  
                }
            }
            if (isArcWrench())
            {

                lastAngle = thisAngle;
                Vector3 halfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

                thisAngle = Mathf.Rad2Deg * Mathf.Atan2(Input.mousePosition.y - halfScreen.y, Input.mousePosition.x - halfScreen.x);

            }
            if (isAttack())
            {
                //   camRay = Camera.main.ScreenPointToRay(aim.GetComponent<RectTransform>().position);
                //  RaycastHit floorHit;
                if (Physics.Raycast(camRay, out floorHit, 10000, floor))
                {


                    transform.LookAt(floorHit.point.zeroHeight());
                    GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
                }
            }


        }

        else if (Input.GetMouseButton(0))
        {






            if (isJetpack())
            {
                // print("Jetpack");
                transform.position += transform.up * Time.deltaTime * 10;



            }

            if (isArcWrench())
            {
                lastAngle = thisAngle;
                Vector3 halfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

                thisAngle = Mathf.Rad2Deg * Mathf.Atan2(Input.mousePosition.y - halfScreen.y, Input.mousePosition.x - halfScreen.x);


                changeCubes(rotateCubePos, thisAngle);





            }
        }
        #endregion
        #region menuIndexes

        if (menuIndex == 0)
        {
            EnableCombine();
        }
        if (menuIndex == 1)
        {
            EnableSynth();
        }
        if (menuIndex == 2)
        {
            EnableHack();
        }
        if (menuIndex == 3)
        {
            EnableWrench();
        }
        if (menuIndex == 4)
        {
            EnableAttack();
        }
        if (menuIndex == 5)
        {
            EnableSelect();
        }
        #endregion

        #region Key Toggles
        if (Input.GetKeyDown(KeyCode.S))
        {
            // EnableSynth();
            ToggleSynthesizer();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //EnableAttack();
            ToggleAttack();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleJetpack();


        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            // EnableHack();

            ToggleHack();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //EnableWrench();
            //ProcessBlueprint(cubes);
            ToggleArcWrench();
        }
        #endregion

        if (Input.GetKeyDown("space")){

            transform.position += new Vector3(0.0f, 1.0f, 0.0f);

        }
        if(Input.GetButtonDown("Jump"))
        {

            transform.position += new Vector3(0.0f, 1.0f, 0.0f);

        }

        if (Input.GetButtonDown("A Button"))
        {

            //  if (Physics.Raycast(camRay, out floorHit, 10000, floor))
            {

                /*IF you go back to MOUSE you need to add the physics.raycast  track mouse at the top of the class
                 */
                // transform.LookAt(floorHit.point.zeroHeight());
                //GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
                processAttack();
            }



        }

        if (Input.GetButtonDown("B Button"))
        {

            print("B BUTTON");
            if (inputState == InputStates.Select)
            {


               
                    selectionRay = new Ray(transform.position + new Vector3(0.0f, .30f, 0.0f), Vector3.forward);
                RaycastHit selectionHit;
                if (Physics.Raycast(selectionRay, out selectionHit, 30.0f, floor))
                {


                    if (selectionHit.collider.gameObject.name.Contains("Robot"))
                    {
                        GameObject selectedObject = selectionHit.collider.gameObject;

                        if (selectedObject.GetComponent<Renderer>().material.color == Color.green)
                            selectedObject.GetComponent<Renderer>().material.color = Color.red;
                        selectedObject.GetComponent<Renderer>().material.color = Color.green;

                        selectedObject.GetComponent<Robot>().selected = true;
                        selectedCubes.Add(selectedObject);
                        selectedObject.layer = 14;
                        selectedCubes.Add(selectedObject);
                    }
                }

            }
        }
        float myAngle = 0.0f;
        float bodyRotation = 0.0f;

        if (Math.Abs(Input.GetAxis("Horizontal")) > .019f || Math.Abs(Input.GetAxis("Vertical")) > .019f)
        {
            oldHorizontal = horizontal;
            oldVertical = vertical;
            horizontal = Math.Abs(Input.GetAxis("Horizontal"));
            vertical = Math.Abs(Input.GetAxis("Vertical"));

            if (horizontal < oldHorizontal || vertical < oldVertical)
            {
               // print(horizontal + " " + oldHorizontal);
                //print(vertical + " " + oldVertical);
                wasShortMag = true;

            }


            else
            {
                if (wasShortMag)
                {
            //        print(wasShortMag);
             //       print(horizontal + " " + oldHorizontal);
            //        print(vertical + " " + oldVertical);
                }
                wasShortMag = false;
                myAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
                bodyRotation = myAngle + Camera.main.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, bodyRotation, 0.0f));
            }
        }
        else
        {
            wasShortMag = false;

            oldHorizontal = 0;
            horizontal = 0;
            vertical = 0;
            oldVertical = 0;
        }

        if (Math.Abs(Input.GetAxis("Horizontal 2")) > .019f || Math.Abs(Input.GetAxis("Vertical 2")) > .019f)
        {
            //  oldHorizontal = horizontal;
            //   oldVertical = vertical;


            float horizontal2 = (Input.GetAxis("Horizontal 2"));
            float vertical2 = (Input.GetAxis("Vertical 2"));

            if(oldStayAngle == 720)
                oldStayAngle = Mathf.Atan2(horizontal2, vertical2) * Mathf.Rad2Deg;

            else
                oldStayAngle = oldCAngle;

            if(CAngle != 720)

               // if(oldCAngle > .001f)
            oldCAngle = CAngle;



            oldRotateDifference = rotateDifference;



            CAngle = Mathf.Atan2(horizontal2, vertical2) * Mathf.Rad2Deg;

            if (CAngle < 0)
                CAngle += 360;
            // print(CAngle);

            // if (CAngle > oldCAngle)


            if (oldCAngle > CAngle)
            {
                //Going clockwise
                rotateDifference = oldCAngle - CAngle;
                if (((CAngle > 0 && CAngle < 30) && (oldCAngle < 360 && oldCAngle > 330)))
                {
                    rotateDifference = (CAngle - oldCAngle) ;
                    print("clockwise");
                }

                if ((oldCAngle < 360 && oldCAngle > 350) && (CAngle > 0 && CAngle < 10))
                {
                    rotateDifference = oldCAngle - CAngle;
                    print("ccw");
                }

            }
            if (CAngle > oldCAngle)
            {

                rotateDifference = (CAngle - oldCAngle);

               


               // ((oldRotateDifference > 0 && oldRotateDifference < 270) && (rotateDifference < 360 && rotateDifference > 270)))
                rotateDifference *= -1;

            //    if ((CAngle < 360 && CAngle > 320) && (oldCAngle > 0 && CAngle < 40))
               //{
                //    rotateDifference = oldCAngle - CAngle;
               //    print("ccw");
                //}
                //if (((CAngle > 0 && CAngle < 10) && (oldCAngle < 360 && oldCAngle > 350)))
                //{
                //    rotateDifference = (CAngle - oldCAngle) * -1;
                //    print("clockwise");
                //}

            }
            if (Math.Abs(rotateDifference) < .001)
            {
                //If the change isnt great enough dont change it so that it can check against the change again
                oldCAngle = oldStayAngle ;
                rotateDifference = 0;
            }
       //     else 
         //      if ((oldRotateDifference > 0 && oldRotateDifference < 270) && (rotateDifference < 360 && rotateDifference > 270))
                changeCubes(rotateCubePos, rotateDifference);

            //if (oldRotateDifference > rotateDifference || ((oldRotateDifference > 0 && oldRotateDifference < 270) && (rotateDifference < 360 && rotateDifference > 270)))
            //  Axis2TurnClockwise = true;

            //else if (rotateDifference > oldRotateDifference || ((rotateDifference > 0 && rotateDifference < 270) && (oldRotateDifference < 360 && oldRotateDifference > 270)))
            //  Axis2TurnClockwise = false;
         //   print(rotateDifference + " " + oldCAngle + " " + CAngle);
           // else
           //     rotateDifference = CAngle - oldCAngle;


          /*  if ((rotateDifference) < .0009f)
            {
                oldCAngle = oldStayAngle;

                rotateDifference = 0;

                if ((oldCAngle - CAngle) >= .0009f)
                {

                    rotateDifference = oldCAngle - CAngle;

                    oldCAngle = CAngle;
                    // oldCAngle = oldStayAngle;

                    //If the old stay angle that was saved in oldCAngle has the right differential, save the current CAngle in oldCangle. If not
                    //Keep the oldStayAngle in oldCAngle so that when it saves when it returns it will keep the same oldStayAngle

                //    print("OldStay");
                  //  print(rotateDifference);

                }

            }*/

            //CCW doesnt work but it is really the clockwise that isnt working because thats the natural clockwise
            /* if (rotateDifference < 0)

            {




                rotateDifference = CAngle - oldCAngle;
                print("Less than Rotate Difference" + rotateDifference);

            }
            //If it is greater than 300 we need to switch the two angles and take the diference
            else if(rotateDifference > 300f)
            {


                oldCAngle = oldStayAngle;

                rotateDifference = Math.Abs(CAngle) - oldCAngle;

                if ((CAngle - oldCAngle) >= .0009f)
                {

                    rotateDifference = CAngle - oldCAngle;

                    oldCAngle = CAngle;
                    // oldCAngle = oldStayAngle;

                    //If the old stay angle that was saved in oldCAngle has the right differential, save the current CAngle in oldCangle. If not
                    //Keep the oldStayAngle in oldCAngle so that when it saves when it returns it will keep the same oldStayAngle

                    //    print("OldStay");
                    //  print(rotateDifference);

                }

            }
            //Going clockwise

            if(rotateDifference < .0009f)
            {
           
                oldCAngle = oldStayAngle;

                rotateDifference = CAngle - oldCAngle;

                if ((CAngle - oldCAngle) <= .0009f)
                {

                    rotateDifference = CAngle - oldCAngle;

                    oldCAngle = CAngle;
                    // oldCAngle = oldStayAngle;

                    //If the old stay angle that was saved in oldCAngle has the right differential, save the current CAngle in oldCangle. If not
                    //Keep the oldStayAngle in oldCAngle so that when it saves when it returns it will keep the same oldStayAngle

                    //    print("OldStay");
                    //  print(rotateDifference);

                }


            }
            if (rotateDifference < .0009f)
            {


                if (oldRotateDifference > rotateDifference || ((oldRotateDifference > 0 && oldRotateDifference < 270) && (rotateDifference < 360 && rotateDifference > 270)))
                    Axis2TurnClockwise = true;

                else if (rotateDifference > oldRotateDifference || ((rotateDifference > 0 && rotateDifference < 270) && (oldRotateDifference < 360 && oldRotateDifference > 270)))
                    Axis2TurnClockwise = false;

                oldRotateDifference = rotateDifference;



            }


            // if (difference > .001f)

            // print(oldCAngle + " " + CAngle);

            //       print(rotateDifference);

           /* if (Axis2TurnClockwise)
            {
                if (rotateDifference > 0)
                {
        //            print(rotateDifference);
                   // rotateDifference += 360;
                    print("Axis CW but" + rotateDifference);
                }
                    changeCubes(rotateCubePos, rotateDifference);

            }
            if (!Axis2TurnClockwise)
            {
                if(rotateDifference < 0)
                {
          //          print(rotateDifference);
                  print("Axis CCW but" + rotateDifference);
                    //rotateDifference *= -1;
                }
                changeCubes(rotateCubePos, rotateDifference);

            }
           */
            changeCubes(rotateCubePos, rotateDifference);


            //If the rotate difference isnt at the threshold keep the rotate difference for the next set
            if(Math.Abs(rotateDifference)< .0009f)                
                    {
                        rotateDifference = oldRotateDifference;
                    }

        }
        // if(Math.Abs(Input.GetButton()))

        if (Input.GetAxis("Vertical 2") < -.01f)
        {


            transform.position -= Vector3.up * Time.deltaTime * 10;
        }

        if (Input.GetAxis("Vertical 2") > .01f)
        {


            transform.position += Vector3.up * Time.deltaTime * 10;
        }

        if (Input.GetButtonDown("Right Bumper"))
        {

            menuIndex++;
            if (menuIndex > 5)
                menuIndex = 0;
        }
        if (Input.GetButtonDown("Left Bumper"))
        {

            menuIndex--;
            if (menuIndex < 0)
                menuIndex = 5;
        }
        if (Input.GetAxis("Left Trigger") > .001f)
        {

            print("Left trigger");

            if (!LTrigDown)
            {
                ChangeCurrentWeapon(-1);
                LTrigDown = true;
            }

        }
        else
            LTrigDown = false;
        if (Input.GetAxis("Right Trigger") > .001f)
        {

            if (!RTrigDown)
            {
                ChangeCurrentWeapon(1);
                RTrigDown = true;
            }


        }
        else
            RTrigDown = false;
        if (Input.GetAxis("Vertical") > .019f)
        {



            transform.position += Vector3.forward * Time.deltaTime * 10;

            
        }

     
        if (Input.GetAxis("Vertical") <- .019f)
        {


            transform.position -= Vector3.forward * Time.deltaTime * 10;
        }

        if (Input.GetAxis("Horizontal") > .019f)
        {


            transform.position += Vector3.right * Time.deltaTime * 10;
        }


        if (Input.GetAxis("Horizontal") < -.019f)
        {


            transform.position -= Vector3.right * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.UpArrow))
            {

            transform.position += Vector3.forward * Time.deltaTime * 10;
          //  transform.position += transform.forward * Time.deltaTime * 10;
          //  Camera.main.transform.position = transform.position;
          //  Camera.main.transform.Translate(new Vector3(0.0f, 2.0f, -2.0f));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
            transform.position -= Vector3.forward * Time.deltaTime * 10;
            //  transform.position -= transform.forward * Time.deltaTime * 10;
            //  Camera.main.transform.position = transform.position;
            //  Camera.main.transform.Translate(new Vector3(0.0f, 2.0f, -2.0f));
        }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
            transform.position -= Vector3.right * Time.deltaTime * 10;
            //   transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));

            //  Camera.main.transform.position = transform.position;
            //  Camera.main.transform.Rotate(new Vector3(0, 1, 0), -1.0f);

            // Camera.main.transform.Translate(new Vector3(0.0f, 2.0f, -2.0f));
            //transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * 10;

        }
            if (Input.GetKey(KeyCode.RightArrow))
            {
            transform.position += Vector3.right * Time.deltaTime * 10;
            //  Camera.main.transform.position = transform.position;
            // Camera.main.transform.Rotate(new Vector3(0, 1, 0), 1.0f);

            // Camera.main.transform.Translate(new Vector3(0.0f, 2.0f, -2.0f));
            // transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
            //transform.rotation *= Quaternion.Euler(1.2f, 0.0f, 0.0f);
            //    transform.position += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * 10;

        }
            

       
                //3.21 -5 .6 6
                if (Input.GetMouseButtonDown(1))
{




            //Taking out on 12/26/20
/*camRay = Camera.main.ScreenPointToRay(aim.GetComponent<RectTransform>().position);

if (Physics.Raycast(camRay, out floorHit, 10000, floor))
{


  //  movingState = MovingStates.Move;

 //   targetDestination = floorHit.point;// + new Vector3(0.0f, 2.0f, 0.0f);

  //  navMeshAgent.SetDestination(targetDestination);
    /*print(transform.rotation);
    Quaternion fakeTrans = transform.rotation;

    GameObject fakeObject = new GameObject();
    fakeObject.transform.position = transform.position;
    fakeObject.transform.rotation = transform.rotation;
    fakeObject.transform.LookAt(targetDestination);

    fakeObject.transform.rotation = Quaternion.Euler(camRay.direction);
   // fakeTrans.LookAt(targetDestination);
    //print(transform.rotation);
    GameObject bullet = Instantiate(Bullet, fakeObject.transform.position, fakeObject.transform.rotation);
    // bullet.transform.forward = camRay.direction;
    Destroy(fakeObject);
*/
  //          }
        }
    }
}
