# WLED control

<note type="tip">

This is an experimental feature, and may not work as intended. File an
issue on GitHub, or contact me on Discord if you're facing problems.

</note>

The WLED implementation in the plugin utilises the WLED JSON API to send requests
to the instance at the IP address specified in the plugins settings. Because of
this, you'll need to ensure that your WLED instance isn't currently getting a live
feed from any other sources, otherwise the JSON API calls will fail to make any 
changes to your WLED lights.

For the time being, the implementation only acts upon the following flags:

- Black
- Blue
- Green
- White
- Yellow

There are some sensible default JSON messages that the plugin will send to your
WLED instance based on the above flags, but you may customise them to your own
liking in the plugin's settings. If you need to restore the originals, you can
grab them below:

```json
    // Black Flag
    {"v": true, "bri": 255, "seg": [{"col": [[255,255,255], [0,0,0]], "pal": 2, "fx": 1, "sx": 220, "ix": 120}]}

    // Blue Flag
    {"v": true, "bri": 255, "seg": [{"col": [[0,0,255], [0,0,0]], "pal": 2, "fx": 1, "sx": 200, "ix": 120}]}
    
    // Green Flag
    {"v": true, "bri": 255, "seg": [{"col": [[0,255,0]], "pal": 2, "fx": 0}]}

    // White Flag
    {"v": true, "bri": 255, "seg": [{"col": [[128,255,255]], "pal": 2, "fx": 0}]}
    
    // Yellow Flag
    {"v": true, "bri": 255, "seg": [{"col": [[255,200,0]], "pal": 2, "fx": 0}]}
    
    // No Flag
    {"v": true, "bri": 255, "seg": [{"col": [[0,0,0]], "pal": 2, "fx": 0}]}
```

[Check the WLED documentation](https://kno.wled.ge/interfaces/json-api/#setting-new-values)
for the values you can set in these fields, some experimentation may be required!
