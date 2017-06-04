using UnityEngine;
using UnityEngine.UI;

public class BluetoothControl : MonoBehaviour
{
    public Text textDevices;
    public Button button;

    //만든 플러그인을 사용할 준비
    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    AndroidJavaObject _activity;

    void Start()
    {
        _activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public void BluetoothStart()
    {
        textDevices.text = "Start Bluetooth";
        CallBluetoothInit();    //블루투스 초기화
        CallConnectedDevice("BLUE");  //BLUE라는 이름의 블루투스 기기 연결 시도(기기 이름 변경 가능)

        if (_activity.Call<int>("isBluetoothConnected") == 1)  //연결 상태 확인
        {
            textDevices.text = "Success";
        }
    }

    //플러그인에서 만든 클래스의 SendData 메소드에 10이라는 인자를 주며 호출
    public void CallSendMessage()
    {
        _activity.Call("SendData", 10);
    }

    //BluetoothInit 메소드를 플러그인에서 호출
    void CallBluetoothInit()
    {
        _activity.Call("BluetoothInit");
    }

    //CallConnectedDevice 메서드를 플러그인에서 호출
    void CallConnectedDevice(string name)
    {
        _activity.Call("ConnectedDevice", name);
    }

    //플러그인에서 에러발생시 호출하는 메서드
    void ErrorMessage(string errorMsg)
    {
        textDevices.text = errorMsg;
    }

    //플러그인에서 찾은 기기 목록 데이터 전송
    void SearchDevices(string devices)
    {
        textDevices.text = devices;
    }

    //플러그인에서 받은 데이터
    public void ReceiveData(string str)
    {
        textDevices.text = str;
    }
}
