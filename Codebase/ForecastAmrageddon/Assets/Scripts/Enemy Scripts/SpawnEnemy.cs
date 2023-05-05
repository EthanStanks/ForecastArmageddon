using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private int mWave = 1;
    public bool mStoppedSpawning = false;
    public int mMaxBasicSpawnAmount;
    public int mMaxTankSpawnAmount;
    public int mMaxFastSpawnAmount;

    //audio stuff
    AudioSource source;
    public AudioClip round_start;
    public AudioClip game_start;

    private void Start()
    {
        GameManager.Instance.isSpawnAgain = true;
        source = GetComponent<AudioSource>();
    }
    public void Update()
    {
        
        if (GameManager.Instance.isSpawnAgain)
        {
            SpawnWave(mWave);
            GameManager.Instance.isSpawnAgain = false;
        }
    }

    #region Spawn Types
    void SpawnBasic()
    {
        GameObject basicEnemy = Instantiate(GameManager.Instance.basicEnemyPrefab, transform.position, transform.rotation);
        basicEnemy.name = "Basic Enemy";
        basicEnemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 3.5f;
        GameManager.Instance.activeEnemiesInGame.Add(basicEnemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            basicEnemy.GetComponent<BasicEnemyScript>().SnowEffect();
            basicEnemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnBasicEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnBasic();

    }
    void SpawnTank()
    {
        GameObject tankEnemy = Instantiate(GameManager.Instance.tankEnemyPrefab, transform.position, transform.rotation);
        tankEnemy.name = "Tank Enemy";
        tankEnemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 2;
        GameManager.Instance.activeEnemiesInGame.Add(tankEnemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            tankEnemy.GetComponent<BasicEnemyScript>().SnowEffect();
            tankEnemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnTankEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnTank();

    }
    void SpawnFast()
    {
        GameObject fastEnemy = Instantiate(GameManager.Instance.fastEnemyPrefab, transform.position, transform.rotation);
        fastEnemy.name = "Fast Enemy";
        fastEnemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 7;
        GameManager.Instance.activeEnemiesInGame.Add(fastEnemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            fastEnemy.GetComponent<BasicEnemyScript>().SnowEffect();
            fastEnemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnFastEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnFast();

    }
    void SpawnSludger()
    {
        GameObject enemy = Instantiate(GameManager.Instance.sludgerEnemyPrefab, transform.position, transform.rotation);
        enemy.name = "Sludger Enemy";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 3.5f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnSludgerEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnSludger();
    }

    void SpawnTowerKiller()
    {
        GameObject enemy = Instantiate(GameManager.Instance.towerKillerEnemyPrefab, transform.position, transform.rotation);
        enemy.name = "Tower Killer Enemy";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 3.5f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnTowerKillerEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnTowerKiller();
    }
    void SpawnBaseKiller()
    {
        GameObject enemy = Instantiate(GameManager.Instance.baseKillerEnemyPrefab, transform.position, transform.rotation);
        enemy.name = "Base Killer Enemy";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 2f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnBaseKillerEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnBaseKiller();
    }
    void SpawnAcid()
    {
        GameObject enemy = Instantiate(GameManager.Instance.acidEnemyPrefab, transform.position, transform.rotation);
        enemy.name = "Acid Enemy";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 3.5f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnAcidEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnAcid();
    }
    void SpawnBuffer()
    {
        GameObject enemy = Instantiate(GameManager.Instance.bufferBossPrefab, transform.position, transform.rotation);
        enemy.name = "Buffer Boss";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 1f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnBufferBoss(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnBuffer();
    }
    void SpawnSummoner()
    {
        GameObject enemy = Instantiate(GameManager.Instance.bufferBossPrefab, transform.position, transform.rotation);
        enemy.name = "Summoner Boss";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 1f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnSummonerBoss(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnSummoner();
    }
    void SpawnTransporter()
    {
        GameObject enemy = Instantiate(GameManager.Instance.transporterBossPrefab, transform.position, transform.rotation);
        enemy.name = "Transporter Boss";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 1f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
            enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
        GameManager.Instance.enemyCount++;
    }
    IEnumerator SpawnTransporterBoss(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnTransporter();
    }
    #endregion

    void SpawnWave(int _wave)
    {
        switch (_wave)
        {
            case 1:
                source.PlayOneShot(game_start);
                mStoppedSpawning = false;
                GameManager.Instance.roundCount = 1;
                GameManager.Instance.weather1Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                GameManager.Instance.currentWeatherScript = GameManager.Instance.weather1Obj.GetComponent<WeatherEvents>();
                SpawnRound1();
                mStoppedSpawning = true;
                break;
            case 2:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather1Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 2;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather1Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound2();
                mStoppedSpawning = true;
                break;
            case 3:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather1Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 3;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather1Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound3();
                mStoppedSpawning = true;
                break;
            case 4:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather1Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 4;
                GameManager.Instance.weatherRoundCount = 3;
                GameManager.Instance.currentWeatherImg.sprite = GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.nextWeatherImg.sprite = GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                GameManager.Instance.currentWeatherScript = GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>();
                DebuffCardTowerEnhance();
                SpawnRound4();
                mStoppedSpawning = true;
                break;
            case 5:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 5;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound5();
                mStoppedSpawning = true;
                break;
            case 6:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 6;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound6();
                mStoppedSpawning = true;
                break;
            case 7:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather2Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 7;
                GameManager.Instance.weatherRoundCount = 3;
                GameManager.Instance.currentWeatherImg.sprite = GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.nextWeatherImg.sprite = GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                GameManager.Instance.currentWeatherScript = GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>();
                DebuffCardTowerEnhance();
                SpawnRound7();
                mStoppedSpawning = true;
                break;
            case 8:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 8;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound8();
                mStoppedSpawning = true;
                break;
            case 9:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 9;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound9();
                mStoppedSpawning = true;
                break;
            case 10:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather3Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 10;
                GameManager.Instance.weatherRoundCount = 3;
                GameManager.Instance.currentWeatherImg.sprite = GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.nextWeatherImg.sprite = GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                GameManager.Instance.currentWeatherScript = GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>();
                DebuffCardTowerEnhance();
                SpawnRound10();
                mStoppedSpawning = true;
                break;
            case 11:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 11;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound11();
                mStoppedSpawning = true;
                break;
            case 12:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 12;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound12();
                mStoppedSpawning = true;
                break;
            case 13:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather4Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 13;
                GameManager.Instance.weatherRoundCount = 3;
                GameManager.Instance.currentWeatherImg.sprite = GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.nextWeatherImg.sprite = GameManager.Instance.weather6Obj.GetComponent<WeatherEvents>().mIconSprite;
                GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                GameManager.Instance.currentWeatherScript = GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>();
                DebuffCardTowerEnhance();
                SpawnRound13();
                mStoppedSpawning = true;
                break;
            case 14:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 14;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound14();
                break;
            case 15:
                source.PlayOneShot(round_start);
                mStoppedSpawning = false;
                GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>().StopWeatherEventsCall();
                GameManager.Instance.roundCount = 15;
                GameManager.Instance.weatherRoundCount--;
                GameManager.Instance.weather5Obj.GetComponent<WeatherEvents>().WeatherEventsCall();
                DebuffCardTowerEnhance();
                SpawnRound15();
                mStoppedSpawning = true;
                break;
        }
        GameManager.Instance.UpdateHudVisuals();
        if (mWave != 15)
            mWave++;

    }

    void SpawnRound1()
    {
        StartCoroutine(SpawnBasicEnemy(1));
        StartCoroutine(SpawnBasicEnemy(1.5f));
        StartCoroutine(SpawnBasicEnemy(2));
        StartCoroutine(SpawnBasicEnemy(2.5f));
    }
    void SpawnRound2()
    {
        SpawnRound1();
        StartCoroutine(SpawnTankEnemy(3f));
    }
    void SpawnRound3()
    {
        SpawnRound2();
        StartCoroutine(SpawnFastEnemy(3.5f));
    }
    void SpawnRound4()
    {
        SpawnRound3();
        StartCoroutine(SpawnFastEnemy(4f));
    }
    void SpawnRound5()
    {
        SpawnRound4();
        StartCoroutine(SpawnTowerKillerEnemy(4.5f));
    }
    void SpawnRound6()
    {
        SpawnRound5();
        StartCoroutine(SpawnAcidEnemy(5f));
    }
    void SpawnRound7()
    {
        SpawnRound6();
        StartCoroutine(SpawnSludgerEnemy(5.5f));
    }
    void SpawnRound8()
    {
        SpawnRound7();
        StartCoroutine(SpawnBaseKillerEnemy(6f));
    }
    void SpawnRound9()
    {
        SpawnRound8();
        StartCoroutine(SpawnTankEnemy(6.5f));
        StartCoroutine(SpawnFastEnemy(7f));
        StartCoroutine(SpawnAcidEnemy(7.5f));
    }
    void SpawnRound10()
    {
        SpawnRound9();
        StartCoroutine(SpawnTankEnemy(8f));
        StartCoroutine(SpawnTowerKillerEnemy(8.5f));
    }
    void SpawnRound11()
    {
        SpawnRound10();
        StartCoroutine(SpawnSludgerEnemy(9f));
        StartCoroutine(SpawnFastEnemy(9.5f));
    }
    void SpawnRound12()
    {
        SpawnRound11();
        StartCoroutine(SpawnBaseKillerEnemy(10f));
        StartCoroutine(SpawnFastEnemy(10.5f));
    }
    void SpawnRound13()
    {
        SpawnRound12();
        StartCoroutine(SpawnTankEnemy(11f));
    }
    void SpawnRound14()
    {
        SpawnRound13();
        StartCoroutine(SpawnAcidEnemy(11.5f));
        StartCoroutine(SpawnFastEnemy(12f));
    }
    void SpawnRound15()
    {
        SpawnRound14();

        if (GameManager.Instance.playingMapNum == 1)
            StartCoroutine(SpawnSummonerBoss(12.5f));
        else if (GameManager.Instance.playingMapNum == 2 || GameManager.Instance.playingMapNum == 2)
            StartCoroutine(SpawnBufferBoss(12.5f));
    }

    void DebuffCardTowerEnhance()
    {
        foreach (var towers in GameManager.Instance.activeTowerInGame)
        {
            BasicTowerScript towerScript = towers.GetComponent<BasicTowerScript>();
            towerScript.mFireRate -= .04f;
        }
    }
}
