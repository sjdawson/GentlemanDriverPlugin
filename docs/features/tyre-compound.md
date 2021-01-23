# Tyre compound

<note type="tip">

Different games will have different mappings of tyre compounds, and may not
be supported initially. [Get in touch if you have a game you'd like supported][1].

</note>

## Properties

### Actual tyre compound

Get the name of the actual tyre compound in use, examples are `C1`, `C5`,
`Intermediate`, `Wet`. Returns `N/A` when the game doesn't have tyre compounds
defined.

```ncalc
[GentlemanDriverPlugin.Tyres.ActualTyreCompound]
```
```js
$prop('GentlemanDriverPlugin.Tyres.ActualTyreCompound')
```

---

### Visual tyre compound

Get the name of the visual tyre compound in use, examples are `Soft`, `Hard`,
`Intermediate`, `Wet`. Returns `N/A` when the game doesn't have tyre compounds
defined.

```ncalc
[GentlemanDriverPlugin.Tyres.VisualTyreCompound]
```
```js
$prop('GentlemanDriverPlugin.Tyres.VisualTyreCompound')
```

[1]: https://github.com/sjdawson/GentlemanDriverPlugin/discussions
