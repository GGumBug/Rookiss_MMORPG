using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region SingleTone
    private static Managers                 s_instance = null;
    private static Managers                 Instance { get { Init(); return s_instance; } }
    #endregion

    #region Content
    private GameManager _game = new GameManager();

    public static GameManager Game { get { return s_instance._game; } }
    #endregion

    #region Core
    private InputManager            _input = new InputManager();
    private PoolManager             _pool = new PoolManager();
    private ResourceManager         _resource = new ResourceManager();
    private SceneManagerEx          _scene = new SceneManagerEx();
    private SoundManager            _sound = new SoundManager();
    private UIManager               _ui = new UIManager();
    private DataManager             _data = new DataManager();

    public static InputManager      Input { get { return Instance._input; } }
    public static PoolManager       Pool { get { return Instance._pool; } }
    public static ResourceManager   Resource { get { return Instance._resource; }}
    public static SceneManagerEx    Scene { get { return Instance._scene; } }
    public static SoundManager      Sound { get { return Instance._sound; } }
    public static UIManager         UI { get { return Instance._ui; } }
    public static DataManager       Data { get { return Instance._data; } }
    #endregion

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
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._pool.Init();
            s_instance._sound.Init();
            s_instance._data.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();
        Sound.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
