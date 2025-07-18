﻿using ColdFusionTool1.Models;
using System.Text.Json;

namespace ColdFusionTool1.Classes;
public class Configurations
{
    /// <summary>
    /// Initializes and returns a new instance of the <see cref="ApplicationConfiguration"/> class 
    /// with predefined application settings.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ApplicationConfiguration"/> containing the default configuration values.
    /// </returns>
    public static ApplicationConfiguration SetAppSettings() =>
        new()
        {
            RootFolder = "C:\\OED\\WebApps\\cf11-EDwebMain",
            LogFileName = "Log.txt",
            FilePatterns = new FilePattern
            {
                Include = ["**/*.cfm", "**/*.cfc"],
                Exclude = [
                    "**/_Retired/**",
                    "**/XX-DO-NOT-USE**"
                ]
            }
        };

    /// <summary>
    /// Saves the application configuration settings to a specified file in JSON format.
    /// </summary>
    /// <param name="filePath">
    /// The file path where the configuration settings will be saved. 
    /// Defaults to "configuration.json" if no value is provided.
    /// </param>
    /// <exception cref="IOException">
    /// Thrown when an I/O error occurs while writing to the file.
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the application does not have the required permissions to write to the file.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when an unexpected error occurs during the save operation.
    /// </exception>
    public static void SaveSettingsToFile(string filePath = "configuration.json")
    {
        var settings = SetAppSettings();
        
        try
        {
            string json = JsonSerializer.Serialize(settings, Indented);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing settings to file: {ex.Message}");
        }
    }

    public static JsonSerializerOptions Indented => new() { WriteIndented = true };
    /// <summary>
    /// Loads the application configuration settings from a specified JSON file.
    /// </summary>
    /// <param name="filePath">
    /// The file path from which the configuration settings will be loaded. 
    /// Defaults to "configuration.json" if no value is provided.
    /// </param>
    /// <returns>
    /// An instance of <see cref="ApplicationConfiguration"/> containing the loaded configuration values.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the specified configuration file does not exist.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown when the JSON content in the file is invalid or cannot be deserialized into an <see cref="ApplicationConfiguration"/> object.
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the application does not have the required permissions to read the file.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when an unexpected error occurs during the load operation.
    /// </exception>
    public static ApplicationConfiguration LoadSettingsFromFile(string filePath = "configuration.json")
    {
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Configuration file not found: {filePath}");
            return null!;
        }

        string json = File.ReadAllText(filePath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var config = JsonSerializer.Deserialize<ApplicationConfiguration>(json, options);
        return config!;
    }
}
