using System;
using NUnit.Framework;
using UnityEngine.UIElements;

public class ElementsTests {
    [TestCase("<ui:VisualElement />", typeof(VisualElement))]
    [TestCase("<ui:ScrollView />", typeof(ScrollView))]
    [TestCase("<ui:ListView />", typeof(ListView))]
    [TestCase("<ui:TreeView />", typeof(TreeView))]
    [TestCase("<ui:GroupBox />", typeof(GroupBox))]
    [TestCase("<ui:Label />", typeof(Label))]
    [TestCase("<ui:Button />", typeof(Button))]
    [TestCase("<ui:Toggle />", typeof(Toggle))]
    [TestCase("<ui:Scroller />", typeof(Scroller))]
    [TestCase("<ui:TextField />", typeof(TextField))]
    [TestCase("<ui:Foldout />", typeof(Foldout))]
    [TestCase("<ui:Slider />", typeof(Slider))]
    [TestCase("<ui:SliderInt />", typeof(SliderInt))]
    [TestCase("<ui:MinMaxSlider />", typeof(MinMaxSlider))]
    [TestCase("<ui:ProgressBar />", typeof(ProgressBar))]
    [TestCase("<ui:DropdownField />", typeof(DropdownField))]
    [TestCase("<ui:EnumField />", typeof(EnumField))]
    [TestCase("<ui:RadioButton />", typeof(RadioButton))]
    [TestCase("<ui:RadioButtonGroup />", typeof(RadioButtonGroup))]
    [TestCase("<ui:IntegerField />", typeof(IntegerField))]
    [TestCase("<ui:FloatField />", typeof(FloatField))]
    [TestCase("<ui:LongField />", typeof(LongField))]
    [TestCase("<ui:DoubleField />", typeof(DoubleField))]
    [TestCase("<ui:Hash128Field />", typeof(Hash128Field))]
    [TestCase("<ui:Vector2Field />", typeof(Vector2Field))]
    [TestCase("<ui:Vector3Field />", typeof(Vector3Field))]
    [TestCase("<ui:Vector4Field />", typeof(Vector4Field))]
    [TestCase("<ui:RectField />", typeof(RectField))]
    [TestCase("<ui:BoundsField />", typeof(BoundsField))]
    [TestCase("<ui:UnsignedIntegerField />", typeof(UnsignedIntegerField))]
    [TestCase("<ui:UnsignedLongField />", typeof(UnsignedLongField))]
    [TestCase("<ui:Vector2IntField />", typeof(Vector2IntField))]
    [TestCase("<ui:Vector3IntField />", typeof(Vector3IntField))]
    [TestCase("<ui:RectIntField />", typeof(RectIntField))]
    [TestCase("<ui:BoundsIntField />", typeof(BoundsIntField))]
    public void TestVisualElementCreation(string elementXml, Type expectedType) {
        VisualElement element = UIBuddy.Build<VisualElement>(elementXml);
        Assert.NotNull(element);
        Assert.AreEqual(expectedType, element.GetType());

        //coment
    }
}