using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.CorticonConnector
{
    /// <summary>
    /// Corticon OData service endpoint provider
    /// </summary>
    /// <seealso cref="Telerik.Sitefinity.Web.Services.Contracts.Operations.IOperationProvider" />
    public class CorticonOperationProvider : IOperationProvider
    {
        /// <inheritDocs />
        public IEnumerable<OperationData> GetOperations(Type clrType)
        {
            if (clrType == null)
            {
                var corticonResultOperation = OperationData.Create<Guid, string, string>(this.CorticonResult);
                corticonResultOperation.OperationType = OperationType.Unbound;
                corticonResultOperation.IsAllowedUnauthorized = true;
                corticonResultOperation.IsRead = false;

                return new OperationData[] { corticonResultOperation };
            }

            return Enumerable.Empty<OperationData>();
        }

        private string CorticonResult(OperationContext context, Guid formId, string fieldsJSON)
        {
            var form = FormsManager.GetManager().GetForms().FirstOrDefault(f => f.Id == formId);
            if (form != null)
            {
                var formAttributes = FormsManager.GetManager().GetForm(formId).Attributes;
                if (formAttributes.ContainsKey("CorticonURL") && !string.IsNullOrEmpty(formAttributes["CorticonURL"]))
                {
                    string corticonURL = formAttributes["CorticonURL"];
                    HttpClient client = new HttpClient();
                    var data = new StringContent(fieldsJSON, Encoding.UTF8, "application/json");
                    var corticonResult = client.PostAsync(corticonURL, data).Result.Content.ReadAsStringAsync().Result;

                    return corticonResult;
                }
                else
                {
                    throw new InvalidOperationException("Corticon URL is not set in Form properties!");
                }
            } 

            throw new ArgumentException("Form not found!");
        }
    }
}