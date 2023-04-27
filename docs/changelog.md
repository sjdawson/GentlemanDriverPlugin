# Changelog

---

### `v1.1.0` Multiple new properties and Experimental WLED control

Adds multiple new properties in different sections, and adds experimental WLED control

- [Added property Laps.Display](/features/laps#display)
- [Added property Laps.StintTime](/features/laps#stint-time)
- [Added action LaunchModeToggle](/features/launch-mode#launch-mode-toggle)
- [Added property LaunchMode.Active](/features/launch-mode#active)
- [Added property Tyres.OptimalTyreTemperaturePercent{Wheel}](/features/tyre-temperature#optimal-tyre-temperature-represented-as-a-percent)
- [Added experimental WLED control](/features/wled-control)

---

### `v1.0.3` Session Duration & TimeLeftPercent

Adds a new Session subset, starting with Duration and TimeLeftPercent

- [Added property Session.Duration](/features/session#duration)
- [Added property Session.TimeLeftPercent](/features/session#time-left-as-percent)

---

### `v1.0.2` LastInLap/LastOutLap

Adds the following new properties

- [Added property Laps.LastInLap](/features/laps#last-in-lap)
- [Added property Laps.LastOutLap](/features/laps#last-out-lap)

---

### `v1.0.1` GameRunningDelayed

Adds the following new property

- [Added property GameRunning.DelayedNNs](/features/game-running-delayed#game-running-delayed-by-n-seconds)

---

### `v1.0.0` Primary release, additional properties and actions

This bumps the plugin out of pre-release, and launches fully with the following
additional properties and events.

- [Added property Laps.PredictedLapTime](/features/laps#predicted-lap-time)
- [Added property Tyres.ActualTyreCompound](/features/tyre-compound#actual-tyre-compound)
- [Added property Tyres.VisualTyreCompound](/features/tyre-compound#visual-tyre-compound)
- [Added action IncreaseOptimalTyreTemp](/features/tyre-temperature#increase-or-decrease-optimal-tyre-temperature)
- [Added action DecreaseOptimalTyreTemp](/features/tyre-temperature#increase-or-decrease-optimal-tyre-temperature)
- [Added property Tyres.OptimalTyreTemperature](/features/tyre-temperature#optimal-tyre-temperature)
- [Added property Tyres.OptimalTyreTemperatureHex{Wheel}](/features/tyre-temperature#optimal-tyre-temperature-represented-as-a-hex-colour)

---

### `v0.0.1` Initial pre-release

A quick pre-release of the plugin to setup framework and add the following
initial property.

- [Added property Laps.StintTotal](/features/laps#stint-total)
