using System;
using System.Text.RegularExpressions;
using UnityEngine;

internal static class UrlParser {
    internal static string ExtractUrlOrResource(string cssValue) {
        Match match = Regex.Match(cssValue, @"url\(\s*""([^""]+)""\s*\)|resource\('([^']+)'\)");
        return match.Success ? match.Groups[1].Value != "" ? match.Groups[1].Value : match.Groups[2].Value : null;
    }

    internal static string ConvertToRelativePath(string url) {
        string prefix = "project://database/";
        if (url.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) {
            url = url.Substring(prefix.Length);
        }

        string assetsPrefix = "Assets/";
        if (url.StartsWith(assetsPrefix, StringComparison.OrdinalIgnoreCase)) {
            url = url.Substring(assetsPrefix.Length);
        }

        // Remove the file extension if present
        int extIndex = url.LastIndexOf('.');
        if (extIndex > 0) {
            url = url.Substring(0, extIndex);
        }

        return url;
    }

    internal static Texture2D PathToTexture2d(string relativePath) {
        Texture2D texture = Resources.Load<Texture2D>(relativePath);
        if (texture != null) {
            return texture;
        }

        return null;
    }

    internal static Texture2D LoadResourceTexture(string resourcePath) {
        return Resources.Load<Texture2D>(resourcePath);
    }

    internal static Texture2D UrlStringToTexture2d(string urlString) {
        string path = ExtractUrlOrResource(urlString);
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Invalid url value");
        }

        Debug.Log(path);


        if (urlString.StartsWith("resource('")) {
            Texture2D texture = LoadResourceTexture(path);
            if (texture != null) {
                return texture;
            }
        } else {
            string relativePath = ConvertToRelativePath(path);
            Texture2D texture = PathToTexture2d(relativePath);
            if (texture != null) {
                return texture;
            }
        }

        throw new ArgumentException("Invalid url or resource value");
    }
}