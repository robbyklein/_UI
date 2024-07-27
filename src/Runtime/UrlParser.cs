using System;
using System.Text.RegularExpressions;
using UnityEngine;

internal static class UrlParser {
    private static string ExtractUrlOrResource(string cssValue) {
        Match match = Regex.Match(cssValue, @"url\(\s*""([^""]+)""\s*\)|resource\('([^']+)'\)");
        return match.Success ? match.Groups[1].Value != "" ? match.Groups[1].Value : match.Groups[2].Value : null;
    }

    private static string ConvertToRelativePath(string url) {
        // Remove 'project://database/' from the beginning
        string prefix = "project://database/";
        if (url.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) { url = url.Substring(prefix.Length); }

        // Remove 'Assets/' prefix if present
        string assetsPrefix = "Assets/";
        if (url.StartsWith(assetsPrefix, StringComparison.OrdinalIgnoreCase)) {
            url = url.Substring(assetsPrefix.Length);
        }

        // Remove the file extension if present
        int extIndex = url.LastIndexOf('.');
        if (extIndex > 0) { url = url.Substring(0, extIndex); }

        return url;
    }

    private static Texture2D PathToTexture2d(string relativePath) {
        string[] supportedExtensions = { ".png", ".jpg", ".jpeg", ".tga", ".bmp", ".gif", ".psd", ".tiff" };

        foreach (string extension in supportedExtensions) {
            Texture2D texture = Resources.Load<Texture2D>(relativePath + extension);
            if (texture != null) { return texture; }
        }

        return null;
    }

    private static Texture2D LoadResourceTexture(string resourcePath) {
        return Resources.Load<Texture2D>(resourcePath);
    }

    internal static Texture2D UrlStringToTexture2d(string urlString) {
        // Extract the URL or resource path
        string path = ExtractUrlOrResource(urlString);
        if (string.IsNullOrEmpty(path)) throw new ArgumentException("Invalid url value");

        // Check if it's a resource path
        if (urlString.StartsWith("resource('")) {
            Texture2D texture = LoadResourceTexture(path);
            if (texture != null) return texture;
        }
        else {
            // Otherwise, treat it as a URL
            string relativePath = ConvertToRelativePath(path);

            // Try to get the texture
            Texture2D texture = PathToTexture2d(relativePath);
            if (texture != null) return texture;
        }

        // If we get here it didn't work
        throw new ArgumentException("Invalid url or resource value");
    }
}