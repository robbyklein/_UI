using System;
using System.Xml;
using UIBuddyTypes;
using UnityEngine.UIElements;


public static class UIBuddy {
    public static T Build<T>(string uiString) where T : VisualElement {
        // Create an xml document
        XmlDocument doc = new();

        // Wrap with UXML element so it has ui namespace
        doc.LoadXml("<ui:UXML xmlns:ui=\"UnityEngine.UIElements\">" + uiString + "</ui:UXML>");

        // Since we wrapped, the first child is root
        if (doc.DocumentElement != null) {
            XmlNode root = doc.DocumentElement.FirstChild;
            return CreateElement<T>(root);
        }

        throw new InvalidOperationException("The XML document does not have a valid root element.");
    }

    public static void Style(VisualElement el, StyleProperty property, string value) {
        switch (property) {
            case StyleProperty.AlignItems:
                USS.ApplyAlignItems(el, value);
                break;
            case StyleProperty.AlignContent:
                USS.ApplyAlignContent(el, value);
                break;
            case StyleProperty.AlignSelf:
                USS.ApplyAlignSelf(el, value);
                break;
            case StyleProperty.FlexDirection:
                USS.ApplyFlexDirection(el, value);
                break;
            case StyleProperty.Display:
                USS.ApplyDisplay(el, value);
                break;
            case StyleProperty.FlexWrap:
                USS.ApplyFlexWrap(el, value);
                break;
            case StyleProperty.JustifyContent:
                USS.ApplyJustifyContent(el, value);
                break;
        }
    }

    private static T CreateElement<T>(XmlNode node) where T : VisualElement {
        // Step 1: Create base element
        T element = Elements.Create<T>(node.LocalName);

        // Step 2: Add attributes (ex. style)
        if (element != null && node.Attributes != null) {
            foreach (XmlAttribute attribute in node.Attributes) {
                Attributes.Add(element, attribute, node);
            }
        }

        // Step 3: Build nested elements recursively
        if (element != null && node.HasChildNodes) {
            foreach (XmlNode childNode in node.ChildNodes) {
                VisualElement childElement = CreateElement<VisualElement>(childNode);

                if (childElement != null) {
                    element.Add(childElement);
                }
            }
        }

        // Step 4: Return the finished root element
        return element ??
               throw new InvalidOperationException($"The created element is not of type {typeof(T).Name}");
    }
}