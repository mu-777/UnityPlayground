using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProcesser
{
    void Activate();
    void Deactivate();
    void Process();
}

public interface IProcesserGenerator<Param>
{
    IProcesser Generate(Param param);
}

public class ProcesserAParam
{
    public float A;
}

public class ProcesserA : IProcesser
{
    private ProcesserAParam _param;
    public ProcesserA(ProcesserAParam param)
    {
        _param = param;
    }

    public void Activate()
    {
        Debug.Log($"Activate processerA: {_param.A}");
    }

    public void Deactivate()
    {
        Debug.Log($"Deactivate processerA: {_param.A}");
    }

    public void Process()
    {
        Debug.Log($"Process processerA: {_param.A}");
    }
}

public class DefaultProcesserParam
{

}
public class DefaultProcesser : IProcesser
{
    public DefaultProcesser(DefaultProcesserParam param)
    {
    }

    public void Activate()
    {
        Debug.Log($"Activate DefaultProcesser");
    }

    public void Deactivate()
    {
        Debug.Log($"Deactivate DefaultProcesser");
    }

    public void Process()
    {
        Debug.Log($"Process DefaultProcesser");
    }
}


public class ProcesserFactory
{
    public static IProcesser Create<Param>(Param param)
    {
        if(typeof(Param) == typeof(ProcesserAParam))
        {
            return new ProcesserA(param as ProcesserAParam);
        }
        else if (typeof(Param) == typeof(DefaultProcesserParam))
        {
            return new DefaultProcesser(param as DefaultProcesserParam);
        }
        return new DefaultProcesser(new DefaultProcesserParam());
    }
}

public enum ProcesserType
{
    A, Default
}

public class Processer : MonoBehaviour
{
    [SerializeField]
    private ProcesserType _processerType;

    [SerializeField]
    private ProcesserAParam _processerAParam;
    [SerializeField]
    private DefaultProcesserParam _defaultProcesserParam;

    private IProcesser processser;
    void Awake()
    {
        var createMap = new Dictionary<ProcesserType, Func<IProcesser>>
        {
            {ProcesserType.A, () => {return ProcesserFactory.Create(_processerAParam); } },
            {ProcesserType.Default, () => {return ProcesserFactory.Create(_defaultProcesserParam); } }
        };
        processser = createMap[_processerType]();
    }

    void OnEnable()
    {
        processser.Activate();
    }

    void OnDisable()
    {
        processser.Deactivate();
    }

    void Update()
    {
        processser.Process();
    }
}
