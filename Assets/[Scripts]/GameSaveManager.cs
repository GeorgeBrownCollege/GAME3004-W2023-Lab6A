using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        var positionString = JsonUtility.ToJson(player.position);
        var rotationString = JsonUtility.ToJson(player.localEulerAngles);

        PlayerPrefs.SetString("position", positionString);
        PlayerPrefs.SetString("rotation", rotationString);
        PlayerPrefs.Save();
    }

    // Data Deserialization = Decoding the Data
    private void LoadData()
    {
        var position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("position"));
        var rotation = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("rotation"));
        var camera = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("camera"));

        player.gameObject.GetComponent<CharacterController>().enabled = false;
        player.position = position;
        player.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        playerCam.rotation = Quaternion.Euler(camera.x, camera.y, camera.z);
        player.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnSaveButton_Pressed()
    {

    }

    public void OnLoadButton_Pressed()
    {

    }
}
