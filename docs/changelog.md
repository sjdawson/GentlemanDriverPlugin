# Changelog

---

### `v1.2.1` WLED rate limit

A small bugfix and stability release, that limits the rate at which packets are
sent to WLED to avoid overloading the receiver, and sets a default IP to avoid
some errors that would show when the plugin is first installed.

- FEAT: Rate limit the packets sent to WLED to 60 fps (configurable to follow)
- BUGFIX: Set a default value for initial plugin install to avoid error on boot
  
---

### `v1.2.0` WLED switch to WARLS

In this release, the WLED integration has been converted to use WLED's live mode by
sending UDP to your WLED instance, instead of pinging the HTTP API with a JSON payload.

This move allows for better real-time control of WLED, and is a precursor to (_hopefully_)
be able to have WLED respond as something more complex, like RPM or shift lights.

- Removed "JSON" payloads and no longer sends those to WLED
- Added "Colour" settings to allow tweaking of what colour WLED will display
- Fixed ACC not generating a full `Flag_Yellow` event to send to WLED

---

### `v1.1.1` WLED control tweaks

Some smaller QOL features and tweaks to the WLED Control aspects

- Added "TEST" buttons to the JSON fields, to test effects without having to launch a game
- Tweaked default JSON values
- Added checkered and orange flags
- On enabling, the plugin will attempt a connection to the WLED instance and give some feedback on status

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
