# Launch Mode

Launch mode is intended to allow for a dashboard overlay that can be toggled
visible with the `LaunchMode.Active` property, which can be toggled on by the
appropriate action. The property will turn itself back off after you've changed
gear away from whatever you're launching in. This allows you to have a display
that may concentrate more heavily on RPM for example, in order to obtain the
best launch.

## Actions

### Launch mode toggle

An action to allow for the toggling of LaunchMode.

```
LaunchModeToggle
```

---

## Properties

### Active

Intended for use as a visibility binding for an overlay element of a dashboard 
that will show information more closely related to getting an optimal launch.

```ncalc
[GentlemanDriverPlugin.LaunchMode.Active]
```
```js
$prop('GentlemanDriverPlugin.LaunchMode.Active')
```