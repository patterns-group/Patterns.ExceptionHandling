using System;

namespace Patterns.ExceptionHandling
{
  public class ExceptionState
  {
    public ExceptionState() {}

    public ExceptionState(Exception error, bool isHandled)
    {
      Error = error;
      IsHandled = isHandled;
    }

    public ExceptionState WithError(Exception error)
    {
      Error = error;
      return this;
    }

    public ExceptionState WithFlag(bool isHandled)
    {
      IsHandled = isHandled;
      return this;
    }

    public Exception Error{ get; private set; }
    public bool IsHandled{ get; private set; }
  }
}
