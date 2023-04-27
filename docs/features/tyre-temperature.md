# Tyre temperature

The tyre temperature section of GentlemanDriverPlugin allows you to manage the
optimal tyre temperatures on a per game and car basis. For games that also supply
some form of tyre compound, the value is also set referncing that particular
compound, so you can manage different sets of tyre temperatures where required.

<note type="tip">

By default, the plugin sets an optimal tyre temperature to a baseline of 80°C.
You'll want to tweak that with the actions available, to ensure it matches your
current setup.

</note>

## Actions

### Increase or decrease optimal tyre temperature

So that you can manage your temperatures on the go, the plugin exposes two
actions. One for increasing and one for decreasing the optimal temerature of
your tyres. Note that when using these actions, it will update the default value
of the plugin, and the default value for the current game. Along with those
values, it will also add or update a setting for the current car.

If the game supports tyre compounds, it will add or update for that car and
compound, instead.

```
IncreaseOptimalTyreTemp
```
```
DecreaseOptimalTyreTemp
```

---

## Properties

### Optimal tyre temperature

Get the current setting for the optimal tyre temperature. This will update based
on game and car, or game, car and tyre compound.

```ncalc
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperature]
```
```js
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperature')
```

---

### Optimal tyre temperature represented as a hex colour

For each corner of the car, this will report the temperature of the wheels as
a hex colour, working through a gradient from a dark blue (very cold), through
green (optimal temperature), up to red (overheating). These can be used in a
colour binding in a dashboard to quickly map temperature to a colour.

<div class="gradient-example">
    <span style="background-color: #0000ff"></span>
    <span style="background-color: #003dff"></span>
    <span style="background-color: #0058ff"></span>
    <span style="background-color: #006dff"></span>
    <span style="background-color: #007eff"></span>
    <span style="background-color: #008eff"></span>
    <span style="background-color: #009dff"></span>
    <span style="background-color: #00abff"></span>
    <span style="background-color: #00b7fb"></span>
    <span style="background-color: #56c3f2"></span>
    <span style="background-color: #87ceeb"></span>
    <span style="background-color: #5dd6f4"></span>
    <span style="background-color: #00ddf8"></span>
    <span style="background-color: #00e5f5"></span>
    <span style="background-color: #00ebec"></span>
    <span style="background-color: #00f1db"></span>
    <span style="background-color: #00f6c4"></span>
    <span style="background-color: #00faa7"></span>
    <span style="background-color: #00fd84"></span>
    <span style="background-color: #00ff58"></span>
    <span style="background-color: #00ff00"></span>
    <span style="background-color: #57fc00"></span>
    <span style="background-color: #79f800"></span>
    <span style="background-color: #93f500"></span>
    <span style="background-color: #a9f100"></span>
    <span style="background-color: #bbed00"></span>
    <span style="background-color: #cce800"></span>
    <span style="background-color: #dbe400"></span>
    <span style="background-color: #e8e000"></span>
    <span style="background-color: #f4db00"></span>
    <span style="background-color: #ffd700"></span>
    <span style="background-color: #ffc700"></span>
    <span style="background-color: #ffb700"></span>
    <span style="background-color: #ffa600"></span>
    <span style="background-color: #ff9400"></span>
    <span style="background-color: #ff8100"></span>
    <span style="background-color: #ff6d00"></span>
    <span style="background-color: #ff5700"></span>
    <span style="background-color: #ff3b00"></span>
    <span style="background-color: #ff0000"></span>
</div>

```ncalc
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexFrontLeft]
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexFrontRight]
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexRearLeft]
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexRearRight]
```
```js
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexFrontLeft')
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexFrontRight')
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexRearLeft')
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperatureHexRearRight')
```

---

### Optimal tyre temperature represented as a percent

For each corner of the car, this will report the temperature of the wheels as a
percentage, from 0 to 1, where 0.5 is considered the optimum temperature.

```ncalc
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentFrontLeft]
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentFrontRight]
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentRearLeft]
[GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentRearRight]
```
```js
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentFrontLeft')
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentFrontRight')
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentRearLeft')
$prop('GentlemanDriverPlugin.Tyres.OptimalTyreTemperaturePercentRearRight')
```

---