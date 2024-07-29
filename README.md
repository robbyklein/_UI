# UIBuddy

A Unity package that simplifies the creation of dynamic UI elements and the application of dynamic styles.

⚠️ This library is under active development and is subject to breaking changes.

## Installation

UIBuddy can be installed with the Unity Package Manager via git url.

```
git@github.com:robbyklein/UIBuddy.git?path=src
```

<img width="330" alt="image" src="https://github.com/robbyklein/UIBuddy/assets/6813372/e0619b86-b5df-4191-9842-dbe8774c6293">

<img width="258" alt="image" src="https://github.com/robbyklein/UIBuddy/assets/6813372/ee835cf7-b6bc-4d38-8a7f-5007cbe27f12">

## Examples

### UI Element construction

```cs
VisualElement menuItem = UIBuddy.Build<VisualElement>(@"
    <ui:VisualElement name=""MenuItem"" view-data-key=""aabc"" picking-mode=""Ignore"" tooltip=""Click to select"" usage-hints=""DynamicTransform"" tabindex=""1"" focusable=""true"" style=""flex-grow: 1;"">
        <ui:Label tabindex=""2"" text=""Option"" parse-escape-sequences=""false"" display-tooltip-when-elided=""true"" view-data-key=""qwe"" usage-hints=""GroupTransform"" focusable=""true"" binding-path=""apples"" enable-rich-text=""false"" />
        <ui:Button text=""Select"" parse-escape-sequences=""true"" display-tooltip-when-elided=""false"" view-data-key=""xcbv"" usage-hints=""DynamicTransform"" tabindex=""3"" focusable=""false"" binding-path=""potatoe"" enable-rich-text=""false"" />
    </ui:VisualElement>
");
```

### Styling

```csharp
UIBuddy.Style(menuItem, StyleProperty.FlexDirection, "row");
UIBuddy.Style(menuItem, StyleProperty.AlignItems, "flex-end");
```

## Issues

- It's currently not possible to use percentage based border widths

## Notes
- Index needs to come after choices in uxml
- if -unity-font is found, it will set -unity-font-definition to none


## Support tables

### Elements

| Element              | Attributes (Ex. Style) |
| -------------------- | ---------------------- |
| VisualElement        | ✅                     |
| ScrollView           | ✅                     |
| ListView             | ✅                     |
| TreeView             | ✅                     |
| GroupBox             | ✅                     |
| Label                | ✅                     |
| Button               | ✅                     |
| Toggle               | ✅                     |
| Scroller             | ✅                     |
| TextField            | ✅                     |
| Foldout              | ✅                     |
| Slider               | ✅                     |
| SliderInt            | ✅                     |
| MinMaxSlider         | ✅                     |
| ProgressBar          | ✅                     |
| DropdownField        | ✅                     |
| EnumField            | ✅                     |
| RadioButton          | ✅                     |
| RadioButtonGroup     | ✅                     |
| IntegerField         | ✅                     |
| FloatField           | ✅                     |
| LongField            | ✅                     |
| DoubleField          | ✅                     |
| Hash128Field         | ✅                     |
| Vector2Field         | ✅                     |
| Vector3Field         | ✅                     |
| Vector4Field         | ✅                     |
| RectField            | ✅                     |
| BoundsField          | ✅                     |
| UnsignedIntegerField | ✅                     |
| UnsignedLongField    | ✅                     |
| Vector2IntField      | ✅                     |
| Vector3IntField      | ✅                     |
| RectIntField         | ✅                     |
| BoundsIntField       | ✅                     |


### Styles

⚠️represents a deprecated property.

| Property                           | Implemented   |
| ---------------------------------- |---------------|
| align-content                      | ✅             |
| align-items                        | ✅             |
| align-self                         | ✅             |
| all                                | ❌             |
| background-color                   | ✅             |
| background-image                   | ✅             |
| background-position                | ✅             |
| background-position-x              | ✅             |
| background-position-y              | ✅             |
| background-repeat                  | ❌             |
| background-size                    | ✅             |
| border-bottom-color                | ✅             |
| border-bottom-left-radius          | ✅             |
| border-bottom-right-radius         | ✅             |
| border-bottom-width                | ✅             |
| border-color                       | ✅             |
| border-left-color                  | ✅             |
| border-left-width                  | ✅             |
| border-radius                      | ✅             |
| border-right-color                 | ✅             |
| border-right-width                 | ✅             |
| border-top-color                   | ✅             |
| border-top-left-radius             | ✅             |
| border-top-right-radius            | ✅             |
| border-top-width                   | ✅             |
| border-width                       | ✅             |
| bottom                             | ✅             |
| color                              | ✅             |
| cursor                             | ✅             |
| display                            | ✅             |
| flex                               | ✅             |
| flex-basis                         | ✅             |
| flex-direction                     | ✅             |
| flex-grow                          | ✅             |
| flex-shrink                        | ✅             |
| flex-wrap                          | ✅             |
| font-size                          | ✅             |
| height                             | ✅             |
| justify-content                    | ✅             |
| left                               | ✅             |
| letter-spacing                     | ✅             |
| margin                             | ✅             |
| margin-bottom                      | ✅             |
| margin-left                        | ✅             |
| margin-right                       | ✅             |
| margin-top                         | ✅             |
| max-height                         | ✅             |
| max-width                          | ✅             |
| min-height                         | ✅             |
| min-width                          | ✅             |
| opacity                            | ✅             |
| overflow                           | ✅             |
| padding                            | ✅             |
| padding-bottom                     | ✅             |
| padding-left                       | ✅             |
| padding-right                      | ✅             |
| padding-top                        | ✅             |
| position                           | ✅             |
| right                              | ✅             |
| rotate                             | ✅             |
| scale                              | ✅             |
| text-overflow                      | ✅             |
| text-shadow                        | ✅             |
| top                                | ✅             |
| transform-origin                   | ✅             |
| transition                         | ✅             |
| transition-delay                   | ✅             |
| transition-duration                | ✅             |
| transition-property                | ✅             |
| transition-timing-function         | ✅             |
| translate                          | ✅             |
| -unity-background-image-tint-color | ✅             |
| -unity-background-scale-mode       | ⚠️            |
| -unity-font                        | ✅             |
| -unity-font-definition             | ✅             |
| -unity-font-style                  | ✅             |
| -unity-overflow-clip-box           | ✅             |
| -unity-paragraph-spacing           | ✅             |
| -unity-slice-bottom                | ✅             |
| -unity-slice-left                  | ✅             |
| -unity-slice-right                 | ✅             |
| -unity-slice-scale                 | ✅             |
| -unity-slice-top                   | ✅             |
| -unity-text-align                  | ✅             |
| -unity-text-outline                | ✅             |
| -unity-text-outline-color          | ✅             |
| -unity-text-outline-width          | ✅             |
| -unity-text-overflow-position      | ✅             |
| visibility                         | ✅             |
| white-space                        | ✅             |
| width                              | ✅             |
| word-spacing                       | ✅             |