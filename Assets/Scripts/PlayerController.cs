using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidBody { get; private set; }
    private Vector3 downForce = Vector3.down * 10;
    private float waterDensity = 1f;
    private bool isDiving = false;
    private float initialDiveTime = 3f;
    private float initialDiveStartTime;
    private bool isInitialDive = false;
    private int baseStamina = 100;
    private int maxStamina;
    public int currentStamina;
    private int speedLevel;

    public GameObject water;
    public GameObject followerSea;
    public Image staminaImage;
    public SkinnedMeshRenderer playerHead;
    public Renderer headMaterial;
    public float materialColorValue;
    public float materialColorCounter;
    public TextMeshProUGUI mText;
    public float maxMeter;
    public GameObject winPanel;
    public bool isWin;
    public bool isDive;
    public float coinTextTime;
    public GameObject coinTextPrefab;

    public GameObject seaObject1;
    public GameObject seaObject2;
    public GameObject seaObject3;
    public GameObject seaObject4;
    public GameObject seaObject5;
    public GameObject seaObject6;

    public GameObject[] polyObject;
    public int seaObjectCount;
    public ParticleSystem poof;

    private void Awake()
    {
        BindEvents();

    }
    
    private void OnDestroy()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        EventManager.onDive += DiveDown;
        EventManager.onOceanColorSelected += OnOceanColorChanged;
    }

    private void UnbindEvents()
    {
        EventManager.onDive -= DiveDown;
        EventManager.onOceanColorSelected -= OnOceanColorChanged;
    }

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        downForce = downForce / waterDensity;
        maxMeter = PlayerPrefs.GetFloat("SaveMeter", -49);
        mText.text = ((-1 * (int)maxMeter + (int)transform.position.y) + "m").ToString();
        poof.Stop();
        seaObject1.transform.position = new Vector3(transform.position.x, transform.position.y +(maxMeter /2) , transform.position.z);
        seaObject2.transform.position = new Vector3(transform.position.x, transform.position.y + (maxMeter / 3), transform.position.z);
        seaObject3.transform.position = new Vector3(transform.position.x, transform.position.y + (maxMeter / 1.5f), transform.position.z);
        if(maxMeter <= -150)
        {
            seaObject4.transform.position = new Vector3(transform.position.x, transform.position.y + (maxMeter / 2.5f), transform.position.z);
            seaObject5.transform.position = new Vector3(transform.position.x, transform.position.y + (maxMeter + 15), transform.position.z);
            seaObject6.transform.position = new Vector3(transform.position.x, transform.position.y + (maxMeter + 30), transform.position.z);
        }
        
    }

    private void Update()
    {
        if (currentStamina <= 100)
        {
            playerHead.materials[0].color = new Color(1, materialColorValue, materialColorValue);
            playerHead.materials[1].color = new Color(1, materialColorValue, materialColorValue);            
        }
        else if (currentStamina > 101)
        {
            playerHead.materials[0].color = new Color(1, 1, 1);
            playerHead.materials[1].color = new Color(1, 1, 1);
            materialColorValue = 1;
            materialColorCounter = 1;
        }
        if(materialColorValue >= 1)
        {
            materialColorValue = 1;
            materialColorCounter = 1;
        }
        if(materialColorValue <= 0.01)
        {
            materialColorValue = 0;
            
        }
        
        
        //        -51                   -50 
        if(transform.position.y <= maxMeter - 1 && !isWin)
        {
            Debug.Log("Game Win");
            winPanel.SetActive(true);
            maxMeter -= 50;
            PlayerPrefs.SetFloat("SaveMeter", maxMeter);
            if (GameManager.Instance.GameState == eGameState.Gameplay)
            {
                Stop();
            }
            isWin = true;
            isDive = false;
        }

        if (Input.GetMouseButton(0) && !isWin)
        {
            isDive = true;
            mText.text = ((-1 * (int)maxMeter + (int)transform.position.y) + "m").ToString();
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDive = false;
            
            if (GameManager.Instance.GameState == eGameState.Gameplay)
            {
                Stop();
            }
        }


    }


    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && isDive)
        {
            if (playerRigidBody == null)
            {
                return;
            }
            if (GameManager.Instance.GameState == eGameState.Gameplay)
            {
                Dive();
                followerSea.SetActive(true);
                coinTextTime -= Time.deltaTime;
                if (coinTextTime <= 0)
                {
                    Instantiate(coinTextPrefab, transform.position, Quaternion.identity);
                    coinTextTime = 1;
                }

            }
           
        }
   
       
        if (playerRigidBody == null)
        {
            return;
        }

        if (isDiving && !isWin)
        {
            float currentTime = Time.time;
            float t;
            t = (currentTime-initialDiveStartTime)/initialDiveTime;
            if (isInitialDive)
            {
                DiveLerp(t);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SeaObject"))
        {
            poof.Play();
            polyObject[seaObjectCount].SetActive(true);
            Destroy(other.gameObject);
            seaObjectCount++;
            speedLevel += 2;
        }
    }


    void InitPowerUpLevels()
    {
        var powerUpsManager = GameManager.Instance.GetPowerUpsManager();
        maxStamina = baseStamina + 10 * powerUpsManager.StaminaLevel;
        currentStamina = maxStamina;
        speedLevel = powerUpsManager.SpeedLevel;
    }

    void Dive()
    {

        if(water != null)
        {
            Destroy(water, 0.5f);
        }

        playerRigidBody.velocity = downForce;
        LerpMoveHorizontally();
    }

    void LerpMoveHorizontally()
    {
        var xPosition = transform.position.x + Input.GetAxis("Mouse X") * 0.1f;
        xPosition = Mathf.Clamp(xPosition, 248.5f, 251.5f);
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
    void Stop()
    {
        //playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.velocity = Vector3.Lerp(transform.position, Vector3.zero, 1);
        //EventManager.onPlayerStop?.Invoke();

    }
    void DiveDown()
    {
        InitPowerUpLevels();
        InitDownForce();
        initialDiveStartTime = Time.time;
        isInitialDive = true;
        isDiving = true;
        StartCoroutine(DepleteRecoverStamina());
    }

    void InitDownForce()
    {
        var force = downForce + Vector3.down * speedLevel;
        downForce = force;
    }

    void DiveLerp(float t)
    {
        playerRigidBody.velocity = Vector3.Lerp(downForce, Vector3.zero, t);
        if (t >= 1)
        {
            isInitialDive = false;
        }
    }

    void OnGameOver()
    {
        Stop();
    }

    private void OnOceanColorChanged(OceanColor oceanColor)
    {
        waterDensity = oceanColor.density;
    }

    IEnumerator DepleteRecoverStamina()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (true)
        {
            if (playerRigidBody.velocity.y == 0)
            {
                currentStamina+= 5;
                staminaImage.DOColor(new Color(0, 0.4509804f, 1, 0), 0.25f);
                materialColorCounter += 0.05f; 
                //materialColorCounter = Mathf.Pow(materialColorCounter, 2);
                materialColorValue = materialColorCounter;
                //materialColorValue += 0.1f;
            }
            else
            {
                currentStamina -= 10;
                staminaImage.DOColor(new Color(0, 0.4509804f, 1, 1), currentStamina * 0.0001f);
                materialColorCounter -= 0.01f;
                materialColorCounter = Mathf.Pow(materialColorCounter, 2);
                materialColorValue = materialColorCounter;
            }
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            float ratio = 1f - currentStamina / (float) maxStamina;
            EventManager.onStaminaUpdated?.Invoke(ratio);
            if (currentStamina == 0)
            {
                EventManager.OnGameOver();
                OnGameOver();
                yield break;
            }
            yield return waitTime;
        }
        
    }

}

public enum eDirection
{
    up,
    down
}
