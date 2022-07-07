using System;
using System.Text;
using UnityEngine;
using TinyFramework;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using UnityEngine.UI;

public class Player : BaseData
{
    public short atk;

    public override int GetBytesNum()
    {
        return sizeof(short);
    }

    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        atk = ReadShort(bytes, ref index);
        return index - beginIndex;
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] data = new byte[GetBytesNum()];
        WriteShort(data, atk, ref index);
        return data;
    }
}

public class Person : BaseData
{
    public string name;
    public int level;
    public bool sex;
    public Player p;

    public override int GetBytesNum()
    {
        return sizeof(int) + Encoding.UTF8.GetBytes(name).Length + sizeof(int) + sizeof(byte) + p.GetBytesNum();
    }

    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        name = ReadString(bytes, ref index);
        level = ReadInt(bytes, ref index);
        sex = ReadBool(bytes, ref index);
        p = ReadData<Player>(bytes, ref index);
        return index - beginIndex;
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] data = new byte[GetBytesNum()];
        WriteString(data, name, ref index);
        WriteInt(data, level, ref index);
        WriteBool(data, sex, ref index);
        WriteData(data, p, ref index);
        return data;
    }
}

public class Test : MonoBehaviour
{
    public Button button;
    public InputField inputField;

    void Start()
    {
        button.onClick.AddListener(()=> 
        {
            NetManager.Instance.Send(inputField.text);
        });
    }

    public void Serialization() 
    {
        Player player = new Player
        {
            atk = 100
        };

        Person person = new Person
        {
            name = "����",
            level = 22,
            sex = true,
            p = player
        };

        // ���л�
        byte[] data = person.Writing();
        // �����л�
        Person person1 = new Person();
        person1.Reading(data);

        print($"���л���{person.name} {person.level} {person.sex} {person.p.atk}");
        print($"�����л���{person1.name} {person1.level} {person1.sex} {person1.p.atk}");
    }

    public void SocketTCP()
    {
        Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

        try 
        {
            socketTcp.Connect(iPEndPoint);
        } 
        catch (SocketException e)
        {
            print($"�ͻ������ӷ��������������룺{e.ErrorCode}");
            return;
        }
        print("���ӷ������ɹ�");

        byte[] buffer = new byte[1024];
        int receiveNum = socketTcp.Receive(buffer);
        string receiveStr = Encoding.UTF8.GetString(buffer, 0, receiveNum);
        print($"�յ�����������Ϣ��{receiveStr}");

        string sendStr = "����007";
        socketTcp.Send(Encoding.UTF8.GetBytes(sendStr));

        socketTcp.Shutdown(SocketShutdown.Both);
        socketTcp.Close();
    }
}
