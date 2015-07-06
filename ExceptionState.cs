using System;

namespace Patterns.ExceptionHandling
{
  public class ExceptionState
  {
    private Action<ExceptionState> _action;

    public ExceptionState() {}

    public ExceptionState(Exception error, bool isHandled)
    {
      Error = error;
      IsHandled = isHandled;
    }

    public ExceptionState Evaluate()
    {
      if (_action != null) _action(this);
      return this;
    }

    public ExceptionState WithAction(Action<ExceptionState> action)
    {
      _action = action;
      return this;
    }

    public ExceptionState WithError(Exception error)
    {
      Error = error;
      return this;
    }

    public ExceptionState Handled(bool isHandled)
    {
      IsHandled = isHandled;
      return this;
    }

    public Exception Error{ get; private set; }
    public bool IsHandled{ get; private set; }
  }
}
