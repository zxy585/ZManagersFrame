using Lenovo.Starview.Manager;
using UnityEngine;

public class InputEventHandleAndroid: InputEventHandle
{
    public override PlatformType SetPlatformType() { return PlatformType.Android; }

    public override void OnOkSingleClick()
    {
        Debug.Log($"{manager} {SetPlatformType()} OnOkSingleClick" );
    }

    public override void OnOkPress()
    {
        Debug.Log($"{manager} {SetPlatformType()} OnOkPress");
    }


    public override void OnBackSingleClick()
    {
        Debug.Log($"{manager} {SetPlatformType()} OnBackSingleClick");
    }

    public override void OnBackDoubleClick()
    {
        Debug.Log($"{manager} {SetPlatformType()} OnBackDoubleClick");
    }

    public override void OnBackPress()
    {
        Debug.Log($"{manager} {SetPlatformType()} OnBackPress");
    }


}
