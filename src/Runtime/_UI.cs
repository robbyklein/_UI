using System;
using System.Collections.Generic;
using System.Xml;
using _UITypes;
using UnityEngine.UIElements;

public class _UI {
    /*
     *  Public API
     */
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

    public static void Style(VisualElement element, string ussString) {
        // Parse the USS
        Dictionary<StyleProperty, string> styles = USS.ParseUSS(ussString);

        // Apply the styles
        foreach (KeyValuePair<StyleProperty, string> kvp in styles) {
            ApplyStyle(element, kvp.Key, kvp.Value);
        }
    }

    public static void Style(VisualElement element, StyleProperty property, string value) {
        ApplyStyle(element, property, value);
    }

    public static void Style(VisualElement element, string property, string value) {
        if (Maps.StyleProperties.TryGetValue(property, out StyleProperty styleProperty)) {
            ApplyStyle(element, styleProperty, value);
        } else {
            Logging.StylePropertyInvalidWarning(property);
        }
    }


    /*
     *  Internals
     */

    internal static void ApplyStyle(VisualElement el, StyleProperty property, string value) {
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
            case StyleProperty.BackgroundColor:
                USS.ApplyBackgroundColor(el, value);
                break;
            case StyleProperty.BorderColor:
                USS.ApplyBorderColor(el, value, USSBorderSide.All);
                break;
            case StyleProperty.BorderTopColor:
                USS.ApplyBorderColor(el, value, USSBorderSide.Top);
                break;
            case StyleProperty.BorderBottomColor:
                USS.ApplyBorderColor(el, value, USSBorderSide.Bottom);
                break;
            case StyleProperty.BorderLeftColor:
                USS.ApplyBorderColor(el, value, USSBorderSide.Left);
                break;
            case StyleProperty.BorderRightColor:
                USS.ApplyBorderColor(el, value, USSBorderSide.Right);
                break;
            case StyleProperty.Color:
                USS.ApplyColor(el, value);
                break;
            case StyleProperty.UnityBackgroundImageTintColor:
                USS.ApplyUnityBackgroundImageTintColor(el, value);
                break;
            case StyleProperty.UnityTextOutlineColor:
                USS.ApplyUnityTextOutlineColor(el, value);
                break;
            case StyleProperty.PaddingTop:
                USS.ApplyPadding(el, value, USSDirection.Top);
                break;
            case StyleProperty.PaddingRight:
                USS.ApplyPadding(el, value, USSDirection.Right);
                break;
            case StyleProperty.PaddingBottom:
                USS.ApplyPadding(el, value, USSDirection.Bottom);
                break;
            case StyleProperty.PaddingLeft:
                USS.ApplyPadding(el, value, USSDirection.Left);
                break;
            case StyleProperty.Padding:
                USS.ApplyPadding(el, value, USSDirection.All);
                break;
            case StyleProperty.MarginTop:
                USS.ApplyMargin(el, value, USSDirection.Top);
                break;
            case StyleProperty.MarginRight:
                USS.ApplyMargin(el, value, USSDirection.Right);
                break;
            case StyleProperty.MarginBottom:
                USS.ApplyMargin(el, value, USSDirection.Bottom);
                break;
            case StyleProperty.MarginLeft:
                USS.ApplyMargin(el, value, USSDirection.Left);
                break;
            case StyleProperty.Margin:
                USS.ApplyMargin(el, value, USSDirection.All);
                break;
            case StyleProperty.BorderTopWidth:
                USS.ApplyBorderWidth(el, value, USSDirection.Top);
                break;
            case StyleProperty.BorderRightWidth:
                USS.ApplyBorderWidth(el, value, USSDirection.Right);
                break;
            case StyleProperty.BorderBottomWidth:
                USS.ApplyBorderWidth(el, value, USSDirection.Bottom);
                break;
            case StyleProperty.BorderLeftWidth:
                USS.ApplyBorderWidth(el, value, USSDirection.Left);
                break;
            case StyleProperty.BorderWidth:
                USS.ApplyBorderWidth(el, value, USSDirection.All);
                break;
            case StyleProperty.Top:
                USS.ApplyDirection(el, value, USSDirection.Top);
                break;
            case StyleProperty.Bottom:
                USS.ApplyDirection(el, value, USSDirection.Bottom);
                break;
            case StyleProperty.Left:
                USS.ApplyDirection(el, value, USSDirection.Left);
                break;
            case StyleProperty.Right:
                USS.ApplyDirection(el, value, USSDirection.Right);
                break;
            case StyleProperty.Width:
                USS.ApplyWidth(el, value);
                break;
            case StyleProperty.MinWidth:
                USS.ApplyMinWidth(el, value);
                break;
            case StyleProperty.MaxWidth:
                USS.ApplyMaxWidth(el, value);
                break;
            case StyleProperty.Height:
                USS.ApplyHeight(el, value);
                break;
            case StyleProperty.MinHeight:
                USS.ApplyMinHeight(el, value);
                break;
            case StyleProperty.MaxHeight:
                USS.ApplyMaxHeight(el, value);
                break;
            case StyleProperty.BorderTopLeftRadius:
                USS.ApplyBorderRadius(el, value, USSCorner.TopLeft);
                break;
            case StyleProperty.BorderTopRightRadius:
                USS.ApplyBorderRadius(el, value, USSCorner.TopRight);
                break;
            case StyleProperty.BorderBottomLeftRadius:
                USS.ApplyBorderRadius(el, value, USSCorner.BottomLeft);
                break;
            case StyleProperty.BorderBottomRightRadius:
                USS.ApplyBorderRadius(el, value, USSCorner.BottomRight);
                break;
            case StyleProperty.BorderRadius:
                USS.ApplyBorderRadius(el, value, USSCorner.All);
                break;
            case StyleProperty.FontSize:
                USS.ApplyFontSize(el, value);
                break;
            case StyleProperty.LetterSpacing:
                USS.ApplyLetterSpacing(el, value);
                break;
            case StyleProperty.Opacity:
                USS.ApplyOpacity(el, value);
                break;
            case StyleProperty.WordSpacing:
                USS.ApplyWordSpacing(el, value);
                break;
            case StyleProperty.FlexBasis:
                USS.ApplyFlexBasis(el, value);
                break;
            case StyleProperty.FlexGrow:
                USS.ApplyFlexGrow(el, value);
                break;
            case StyleProperty.FlexShrink:
                USS.ApplyFlexShrink(el, value);
                break;
            case StyleProperty.Overflow:
                USS.ApplyOverflow(el, value);
                break;
            case StyleProperty.Position:
                USS.ApplyPosition(el, value);
                break;
            case StyleProperty.Rotate:
                USS.ApplyRotate(el, value);
                break;
            case StyleProperty.TextOverflow:
                USS.ApplyTextOverflow(el, value);
                break;
            case StyleProperty.Scale:
                USS.ApplyScale(el, value);
                break;
            case StyleProperty.UnityBackgroundScaleMode:
                USS.ApplyUnityBackgroundScaleMode(el, value);
                break;
            case StyleProperty.WhiteSpace:
                USS.ApplyWhiteSpace(el, value);
                break;
            case StyleProperty.UnityFontStyle:
                USS.ApplyUnityFontStyle(el, value);
                break;
            case StyleProperty.UnityTextAlign:
                USS.ApplyUnityTextAlign(el, value);
                break;
            case StyleProperty.UnityTextOverflowPosition:
                USS.ApplyUnityTextOverflowPosition(el, value);
                break;
            case StyleProperty.Visibility:
                USS.ApplyVisibility(el, value);
                break;
            case StyleProperty.UnityTextOutlineWidth:
                USS.ApplyUnityTextOutlineWidth(el, value);
                break;
            case StyleProperty.UnitySliceLeft:
                USS.ApplyUnitySliceLeft(el, value);
                break;
            case StyleProperty.UnitySliceRight:
                USS.ApplyUnitySliceRight(el, value);
                break;
            case StyleProperty.UnitySliceTop:
                USS.ApplyUnitySliceTop(el, value);
                break;
            case StyleProperty.UnitySliceBottom:
                USS.ApplyUnitySliceBottom(el, value);
                break;
            case StyleProperty.UnitySliceScale:
                USS.ApplyUnitySliceScale(el, value);
                break;
            case StyleProperty.UnityParagraphSpacing:
                USS.ApplyUnityParagraphSpacing(el, value);
                break;
            case StyleProperty.UnityOverflowClipBox:
                USS.ApplyUnityOverflowClipBox(el, value);
                break;
            case StyleProperty.BackgroundImage:
                USS.ApplyBackgroundImage(el, value);
                break;
            case StyleProperty.BackgroundSize:
                USS.ApplyBackgroundSize(el, value);
                break;
            case StyleProperty.BackgroundPositionY:
                USS.ApplyBackgroundPosition(el, value, USSAxis.Y);
                break;
            case StyleProperty.BackgroundPositionX:
                USS.ApplyBackgroundPosition(el, value, USSAxis.X);
                break;
            case StyleProperty.BackgroundPosition:
                USS.ApplyBackgroundPosition(el, value, USSAxis.All);
                break;
            case StyleProperty.Cursor:
                USS.ApplyCursor(el, value);
                break;
            case StyleProperty.Flex:
                USS.ApplyFlex(el, value);
                break;
            case StyleProperty.UnityTextOutline:
                USS.ApplyUnityTextOutline(el, value);
                break;
            case StyleProperty.TextShadow:
                USS.ApplyTextShadow(el, value);
                break;
            case StyleProperty.UnityFont:
                USS.ApplyUnityFont(el, value, false);
                break;
            case StyleProperty.UnityFontDefinition:
                USS.ApplyUnityFont(el, value, true);
                break;
            case StyleProperty.TransitionDelay:
                USS.ApplyTransitionDelay(el, value);
                break;
            case StyleProperty.TransitionProperty:
                USS.ApplyTransitionProperty(el, value);
                break;
            case StyleProperty.TransitionDuration:
                USS.ApplyTransitionDuration(el, value);
                break;
            case StyleProperty.TransitionTimingFunction:
                USS.ApplyTransitionTimingFunction(el, value);
                break;
            case StyleProperty.Transition:
                USS.ApplyTransition(el, value);
                break;
            case StyleProperty.TransformOrigin:
                USS.ApplyTransformOrigin(el, value);
                break;
            case StyleProperty.Translate:
                USS.ApplyTranslate(el, value);
                break;
            case StyleProperty.BackgroundRepeat:
                USS.ApplyBackgroundRepeat(el, value);
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