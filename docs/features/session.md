# Session

## Properties

### Duration

How long is the duration of the current session. Where a game doesn't report this
information directly, it will be taken from the value when you enter the session.

For example, if you enter a 10 min practice session, and 2 mins have already
elapsed, then this property will report 8 min session duration.

```ncalc
[GentlemanDriverPlugin.Session.Duration]
```
```js
$prop('GentlemanDriverPlugin.Session.Duration')
```

### Time left as percent

A percentage value that represents the remaining time in a session as a percentage.
Uses the Duration property above, so this percentage **may** be related to the
time that you entered the session and not it's entire duration.

```ncalc
[GentlemanDriverPlugin.Session.TimeLeftPercent]
```
```js
$prop('GentlemanDriverPlugin.Session.TimeLeftPercent')
```