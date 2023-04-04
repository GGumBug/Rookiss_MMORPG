using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region SingleTone
    private static Managers                 s_instance = null;
    private static Managers                 Instance { get { Init(); return s_instance; } }
    #endregion

    private InputManager            _input = new InputManager();
    public static InputManager      Input { get { return Instance._input; } }

    private ResourceManager         _resource = new ResourceManager();
    public static ResourceManager   Resource { get { return Instance._resource; }}

    private UIManager               _ui = new UIManager();
    public static UIManager         UI { get { return Instance._ui; } }

    private SceneManagerEx          _scene = new SceneManagerEx();
    public static SceneManagerEx    Scene { get { return Instance._scene; } }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
