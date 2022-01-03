using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    //karakter yang akan dispawn
    public GameObject[] character;
    //powerup yang akan dispawn
    public GameObject[] powerup;
    //teks wave dan score
    public Text waveText;
    public Text scoreText;
    //area spawn
    private float spawnRangeA = 15f;
    private float spawnRangeB = -15f;
    //
    public int enemyCount;
    //nilai awal wave dan score
    public int waveNumber = 1;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRandom(waveNumber);
        waveText.text = waveNumber.ToString();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //menghitung jml enemy
        enemyCount = FindObjectsOfType<Enemy>().Length;
        //jika enemy kosong, wave baru datang
        if(enemyCount == 0)
        {
            waveNumber++;
            score += 10;
            SpawnRandom(waveNumber);
            waveText.text = waveNumber.ToString();
            scoreText.text = score.ToString();
        }
    }

    void SpawnRandom(int wave)
    {
        for(int i = 0; i< wave; i++)
        {
            //pengambilan posisi secara random
            Vector3 spawnPos = new Vector3(Random.Range(spawnRangeB, spawnRangeA), 0, Random.Range(spawnRangeB, spawnRangeA));
            //pengambilan index objek secara random
            int characterIndex = Random.Range(0, character.Length);
            //instansiasi
            Instantiate(character[characterIndex], spawnPos, character[characterIndex].transform.rotation);
            
        }
        
        //jika berada di wave dengan kelipatan 5, powerup spawn
        if ((wave % 5) == 0)
        {
            //pengambilan index objek secara random
            int powerupIndex = Random.Range(0, powerup.Length);
            //pengambilan posisi secara random
            Vector3 spawnPos2 = new Vector3(Random.Range(spawnRangeB, spawnRangeA), powerup[powerupIndex].transform.position.y, Random.Range(spawnRangeB, spawnRangeA));
            //instansiasi
            Instantiate(powerup[powerupIndex], spawnPos2, powerup[powerupIndex].transform.rotation);
        }
        //}

    }
}
