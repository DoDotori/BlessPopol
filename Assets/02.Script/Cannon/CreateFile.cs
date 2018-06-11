using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateFile : MonoBehaviour {
    private static CreateFile _instance =null;
    public static CreateFile Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(CreateFile)) as CreateFile;
            }

            return _instance;
        }
    }

    string m_strPath = "Assets/Resources/";
    // Use this for initialization
   
    public void WriteData(string strData)
    {
        FileStream f = new FileStream(m_strPath + "FireData.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
        f.Close();
    }
    public void WriteData(string num, string startSetaX, string startSetaY, string startPower)
    {
        string infor = string.Format(num + "," + startSetaX + "," + startSetaY + "," + startPower);
        WriteData(infor);
    }

    public void Parse()
    {
        FileStream f = new FileStream("FireData.txt", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(f);

        // 먼저 한줄을 읽는다. 
        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )

        while (source != null)
        {
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            // _list.Add(int.Parse(values[0]), source);  <<< 이런식으로 dictionary에 저장 ->타일넘버대로 정렬됨
            if (values.Length == 0)
            {
                sr.Close();
                return;
            }
            source = sr.ReadLine();    // 한줄 읽는다.

        }
    }
}
