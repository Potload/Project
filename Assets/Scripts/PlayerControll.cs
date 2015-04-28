using UnityEngine;
using System.Collections;
//Bodhi Donselaar 2015
public class PlayerControll : MonoBehaviour {
    public GameObject CameraDirection;
    public int Player;
    public float WalkSpeedInput;
    float WalkSpeed;
    float TegenDruk;
    float TegenDrukVibratie;
    Vector3 CamForward;
    Vector3 CamRight;
    Quaternion PrevDir;
    Quaternion GoDir;
    float LerpShoot=10;
    float Hoek=0;

    void Start()
    {
        GoDir = transform.rotation;
    }

	void Update () 
    {
        WalkSpeed = WalkSpeedInput * Time.deltaTime;

        if (xbox.Connected [Player])
        {
            Walking();   
            Aiming();
            Shooting();
        } 
	}

    void Walking () 
    {
        PrevDir = transform.rotation;
        //LY
        CamForward=CameraDirection.transform.forward;
        CamForward = new Vector3(CamForward.x, 0, CamForward.z);
        CamForward = Vector3.ClampMagnitude(CamForward, 1);
        
        //LX
        CamRight=CameraDirection.transform.right;
        CamRight = new Vector3(CamRight.x, 0, CamRight.z);
        CamRight = Vector3.ClampMagnitude(CamRight, 1);
        
        transform.rotation=Quaternion.LookRotation(CamForward);
        transform.position += (CamForward * xbox.LY [Player]) * WalkSpeed;
        transform.position += (CamRight * xbox.LX [Player]) * WalkSpeed;
        transform.rotation = PrevDir;
    }

    void Shooting()
    {
        //Shooting RT
        if (xbox.RT [Player] > 0.5&&LaserCalculate.CoolDown[Player]>0&&!LaserCalculate.OverHeated[Player]&&LaserCalculate.Disable[Player]<0.1)
        {
            LerpShoot = 1;
            LaserCalculate.Shoot [Player] = true;
            LaserCalculate.CoolingDown[Player]=0;
            if (Hoek==0)
            {
                xbox.VR [Player] = 0.2f;
            }

        } else
        {
            LerpShoot = 10;
            LaserCalculate.Shoot [Player] = false;
        }
    }

    void Aiming()
    {
        PrevDir = transform.rotation;
        if (Mathf.Abs(xbox.RX[Player]) > xbox.DeadZone || Mathf.Abs(xbox.RY[Player]) > xbox.DeadZone)
        {
            transform.rotation= Quaternion.LookRotation(new Vector3(xbox.RX [Player], 0, xbox.RY [Player]));
            transform.Rotate(0, CameraDirection.transform.eulerAngles.y, 0);           
        }


        Hoek = LaserCalculate.Hoek [Player];
        if (Hoek != 0)
        {
            TegenDruk = 4000 * Mathf.Sin(Mathf.Abs(Hoek * 0.01745f));
           
            TegenDrukVibratie = Mathf.Sin(Mathf.Abs(Hoek * 0.01745f))+0.2f;
            TegenDrukVibratie = Mathf.Clamp(TegenDrukVibratie, 0, 1);
            xbox.VR [Player] = TegenDrukVibratie;

            if (Hoek > 0)
            {
                //print("Draai "+Player+" Naar Rechts");
                //transform.Rotate(0, Time.deltaTime * TegenDruk, 0);

            } else
            {
                //print("Draai "+Player+" Naar Links");
               // transform.Rotate(0, -Time.deltaTime * TegenDruk, 0);
            }

        } 
        GoDir=transform.rotation;

        transform.rotation = PrevDir;
        transform.rotation=Quaternion.Lerp(transform.rotation,GoDir,Time.deltaTime*LerpShoot);
    }
}
