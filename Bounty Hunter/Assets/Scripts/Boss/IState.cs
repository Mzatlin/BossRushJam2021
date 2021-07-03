using System;

public interface IState 
{
    Type Tick();
    void BeginState();
    void EndState();
}
