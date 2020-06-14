using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


public class SaveData : MonoBehaviour
{
    public GameData data;//call inside gamedata
    string dataFilePath;
    BinaryFormatter Bf;//storl to binary

    public Text txtCoinCount;

    void Awake()
    {
        Bf = new BinaryFormatter(); //creat binary
        dataFilePath = Application.persistentDataPath + "/Game.dat"; //find file install location, where to save
        //Debug.Log(dataFilePath);
    }
    public void saveData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create); //unwarp binart file,(where is parth),use parth to creat new fie
        Bf.Serialize(fs, data);//save data to fs
        fs.Close();
    }
    public void loadData()
    {
        if (File.Exists(dataFilePath))//check if file is exist or not
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)Bf.Deserialize(fs);//acces data decode
            fs.Close();
            Debug.Log("Number of Coin" + data.coinCount);
            txtCoinCount.text = data.coinCount.ToString();
        }
    }
    
    public void UpdateCoinCount()
    {
        data.coinCount += 1;
        txtCoinCount.text = data.coinCount.ToString();
    }
    public void ResetCoinCount()
    {
        data.coinCount = 0;
        txtCoinCount.text = data.coinCount.ToString();
        Debug.Log("Data Save");
        saveData();
    }

    private void OnEnable()
    {
        Debug.Log("Data Loded");
        loadData();
    }
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Data Save");
            saveData();
        }
    }

}


