using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyFramework;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

public class NetManager : UnitySingleton<NetManager>
{
    private Socket socketTcp;
    private readonly Queue<string> sendMsgQueue = new Queue<string>();
    private readonly Queue<string> receiveMsgQueue = new Queue<string>();
    private readonly byte[] buffer = new byte[1024 * 1024];
    private int receiveNum;
    private bool isConnected = false;

    public void Connect(string ip, int port)
    {
        if (isConnected)
            return;

        if (socketTcp == null)
            socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
            socketTcp.Connect(iPEndPoint);
            isConnected = true;
            ThreadPool.QueueUserWorkItem(SendMsg);
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }
        catch (SocketException e)
        {
            print($"客户端连接服务器报错：{e.Message}，错误码：{e.ErrorCode}");
            Close();
        }
        print("连接服务器成功");
    }

    private void Update()
    {
        if (receiveMsgQueue.Count > 0)
        {
            print($"收到服务发来的信息：{receiveMsgQueue.Dequeue()}");
        }
    }

    private void OnDestroy()
    {
        Close();
    }

    public void Send(string str) 
    {
        sendMsgQueue.Enqueue(str);
    }

    private void SendMsg(object obj)
    {
        while (isConnected) 
        {
            if (sendMsgQueue.Count > 0)
            {
                socketTcp.Send(Encoding.UTF8.GetBytes(sendMsgQueue.Dequeue()));
            }
        }
    }

    private void ReceiveMsg(object obj) 
    {
        while (isConnected)
        {
            if (socketTcp.Available > 0)
            {
                receiveNum = socketTcp.Receive(buffer);
                receiveMsgQueue.Enqueue(Encoding.UTF8.GetString(buffer, 0, receiveNum));
            }
        }
    }

    private void Close() 
    {
        if (socketTcp != null) 
        {
            socketTcp.Shutdown(SocketShutdown.Both);
            socketTcp.Close();
            isConnected = false;
        }
    }
}
