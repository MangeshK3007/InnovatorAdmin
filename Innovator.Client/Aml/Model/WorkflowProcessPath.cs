using Innovator.Client;
using System;

namespace Innovator.Client.Model
{
  ///<summary>Class for the item type Workflow Process Path </summary>
  public class WorkflowProcessPath : Item, INullRelationship<Activity>, IRelationship<Activity>
  {
    protected WorkflowProcessPath() { }
    public WorkflowProcessPath(ElementFactory amlContext, params object[] content) : base(amlContext, content) { }
    static WorkflowProcessPath() { Innovator.Client.Item.AddNullItem<WorkflowProcessPath>(new WorkflowProcessPath { _attr = ElementAttributes.ReadOnly | ElementAttributes.Null }); }

    /// <summary>Retrieve the <c>authentication</c> property of the item</summary>
    public IProperty_Text Authentication()
    {
      return this.Property("authentication");
    }
    /// <summary>Retrieve the <c>behavior</c> property of the item</summary>
    public IProperty_Text Behavior()
    {
      return this.Property("behavior");
    }
    /// <summary>Retrieve the <c>is_complete</c> property of the item</summary>
    public IProperty_Boolean IsComplete()
    {
      return this.Property("is_complete");
    }
    /// <summary>Retrieve the <c>is_default</c> property of the item</summary>
    public IProperty_Boolean IsDefault()
    {
      return this.Property("is_default");
    }
    /// <summary>Retrieve the <c>is_disabled</c> property of the item</summary>
    public IProperty_Boolean IsDisabled()
    {
      return this.Property("is_disabled");
    }
    /// <summary>Retrieve the <c>is_override</c> property of the item</summary>
    public IProperty_Boolean IsOverride()
    {
      return this.Property("is_override");
    }
    /// <summary>Retrieve the <c>label</c> property of the item</summary>
    public IProperty_Text Label()
    {
      return this.Property("label");
    }
    /// <summary>Retrieve the <c>name</c> property of the item</summary>
    public IProperty_Text NameProp()
    {
      return this.Property("name");
    }
    /// <summary>Retrieve the <c>segments</c> property of the item</summary>
    public IProperty_Text Segments()
    {
      return this.Property("segments");
    }
    /// <summary>Retrieve the <c>sort_order</c> property of the item</summary>
    public IProperty_Number SortOrder()
    {
      return this.Property("sort_order");
    }
    /// <summary>Retrieve the <c>x</c> property of the item</summary>
    public IProperty_Number X()
    {
      return this.Property("x");
    }
    /// <summary>Retrieve the <c>y</c> property of the item</summary>
    public IProperty_Number Y()
    {
      return this.Property("y");
    }
  }
}