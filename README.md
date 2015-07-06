# Patterns.ExceptionHandling

## Context

```csharp
public static class ForExample
{
  public static void SometimesVoidMethodsThrow()
  {
    AppException();
  }

  public static string AndSometimesValuedMethodsThrow()
  {
    AppException();
    return "never gonna get it";
  }

  private static void AppException()
  {
    throw new ApplicationException("an assumption proved false");
  }
}
```

## Solution

Patterns.ExceptionHandling contains a tool, `Try`, that helps to streamline code dealing with "suspect" components.

### Installation

```bash
dnu install Patterns.ExceptionHandling
```

```PowerShell
Install-Package Patterns.ExceptionHandling
```

### Usage

```csharp
Try.Do(ForExample.SometimesVoidMethodsThrow); // ApplicationException >> Console
string result = Try.Get(ForExample.AndSometimesValuedMethodsThrow); // result = null
```

