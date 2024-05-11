using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public bool eActive, bActive, fActive, pActive, lActive;

    public bool enterKeyActivate;
    public GameObject eGreenImage, eRedImage;
    public GameObject bGreenImage, bRedImage;
    public GameObject fGreenImage, fRedImage;
    public GameObject pGreenImage, pRedImage;
    public GameObject lGreenImage, lRedImage;

    public Power power;
    //bool to get that enemy is detected or not
    public bool isEnemyDetect;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        eActive = false;
        bActive = false;
        fActive = false;
        pActive = false;
        lActive = false;

        eRedImage.SetActive(true);
        eGreenImage.SetActive(false);
        bRedImage.SetActive(true);
        bGreenImage.SetActive(false);
        fRedImage.SetActive(true);
        fGreenImage.SetActive(false);
        pRedImage.SetActive(true);
        pGreenImage.SetActive(false);
        lRedImage.SetActive(true);
        lGreenImage.SetActive(false);
    }
   
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            EButtonActive();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BButtonActivate();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FButtonActivate();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            pButtonActivate();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LButtonActive();
        }
    }

    public void EButtonActive()
    {
        if (!eActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            eActive = true;
            eGreenImage.SetActive(true);
            eRedImage.SetActive(false);
        }
        else if( eActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            eActive = false;
            eGreenImage.SetActive(false);
            eRedImage.SetActive(true);
        }
        fRedImage.SetActive(true);
        fGreenImage.SetActive(false);
        bRedImage.SetActive(true);
        bGreenImage.SetActive(false);
        pRedImage.SetActive(true);
        pGreenImage.SetActive(false);
        lRedImage.SetActive(true);
        lGreenImage.SetActive(false);

        fActive = false;
        bActive = false;
        pActive = false;
        lActive = false;
    }
    public void BButtonActivate()
    {
        if (!bActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            bActive = true;
            bGreenImage.SetActive(true);
            bRedImage.SetActive(false);
        }
        else if(bActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            bActive = false;
            bGreenImage.SetActive(false);
            bRedImage.SetActive(true);
        }
        eRedImage.SetActive(true);
        eGreenImage.SetActive(false);
        fRedImage.SetActive(true);
        fGreenImage.SetActive(false);
        pRedImage.SetActive(true);
        pGreenImage.SetActive(false);
        lRedImage.SetActive(true);
        lGreenImage.SetActive(false);

        eActive = false;
        fActive = false;
        pActive = false;
        lActive = false;
    }
    public void FButtonActivate()
    {
        eRedImage.SetActive(false);
        if (!fActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            fActive = true;
            fGreenImage.SetActive(true);
            fRedImage.SetActive(false);
        }
        else if (fActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            fActive = false;
            fGreenImage.SetActive(false);
            fRedImage.SetActive(true);
        }
        eRedImage.SetActive(true);
        eGreenImage.SetActive(false);
        bRedImage.SetActive(true);
        bGreenImage.SetActive(false);
        pRedImage.SetActive(true);
        pGreenImage.SetActive(false);
        lRedImage.SetActive(true);
        lGreenImage.SetActive(false);

        eActive = false;
        bActive = false;
        pActive = false;
        lActive = false;
    }
    public void pButtonActivate()
    {
        //pRedImage.SetActive(false);
        if (!pActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            pActive = true;
            pGreenImage.SetActive(true);
            pRedImage.SetActive(false);
        }
        else if (pActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            pActive = false;
            pGreenImage.SetActive(false);
            pRedImage.SetActive(true);
        }
        eRedImage.SetActive(true);
        eGreenImage.SetActive(false);
        bRedImage.SetActive(true);
        bGreenImage.SetActive(false);
        fRedImage.SetActive(true);
        fGreenImage.SetActive(false);
        lRedImage.SetActive(true);
        lGreenImage.SetActive(false);

        eActive = false;
        bActive = false;
        fActive = false;
        lActive = false;
    }
    public void LButtonActive()
    {
        if (!lActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            lActive = true;
            lGreenImage.SetActive(true);
            lRedImage.SetActive(false);
        }
        else if (lActive && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            lActive = false;
            lGreenImage.SetActive(false);
            lRedImage.SetActive(true);
        }
        eRedImage.SetActive(true);
        eGreenImage.SetActive(false);
        bRedImage.SetActive(true);
        bGreenImage.SetActive(false);
        fRedImage.SetActive(true);
        eGreenImage.SetActive(false);
        pRedImage.SetActive(true);
        pGreenImage.SetActive(false);

        eActive = false;
        bActive = false;
        fActive = false;
        pActive = false;
    }
    public void returnButton()
    {
        enterKeyActivate = true;
    }
    public void CloseApp()
    {
        Application.Quit();
    }
    public void PowerEnergyAdd()
    {
        if (pActive)
        {
            if((power.enginePower >= 0 && power.enginePower <= 10) && (power.reactorPower >=0 && power.reactorPower <= 10)
                && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                EnergyAdd();
                //Invoke("EnergyAdd", 3f);
            }
        }
    }
    public void PowerEnergyMinus()
    {
        if(pActive)
        {
            if(power.enginePower >= 0 && power.enginePower <= 10 && (power.reactorPower >= 0 && power.reactorPower <= 10))
            {
                power.enginePower--;
                power.reactorPower++;
            }
        }
    }
    public void PowerWeaponAdd()
    {
        if(pActive)
        {
            if(power.weaponPower >= 0 && power.weaponPower <= 10 && (power.reactorPower >= 0 && power.reactorPower <= 10))
            {
                WeaponAdd();
                //Invoke("WeaponAdd", 3f);
            }
        }
    }
    public void PowerWeaponMinus()
    {
        if (pActive)
        {
            if(power.weaponPower >= 0 && power.weaponPower <= 10 && (power.reactorPower >= 0 && power.reactorPower <= 10))
            {
                power.weaponPower--;
                power.reactorPower++;
            }
            
        }
    }
    public void PowerSensorAdd()
    {
        if(power.sensorPower >= 0 && power.sensorPower <= 10 && (power.reactorPower >= 0 && power.reactorPower <= 10)
            && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            SensorAdd();
            //Invoke("SensorAdd", 3f);
        }
    }
    public void PowerSensorMinus()
    {
        if(power.sensorPower >= 0 && power.sensorPower <= 10 && (power.reactorPower >= 0 && power.reactorPower <= 10)
            && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            power.sensorPower--;
            power.reactorPower++;
        }
    }
    void EnergyAdd()
    {
        power.enginePower++;
        power.reactorPower--;
    }
    void WeaponAdd()
    {
        power.weaponPower++;
        power.reactorPower--;
    }
    void SensorAdd()
    {
        power.sensorPower++;
        power.reactorPower--;
    }
}