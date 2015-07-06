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

#TODO: add additional use cases here

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
