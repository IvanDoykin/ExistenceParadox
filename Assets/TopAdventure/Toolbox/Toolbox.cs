using System;
using System.Collections.Generic;
using TopAdventure;


public class Toolbox : Singleton<Toolbox>
{  //делаем тулбокс синглтоном 


    /// <summary>
    /// //словарь-контейнер для всех менеджеров, ключ-тип менеджера 
    /// </summary>
    private Dictionary<Type, object> data = new Dictionary<Type, object>();

    /// <summary>
    /// метод для добавления менеджеров в тулбокс
    /// </summary>
    /// <param name="obj"></param>
    public static void Add(object obj)
    {
        var add = obj;
        var manager = obj as ManagerBase;// проверяем, действительно ли был предоставлен менеджер, а не что-то другое

        if (manager != null)
            add = Instantiate(manager); // делаем копию менеджера, чтобы не менять данные в оригинальном scriptableObject
        else return;



        Instance.data.Add(obj.GetType(), add);  // добавление менеджера в словарь и получение типа, по которому можно обращаться к тулбоксу

        if (add is IAwake) // добавление возможности вызова события Awake
        {
            (add as IAwake).OnAwake();
        }
    }


    public static T Get<T>() // возвращение одного из менеджеров по ключу - типу менеджера
    {
        object resolve; //instance manager
        Type managerType = typeof(T);
        Instance.data.TryGetValue(managerType, out resolve);

        return (T)resolve;

    }


    public static void ClearScene()
    {
        Instance.data.Clear();
    }
}
