function CheckedListBox_Init() 
{
    if ( typeof( document.getElementById ) == "undefined" ) return;
    if (typeof(Page_CheckedListBoxes) == "undefined") return;
    var i, checkedListBox;
    for (i = 0; i < Page_CheckedListBoxes.length; i++) 
    {
        checkedListBox = Page_CheckedListBoxes[i];
        if (typeof(checkedListBox) == "string") 
        {
            checkedListBox = document.getElementById( checkedListBox );
            if ( checkedListBox != null ) 
            {
                CheckedListBox_Load( checkedListBox );
            }
        }
    }
}
function CheckedListBox_Load( checkedListBox ) 
{
	CheckedListBox_EnsureProperties( checkedListBox );
	checkedListBox.resizeWidth = CheckedListBox_resizeWidth;
	
	if ( checkedListBox.style.width == "" ) 
	{
		checkedListBox.resizeWidth();
	}
}
function CheckedListBox_EnsureProperties( control )
 {
    if ( typeof(control.style.borderColor) == "undefined" || control.style.borderColor == "" ) {
        control.style.borderColor = "ButtonFace";
    }
}
function CheckedListBox_resizeWidth() 
{
    var container = document.getElementById( this.id + "_Container" );
    var ScrollbarPadding = 20;
    var ContainerWidth;
    if( typeof( document.defaultView ) != "undefined" ) { // The w3c standard
        ContainerWidth = document.defaultView.getComputedStyle( container, "" ).getPropertyValue("width");
    } else if ( typeof( document.all ) != "undefined" ) { // ie
        ContainerWidth = container.offsetWidth;
    }
    this.style.width = parseInt( ContainerWidth ) + ScrollbarPadding + "px";
}
