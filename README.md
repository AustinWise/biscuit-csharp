# Biscuit Tokens for C\#

An experimental C# library for using [Biscuit authroization tokens](https://www.biscuitsec.org/).
This library wraps the
[C API version](https://github.com/eclipse-biscuit/biscuit-rust/blob/main/biscuit-capi/README.md)
of the
[Rust Biscuit library](https://github.com/eclipse-biscuit/biscuit-rust/).

Currently only lightly tested.

## Why?

According to [this page](https://github.com/eclipse-biscuit/biscuit), the
[existing .NET library](https://github.com/dmunch/biscuit-net) for Biscuits only supports V1 of the
specification. For me it is write a new C# library that wraps unmanaged code than it would be to
understand the Biscuit specification changes in enough detail to update a pure C# implementation.
Also I think Biscuit is sufficiently complicated that it might be hard to keep multiple independent
implementations in sync. Also the
[Python](https://github.com/eclipse-biscuit/biscuit-python/) and
[WASM/JavaScript](https://github.com/eclipse-biscuit/biscuit-wasm)
versions of Biscuit already wrap the Rust library.

## TODO

* Consider making the library tolerant of thread aborts. Currently this library only tries to not
  leak Rust-allocated memory in the event of a .NET out of memory error.
* Consider switching to use SafeHandles to manage Rust-allocated memory. This probably would help
  with the above and simplify the code somewhat. The downside is it would likely make the library no
  longer thread-safe.
* Consider distributing a build of the native libraries. This is something I'd rather not take
  responsibility for, but would make this library easier to use.
