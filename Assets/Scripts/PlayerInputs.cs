using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    UserInput userInput;

    [SerializeField]
    RobotMovement[] robotMovements;

    int inputSet = 0;

    private void Awake()
    {
        userInput = new UserInput();
    }

    private void OnEnable()
    {
        userInput.Enable();
        userInput.ChangeInput.SetInput_1.performed += SwitchInput;
        userInput.ChangeInput.SetInput_2.performed += SwitchInput;
        userInput.ChangeInput.SetInput_3.performed += SwitchInput;

        //Movement
        //left
        userInput.Player_1.Left.performed += MoveRobotLeftStart;
        userInput.Player_1.Left.canceled += MoveRobotLeftStop;

        userInput.Player_2.Left.performed += MoveRobotLeftStart;
        userInput.Player_2.Left.canceled += MoveRobotLeftStop;

        userInput.Player_3.Left.performed += MoveRobotLeftStart;
        userInput.Player_3.Left.canceled += MoveRobotLeftStop;

        //right
        userInput.Player_1.Right.performed += MoveRobotRightStart;
        userInput.Player_1.Right.canceled += MoveRobotRightStop;

        userInput.Player_2.Right.performed += MoveRobotRightStart;
        userInput.Player_2.Right.canceled += MoveRobotRightStop;

        userInput.Player_3.Right.performed += MoveRobotRightStart;
        userInput.Player_3.Right.canceled += MoveRobotRightStop;

        //Special
        userInput.Player_1.Special.performed += SpecialRobotStart;
        userInput.Player_1.Special.canceled += SpecialRobotStop;

        userInput.Player_2.Special.performed += SpecialRobotStart;
        userInput.Player_2.Special.canceled += SpecialRobotStop;

        userInput.Player_3.Special.performed += SpecialRobotStart;
        userInput.Player_3.Special.canceled += SpecialRobotStop;

        //Jump
        userInput.Player_1.Jump.performed += RobotJumpAction;
        userInput.Player_2.Jump.performed += RobotJumpAction;
        userInput.Player_3.Jump.performed += RobotJumpAction;
    }

    //switch controls
    public void SwitchInput(InputAction.CallbackContext context)
    {
        inputSet = Mathf.RoundToInt(context.action.ReadValue<float>());
        inputSet--;
        StopAllCoroutines();
        special = new bool[3];
        Debug.Log("Input Set: " + inputSet);
    }


    Coroutine[] MoveRobotLeftCorutine = new Coroutine[3];
    //Move Left
    public void MoveRobotLeftStart(InputAction.CallbackContext context)
    {

        int robotNum = Mathf.RoundToInt(context.action.ReadValue<float>());

        robotNum = CalcRobotNum(robotNum);

        MoveRobotLeftCorutine[robotNum] = StartCoroutine(MoveRobotLeft(robotNum));
    }

    public void MoveRobotLeftStop(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        int robotNum = Mathf.RoundToInt(context.action.ReadValue<float>());
        robotNum = CalcRobotNum(robotNum);
        try
        {
            StopCoroutine(MoveRobotLeftCorutine[robotNum]);
        }
        catch { }
    }

    public IEnumerator MoveRobotLeft(int inputNum)
    {
        while (true)
        {
            robotMovements[inputNum].MoveLeft();
            yield return null;
        }
    }

    //Move Right
    Coroutine[] MoveRobotRigCorutine = new Coroutine[3];

    public void MoveRobotRightStart(InputAction.CallbackContext context)
    {
        int robotNum = Mathf.RoundToInt(context.action.ReadValue<float>());
        robotNum = CalcRobotNum(robotNum);

        MoveRobotRigCorutine[robotNum] = StartCoroutine(MoveRobotRight(robotNum));
    }

    public void MoveRobotRightStop(InputAction.CallbackContext context)
    {
        int robotNum = Mathf.RoundToInt(context.action.ReadValue<float>());
        robotNum = CalcRobotNum(robotNum);
        try
        {
            StopCoroutine(MoveRobotRigCorutine[robotNum]);
        }
        catch { }
    }

    public IEnumerator MoveRobotRight(int inputNum)
    {
        while (true)
        {
            robotMovements[inputNum].MoveRight();
            yield return null;
        }
    }

    //Special
    bool[] special = new bool[3];
    public void SpecialRobotStart(InputAction.CallbackContext context)
    {
        int robotNum = Mathf.RoundToInt(context.action.ReadValue<float>());
        robotNum = CalcRobotNum(robotNum);
        robotMovements[robotNum].SpecialActionStart();
    }

    public void SpecialRobotStop(InputAction.CallbackContext context)
    {
        int robotNum = Mathf.RoundToInt(context.action.ReadValue<float>());
        robotNum = CalcRobotNum(robotNum);
        robotMovements[robotNum].SpecialActionStop();
    }

    //Jump
    public void RobotJumpAction(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        int robotNum = Mathf.RoundToInt(context.action.ReadValue<float>());
        robotNum = CalcRobotNum(robotNum);
        if (special[robotNum])
        {
            RobotHighJump(robotNum);
        }
        else
        {
            RobotJump(robotNum);
        }
    }

    public void RobotHighJump(int robotNum)
    {
        robotMovements[robotNum].SpecialActionStart();
    }

    public void RobotJump(int robotNum)
    {
        robotMovements[robotNum].Jump();
    }

    //utilities
    public int CalcRobotNum(int robotNum)
    {
        robotNum += inputSet;
        if (robotNum > 2)
        {
            robotNum -= 3;
        }
        return robotNum;
    }
}
