# Vulcan
[![NuGet Version](https://img.shields.io/nuget/v/teconas.Vulcan)](https://www.nuget.org/packages/teconas.Vulcan)
[![CI](https://github.com/teconas/Vulcan/actions/workflows/ci-and-pack.yml/badge.svg)](https://github.com/teconas/Vulcan/actions/workflows/ci-and-pack.yml)

An opinionated C# utility library. Collected from things I kept reimplementing.

Works with ASP.NET, Unity, and general-purpose C#. Targets `netstandard2.0` + `net10.0`.

## What's in it

### Fluency — left-to-right pipelines
The thing I miss most in C#. `.Pipe()` is like `Select` but for a single value; `.Call()` is a side-effect that returns `this`.
```csharp
value.Pipe(Transform).Pipe(Format).Call(Log)
```

### LINQ gaps
Things that are just... missing.
```csharp
items.SkipNull()            // removes nulls, infers non-nullable type
items.None()                // opposite of .Any()
items.ForEach(action)       // executes and returns the materialized collection
items.Distinct(x => x.Id)   // distinct by selector
items.SelectMany()          // flatten IEnumerable<IEnumerable<T>>
items.Join(", ")            // fluent string.Join
```

### Collection
```csharp
collection.AddRange(a, b, c)       // AddRange with params overloads on ICollection<T>
```

### String
```csharp
str.IsSet()                        // !string.IsNullOrWhiteSpace
str.IsNotSet()
str.EmptyToNull()                  // "" → null, "x" → "x"
str.EmptyOrWhitespaceToNull()
"=-".Times(20)                     // repeat a string/char n times
```

### Tools
```csharp
// Go-style defer — runs on scope exit, in reverse order
using static Vulcan.DeferTool;
using var _ = Defer(() => Cleanup());

// Readable range iteration
foreach (var i in Count.Up(1).To(10)) ...
Count.Down(10).To(1).Step(2)

// SemaphoreSlim as IDisposable
using var _ = await myLock.AcquireAsync();
```

### Data Structures
- `CircleBuffer<T>` → fixed-capacity circular buffer
- `Grouping<TKey, TElement>` → lightweight `IGrouping<K,V>` implementation

## Opinionated?
Yep. Functional patterns, immutability, left-to-right flow. If that's not your thing, wrong library.
