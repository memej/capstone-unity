﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseJson : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello" + " world");

        //PlayerData player_data = new PlayerData();
        //player_data.position = new Vector3(5, 0);
        //player_data.health = 70;


        // Convert to JSON
        //string json = JsonUtility.ToJson(player_data);
        //Debug.Log(json);


        /*
        // Write to File
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
        */

        // Read from File
        //string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        //Debug.Log(json);

        //Test ethernet = JsonUtility.FromJson<Test>(json);
        //Debug.Log("**Serial: " + ethernet.ethernet[0].serial);

        /* Capstone */
        string json = File.ReadAllText(Application.dataPath + "/json1.json");
        Debug.Log(json);

        Topology loaded_data = JsonUtility.FromJson<Topology>(json);

        /*  - Ethernet */
        //EthClient loaded_player_data = JsonUtility.FromJson<EthClient>(json);
        //Debug.Log("Client - Idle: " + loaded_data.eth_clients[0].clients[0].idle);
        //Debug.Log("Client - TMac: " + loaded_data.eth_clients[0].clients[0].target_mac);
        //Debug.Log("Serial: " + loaded_data.eth_clients[0].serial);

        Debug.Log("eth_client: " + loaded_data.eth_clients[0].clients[0].idle);
        Debug.Log("mesh_links: " + loaded_data.mesh_links[0].isMaster);

        /*  - Mesh Links */


        /* Test JSON */
        //string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        //Debug.Log(json);

        //PlayerData loaded_player_data = JsonUtility.FromJson<PlayerData>(json);
        //Debug.Log("Health: " + loaded_player_data.data[0].health);
        //Debug.Log("Pos: " + loaded_player_data.data[0].position[0]);
    }

    /* Testing Classes */
    [System.Serializable]
    private class PlayerData
    {
        public Data[] data;
    }

    [System.Serializable]
    private class Data
    {
        public Vector3[] position;
        public int health;
    }
    /********************/

    [System.Serializable]
    private class Topology
    {
        public EthConnection[] eth_clients;    // eth_clients
        //public StaConnection[] wifi;        // sta_clients
        public MeshLink[] mesh_links;       // mesh_links
    }

    //[System.Serializable]
    //private class EthClient
    //{
    //    public EthConnection[] clients;
    //}

    //[System.Serializable]
    //private class StaClient
    //{
    //    public StaConnection[] sta_clients;
    //}

    [System.Serializable]
    private class EthConnection : Connection<Eth>
    {

    }

    [System.Serializable]
    private class StaConnection : Connection<Sta>
    {

    }

    [System.Serializable]
    private class Connection<T>
    {
        public T[] clients;
        public string serial;
    }

    // JSON Objects
    [System.Serializable]
    private class Eth
    {
        /*
         * Example Values      
            'idle': '9.10',
            'target_mac': '00:40:ad:91:be:a0'
        */
        public string idle;         // Made into string to store exact numbers; if double -> value = 9.1000000012
        public string target_mac;
    }

    [System.Serializable]
    private class MeshLink
    {
        public Device[] connected_to;         // connected_to
        public bool isMaster;
        public string serial;
    }

    [System.Serializable]
    private class Sta
    {
        /*
         Example Values
            'rssi': -300,
            'rxpr': 130,
            'target_mac': 'd8:31:34:00:bd:8f',
            'txpr': 144
        */
        public int rssi;
        public int rxpr;
        public string target_mac;
        public int txpr;
    }

    [System.Serializable]
    private class Device
    {
        public int rssi;
        public string serial;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
