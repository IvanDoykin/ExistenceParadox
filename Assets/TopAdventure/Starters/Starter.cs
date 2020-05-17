using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{

    public List<ManagerBase> managers = new List<ManagerBase>(); // список всех менеджеров


    void Awake()
    {
        Toolbox.ClearScene();
        foreach (var managerBase in managers)
        {
            Toolbox.Add(managerBase); //менеджеры на старте проги(сцены) добавляются в тулбокс
        }
    }


}
