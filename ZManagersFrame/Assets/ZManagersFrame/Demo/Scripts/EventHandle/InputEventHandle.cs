
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
        #region 子类可重写的虚方法

        public virtual PlatformType SetPlatformType() { return PlatformType.None; }

        /// <summary>
        /// 单击OK键
        /// </summary>
        public virtual void OnOkSingleClick() { }

        /// <summary>
        /// 双击OK键
        /// </summary>
        public virtual void OnOkDoubleClick() { }

        /// <summary>
        /// 长按OK键
        /// </summary>
        public virtual void OnOkPress() { }

        /// <summary>
        /// 单击Back键
        /// </summary>
        public virtual void OnBackSingleClick() { }

        /// <summary>
        /// 双击Back键
        /// </summary>
        public virtual void OnBackDoubleClick() { }

        /// <summary>
        /// 长按Back键
        /// </summary>
        public virtual void OnBackPress() { }

        #endregion


    }
}


