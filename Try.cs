using System;

namespace Patterns.ExceptionHandling
{
  public static class Try
  {
    public static Output Get<Output>(Func<Output> retriever,
      Func<ExceptionState,ExceptionState> handler = null, Func<Output> fallback = null,
      Action<Output> cleanup = null)
    {
      Output result = default(Output);

      try
      {
        result = retriever();
        return result;
      }
      catch (Exception error)
      {
        if (!HandleError(error, handler)) throw;
        return fallback != null ? Get(fallback, handler) : default(Output);
      }
      finally
      {
        if (cleanup != null) Do(() => cleanup(result), HandleErrors.SuppressErrors);
      }
    }

    public static void Do(Action action, Func<ExceptionState,ExceptionState> handler = null,
      Action cleanup = null)
    {
      try
      {
        action();
      }
      catch (Exception error)
      {
        if (!HandleError(error, handler)) throw;
      }
      finally
      {
        if (cleanup != null) Do(cleanup, HandleErrors.SuppressErrors);
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

      public static ExceptionState SuppressErrors(ExceptionState state)
      {
        return state.WithFlag(true);
      }
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
