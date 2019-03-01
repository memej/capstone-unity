using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseJson : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Parsing JSON");

        /* Capstone */
        string json = File.ReadAllText(Application.dataPath + "/json2.json");
        Debug.Log(json);

        Topology loaded_data = JsonUtility.FromJson<Topology>(json);

        /*  - Ethernet */
        Debug.Log("Extracting: Ethernet Clients");

        EthConnection[] eth_clients = loaded_data.eth_clients;
        for (int i = 0; i < eth_clients.Length; ++i)
        {
            // eth_client
            EthConnection eth_client = eth_clients[i];
            string serial = eth_client.serial;

            //  - clients[]
            //      - idle
            //      - target_mac
            for (int j = 0; j < eth_client.clients.Length; ++j)
            {
                Eth curr_client = eth_client.clients[j];
                string idle = curr_client.idle;
                string target_mac = curr_client.target_mac;

                Debug.Log(i + "." + j + ") " + "Idle: " + idle + " | target_mac: " + target_mac);
            }

            //  - serial
            Debug.Log(i + ") " + "Serial: " + serial);
        }

        /*  - Mesh Links */
        Debug.Log("Extracting: Mesh Links");

        MeshLink[] mesh_links = loaded_data.mesh_links;
        for (int i = 0; i < mesh_links.Length; ++i)
        {
            // mesh_link
            MeshLink mesh_link = mesh_links[i];
            bool is_master = mesh_link.isMaster;
            string main_serial = mesh_link.serial;

            //  - connected_to[]
            //      - rssi
            //      - serial
            for (int j = 0; j < mesh_link.connected_to.Length; ++j)
            {
                Device curr_device = mesh_link.connected_to[j];
                int rssi = curr_device.rssi;
                string serial = curr_device.serial;

                Debug.Log(i + "." + j + ") " + "rssi: " + rssi + " | target_mac: " + serial);
            }

            //  - isMaster
            //  - serial (mesh_link object / main)
            Debug.Log(i + ") " + "isMaster: " + is_master + " | serial: " + main_serial);
        }

        /*  - Sta (Wireless) */
        Debug.Log("Extracting: Sta Clients");

        StaConnection[] sta_clients = loaded_data.sta_clients;
        for (int i = 0; i < loaded_data.sta_clients.Length; i++)
        {
            // sta_client
            StaConnection sta_client = sta_clients[i];
            string main_serial = sta_client.serial;

            //  - clients[]
            for (int j = 0; j < sta_client.clients.Length; ++j)
            {
                Sta curr_client = sta_client.clients[j];
                int rssi = curr_client.rssi;
                int rxpr = curr_client.rxpr;
                string target_mac = curr_client.target_mac;
                int txpr = curr_client.txpr;

                Debug.Log(i + "." + j + ") " + "rssi: " + rssi + " | rxpr: " + rxpr + " | target_mac: " + target_mac + " | txpr: " + txpr);
            }

            //  - serial (mesh_link object / main)
            Debug.Log(i + ") " + "serial: " + main_serial);
        }

        /* Test JSON */
        //string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        //Debug.Log(json);

        //PlayerData loaded_player_data = JsonUtility.FromJson<PlayerData>(json);
        //Debug.Log("Health: " + loaded_player_data.data[0].health);
        //Debug.Log("Pos: " + loaded_player_data.data[0].position[0]);
    }

    /* Testing Classes - For JSON  */
    //[System.Serializable]
    //private class PlayerData
    //{
    //    public Data[] data;
    //}

    //[System.Serializable]
    //private class Data
    //{
    //    public Vector3[] position;
    //    public int health;
    //}
    /********************/

    [System.Serializable]
    private class Topology
    {
        public EthConnection[] eth_clients;    // eth_clients
        public StaConnection[] sta_clients;        // sta_clients
        public MeshLink[] mesh_links;       // mesh_links
    }

    [System.Serializable]
    private class EthConnection : Connection<Eth> { }

    [System.Serializable]
    private class StaConnection : Connection<Sta> { }

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
        public string idle;         // Made into string to store exact numbers = "9.10" vs. if double -> value = 9.1000000012
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
