using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherEvents : MonoBehaviour
{
    public Sprite mIconSprite; // used for updating the icon's image
    public int mWeatherEventIndex; // 0 = sunny // 1 = rain // 2 = wind // 3 = lightning // 4 = ice storm // 5 = snow
    public bool mIsRainHealAvailable;
    public bool mIsLightningAvailable;
    public bool mIsIceStormAvailable;
    public bool mIsConveyorSlowed;
    public bool mStopIceStorm;
    public bool mStopRain;
    public bool mStopLightning;
    public bool mSkipWeather;

    // to know if the particles are in the world
    public bool mIsRainParticleActivated;
    public bool mIsSunnyParticleActivated;
    public bool mIsWindParticleActivated;
    public bool mIsSnowParticleActivated;
    public bool mIsLightningParticleActivated;

    private void Start()
    {
        mIsRainParticleActivated = false;
        mIsSunnyParticleActivated = false;
        mIsWindParticleActivated = false;
        mIsSnowParticleActivated = false;
        mIsLightningParticleActivated = false;
        mSkipWeather = false;
    }
    void Update()
    {
        if (mIsRainHealAvailable)
            ActivateRainWeatherEvent();

        if (mIsLightningAvailable)
            ActivateLightningWeatherEvent();

        if (mIsIceStormAvailable)
            ActivateIceStormWeatherEvent();
    }
    public void OverrideWeather(int newIndex, Sprite newSprite, string newObjName)
    {
        if (GameManager.Instance.playingMapNum != 4)
        {
            mSkipWeather = true;
            StopWeatherEventsCall();
            mWeatherEventIndex = newIndex;
            mIconSprite = newSprite;
            GameManager.Instance.currentWeatherImg.sprite = mIconSprite;
            gameObject.name = newObjName;
            WeatherEventsCall();
            mSkipWeather = false;
        }
    }
    public void WeatherEventsCall()
    {
        if (GameManager.Instance.playingMapNum != 4)
        {
            switch (mWeatherEventIndex)
            {
                case 0:
                    ActivateSunnyWeatherEvent();
                    break;
                case 1:
                    mIsRainHealAvailable = true;
                    break;
                case 2:
                    ActivateWindWeatherEvent();
                    break;
                case 3:
                    mIsLightningAvailable = true;
                    break;
                case 4:
                    mIsIceStormAvailable = true;
                    break;
                case 5:
                    ActivateSnowWeatherEvent();
                    break;
            }
        }
    }
    public void StopWeatherEventsCall()
    {
        if (GameManager.Instance.playingMapNum != 4)
        {
            switch (mWeatherEventIndex)
            {
                case 0:
                    DeactivateSunnyWeatherEvent();
                    break;
                case 1:
                    DeactivateRainWeatherEvent();
                    break;
                case 2:
                    DeactivateWindWeatherEvent();
                    break;
                case 3:
                    DeactivateLightningWeatherEvent();
                    break;
                case 4:
                    DeactivateIceStormWeatherEvent();
                    break;
                case 5:
                    DeactivateSnowWeatherEvent();
                    break;
            }
        }
    }

    #region Weather Event Activation and Deactivaton

    #region Sunny Weather
    // sunny weather method
    void ActivateSunnyWeatherEvent()
    {
        if (!mIsSunnyParticleActivated)
        {
            mIsSunnyParticleActivated = true;
            GameObject sunnyParticleObj = Instantiate(GetSunObj(GameManager.Instance.playingMapNum));
            GameManager.Instance.activeWeatherParticleObj = sunnyParticleObj;
        }
        foreach (var tower in GameManager.Instance.activeTowerInGame) // for ever tower in the scene
        {
            if (!tower.GetComponent<BasicTowerScript>().mIsEffectedBySun) // if the tower isnt already effected by the sun effect
            {
                tower.GetComponent<BasicTowerScript>().mIsEffectedBySun = true;
                tower.GetComponent<BasicTowerScript>().SunEffect();
            }
        }

    }
    public void DeactivateSunnyWeatherEvent()
    {
        Destroy(GameManager.Instance.activeWeatherParticleObj);
        mIsSunnyParticleActivated = false;
        foreach (var tower in GameManager.Instance.activeTowerInGame) // for ever tower in the scene
        {
            tower.GetComponent<BasicTowerScript>().mIsEffectedBySun = false;
            tower.GetComponent<BasicTowerScript>().RevertSunEffect(); // sets the damage on the towers back to the master
        }

    }

    #endregion

    #region Rain Weather
    // rain weather method
    void ActivateRainWeatherEvent()
    {
        mIsRainHealAvailable = false;

        if (!mIsRainParticleActivated)
        {
            mIsRainParticleActivated = true;
            GameObject rainParticleObj = Instantiate(GetRainObj(GameManager.Instance.playingMapNum));
            GameManager.Instance.activeWeatherParticleObj = rainParticleObj;
            GameManager.Instance.worldLight.intensity = 0.6f;
        }
        foreach (var tower in GameManager.Instance.activeTowerInGame) // for ever tower in the scene
        {
            tower.GetComponent<BasicTowerScript>().RainEffect(); // adds 1 hp to each tower
        }
        if (GameManager.Instance.baseHealthCurrent < GameManager.Instance.baseHealthOriginal)
        {
            GameManager.Instance.baseHealthCurrent++; // adds 1 health to base health
            GameManager.Instance.UpdateHudVisuals(); // since we updated base health we need to update the display bar
        }
        StartCoroutine(RainCoolDown(10));
    }
    public void DeactivateRainWeatherEvent()
    {
        Destroy(GameManager.Instance.activeWeatherParticleObj);
        GameManager.Instance.worldLight.intensity = 1;
        mStopRain = true;
        mIsRainHealAvailable = false;
        mIsRainParticleActivated = false;
    }
    IEnumerator RainCoolDown(float wait)
    {
        yield return new WaitForSeconds(wait);
        if (!mStopRain)
            mIsRainHealAvailable = true;
    }

    #endregion

    #region Wind Weather
    // wind weather method
    void ActivateWindWeatherEvent()
    {
        if (!mIsWindParticleActivated)
        {
            mIsWindParticleActivated = true;
            GameObject windParticleObj = Instantiate(GetWindObj(GameManager.Instance.playingMapNum));
            GameManager.Instance.activeWeatherParticleObj = windParticleObj;
        }
        foreach (var tower in GameManager.Instance.activeTowerInGame) // for ever tower in the scene
        {
            if (!tower.GetComponent<BasicTowerScript>().mIsEffectedByWind) // if the tower isnt already effected by the sun effect
            {
                tower.GetComponent<BasicTowerScript>().mIsEffectedByWind = true;
                tower.GetComponent<BasicTowerScript>().WindEffect();
            }
        }
    }
    public void DeactivateWindWeatherEvent()
    {
        Destroy(GameManager.Instance.activeWeatherParticleObj);
        mIsWindParticleActivated = false;
        foreach (var tower in GameManager.Instance.activeTowerInGame) // for ever tower in the scene
        {
            tower.GetComponent<BasicTowerScript>().mIsEffectedByWind = false;
            tower.GetComponent<BasicTowerScript>().RevertWindEffect();

        }
    }

    #endregion

    #region Lightning Weather
    // lightning weather method
    void ActivateLightningWeatherEvent()
    {
        mIsLightningAvailable = false;
        if (!mIsRainParticleActivated)
        {
            mIsRainParticleActivated = true;
            GameObject rainParticleObj = Instantiate(GetRainObj(GameManager.Instance.playingMapNum));
            GameManager.Instance.activeWeatherParticleObj = rainParticleObj;
            GameManager.Instance.worldLight.intensity = 0.3f;
        }
        if (!mIsLightningParticleActivated)
        {
            mIsLightningParticleActivated = true;
            GameObject lightningParticleObj = Instantiate(GetLightningObj(GameManager.Instance.playingMapNum));
            Destroy(lightningParticleObj, 2);
        }
        if (GameManager.Instance.lightningStrikedEnemies.Count > 0)
        {
            foreach (var enemy in GameManager.Instance.lightningStrikedEnemies) // for ever tower in the scene
            {
                if (enemy != null)
                    enemy.GetComponent<BasicEnemyScript>().LightningEffect();
            }
        }
        StartCoroutine(LightningCoolDown(13));
    }
    public void DeactivateLightningWeatherEvent()
    {
        if (GameManager.Instance.weatherRoundCount == 0 || mSkipWeather)
        {
            Destroy(GameManager.Instance.activeWeatherParticleObj);
            GameManager.Instance.worldLight.intensity = 1;
            mStopLightning = true;
            mIsLightningAvailable = false;
            mIsLightningParticleActivated = false;
            mIsRainParticleActivated = false;
        }
    }
    IEnumerator LightningCoolDown(float wait)
    {
        yield return new WaitForSeconds(wait);
        if (!mStopLightning)
        {
            mIsLightningAvailable = true;
            mIsLightningParticleActivated = false;
        }
    }

    #endregion

    #region Ice Storm Weather
    // ice storm weather method
    void ActivateIceStormWeatherEvent()
    {
        mIsIceStormAvailable = false;
        if (!mIsSnowParticleActivated)
        {
            mIsSnowParticleActivated = true;
            GameObject snowParticleObj = Instantiate(GetSnowObj(GameManager.Instance.playingMapNum));
            GameManager.Instance.activeWeatherParticleObj = snowParticleObj;
            GameManager.Instance.worldLight.intensity = 0.1f;
        }
        if (!mIsLightningParticleActivated)
        {
            mIsLightningParticleActivated = true;
            GameObject lightningParticleObj = Instantiate(GetLightningObj(GameManager.Instance.playingMapNum));
            Destroy(lightningParticleObj, 2);
        }
        if (!mIsConveyorSlowed)
        {
            float speedA = GameManager.Instance.mConveyor1Moving.speed;
            float speedB = GameManager.Instance.cardMoving.speed;
            speedA *= .5f;
            speedB *= .5f;
            GameManager.Instance.SetConveyorSpeed(speedA, speedB);
            mIsConveyorSlowed = true;
        }

        if (GameManager.Instance.lightningStrikedEnemies.Count > 0)
        {
            foreach (var obj in GameManager.Instance.iceStrikeObjs) // for ever tower in the scene
            {
                if (obj != null)
                {
                    if (obj.CompareTag("Enemy"))
                        obj.GetComponent<BasicEnemyScript>().IceStormEffect();
                    else if (obj.CompareTag("Tower"))
                        obj.GetComponent<BasicTowerScript>().IceStormEffect();
                }

            }
        }
        StartCoroutine(IceStormCoolDown(20));
    }
    public void DeactivateIceStormWeatherEvent()
    {
        if (GameManager.Instance.weatherRoundCount == 0 || mSkipWeather)
        {
            Destroy(GameManager.Instance.activeWeatherParticleObj);
            GameManager.Instance.worldLight.intensity = 1;
            GameManager.Instance.SetConveyorSpeed(GameManager.Instance.conveyorSpeedMaster, GameManager.Instance.cardSpeedMaster);
            mIsConveyorSlowed = false;
            mIsLightningParticleActivated = false;
            mStopIceStorm = true;
            mIsIceStormAvailable = false;
            mIsSnowParticleActivated = false;
        }
    }
    IEnumerator IceStormCoolDown(float wait)
    {
        yield return new WaitForSeconds(wait);
        if (!mStopIceStorm)
        {
            mIsIceStormAvailable = true;
            mIsLightningParticleActivated = false;
        }
    }
    // snow weather method

    #endregion

    #region Snow Weather
    void ActivateSnowWeatherEvent()
    {
        if (!mIsSnowParticleActivated)
        {
            mIsSnowParticleActivated = true;
            GameObject snowParticleObj = Instantiate(GetSnowObj(GameManager.Instance.playingMapNum));
            GameManager.Instance.activeWeatherParticleObj = snowParticleObj;
            GameManager.Instance.worldLight.intensity = 0.1f;
        }
        foreach (var enemy in GameManager.Instance.activeEnemiesInGame) // for ever tower in the scene
        {
            if (!enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow) // if the tower isnt already effected by the sun effect
            {
                enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
                enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            }
        }
    }
    public void DeactivateSnowWeatherEvent()
    {
        Destroy(GameManager.Instance.activeWeatherParticleObj);
        mIsSnowParticleActivated = false;
        GameManager.Instance.worldLight.intensity = 1;
        foreach (var enemy in GameManager.Instance.activeEnemiesInGame) // for ever tower in the scene
        {
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = false;
            enemy.GetComponent<BasicEnemyScript>().RevertSnowEffect();
        }
    }
    #endregion

    #endregion

    #region Get Particle and Sound Prefab
    GameObject GetSunObj(int map)
    {
        switch (map)
        {
            case 1:
                return GameManager.Instance.sunnyObjectMap1;
            case 2:
                return GameManager.Instance.sunnyObjectMap2;
            case 3:
                return GameManager.Instance.sunnyObjectMap3;
        }
        return null;
    }
    GameObject GetRainObj(int map)
    {
        switch (map)
        {
            case 1:
                return GameManager.Instance.rainObjectMap1;
            case 2:
                return GameManager.Instance.rainObjectMap2;
            case 3:
                return GameManager.Instance.rainObjectMap3;
        }
        return null;
    }
    GameObject GetWindObj(int map)
    {
        switch (map)
        {
            case 1:
                return GameManager.Instance.windObjectMap1;
            case 2:
                return GameManager.Instance.windObjectMap2;
            case 3:
                return GameManager.Instance.windObjectMap3;
        }
        return null;
    }
    GameObject GetSnowObj(int map)
    {
        switch (map)
        {
            case 1:
                return GameManager.Instance.snowObjectMap1;
            case 2:
                return GameManager.Instance.snowObjectMap2;
            case 3:
                return GameManager.Instance.snowObjectMap3;
        }
        return null;
    }
    GameObject GetLightningObj(int map)
    {
        switch (map)
        {
            case 1:
                return GameManager.Instance.lightningMap1;
            case 2:
                return GameManager.Instance.lightningMap2;
            case 3:
                return GameManager.Instance.lightningMap3;
        }
        return null;
    }

    #endregion
}
