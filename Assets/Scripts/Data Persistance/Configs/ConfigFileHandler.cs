using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ConfigFileHandler
{
    private string configDirPath = "";
    private string configFileName = "";

    public ConfigFileHandler(string configDirPath, string configFileName)
    {
        this.configDirPath = configDirPath;
        this.configFileName = configFileName;
    }

    public Configs Load()
    {
        string fullPath = Path.Combine(configDirPath, configFileName);
        Configs loadedConfigs = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string configsToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        configsToLoad = reader.ReadToEnd();
                    }
                }
                //Data is (hopefully) loaded, now to convert from JSON to Configs
                loadedConfigs = JsonUtility.FromJson<Configs>(configsToLoad);
                Debug.Log("Read configs from " + fullPath);
            }
            catch (Exception e)
            {
                Debug.Log("Error reading configs from file " + fullPath + ": " + e);
            }
        }
        return loadedConfigs;
    }

    public void Save(Configs configs)
    {
        string fullPath = Path.Combine(configDirPath, configFileName);
        try
        {
            //Make directory incase it don't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //Turn Configs (C#) into JSON (JSON)
            string configsToStore = JsonUtility.ToJson(configs, true);
            //Write JSON to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(configsToStore);
                }
            }
            Debug.Log("Wrote configs to " + fullPath);
        }
        catch (Exception e)
        {
            Debug.Log("Error writing configs to file " + fullPath + ": " + e);
        }
    }
}
