using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class StylesTests {
    [Test]
    public void AlignContent() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""align-content: center;"" />
        ");
        Assert.AreEqual(Align.Center, el.style.alignContent.value);
    }

    [Test]
    public void AlignItems() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""align-items: center;"" />
        ");
        Assert.AreEqual(Align.Center, el.style.alignItems.value);
    }

    [Test]
    public void AlignSelf() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""align-self: center;"" />
        ");
        Assert.AreEqual(Align.Center, el.style.alignSelf.value);
    }

    [Test]
    public void BackgroundColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""background-color: red;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.red, el.style.backgroundColor.value);
    }

    [Test]
    public void BackgroundImage() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""background-image: url('image.png');"" />
        ");
        Assert.IsNotNull(el.style.backgroundImage.value);
    }

    [Test]
    public void BackgroundPosition() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""background-position: top 50% left 50%;"" />
        ");

        Assert.AreEqual(new Length(50, LengthUnit.Percent), el.style.backgroundPositionX.value.offset);
        Assert.AreEqual(new Length(50, LengthUnit.Percent), el.style.backgroundPositionY.value.offset);
    }

    [Test]
    public void BackgroundPositionX() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""background-position-x: right 40px;"" />
        ");
        Assert.AreEqual(new BackgroundPosition(BackgroundPositionKeyword.Right, new Length(40, LengthUnit.Pixel)),
            el.style.backgroundPositionX.value);
    }

    [Test]
    public void BackgroundPositionY() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""background-position-y: top;"" />
        ");
        Assert.AreEqual(BackgroundPositionKeyword.Top, el.style.backgroundPositionY.value.keyword);
    }

    [Test]
    public void BackgroundRepeat() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""background-repeat: repeat-x;"" />
        ");
        BackgroundRepeat repeat = el.style.backgroundRepeat.value;
        Assert.AreEqual(Repeat.Repeat, repeat.x);
        Assert.AreEqual(Repeat.NoRepeat, repeat.y);
    }

    [Test]
    public void BackgroundSize() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""background-size: 70px;"" />
        ");
        Assert.AreEqual(new BackgroundSize(new Length(70, LengthUnit.Pixel), new Length(70, LengthUnit.Pixel)),
            el.style.backgroundSize.value);
    }

    [Test]
    public void BorderColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-color: black;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderLeftColor.value);
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderRightColor.value);
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderTopColor.value);
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderBottomColor.value);
    }

    [Test]
    public void BorderBottomColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-bottom-color: black;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderBottomColor.value);
    }

    [Test]
    public void BorderBottomLeftRadius() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-bottom-left-radius: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.borderBottomLeftRadius.value.value);
    }

    [Test]
    public void BorderBottomRightRadius() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-bottom-right-radius: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.borderBottomRightRadius.value.value);
    }

    [Test]
    public void BorderBottomWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-bottom-width: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.borderBottomWidth.value);
    }

    [Test]
    public void BorderLeftColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-left-color: black;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderLeftColor.value);
    }

    [Test]
    public void BorderLeftWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-left-width: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.borderLeftWidth.value);
    }

    [Test]
    public void BorderRadius() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-radius: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.borderTopLeftRadius.value.value);
        Assert.AreEqual(5f, el.style.borderTopRightRadius.value.value);
        Assert.AreEqual(5f, el.style.borderBottomRightRadius.value.value);
        Assert.AreEqual(5f, el.style.borderBottomLeftRadius.value.value);
    }

    [Test]
    public void BorderRightColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-right-color: black;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderRightColor.value);
    }

    [Test]
    public void BorderRightWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-right-width: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.borderRightWidth.value);
    }

    [Test]
    public void BorderTopColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-top-color: black;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.black, el.style.borderTopColor.value);
    }

    [Test]
    public void BorderTopLeftRadius() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-top-left-radius: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.borderTopLeftRadius.value.value);
    }

    [Test]
    public void BorderTopRightRadius() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-top-right-radius: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.borderTopRightRadius.value.value);
    }

    [Test]
    public void BorderTopWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-top-width: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.borderTopWidth.value);
    }

    [Test]
    public void BorderWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""border-width: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.borderLeftWidth.value);
        Assert.AreEqual(10f, el.style.borderRightWidth.value);
        Assert.AreEqual(10f, el.style.borderTopWidth.value);
        Assert.AreEqual(10f, el.style.borderBottomWidth.value);
    }

    [Test]
    public void Bottom() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""bottom: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.bottom.value.value);
    }

    [Test]
    public void Color() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""color: red;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.red, el.style.color.value);
    }

    [Test]
    public void Cursor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""cursor: resource('image2');"" />
        ");
        Assert.IsNotNull(el.style.cursor.value.texture);
    }

    [Test]
    public void Display() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""display: flex;"" />
        ");
        Assert.AreEqual(DisplayStyle.Flex, el.style.display.value);
    }

    [Test]
    public void Flex() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
        <ui:VisualElement style=""flex: 1 1 auto;"" />
    ");
        Assert.AreEqual(1f, el.style.flexGrow.value);
        Assert.AreEqual(1f, el.style.flexShrink.value);
        Assert.AreEqual(StyleKeyword.Auto, el.style.flexBasis.keyword);
    }

    [Test]
    public void FlexBasis() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""flex-basis: 50px;"" />
        ");
        Assert.AreEqual(50f, el.style.flexBasis.value.value);
    }

    [Test]
    public void FlexDirection() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""flex-direction: row;"" />
        ");
        Assert.AreEqual(UnityEngine.UIElements.FlexDirection.Row, el.style.flexDirection.value);
    }

    [Test]
    public void FlexGrow() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""flex-grow: 1;"" />
        ");
        Assert.AreEqual(1f, el.style.flexGrow.value);
    }

    [Test]
    public void FlexShrink() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""flex-shrink: 1;"" />
        ");
        Assert.AreEqual(1f, el.style.flexShrink.value);
    }

    [Test]
    public void FlexWrap() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""flex-wrap: wrap;"" />
        ");
        Assert.AreEqual(Wrap.Wrap, el.style.flexWrap.value);
    }

    [Test]
    public void FontSize() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""font-size: 16px;"" />
        ");
        Assert.AreEqual(16f, el.style.fontSize.value.value);
    }

    [Test]
    public void Height() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""height: 200px;"" />
        ");
        Assert.AreEqual(200f, el.style.height.value.value);
    }

    [Test]
    public void JustifyContent() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""justify-content: center;"" />
        ");
        Assert.AreEqual(Justify.Center, el.style.justifyContent.value);
    }

    [Test]
    public void Left() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""left: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.left.value.value);
    }

    [Test]
    public void LetterSpacing() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""letter-spacing: 1px;"" />
        ");
        Assert.AreEqual(1f, el.style.letterSpacing.value.value);
    }

    [Test]
    public void Margin() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""margin: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.marginLeft.value.value);
        Assert.AreEqual(5f, el.style.marginRight.value.value);
        Assert.AreEqual(5f, el.style.marginTop.value.value);
        Assert.AreEqual(5f, el.style.marginBottom.value.value);
    }

    [Test]
    public void MarginBottom() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""margin-bottom: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.marginBottom.value.value);
    }

    [Test]
    public void MarginLeft() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""margin-left: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.marginLeft.value.value);
    }

    [Test]
    public void MarginRight() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""margin-right: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.marginRight.value.value);
    }

    [Test]
    public void MarginTop() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""margin-top: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.marginTop.value.value);
    }

    [Test]
    public void MaxHeight() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""max-height: 500px;"" />
        ");
        Assert.AreEqual(500f, el.style.maxHeight.value.value);
    }

    [Test]
    public void MaxWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""max-width: 100px;"" />
        ");
        Assert.AreEqual(100f, el.style.maxWidth.value.value);
    }

    [Test]
    public void MinHeight() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""min-height: 100px;"" />
        ");
        Assert.AreEqual(100f, el.style.minHeight.value.value);
    }

    [Test]
    public void MinWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""min-width: 50px;"" />
        ");
        Assert.AreEqual(50f, el.style.minWidth.value.value);
    }

    [Test]
    public void Opacity() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""opacity: 0.5;"" />
        ");
        Assert.AreEqual(0.5f, el.style.opacity.value);
    }

    [Test]
    public void Overflow() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""overflow: hidden;"" />
        ");
        Assert.AreEqual(UnityEngine.UIElements.Overflow.Hidden, el.style.overflow.value);
    }

    [Test]
    public void Padding() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""padding: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.paddingLeft.value.value);
        Assert.AreEqual(10f, el.style.paddingRight.value.value);
        Assert.AreEqual(10f, el.style.paddingTop.value.value);
        Assert.AreEqual(10f, el.style.paddingBottom.value.value);
    }

    [Test]
    public void PaddingBottom() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""padding-bottom: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.paddingBottom.value.value);
    }

    [Test]
    public void PaddingLeft() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""padding-left: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.paddingLeft.value.value);
    }

    [Test]
    public void PaddingRight() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""padding-right: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.paddingRight.value.value);
    }

    [Test]
    public void PaddingTop() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""padding-top: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.paddingTop.value.value);
    }

    [Test]
    public void Position() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""position: absolute;"" />
        ");
        Assert.AreEqual(UnityEngine.UIElements.Position.Absolute, el.style.position.value);
    }

    [Test]
    public void Right() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""right: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.right.value.value);
    }

    [Test]
    public void Rotate() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""rotate: 45deg;"" />
        ");
        Assert.AreEqual(45, el.style.rotate.value.angle.value);
    }

    [Test]
    public void Scale() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
        <ui:VisualElement style=""scale: 2;"" />
    ");
        Assert.AreEqual(new Vector3(2, 2, 1), el.style.scale.value.value);
    }

    [Test]
    public void TextOverflow() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""text-overflow: ellipsis;"" />
        ");
        Assert.AreEqual(UnityEngine.UIElements.TextOverflow.Ellipsis, el.style.textOverflow.value);
    }

    [Test]
    public void TextShadow() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""text-shadow: 1px 1px 2px black;"" />
        ");
        UnityEngine.UIElements.TextShadow shadow = el.style.textShadow.value;
        Assert.AreEqual(new Vector2(1, 1), shadow.offset);
        Assert.AreEqual(2f, shadow.blurRadius);
        Assert.AreEqual(UnityEngine.Color.black, shadow.color);
    }

    [Test]
    public void Top() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""top: 10px;"" />
        ");
        Assert.AreEqual(10f, el.style.top.value.value);
    }

    [Test]
    public void TransformOrigin() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""transform-origin: 50% 50%;"" />
        ");
        TransformOrigin origin = el.style.transformOrigin.value;
        Assert.AreEqual(new Length(50, LengthUnit.Percent), origin.x);
        Assert.AreEqual(new Length(50, LengthUnit.Percent), origin.y);
    }

    [Test]
    public void Transition() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
        <ui:VisualElement style=""transition: width 0.5s ease-in, height 1s ease-out;"" />
    ");
        List<StylePropertyName> properties = el.style.transitionProperty.value;
        Assert.Contains(new StylePropertyName("width"), properties);
        Assert.Contains(new StylePropertyName("height"), properties);

        List<TimeValue> durations = el.style.transitionDuration.value;
        Assert.AreEqual(new TimeValue(0.5f, TimeUnit.Second), durations[0]);
        Assert.AreEqual(new TimeValue(1f, TimeUnit.Second), durations[1]);

        List<EasingFunction> functions = el.style.transitionTimingFunction.value;
        Assert.AreEqual(EasingMode.EaseIn, functions[0].mode);
        Assert.AreEqual(EasingMode.EaseOut, functions[1].mode);
    }

    [Test]
    public void TransitionDelay() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
        <ui:VisualElement style=""transition-delay: 1s, 2s;"" />
    ");
        List<TimeValue> delays = el.style.transitionDelay.value;
        Assert.AreEqual(new TimeValue(1, TimeUnit.Second), delays[0]);
        Assert.AreEqual(new TimeValue(2, TimeUnit.Second), delays[1]);
    }

    [Test]
    public void TransitionDuration() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
        <ui:VisualElement style=""transition-duration: 2s, 4s;"" />
    ");
        List<TimeValue> durations = el.style.transitionDuration.value;
        Assert.AreEqual(new TimeValue(2, TimeUnit.Second), durations[0]);
        Assert.AreEqual(new TimeValue(4, TimeUnit.Second), durations[1]);
    }

    [Test]
    public void TransitionProperty() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""transition-property: width, height;"" />
        ");
        List<StylePropertyName> properties = el.style.transitionProperty.value;
        Assert.Contains(new StylePropertyName("width"), properties);
        Assert.Contains(new StylePropertyName("height"), properties);
    }

    [Test]
    public void TransitionTimingFunction() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""transition-timing-function: ease-in, linear;"" />
        ");
        List<EasingFunction> functions = el.style.transitionTimingFunction.value;
        Assert.AreEqual(EasingMode.EaseIn, functions[0].mode);
        Assert.AreEqual(EasingMode.Linear, functions[1].mode);
    }

    [Test]
    public void Translate() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""translate: 10px 20px;"" />
        ");
        Translate translate = el.style.translate.value;
        Assert.AreEqual(new Length(10, LengthUnit.Pixel), translate.x);
        Assert.AreEqual(new Length(20, LengthUnit.Pixel), translate.y);
    }

    [Test]
    public void UnityBackgroundImageTintColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-background-image-tint-color: blue;"" />
        ");
        Assert.AreEqual(UnityEngine.Color.blue, el.style.unityBackgroundImageTintColor.value);
    }

    [Test]
    public void UnityFont() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-font: resource(slabo)"" />
        ");
        Assert.IsNotNull(el.style.unityFont.value);
    }

    [Test]
    public void UnityFontDefinition() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-font-definition: resource('slabo')"" />
        ");
        Assert.IsNotNull(el.style.unityFontDefinition.value);
    }

    [Test]
    public void UnityFontStyle() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-font-style: bold;"" />
        ");
        Assert.AreEqual(FontStyle.Bold, el.style.unityFontStyleAndWeight.value);
    }

    [Test]
    public void UnityOverflowClipBox() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-overflow-clip-box: padding-box;"" />
        ");
        Assert.AreEqual(OverflowClipBox.PaddingBox, el.style.unityOverflowClipBox.value);
    }

    [Test]
    public void UnityParagraphSpacing() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-paragraph-spacing: 5px;"" />
        ");
        Assert.AreEqual(5f, el.style.unityParagraphSpacing.value.value);
    }

    [Test]
    public void UnitySliceBottom() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-slice-bottom: 10;"" />
        ");
        Assert.AreEqual(10, el.style.unitySliceBottom.value);
    }

    [Test]
    public void UnitySliceLeft() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-slice-left: 10;"" />
        ");
        Assert.AreEqual(10, el.style.unitySliceLeft.value);
    }

    [Test]
    public void UnitySliceRight() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-slice-right: 10;"" />
        ");
        Assert.AreEqual(10, el.style.unitySliceRight.value);
    }

    [Test]
    public void UnitySliceScale() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-slice-scale: 0.5;"" />
        ");
        Assert.AreEqual(0.5f, el.style.unitySliceScale.value);
    }

    [Test]
    public void UnitySliceTop() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-slice-top: 10;"" />
        ");
        Assert.AreEqual(10, el.style.unitySliceTop.value);
    }

    [Test]
    public void UnityTextAlign() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-text-align: upper-left;"" />
        ");
        Assert.AreEqual(TextAnchor.UpperLeft, el.style.unityTextAlign.value);
    }

    [Test]
    public void UnityTextOutline() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-text-outline: 10px black;"" />
        ");
        Assert.AreEqual(10f, el.style.unityTextOutlineWidth.value);
        Assert.AreEqual(UnityEngine.Color.black, el.style.unityTextOutlineColor.value);
    }

    [Test]
    public void UnityTextOutlineColor() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
        <ui:VisualElement style=""-unity-text-outline-color: green;"" />
    ");
        Color expectedGreen = new(0f, 0.5f, 0f, 1f); // Expected value
        Color actualColor = el.style.unityTextOutlineColor.value;
        float tolerance = 0.01f;

        Assert.AreEqual(expectedGreen.r, actualColor.r, tolerance);
        Assert.AreEqual(expectedGreen.g, actualColor.g, tolerance);
        Assert.AreEqual(expectedGreen.b, actualColor.b, tolerance);
        Assert.AreEqual(expectedGreen.a, actualColor.a, tolerance);
    }

    [Test]
    public void UnityTextOutlineWidth() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-text-outline-width: 2px;"" />
        ");
        Assert.AreEqual(2f, el.style.unityTextOutlineWidth.value);
    }

    [Test]
    public void UnityTextOverflowPosition() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""-unity-text-overflow-position: start;"" />
        ");
        Assert.AreEqual(TextOverflowPosition.Start, el.style.unityTextOverflowPosition.value);
    }

    [Test]
    public void Visibility() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""visibility: hidden;"" />
        ");
        Assert.AreEqual(UnityEngine.UIElements.Visibility.Hidden, el.style.visibility.value);
    }

    [Test]
    public void WhiteSpace() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""white-space: nowrap;"" />
        ");
        Assert.AreEqual(UnityEngine.UIElements.WhiteSpace.NoWrap, el.style.whiteSpace.value);
    }

    [Test]
    public void Width() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""width: 300px;"" />
        ");
        Assert.AreEqual(300f, el.style.width.value.value);
    }

    [Test]
    public void WordSpacing() {
        VisualElement el = UIBuddy.Build<VisualElement>(@"
            <ui:VisualElement style=""word-spacing: 2px;"" />
        ");
        Assert.AreEqual(2f, el.style.wordSpacing.value.value);
    }
}