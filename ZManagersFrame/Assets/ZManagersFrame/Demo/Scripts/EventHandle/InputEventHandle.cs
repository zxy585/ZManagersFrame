
namespace Lenovo.Starview.Manager
{
    public class InputEventHandle : BaseManagerEventHandle
    {
        public override IManager manager { get { return InputManager.Instance; } }

        public override PlatformType platformType { get { return SetPlatformType(); } }

        public override void RegisterEventHandle()
        {
            manager.GetEventSystem().RegisterEvent<InputManager>(ProcessManagerEventHandle);
        }

        public override void UnRegisterEventHandle()
        {
            manager.GetEventSystem().UnRegisterEvent<InputManager>(ProcessManagerEventHandle);
        }

        public override void ProcessManagerEventHandle(ManagerMsg managerMsg)
        {
            GlassesInputMsgData msgData = (GlassesInputMsgData)managerMsg.msgData;
            switch (msgData.mCustomKeyCodeData.mKeyCodeType)
            {
                case KeyCodeType.OK:
                    switch (msgData.mCustomKeyCodeData.mStateType)
                    {
                        case StateType.Down:
                            break;
                        case StateType.Up:
                            break;
                        case StateType.SingleClick:
                            OnOkSingleClick();
                            break;
                        case StateType.DoubleClick:
                            OnOkDoubleClick();
                            break;
                        case StateType.Press:
                            OnOkPress();
                            break;
                        default:
                            break;
                    }
                    break;
                case KeyCodeType.Back:
                    switch (msgData.mCustomKeyCodeData.mStateType)
                    {
                        case StateType.Down:
                            break;
                        case StateType.Up:
                            break;
                        case StateType.SingleClick:
                            OnBackSingleClick();
                            break;
                        case StateType.DoubleClick:
                            OnBackDoubleClick();
                            break;
                        case StateType.Press:
                            OnBackPress();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

        }


        //------------------------------------------------------------------------------------------------------------------------------------------------------
        #region �������д���鷽��

        public virtual PlatformType SetPlatformType() { return PlatformType.None; }

        /// <summary>
        /// ����OK��
        /// </summary>
        public virtual void OnOkSingleClick() { }

        /// <summary>
        /// ˫��OK��
        /// </summary>
        public virtual void OnOkDoubleClick() { }

        /// <summary>
        /// ����OK��
        /// </summary>
        public virtual void OnOkPress() { }

        /// <summary>
        /// ����Back��
        /// </summary>
        public virtual void OnBackSingleClick() { }

        /// <summary>
        /// ˫��Back��
        /// </summary>
        public virtual void OnBackDoubleClick() { }

        /// <summary>
        /// ����Back��
        /// </summary>
        public virtual void OnBackPress() { }

        #endregion


    }
}


