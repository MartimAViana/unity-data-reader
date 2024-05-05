using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Factory
{
    public abstract object Create(Dictionary<string, object> args);
}
