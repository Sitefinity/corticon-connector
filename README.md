Progress® Sitefinity® CMS Corticon Connector
====================================================

## Overview

The Sitefinity Corticon Connector enables users to connect their Sitefinity Forms with a hosted Corticon decision service and leverage the powerful decision making capabilities of Corticon rules engine in order to build more interactive forms, get a quote based on form submission by their customers and much more.

For more information on Corticon and on how to host the decision service, see [Corticon Business Rules Management Engine | Progress](https://www.progress.com/corticon)

## Prerequisites

To use the connector, you need to build it from the source code. Make sure that your development system meets the following minimal requirements:

* A valid Sitefinity CMS license.
* Sitefinity CMS 14.0 or later.
* Your setup must comply with the minimum system requirements.
For more information, see the [System requirements](https://www.progress.com/documentation/sitefinity-cms/system-requirements) for the Sitefinity CMS version you are using.
* Visual Studio 2015 or later.

## Building the solution

This readme file assumes that you are using Visual Studio 2019. The older versions of Visual Studio may have small changes in the described UI elements but the process is very similar.

To use `Telerik.Sitefinity.CorticonConnector` with your Sitefinity CMS site perform the following:

1. Download this repository it into your Sitefinity CMS solution on your local drive
2. In Visual Studio open your Sitefinity solution, for example `SitefinityWebApp.sln`.
3. Add the downloaded `Telerik.Sitefinity.CorticonConnector.csproj` to your Visual Studio solution. Perform the following:
   1. Navigate to *File » Add » Existing project...*
   2. Browse to `c:\work\corticon-connector`. The exact path depends on where you have cloned the repository
   3. Browse to Telerik.Sitefinity.CorticonConnector
   4. Select `Telerik.Sitefinity.CorticonConnector.csproj` and click *Open*.
4. In your main Sitefinity project, for example `SitefinityWebApp`, add a project reference to the `Telerik.Sitefinity.CorticonConnector` project. Perform the following:
   1. Select your main Sitefinity project, for example `SitefinityWebApp` in the *Solution Explorer*
   2. In the main menu, Navigate to Project » Add Reference... A dialog opens.
   3. Navigate to *Project » Solution*.
   4. Check the `Telerik.Sitefinity.CorticonConnector` in the list on the right.
   5. Click *OK*
5. Build your Visual Studio solution.

*Note:* The Sitefinity Corticon Connector project depends on specific NuGet packages for Sitefinity CMS. When you include the Sitefinity Corticon Connector project in your solution that depends on a newer Sitefinity version, you must also update the ```Telerik.Sitefinity.Feather``` dependency of the Sitefinity Corticon Connector project to match your Sitefinity version.

## Using the Corticon Connector in your project

After you build the `Telerik.Sitefinity.CorticonConnector` project, you can connect your forms to Corticon. To do this, perform the following:

* Start your Sitefinity CMS solution
* In the browser, navigate to your Sitefinity CMS backend
* Navigate to Content » Forms and create a new form or edit an exisitng one
* Open your form's properties from Content >> Forms >> <your_form> Actions >> Properties
* Expand the 'Send form data to external applications' section
* Enter the URL of your hosted Corticon decision service in the 'Corticon service URL' field and click save
* Open the Form editor by click the title of your form in Content >> Forms
* In the *Corticon* toolbox section you'll see one new control: `Corticon field`.
* Drag and drop it as any other form control to your form.
* Open the field for edit
* Set Label and Field name and save
* Click *Publish* to save your form.

*Note:* You may add as many Corticon fields as you need to your form.

## How does it work

Once a form is created, the "Corticon service URL" is set in the properties of the form and a Corticon field is added to it, it is all set to be used on a Sitefinity page. When users browse the page containing the form they will see a normal form with all Corticon fields hidden. Once the user starts to interact with the form, all form values will be collected in a JSON object and sent to the configured Corticon decision service. The Corticon decision service will execute the configured rules and return an updated JSON. Then all the updated JSON values will set the Corticon Field values you have on your form. 

One way to use the values of the Corticon fields is to read them from Sitefinity form responses since the Corticon fields are saved just like any other field. This is especially useful when you want to display a certain quote to the end user based on their form sumission or to use the end result with some custom integration with a 3rd party solution, notificaiton system, etc.

Another option is to use the Corticon fields as part of [Sitefinity Form Rules](https://www.progress.com/documentation/sitefinity-cms/form-rules) in order to show/hide fields, show different success message, navigate to a specific page, etc.

*Note:* The JSON object sent to Corticon decision service is built using the "Field name"s of the form fields. The field names can be set only while creating a form field and they cannot be modified later on. To set the field name of a field, perform the following:
 * Corticon Field: Open the Corticon field for edit and set the Field name input
 * Other Sitefinity fields: Open the field for edit and navigate to Advanced >> Model >> MetaField and set the FieldName input