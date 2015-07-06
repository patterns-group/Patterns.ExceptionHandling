using System;
using System.Text;

namespace Patterns.ExceptionHandling
{

  public static class Extensions
  {
    //TODO: i18n
    private const string FORMAT = "INNER EXCEPTION: {0}";

    public static string ToFullString(this Exception error)
    {
      StringBuilder builder = new StringBuilder(error.Message)
        .AppendLine()
        .AppendLine(error.StackTrace);

      if (error.InnerException != null) builder.AppendFormat(FORMAT, error.InnerException.ToFullString());

      return builder.ToString();
    }
  }
}
