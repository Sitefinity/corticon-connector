using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.CorticonConnector
{
    public class CorticonResources : FieldResources
    {
        /// <summary>
        /// Gets phrase : The label is visible only in Design mode
        /// </summary>
        [ResourceEntry("LabelVisibleOnlyInDesign",
            Value = "The label is visible only in Design mode",
            Description = "phrase : The label is visible only in Design mode",
            LastModified = "2021/11/16")]
        public string LabelVisibleOnlyInDesign
        {
            get
            {
                return this["LabelVisibleOnlyInDesign"];
            }
        }

        /// <summary>
        /// Gets phrase : The field name can be set only on create
        /// </summary>
        [ResourceEntry("FormFieldCanNotBeEdited",
            Value = "The field name can be set only on create",
            Description = "phrase : The field name can be set only on create",
            LastModified = "2021/11/16")]
        public string FormFieldCanNotBeEdited
        {
            get
            {
                return this["FormFieldCanNotBeEdited"];
            }
        }

        /// <summary>
        /// Gets phrase : Field name
        /// </summary>
        [ResourceEntry("FieldName",
            Value = "Field name",
            Description = "phrase : Field name",
            LastModified = "2021/11/16")]
        public string FieldName
        {
            get
            {
                return this["FieldName"];
            }
        }
    }
}
