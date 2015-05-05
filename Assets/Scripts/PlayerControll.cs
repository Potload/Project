using UnityEngine;
using System.Collections;
//Bodhi Donselaar 2015
public class PlayerControll : MonoBehaviour {
    public bool DebugAlwaysShoot;
    public bool DrijfUitElkaar;
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
    bool Abort = false;
    bool Abort2=false;
    private Ray ray;                    
    private RaycastHit hit;     
    public Transform normal;

    void Start()
    {
        GoDir = transform.rotation;
    }

	void Update () 
    {
        WalkSpeed = WalkSpeedInput * Time.deltaTime;

        //reset
        LaserCalculate.Shoot [gameObject.layer-8] = false;

        if (xbox.Connected [Player]&&Time.timeScale>0)
        {
            Walking();   
            Aiming();
            Shooting();
        } 
        if (!xbox.Connected [Player]&&Time.timeScale>0&&DebugAlwaysShoot)
        {
            Walking();   
            Aiming();
            Shooting();
        } 
        if (DebugAlwaysShoot)
        {
            LaserCalculate.Shoot [gameObject.layer-8] = true;
            LaserCalculate.CoolingDown[gameObject.layer-8]=0;
            LaserCalculate.CoolDown[gameObject.layer-8]=1;
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
        Abort2 = false;
        CheckForWall(transform.position + (CamRight * xbox.LX [Player]) * WalkSpeed);
        if (Abort)
        {
            Abort2=true;
        }
        CheckForWall(transform.position + (CamForward * xbox.LY [Player]) * WalkSpeed);
        Vector3 Aim=((CamRight * xbox.LX [Player]))+((CamForward * xbox.LY [Player]));


        Debug.DrawLine(transform.position,transform.position+(Aim*10000));
        if (Abort||Abort2)
        {

            int layerMask = 1 << gameObject.layer;
            layerMask = ~layerMask;

            ray = new Ray(transform.position,Aim);  
            if(Physics.Raycast(ray.origin,ray.direction, out hit,10,layerMask))
            { 
                ray = new Ray(transform.position,-hit.normal);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit,10,layerMask))
                { 
               
                    normal.rotation=Quaternion.LookRotation(hit.normal);
                    CheckForWall(transform.position - ((normal.right * Vector3.Cross(Aim,hit.normal).y)*WalkSpeed));
                }
            }
        }
        transform.rotation = PrevDir;
    }

    void Shooting()
    {
        //Shooting RT
        if (xbox.RT [Player] > 0.5&&LaserCalculate.CoolDown[Player]>0&&!LaserCalculate.OverHeated[Player]&&LaserCalculate.Disable[Player]<0.1)
        {
            LerpShoot = 1;
            LaserCalculate.Shoot [gameObject.layer-8] = true;
            LaserCalculate.CoolingDown[gameObject.layer-8]=0;
            if (Hoek==0)
            {
                xbox.VR [Player] = 0.2f;
            }

        } else
        {
            LerpShoot = 10;
            LaserCalculate.Shoot [gameObject.layer-8] = false;
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
            if (DrijfUitElkaar)
            {
                if (Hoek > 0)
                {
                    transform.Rotate(0, Time.deltaTime * TegenDruk, 0);
                } else
                {
                    transform.Rotate(0, -Time.deltaTime * TegenDruk, 0);
                }
            }

        } 
        GoDir=transform.rotation;

        transform.rotation = PrevDir;
        transform.rotation=Quaternion.Lerp(transform.rotation,GoDir,Time.deltaTime*LerpShoot);
    }

    void CheckForWall(Vector3 Pos)
    {
        Abort = false;
        Collider[] hitColliders = Physics.OverlapSphere(Pos, 1); 
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders [i].gameObject.layer == 0)
            {
                Abort = true;
            }
            if (hitColliders [i].gameObject.layer != gameObject.layer && hitColliders [i].tag == "Player")
            {
                Abort = true;
            }

        }
        if (!Abort)
        {
            transform.position = Pos;
        }
    }
}
