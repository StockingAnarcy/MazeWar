using UnityEngine;
using Mirror;
using NobleConnect.Mirror;
using TMPro;

public class NetworkLogic : NobleNetworkManager 
{
	public string playerName;

	public GameObject player;

    public MazeGenerator mg;

    
    public void SetupServer() 
	{
		mg.MazeSeed = Random.Range(0,10000); //Needs to change for all clients
        StartHost();	
	}

    public void SetupClient()
    {
        StartClient();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Client connected.");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("Client disconnected.");
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("Server received a client connection.");
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        Debug.Log("Server lost a client.");
    }
    

    // OnServerPrepared is called when the host is listening and has received 
    // their HostEndPoint from the NobleConnect service.
    // Use this HostEndPoint on the client in order to connect to the host.
    // Typically you would use a matchmaking system to pass the HostEndPoint to the client.
    // Look at the Match Up Example for one way to do it. Match Up comes free with any paid plan. 
    public override void OnServerPrepared(string hostAddress, ushort hostPort)
    {
        // Get your HostEndPoint here. 
        Debug.Log("Hosting at: " + hostAddress + ":" + hostPort);
        GameObject go = GameObject.Find("InfoHost");
        TMP_Text inf = go.GetComponent<TMP_Text>();
        inf.text = hostAddress + ":" + hostPort;

    }

}
