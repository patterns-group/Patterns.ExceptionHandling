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

vNext:

```bash
dnu install Patterns.ExceptionHandling
```

Visual Studio:

```PowerShell
Install-Package Patterns.ExceptionHandling
```

#TODO: add MyGet links here

### Usage

```csharp
Try.Do(ForExample.SometimesVoidMethodsThrow); // ApplicationException > STDOUT
string result = Try.Get(ForExample.AndSometimesValuedMethodsThrow); // result = null
```

Errors are handled using the default strategy when an ad-hoc handler is not provided. This strategy can be set using a static property, `Try.HandleErrors.DefaultStrategy`. On `AppDomain` load, the default strategy is to print the full `Exception` details to `STDOUT` and treat the error as "handled", preventing it from propagating further.

```csharp
// default strategy: suppress ArgumentExceptions and bubble the rest
Try.HandleErrors.DefaultStrategy = state => state.Handled(state.Error is ArgumentException);
```

For convenience, a strategy that suppresses all errors is available: `Try.HandleErrors.SuppressErrors`. It is intended for use in rare situations when then error itself does not matter so long as it is not allowed to propagate.

The `Try` class's real power is in its ability to accept ad-hoc strategies. An error handling strategy is one that takes an `ExceptionState` instance, and returns an `ExceptionState` instance. `ExceptionState` provides a fluent interface, allowing an inline expression to serve as the strategy in most instances.

```csharp
// wraps and propagates
Try.Do(ForExample.SometimesVoidMethodsThrow,
  state => state.WithError(new ApplicationException("Semantic Wrapper X", state.Error)));

// wraps, logs, and traps
string value = Try.Get(ForExample.AndSometimesValuedMethodsThrow,
  state => state.WithError(new ApplicationException("Semantic Wrapper Y", state.Error))
    .WithAction(finalState => _log.Error(finalState.Error))
    .Handled(true));
```

It's usually a good idea to build a strategy and reuse it wherever relevant.

```csharp
public class Example
{
  private static readonly ILog _log = LogManager.GetLogger(typeof(Example));
  private static readonly ErrorStrategy _strategy = state => state.Handled(true)
    .WithAction(_ => _log.Error(state.Error));

  private const string DEFAULT = "bar";

  public void Foo()
  {
    Try.Do(ForExample.SometimesVoidMethodsThrow, _strategy);
    string value = Try.Get(ForExample.AndSometimesValuedMethodsThrow, _strategy) ?? DEFAULT;
  }
}
```

## Testing / Development

After cloning this repo:

```bash
$ cd Patterns.ExceptionHandling
$ dnu restore
$ dnu build
```

## Contributions

The community is welcome to submit Issues and Pull Requests; someone from the [patterns-group] will see to it soon. We follow the [git-flow] model, so branches should be prefixed with `feature`, `bugfix`, etc. as appropriate. Also, unless it is a `hotfix` to a particular released version, the target branch should be `dev`, not `master`.

Pull Request rules:

1. Must be a single commit
2. Comment must conform to the template:

```
Issue #{Issue Number}: {Issue Description}

{Optional Descriptive Text}
```

### Example

Let's say we have an Issue with ID `47`, its description is `Reticulate Splines`, and a commit is ready to be submitted as a PR. The following illustrates what that PR "looks" like:

`feature/issue-47` &rarr; `dev`

```
Issue #47: Reticulate Splines

Also tweaked happiness coefficient
```

[git-flow]: http://nvie.com/posts/a-successful-git-branching-model/
[patterns-group]: https://github.com/patterns-group
