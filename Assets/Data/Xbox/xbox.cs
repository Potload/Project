using UnityEngine;
using XInputDotNetPure;
//Bodhi Donselaar 2015
public class xbox : MonoBehaviour
{
    PlayerIndex playerIndex;
    GamePadState state;

    //deadzone
    public float TheDeadZone=0.2f;
    public static float DeadZone = 0.2f;
    public static bool[] Connected;

	//analog
	public static float[] LX;
	public static float[] LY;
	public static float[] RX;
	public static float[] RY;
	public static float[] LT;
	public static float[] RT;

	//digital
	public static int[] A;
	public static int[] B;
	public static int[] X;
	public static int[] Y;
	public static int[] LS;
	public static int[] RS;
	public static int[] LB;
	public static int[] RB;
	public static int[] BACK;
	public static int[] START;
	public static int[] DPADUP;
	public static int[] DPADDOWN;
	public static int[] DPADLEFT;
	public static int[] DPADRIGHT;

	//vibration
	public static float[] VL;
	public static float[] VR;

    void Start()
    {
        LX=new float[4];
        LY=new float[4];
        RX=new float[4];
        RY=new float[4];
        LT=new float[4];
        RT=new float[4];

        A = new int[4];
        B = new int[4];
        X = new int[4];
        Y = new int[4];
        LS = new int[4];
        RS= new int[4];
        LB = new int[4];
        RB = new int[4];
        BACK = new int[4];
        START = new int[4];
        DPADUP = new int[4];
        DPADDOWN = new int[4];
        DPADLEFT = new int[4];
        DPADRIGHT = new int[4];

        VL=new float[4];
        VR = new float[4];

        Connected = new bool[4];
    }

    void Update()
    {
        DeadZone = TheDeadZone;
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                Connected[i]=true;
                playerIndex = testPlayerIndex;

                state = GamePad.GetState(playerIndex,GamePadDeadZone.None);

                LX[i]=state.ThumbSticks.Left.X;
                LY[i]=state.ThumbSticks.Left.Y;
                LT[i]=state.Triggers.Left;
                RX[i]=state.ThumbSticks.Right.X;
                RY[i]=state.ThumbSticks.Right.Y;
                RT[i]=state.Triggers.Right;
                if (state.Buttons.A == ButtonState.Pressed){A[i]=1;}else{A[i]=0;}
                if (state.Buttons.B == ButtonState.Pressed){B[i]=1;}else{B[i]=0;}
                if (state.Buttons.X == ButtonState.Pressed){X[i]=1;}else{X[i]=0;}
                if (state.Buttons.Y == ButtonState.Pressed){Y[i]=1;}else{Y[i]=0;}
                if (state.Buttons.LeftStick == ButtonState.Pressed){LS[i]=1;}else{LS[i]=0;}
                if (state.Buttons.RightStick == ButtonState.Pressed){RS[i]=1;}else{RS[i]=0;}
                if (state.Buttons.LeftShoulder == ButtonState.Pressed){LB[i]=1;}else{LB[i]=0;}
                if (state.Buttons.RightShoulder == ButtonState.Pressed){RB[i]=1;}else{RB[i]=0;}
                if (state.Buttons.Back == ButtonState.Pressed){BACK[i]=1;}else{BACK[i]=0;}
                if (state.Buttons.Start == ButtonState.Pressed){START[i]=1;}else{START[i]=0;}
                if (state.DPad.Up == ButtonState.Pressed){DPADUP[i]=1;}else{DPADUP[i]=0;}
                if (state.DPad.Down == ButtonState.Pressed){DPADDOWN[i]=1;}else{DPADDOWN[i]=0;}
                if (state.DPad.Left == ButtonState.Pressed){DPADLEFT[i]=1;}else{DPADLEFT[i]=0;}
                if (state.DPad.Right == ButtonState.Pressed){DPADRIGHT[i]=1;}else{DPADRIGHT[i]=0;}

                //vibration
                if (!GameSettings.VibrationEnable)
                {
                    VL[i]=0f;
                    VR[i]=0f;
                }
                GamePad.SetVibration(playerIndex, VL[i], VR[i]);
                VL[i]=0f;
                VR[i]=0f;

                //deadzone
                if (Mathf.Abs(LX[i])<DeadZone&&Mathf.Abs(LY[i])<DeadZone)
                {
                    LX[i]=0;
                    LY[i]=0;
                }

                if (Mathf.Abs(RX[i])<DeadZone*2&&Mathf.Abs(RY[i])<DeadZone*2)
                {
                    RX[i]=0;
                    RY[i]=0;
                }
            }
            else
            {
                Connected[i]=false;
            }
        }
    }
}
