using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.CorticonConnector.Mvc.Models
{
    public class CorticonFieldModel : FormFieldModel, IHideable
    {
        /// <summary>
        /// Gets or sets the type of the input element.
        /// </summary>
        /// <value>
        /// The type of the input element.
        /// </value>
        public TextType InputType { get; set; }

        /// <inheritDocs />
        public bool Hidden { get; set; }

        /// <inheritDocs />
        public override object GetViewModel(object value, Telerik.Sitefinity.Metadata.Model.IMetaField metaField)
        {
            this.Value = value as string ?? this.MetaField.DefaultValue ?? string.Empty;
            return this;
        }

        /// <inheritDocs />
        public override IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = base.GetMetaField(formFieldControl);

            if (string.IsNullOrEmpty(metaField.Title))
                metaField.Title = Res.Get<FieldResources>().Untitled;

            return metaField;
        }
    }
}