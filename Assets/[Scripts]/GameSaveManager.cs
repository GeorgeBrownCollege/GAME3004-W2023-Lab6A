using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


[System.Serializable]
class PlayerData
{
    public string playerPosition;
    public string playerRotation;

    public PlayerData()
    {
        playerPosition = "";
        playerRotation = "";
    }
}


public class GameSaveManager : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>().transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveData();
        }
    }
    
    // Data Serialization = Encoding the data
    private void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerData.dat");
        PlayerData data = new PlayerData(); // creates an empty PlayerData object

        data.playerPosition = JsonUtility.ToJson(player.position);
        data.playerRotation = JsonUtility.ToJson(player.localEulerAngles);


        bf.Serialize(file, data);
        file.Close();

        // Player Prefs Example
        //var positionString = JsonUtility.ToJson(player.position);
        //var rotationString = JsonUtility.ToJson(player.localEulerAngles);

        //PlayerPrefs.SetString("position", positionString);
        //PlayerPrefs.SetString("rotation", rotationString);
        //PlayerPrefs.Save();

        print("Player Data Saved!");
    }

    // Data Deserialization = Decoding the Data
    private void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();


            var position = JsonUtility.FromJson<Vector3>(data.playerPosition);
            var rotation = JsonUtility.FromJson<Vector3>(data.playerRotation);
            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = position;
            player.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
            player.gameObject.GetComponent<CharacterController>().enabled = true;
        }

        // Player Prefs Example
        //var position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("position"));
        //var rotation = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("rotation"));

        //player.gameObject.GetComponent<CharacterController>().enabled = false;
        //player.position = position;
        //player.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        //player.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    private void ResetData()
    {
        // Player Prefs Example
        //PlayerPrefs.DeleteAll();
    }

    public void OnSaveButton_Pressed()
    {

    }

    public void OnLoadButton_Pressed()
    {

    }
}
