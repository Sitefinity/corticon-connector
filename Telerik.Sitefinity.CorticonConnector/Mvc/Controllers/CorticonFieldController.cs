using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.CorticonConnector.Mvc.Models;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.CorticonConnector.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "CorticonField", Title = "Corticon field", Toolbox = FormsConstants.FormControlsToolboxName, CssClass = "sfTextboxIcn sfMvcIcn", SectionName = "Corticon")]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(CorticonResources))]
    public class CorticonFieldController : FormFieldControllerBase<CorticonFieldModel>, ISupportRules, ITextField
    {
        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override CorticonFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = new CorticonFieldModel();

                return this.model;
            }
        }

        /// <inheritDocs />
        IDictionary<ConditionOperator, string> ISupportRules.Operators
        {
            get
            {
                switch (this.Model.InputType)
                {
                    case TextType.Color:
                        return new Dictionary<ConditionOperator, string>()
                        {
                            [ConditionOperator.Equal] = Res.Get<Labels>().IsOperator,
                            [ConditionOperator.NotEqual] = Res.Get<Labels>().IsNotOperator
                        };
                    case TextType.Number:
                    case TextType.Range:
                        return new Dictionary<ConditionOperator, string>()
                        {
                            [ConditionOperator.Equal] = Res.Get<Labels>().IsEqualOperator,
                            [ConditionOperator.NotEqual] = Res.Get<Labels>().IsNotEqualOperator,
                            [ConditionOperator.IsGreaterThan] = Res.Get<Labels>().IsGreaterOperator,
                            [ConditionOperator.IsLessThan] = Res.Get<Labels>().IsLessOperator,
                            [ConditionOperator.IsFilled] = Res.Get<Labels>().IsFilledOperator,
                            [ConditionOperator.IsNotFilled] = Res.Get<Labels>().IsNotFilledOperator
                        };
                    case TextType.Date:
                    case TextType.Time:
                    case TextType.Month:
                    case TextType.Week:
                    case TextType.DateTimeLocal:
                        return new Dictionary<ConditionOperator, string>()
                        {
                            [ConditionOperator.Equal] = Res.Get<Labels>().IsOperator,
                            [ConditionOperator.IsLessThan] = Res.Get<Labels>().IsBeforeOperator,
                            [ConditionOperator.IsGreaterThan] = Res.Get<Labels>().IsAfterOperator,
                            [ConditionOperator.IsFilled] = Res.Get<Labels>().IsFilledOperator,
                            [ConditionOperator.IsNotFilled] = Res.Get<Labels>().IsNotFilledOperator
                        };
                    case TextType.Hidden:
                        return new Dictionary<ConditionOperator, string>();
                    default:
                        return new Dictionary<ConditionOperator, string>()
                        {
                            [ConditionOperator.Equal] = Res.Get<Labels>().IsOperator,
                            [ConditionOperator.NotEqual] = Res.Get<Labels>().IsNotOperator,
                            [ConditionOperator.Contains] = Res.Get<Labels>().ContainsOperator,
                            [ConditionOperator.NotContains] = Res.Get<Labels>().NotContainsOperator,
                            [ConditionOperator.IsFilled] = Res.Get<Labels>().IsFilledOperator,
                            [ConditionOperator.IsNotFilled] = Res.Get<Labels>().IsNotFilledOperator
                        };
                }
            }
        }

        /// <inheritDocs />
        string ISupportRules.Title
        {
            get
            {
                return this.MetaField.Title;
            }
        }

        /// <inheritDocs />
        TextType ITextField.InputType
        {
            get
            {
                return this.Model.InputType;
            }
        }

        private CorticonFieldModel model;
    }
}