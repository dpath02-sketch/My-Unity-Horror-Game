using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class ConfigManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private Configs configs;
    private List<IConfigs> configUsers;
    private ConfigFileHandler configHandler;
    public static ConfigManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("no. Config Managers in scene > 1");
        }
        instance = this;
    }

    public void NewConfigs()
    {
        this.configs = new Configs();
    }

    public void LoadConfigs()
    {
        //load from file
        this.configs = configHandler.Load();
        //No configs? defaults 4 u
        if (this.configs == null)
        {
            Debug.Log("No configs. Creating some...");
            NewConfigs();
        }
        //send configs to objects to load
        foreach (IConfigs configUser in  configUsers)
        {
            configUser.LoadConfigs(configs);
        }
        Debug.Log("Loaded config: turn rate = " + configs.turnRateConfig);
    }

    public void SaveConfigs()
    {
        //send configs to objects to change
        foreach (IConfigs configUser in  configUsers)
        {
            configUser.SaveConfigs(ref configs);
        }
        Debug.Log("Saved config: turn rate = " + configs.turnRateConfig);
        //save to file
        configHandler.Save(configs);
    }

    //When to load / save
    void Start()
    {
        //TODO before shipping swap "Application.persistantDataPath" w. "Path.Combine(Application.dataPath, "PersistantData", "Configs")" so configs are stored in game directory, not where-ever the hell unity decides
        this.configHandler = new ConfigFileHandler(Application.persistentDataPath, fileName);
        this.configUsers = FindAllConfigUsers();
        LoadConfigs();
    }

    void OnApplicationQuit()
    {
        this.configUsers = FindAllConfigUsers();
        SaveConfigs();
    }

    //Find all the objects who need configs
    private List<IConfigs> FindAllConfigUsers()
    {
        IEnumerable<IConfigs> configUsers = FindObjectsOfType<MonoBehaviour>().OfType<IConfigs>();
        return new List<IConfigs>(configUsers);
    }
}
