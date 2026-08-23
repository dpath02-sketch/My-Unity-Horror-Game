using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigs
{
    void LoadConfigs(Configs configs);
    void SaveConfigs(ref Configs configs);
}
