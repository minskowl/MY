using System;

namespace Savchin.Web.UI
{
    /// <summary>
    /// PhoneBox
    /// </summary>
    public class PhoneBox : TextBoxEx 
    {

        /// <summary>
        /// Registers client script for generating postback events prior to rendering on the client, if <see cref="P:System.Web.UI.WebControls.TextBox.AutoPostBack"></see> is true.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            MaxLength = 15;
            Attributes.Add("onblur", "phoneFilter(this, '(###) ###-####');");

            Page.ClientScript.RegisterClientScriptBlock(typeof(PhoneBox),"Check phone", @"
function phoneFilter(form, format) 
{
    if(form==null || format ==null)
       return;
      
	var input = form.value;
	
	if(input.length == 0) 
	   return;
	var regularExp=  new RegExp(""[^\\d]"",""g"");
	numbers=input.replace(regularExp ,''); 
	
	if (numbers.length == 7)
    {
        numbers = '000' + numbers;
    }

	if(numbers.length != 7 && numbers.length < 10) 
	{
		alert('The number must be of length 7 or 10');
		form.focus();
        form.value = numbers;
        return;
	}
    
	//remove country code, if any
	var output = ''; //assign numbers here
	//assign numbers to chosen format
	var n = 0, i = 0;
	while(i < format.length && n < numbers.length) 
	{
		var char = format.charAt(i);
		if(char == '#') 
		{
			output += numbers.charAt(n++)
		}
		else 
		{
			output += char;
		}
		i++;
	}

    form.value = output; //output to form		
	    
}
            ", true);

            base.OnPreRender(e);
        }
    }
}