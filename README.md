# FeatureFlags
Experiment with Azure .NET Core Feature Management library.

See https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core

## Testing

Endpoints:
- disabled in code manually by checking `FeatureManager.IsEnabledAsync()` and throwing NotImplemented: https://localhost:5001/WeatherForecast/local
- disabled with a `FeatureGate` attribute (returns NotFound): https://localhost:5001/WeatherForecast/cloudcover
- 
