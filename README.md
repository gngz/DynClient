# DynClient
Yet Another Dynamic DNS Client

## How To Run?
In order to run DynClient it is necessary to specify the flag -c or --config with the path to the configuration file.

```bash
DynClient -c path/of/config/file.json 
```

The configuration file has the json syntax and the following format:

```json
{
    "plugin"  : "Plugin Name",
    ... other attributes required by the plugin
}
```
Example with the CloudFlare plugin:
```json
{
    "plugin"  : "CloudFlare",
    "api-key" :"apikey1010101010101010",
    "zone" :"example.com",
    "domain" :"dns.example.com"
}
```
Plugins must be placed in the Providers (If it does not exist it must be created) folder in the same folder as the executable.