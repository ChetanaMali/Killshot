using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
//UnityEngine.Gizmos.DrawWireSphere;
using UnityEngine.UIElements;

public class SpaceshipMovement : MonoBehaviour
{
    //Quaternion rotationAngle;
    //Vector3 upDownAngle;

    [SerializeField] float SpaceshipMoveSpeed = 0f;

    [SerializeField] TextMeshProUGUI turnAngle;
    [SerializeField] TextMeshProUGUI elevationAngle;

    [SerializeField] float detectionRadius;
    [SerializeField] LayerMask enemyLayer;

    [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI speedText;

    [SerializeField] Power power;

    [SerializeField] float moveSpeed;
    [SerializeField] float targetMovement;
    [SerializeField] bool isMove;

    [SerializeField] float rotSpeed;
    [SerializeField] float targetRotAngle;
    [SerializeField] bool isRotate;

    [SerializeField] bool isRotateArrow;
    [SerializeField] float arrowRotAngle;

    Quaternion currentRotation;
    Vector3 currentPosition;

    [SerializeField] GameObject arrow;
    private void Awake()
    {
        LineRendIn();
    }
    // Update is called once per frame
    private void Start()
    {
        speed = SpaceshipMoveSpeed;
        speedText.text = speed.ToString() + " km/h";

    }
    void Update()
    {
        SpaceshipMove();
        EnemySpaseshipDetection();
        DrawCicle();
    }
    private void FixedUpdate()
    {
        if (isRotate)
        {
            RotateSpaceship();
        }
        if (isMove)
        {
            RaiseSpaceship();
        }
        if(isRotateArrow)
        {
            //RotateArrow();
        }

    }
   void RotateArrow()
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, 0);

