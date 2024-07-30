using System;
using NUnit.Framework;
using UnityEngine.UIElements;

public class ElementsTests {
    [TestCase("<ui:VisualElement />", typeof(VisualElement), TestName = "VisualElement")]
    [TestCase("<ui:ScrollView />", typeof(ScrollView), TestName = "ScrollView")]
    [TestCase("<ui:ListView />", typeof(ListView), TestName = "ListView")]
    [TestCase("<ui:TreeView />", typeof(TreeView), TestName = "TreeView")]
    [TestCase("<ui:GroupBox />", typeof(GroupBox), TestName = "GroupBox")]
    [TestCase("<ui:Label />", typeof(Label), TestName = "Label")]
    [TestCase("<ui:Button />", typeof(Button), TestName = "Button")]
    [TestCase("<ui:Toggle />", typeof(Toggle), TestName = "Toggle")]
    [TestCase("<ui:Scroller />", typeof(Scroller), TestName = "Scroller")]
    [TestCase("<ui:TextField />", typeof(TextField), TestName = "TextField")]
    [TestCase("<ui:Foldout />", typeof(Foldout), TestName = "Foldout")]
    [TestCase("<ui:Slider />", typeof(Slider), TestName = "Slider")]
    [TestCase("<ui:SliderInt />", typeof(SliderInt), TestName = "SliderInt")]
    [TestCase("<ui:MinMaxSlider />", typeof(MinMaxSlider), TestName = "MinMaxSlider")]
    [TestCase("<ui:ProgressBar />", typeof(ProgressBar), TestName = "ProgressBar")]
    [TestCase("<ui:DropdownField />", typeof(DropdownField), TestName = "DropdownField")]
    [TestCase("<ui:EnumField />", typeof(EnumField), TestName = "EnumField")]
    [TestCase("<ui:RadioButton />", typeof(RadioButton), TestName = "RadioButton")]
    [TestCase("<ui:RadioButtonGroup />", typeof(RadioButtonGroup), TestName = "RadioButtonGroup")]
    [TestCase("<ui:IntegerField />", typeof(IntegerField), TestName = "IntegerField")]
    [TestCase("<ui:FloatField />", typeof(FloatField), TestName = "FloatField")]
    [TestCase("<ui:LongField />", typeof(LongField), TestName = "LongField")]
    [TestCase("<ui:DoubleField />", typeof(DoubleField), TestName = "DoubleField")]
    [TestCase("<ui:Hash128Field />", typeof(Hash128Field), TestName = "Hash128Field")]
    [TestCase("<ui:Vector2Field />", typeof(Vector2Field), TestName = "Vector2Field")]
    [TestCase("<ui:Vector3Field />", typeof(Vector3Field), TestName = "Vector3Field")]
    [TestCase("<ui:Vector4Field />", typeof(Vector4Field), TestName = "Vector4Field")]
    [TestCase("<ui:RectField />", typeof(RectField), TestName = "RectField")]
    [TestCase("<ui:BoundsField />", typeof(BoundsField), TestName = "BoundsField")]
    [TestCase("<ui:UnsignedIntegerField />", typeof(UnsignedIntegerField), TestName = "UnsignedIntegerField")]
    [TestCase("<ui:UnsignedLongField />", typeof(UnsignedLongField), TestName = "UnsignedLongField")]
    [TestCase("<ui:Vector2IntField />", typeof(Vector2IntField), TestName = "Vector2IntField")]
    [TestCase("<ui:Vector3IntField />", typeof(Vector3IntField), TestName = "Vector3IntField")]
    [TestCase("<ui:RectIntField />", typeof(RectIntField), TestName = "RectIntField")]
    [TestCase("<ui:BoundsIntField />", typeof(BoundsIntField), TestName = "BoundsIntField")]
    public void ValidInputs(string elementXml, Type expectedType) {
        VisualElement element = _UI.Build<VisualElement>(elementXml);
        Assert.NotNull(element);
        Assert.AreEqual(expectedType, element.GetType());
    }
}