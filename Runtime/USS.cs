using System.Collections.Generic;
using System.Xml;
using UIBuddyTypes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIBuddyTypes {
    public struct StylePropertyValue {
        public string Value;
        public StyleUnit Unit;
    }
}

internal class USS {
    internal static void ParseAndApplyUSS(VisualElement el, XmlNode attr) {
        string ussString = attr.Value;
        if (ussString == null) {
            return;
        }

        Dictionary<StyleProperty, string> styles = ParseUSS(ussString);

        foreach (KeyValuePair<StyleProperty, string> kvp in styles) {
            Debug.Log($"{kvp.Key}: {kvp.Value}");
        }
    }


    public static Dictionary<StyleProperty, string> ParseUSS(string ussString) {
        Dictionary<StyleProperty, string> styles = new();

        // Split into properties
        string[] properties = ussString.Split(";");

        // Loop them
        foreach (string property in properties) {
            // Trim extra white space
            string trimmedProperty = property.Trim();

            // Make sure we actually have a property to work with
            if (string.IsNullOrEmpty(trimmedProperty)) {
                continue;
            }

            // Split by : giving us a key and value
            string[] keyValue = trimmedProperty.Split(new[] { ':' }, 2);

            //  it's not exactly two somethings invalid
            if (keyValue.Length != 2) {
                // TODO: log an error here
                continue;
            }

            // Check key
            string key = keyValue[0].Trim();

            if (!Maps.StyleProperties.TryGetValue(key, out StyleProperty styleProperty)) {
                Logging.StylePropertyInvalidWarning(key);
                continue;
            }

            // Check value
            string value = keyValue[1].Trim();

            if (value == "") {
                Logging.StylePropertyInvalidWarning(key);
                continue;
            }

            styles[styleProperty] = value;
        }

        return styles;
    }
}