using System;
using System.Globalization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.CorticonConnector
{
    /// <summary>
    /// Extends forms definitions by adding Corticon specific fields in the form properties.
    /// </summary>
    internal class CorticonFormsDefinitionsExtender : FormsConnectorDefinitionsExtender
    {
        /// <inheritdoc/>
        public override int Ordinal
        {
            get
            {
                return 10;
            }
        }

        /// <inheritdoc/>
        public override void AddConnectorSettings(ConfigElementDictionary<string, FieldDefinitionElement> sectionFields)
        {
            if (sectionFields == null)
            {
                throw new ArgumentNullException("sectionFields");
            }

            var hubSpotFormNameField = new TextFieldDefinitionElement(sectionFields)
            {   
                Title = "Corticon service URL",
                DataFieldName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", FormAttributesPropertyName, "CorticonURL"),
                DisplayMode = FieldDisplayMode.Write,
                FieldName = "CorticonURL",
                CssClass = "MVCOnlyFrameworkFieldsCss sfMLeft35 sfMTop20 sfShortField250",
                FieldType = typeof(TextField),
                ID = "CorticonURL",
            };

            sectionFields.Add(hubSpotFormNameField);
        }

        private const string FormAttributesPropertyName = "Attributes";
    }
}