        arrow.transform.rotation = Quaternion.RotateTowards(arrow.transform.rotation, Quaternion.Euler(-90, 0, 0), rotSpeed * Time.deltaTime);
        if(arrow.transform.rotation == Quaternion.Euler(-90,0,0))
        {
            isRotateArrow = false;
        }
    }
    void RotateSpaceship()
    {
        Quaternion targetRotation = Quaternion.Euler(0, targetRotAngle, 0);
        
        Quaternion newTargetRotation =  targetRotation * currentRotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newTargetRotation, rotSpeed * Time.deltaTime);
        Quaternion newarrowRotation = Quaternion.Euler(-90, 0, 0);
        arrow.transform.rotation = Quaternion.RotateTowards(arrow.transform.rotation, newarrowRotation, rotSpeed * Time.deltaTime);
        if (transform.rotation == newTargetRotation)
        {
            isRotate = false;
            targetRotAngle = 0;
        }
    }
    void RaiseSpaceship()
    {
        Vector3 targetPosition = new Vector3(0, targetMovement, 0);
        Vector3 newTargetPosition = currentPosition + targetPosition;
        transform.position = Vector3.MoveTowards(transform.position, newTargetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == newTargetPosition)
        {
            isMove = false;
            targetMovement = 0;
        }

    } 
    void SpaceshipMove()
    {
        //CONTINUES FORWORD MOVEMENT 
        transform.Translate(Vector3.forward * Time.deltaTime * SpaceshipMoveSpeed);

        //ELEVATION MOVEMENT
        if (GameManager.Instance.eActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //GOES UPSIDE
                targetMovement += 1f;
                elevationAngle.text = targetMovement.ToString();
                Debug.Log("targetMovement: " + targetMovement);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //GOES DOWNSIDE

                targetMovement -= 1f;
                elevationAngle.text = targetMovement.ToString();
                Debug.Log("targetMovement: " + targetMovement);
            }

        }

        //BEARING MOVEMENT
        if (GameManager.Instance.bActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //TURN LEFT
                targetRotAngle += -15f;
                turnAngle.text = targetRotAngle.ToString();

                arrowRotAngle -= 15f;
                arrow.transform.rotation = Quaternion.Euler(-90, arrowRotAngle, 0);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //TURN RIGHT
                targetRotAngle += 15f;
                turnAngle.text = targetRotAngle.ToString();

                arrowRotAngle += 15f;
                arrow.transform.rotation  = Quaternion.Euler(-90, arrowRotAngle, 0);
            }
        }

        //SMMOTH ROTATION AFTER ENTER
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            #region DEACTIVATE ALL BUTTONS AND TURN ON THE RED IMAGE
            GameManager.Instance.eActive = false;
            GameManager.Instance.bActive = false;
            GameManager.Instance.fActive = false;
            GameManager.Instance.pActive = false;
            GameManager.Instance.lActive = false;

            GameManager.Instance.eRedImage.SetActive(true);
            GameManager.Instance.eGreenImage.SetActive(false);
            GameManager.Instance.bRedImage.SetActive(true);
            GameManager.Instance.bGreenImage.SetActive(false);
            GameManager.Instance.fRedImage.SetActive(true);
            GameManager.Instance.fGreenImage.SetActive(false);
            GameManager.Instance.pRedImage.SetActive(true);
            GameManager.Instance.pGreenImage.SetActive(false);
            GameManager.Instance.lRedImage.SetActive(true);
            GameManager.Instance.lGreenImage.SetActive(false);

            #endregion
            arrowRotAngle = 0;
            // Stores curent rotation and position
            currentRotation = transform.rotation;
            currentPosition = transform.position;
            if (power.enginePower >= 1)
            {
                power.enginePower--;


                isRotate = true;
                isMove = true;
                isRotateArrow = true;

                if (power.enginePower <= 2 && targetRotAngle <= 30)
                {
                    Debug.Log("Safe turn");
                }
                if (power.enginePower <= 2 && targetRotAngle >= 60 && (targetRotAngle <= 105))
                {
                    Debug.Log("sharp turn need more power. chance of damage to stability");
                }
                if (power.enginePower <= 2 && targetRotAngle >= 105)
                {
                    Debug.Log("Aggressive turn, Need more power, higher chances of damage to stability");
                }
                turnAngle.text = "0";
                //------------------UP DOWN TURN--------------------

                elevationAngle.text = "0";

            }

            //ADD POWER TO THE SPEED
            SpaceshipMoveSpeed = speed;

            if (power.sensorPower >= 1)
            {
                detectionRadius += 5;
                power.sensorPower--;
                
            }
        }
    }
    void EnemySpaseshipDetection()
    {
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        foreach (Collider collider in hitCollider)
        {
            Debug.Log("Enemy Detected: " + collider.gameObject.name);
            AudioManager.Instance.PlayEnemyAlert();
        }
    }
    public int segments = 25; 
    public Material circleMat;

    public LineRenderer lineRenderer;
    void LineRendIn()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Set LineRenderer properties
        lineRenderer.positionCount = segments + 1;
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        lineRenderer.useWorldSpace = true;
        //lineRenderer.startColor = circleColor;
        //lineRenderer.endColor = circleColor;
        lineRenderer.material = circleMat;
        DrawCicle();
    }
    void DrawCicle()
    {
        // Calculate segment size
        float angleStep = 360f / segments;

        // Update LineRenderer positions
        for (int i = 0; i <= segments; i++)
        {
            float angle = angleStep * i;
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * detectionRadius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * detectionRadius;

            lineRenderer.SetPosition(i, transform.position + new Vector3(x, 0, z));
        }
    }

    public void SpeedInc()
    {
        if (GameManager.Instance.fActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (speed < 50)
            {
                speed += 5f;
            }
            else
            {
                speed = 50f;
            }
            speedText.text = speed.ToString() + " km/h";
        }



    }
    public void SpeedDec()
    {
        if (GameManager.Instance.fActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (speed > 0f)
            {
                speed -= 5f;
            }
            else
            {
                speed = 0f;
            }
            speedText.text = speed.ToString() + " km/h";
        }


    }
}