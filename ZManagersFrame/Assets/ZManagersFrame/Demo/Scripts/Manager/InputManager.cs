using UnityEngine;
using UnityEngine.Events;

namespace Lenovo.Starview.Manager
{

    public enum StateType
    {
        Down,//����
        Up,//�ɿ�
        SingleClick,//����
        DoubleClick,//˫��
        Press,//����
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

        public float pressTime;//��ס��ʱ��
        public bool isPress;//�Ƿ���
        public float pressTimeLimit = 2;//��ס�೤ʱ�䶨��Ϊ����
        public float clickTimeLimit = 1;//��ס�೤ʱ�䶨��Ϊ����
        public float clickTime;//���ʱ���¼
        public float doubleClickTimeLimit = 1.5f;//��ס�೤ʱ�䶨��Ϊ˫��
    }

    //��Ϣ���ݸ�ʽ
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
        /// OK��
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
                        //����˫��
                        mOKKeyCodeData.mStateType = StateType.DoubleClick;
                        SendKeyCodeData(mOKKeyCodeData);
                        mOKKeyCodeData.isPress = false;
                        return;
                    }

                    //������
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
                    //������
                    mOKKeyCodeData.mStateType = StateType.Press;
                    SendKeyCodeData(mOKKeyCodeData);
                }
            }
        }

        /// <summary>
        /// Back��
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
                        //����˫��
                        mBackKeyCodeData.mStateType = StateType.DoubleClick;
                        SendKeyCodeData(mBackKeyCodeData);
                        mBackKeyCodeData.isPress = false;
                        return;
                    }

                    //����back������
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
                    //����back������
                    mBackKeyCodeData.mStateType = StateType.Press;
                    SendKeyCodeData(mBackKeyCodeData);
                }
            }
        }

    }

}




