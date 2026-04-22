; Here is my comment ;V
V                     <

* Why are `Current` and `CurrentPosition` not kept in sync in `ExecutionContext`? I get that the current position is not the same as the current cell.

I think there's a general problem with `ExecutionContext`. If `Next()` is called, it sets `Current` but not `CurrentPosition`. It **feels** like `Current`'s `Location` should really be considered `CurrentPosition`. However, `Move()` just sets `CurrentPosition` to the next location. But `Move()` maybe should just move to the next `Cell`, not just the next position (+-1 based on direction).

Maybe get rid of `CurrentPosition`, and change `Move()` such that it moves to the next valid cell. `CurrentPosition` becomes `Current.Location`. Actually, make just get rid of `Move()` or make it `private`, and someone can only call `Next()`. Keep in mind that if `IsInStringMode`, we need to know how much "space" is between two cells, that's why there's `Current` and `Previous`.

This may be something we want to do because we can potentially have `ExecutionContext` immutable.

But as I was thinking, `ExecutionContext` wasn't meant to know too much about the instructions. The instructions know what to do, and they use the context to figure out what to do next.