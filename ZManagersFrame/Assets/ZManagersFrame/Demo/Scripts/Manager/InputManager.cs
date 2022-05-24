using UnityEngine;
using UnityEngine.Events;

namespace Lenovo.Starview.Manager
{

    public enum StateType
    {
        Down,//按下
        Up,//松开
        SingleClick,//单击
        DoubleClick,//双击
        Press,//长按
    }

    public enum KeyCodeType
    {
        OK,
        Back,
    }

    public class CustomKeyCodeData
    {
        public CustomKeyCodeData(KeyCodeType keyCodeType)
        {
            mKeyCodeType = keyCodeType;
        }

        public KeyCodeType mKeyCodeType;
        public StateType mStateType;

        public float pressTime;//按住的时间
        public bool isPress;//是否按下
        public float pressTimeLimit = 2;//按住多长时间定义为长按
        public float clickTimeLimit = 1;//按住多长时间定义为单击
        public float clickTime;//点击时间记录
        public float doubleClickTimeLimit = 1.5f;//按住多长时间定义为双击
    }

    //消息数据格式
    public class GlassesInputMsgData
    {
        public CustomKeyCodeData mCustomKeyCodeData;
    }


    public class InputManager : MonManager, ISingleton
    {
        public static InputManager Instance { get { return MonoSingletonProperty<InputManager>.Instance; } }
        public override ManagerType managerType { get { return ManagerType.InputModel; } }

        private GlassesInputMsgData glassesInputMsgData;

        public CustomKeyCodeData mOKKeyCodeData;
        public CustomKeyCodeData mBackKeyCodeData;


        public UnityAction<StateType> inputKeyEventHandle;
        public void OnSingletonInit()
        {

            mOKKeyCodeData = new CustomKeyCodeData(KeyCodeType.OK);
            mBackKeyCodeData = new CustomKeyCodeData(KeyCodeType.Back);
        }

        public override void SendMsg(object msg)
        {
            ManagerMsg managerMsg = new ManagerMsg();
            managerMsg.managerType = managerType;
            managerMsg.msgData = msg;
            Debug.Log($"stARview : SendMsg---msg---{((GlassesInputMsgData)msg).mCustomKeyCodeData.mKeyCodeType.ToString()}---{((GlassesInputMsgData)msg).mCustomKeyCodeData.mStateType.ToString()}");
            SendEvent<InputManager>(managerMsg);
        }

        public void SendKeyCodeData(CustomKeyCodeData customKeyCodeData)
        {
            if (glassesInputMsgData == null)
            {
                glassesInputMsgData = new GlassesInputMsgData();
            }
            glassesInputMsgData.mCustomKeyCodeData = customKeyCodeData;
            SendMsg(glassesInputMsgData);
            inputKeyEventHandle?.Invoke(customKeyCodeData.mStateType);

        }

        protected override void ProcessMsg(ManagerMsg msg)
        {

        }

        protected override void OnBeforeDestroy()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mBackKeyCodeData.mStateType = StateType.Press;
                mBackKeyCodeData.mKeyCodeType = KeyCodeType.OK;
                SendKeyCodeData(mBackKeyCodeData);
            }

            UpdateCheckOkKeyInput();
            UpdateCheckBackKeyInput();
        }

        /// <summary>
        /// OK键
        /// </summary>
        private void UpdateCheckOkKeyInput()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetKeyDown(KeyCode.M))
#elif UNITY_ANDROID
         if (StARkitKeyInput.OkDown())
#endif
            {
                mOKKeyCodeData.isPress = true;
                mOKKeyCodeData.mStateType = StateType.Down;
                SendKeyCodeData(mOKKeyCodeData);
            }
            else
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.M))
#elif UNITY_ANDROID
        if (StARkitKeyInput.OkUp())
#endif
            {
                mOKKeyCodeData.mStateType = StateType.Up;
                SendKeyCodeData(mOKKeyCodeData);

                if (mOKKeyCodeData.isPress && mOKKeyCodeData.pressTime < mOKKeyCodeData.clickTimeLimit)
                {
                    mOKKeyCodeData.pressTime = 0;

                    if ((Time.realtimeSinceStartup - mOKKeyCodeData.clickTime) < mOKKeyCodeData.doubleClickTimeLimit)
                    {
                        //处理双击
                        mOKKeyCodeData.mStateType = StateType.DoubleClick;
                        SendKeyCodeData(mOKKeyCodeData);
                        mOKKeyCodeData.isPress = false;
                        return;
                    }

                    //处理单击
                    mOKKeyCodeData.mStateType = StateType.SingleClick;
                    SendKeyCodeData(mOKKeyCodeData);
                    mOKKeyCodeData.isPress = false;
                    mOKKeyCodeData.clickTime = Time.realtimeSinceStartup;
                }
            }

            if (mOKKeyCodeData.isPress)
            {
                mOKKeyCodeData.pressTime += Time.deltaTime;
                if (mOKKeyCodeData.pressTime >= mOKKeyCodeData.pressTimeLimit)
                {
                    mOKKeyCodeData.isPress = false;
                    mOKKeyCodeData.pressTime = 0;
                    //处理长按
                    mOKKeyCodeData.mStateType = StateType.Press;
                    SendKeyCodeData(mOKKeyCodeData);
                }
            }
        }

        /// <summary>
        /// Back键
        /// </summary>
        private void UpdateCheckBackKeyInput()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.N))
#elif UNITY_ANDROID
        if (StARkitKeyInput.BackDown())
#endif
            {
                mBackKeyCodeData.mStateType = StateType.Down;
                SendKeyCodeData(mBackKeyCodeData);
                mBackKeyCodeData.isPress = true;
            }
            else
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.N))
#elif UNITY_ANDROID
        if (StARkitKeyInput.BackUp())
#endif
            {
                mBackKeyCodeData.mStateType = StateType.Up;
                SendKeyCodeData(mBackKeyCodeData);
                if (mBackKeyCodeData.isPress && mBackKeyCodeData.pressTime < mBackKeyCodeData.clickTimeLimit)
                {
                    mBackKeyCodeData.pressTime = 0;

                    if ((Time.realtimeSinceStartup - mBackKeyCodeData.clickTime) < mBackKeyCodeData.doubleClickTimeLimit)
                    {
                        //处理双击
                        mBackKeyCodeData.mStateType = StateType.DoubleClick;
                        SendKeyCodeData(mBackKeyCodeData);
                        mBackKeyCodeData.isPress = false;
                        return;
                    }

                    //处理back键单击
                    mBackKeyCodeData.mStateType = StateType.SingleClick;
                    SendKeyCodeData(mBackKeyCodeData);
                    mBackKeyCodeData.isPress = false;
                    mBackKeyCodeData.clickTime = Time.realtimeSinceStartup;
                }
            }

            if (mBackKeyCodeData.isPress)
            {
                mBackKeyCodeData.pressTime += Time.deltaTime;
                if (mBackKeyCodeData.pressTime >= mBackKeyCodeData.pressTimeLimit)
                {
                    mBackKeyCodeData.isPress = false;
                    mBackKeyCodeData.pressTime = 0;
                    //处理back键长按
                    mBackKeyCodeData.mStateType = StateType.Press;
                    SendKeyCodeData(mBackKeyCodeData);
                }
            }
        }

    }

}




