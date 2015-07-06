using System;

namespace Patterns.ExceptionHandling
{
  public static class Try
  {
    public static Output Get<Output>(Func<Output> retriever,
      Func<ExceptionState,ExceptionState> handler = null, Func<Output> fallback = null)
    {
      try
      {
        return retriever();
      }
      catch (Exception error)
      {
        if (!HandleError(error, handler)) throw;
        return fallback != null ? Get(fallback, handler) : default(Output);
      }
    }

    public static class HandleErrors
    {
      static HandleErrors()
      {
        DefaultStrategy = state =>
        {
          Console.WriteLine(state.Error.ToFullString());
          return state.WithFlag(true);
        };
      }

      public static Func<ExceptionState,ExceptionState> DefaultStrategy{ get; set; }
    }

    private static bool HandleError(Exception error, Func<ExceptionState,ExceptionState> handler)
    {
      var strategy = handler ?? HandleErrors.DefaultStrategy;
      if (strategy == null) return false;
      var state = strategy(new ExceptionState().WithError(error));
      if (!state.IsHandled && !ReferenceEquals(error, state.Error) && state.Error != null) throw state.Error;
      return state.IsHandled;
    }
  }
}
