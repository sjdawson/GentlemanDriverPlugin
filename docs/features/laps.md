# Laps

## Properties

### Predicted lap time

This property will report on your predicted lap time, by applying your current
delta to the best lap time you have reported for the session.

```ncalc
[GentlemanDriverPlugin.Laps.PredictedLapTime]
```
```js
$prop('GentlemanDriverPlugin.Laps.PredictedLapTime')
```

---

### Stint total

The total number of laps in your current stint (laps since you last left the pit
lane)

```ncalc
[GentlemanDriverPlugin.Laps.StintTotal]
```
```js
$prop('GentlemanDriverPlugin.Laps.StintTotal')
```

---

### Stint time

The total time you've spend in your current stint.

```ncalc
[GentlemanDriverPlugin.Laps.StintTime]
```
```js
$prop('GentlemanDriverPlugin.Laps.StintTime')
```

---

### Last out lap

The lap you last left the pits

```ncalc
[GentlemanDriverPlugin.Laps.LastOutLap]
```
```js
$prop('GentlemanDriverPlugin.Laps.LastOutLap')
```


---

### Last in lap

The lap you last entered the pits

```ncalc
[GentlemanDriverPlugin.Laps.LastInLap]
```
```js
$prop('GentlemanDriverPlugin.Laps.LastInLap')
```


---

### Display

If the race has a total number of laps and a relevant property value for it, this
will display `3/10` as an example. Otherwise, it will only display the current
lap.

```ncalc
[GentlemanDriverPlugin.Laps.Display]
```
```js
$prop('GentlemanDriverPlugin.Laps.Display')
```
