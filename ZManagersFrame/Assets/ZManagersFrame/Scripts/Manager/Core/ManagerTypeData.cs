/// <summary>
/// ͳһManager��Ϣ���͸�ʽ
/// </summary>
public class ManagerMsg
{
    public ManagerType managerType;
    public object msgData;
}

/// <summary>
/// ƽ̨���Ͷ���
/// </summary>
public enum PlatformType
{
    None,
    UnityEditor,
    Android,
    IOS,
    ARCamera,
    Hololens
}

/// <summary>
/// Manager���Ͷ���
/// </summary>
public enum ManagerType
{
    Test,//����
    GameLoop,//��������
    UI,//UI 
    Network,//����
    Audio,//��Ƶ����
    Video,//��Ƶ����
    InputModel,//����ģ��
    Devices,//�豸
}

