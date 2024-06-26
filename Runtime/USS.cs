using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIBuddy {
    internal class USS {
        internal static void ParseAndApplyUSS(VisualElement el, XmlNode attr) {
            string ussString = attr.Value;
            if (ussString == null) {
                return;
            }

            Dictionary<string, string> styles = ParseUSS(ussString);

            foreach (KeyValuePair<string, string> kvp in styles) {
                Debug.Log($"{kvp.Key}: {kvp.Value}");
            }
        }


        public static Dictionary<string, string> ParseUSS(string ussString) {
            Dictionary<string, string> styles = new();
            // "font-size: 200px; color: #ffffff; background-size: 40px;"

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

                // Remove any excess whitespace
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                // Add to the dictionary
                styles[key] = value;
            }

            return styles;
        }
    }
}