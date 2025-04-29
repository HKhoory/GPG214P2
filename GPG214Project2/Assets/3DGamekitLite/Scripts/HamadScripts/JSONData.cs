using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONData : MonoBehaviour
{
    private PlayerData data = new PlayerData();

    [SerializeField] private string playerPrefabName;
    [SerializeField] private GameObject player;
    [SerializeField] private Damageable health;
    [SerializeField] private int healthPoints;
    [SerializeField] private string fileName;
    [SerializeField] private bool isAchieved;
    private string filePath;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        player = GameObject.Find(playerPrefabName);
        health = player.GetComponent<Damageable>();
        healthPoints = health.currentHitPoints;

    }

    // Update is called once per frame
    void Update()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("Error, filePath doesn't exist");
            return;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }
    }

    private void SaveData()
    {
        if (player != null)
        {
            data.SetPlayerPosition(player.transform.position);
            data.SetHealth(healthPoints);
            string savedData = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, savedData);
        }
        else
        {
            Debug.Log("Error: Player not found, attempting to relocate player");
            player = GameObject.Find(playerPrefabName);
            health = player.GetComponent<Damageable>();
            healthPoints = health.currentHitPoints;
        }

    }

    private void LoadData()
    {
        if (File.Exists(filePath))
        {

            string jsonLoader = File.ReadAllText(filePath);

            data = JsonUtility.FromJson<PlayerData>(jsonLoader);
            transform.position = data.ReturnPlayerPosition();
            healthPoints = data.ReturnHealth();

        }
        else
        {
            Debug.Log("Error: Save File Not Found");

        }
    }
}
