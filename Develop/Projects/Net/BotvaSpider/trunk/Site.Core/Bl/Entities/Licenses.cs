

/******************************************
 * Auto-generated by CodeRocket
 * 08.11.2009 21:32:07
 ******************************************/
using System;
using System.Xml.Serialization;
using Savchin.Validation;
using Site.Core;
using Site.Dal.Entities;


namespace Site.Bl.Entities 
{    
    
	/// <summary>
	/// Base License
	/// </summary>
	[Serializable]
	public class License : EntityBase<LicenseValue>
{

     	
	
	 #region Properties

        
              /// <summary>
        /// Gets or sets the Count.
        /// </summary>
        /// <value>The Count.</value>   
		[XmlAttribute]
  		public System.Int32 Count 
		{
			get{return objectValue.Count;}
			set{objectValue.Count = value;}
		}			

              /// <summary>
        /// Gets or sets the CreationDate.
        /// </summary>
        /// <value>The CreationDate.</value>   
		[XmlAttribute]
  		public System.DateTime CreationDate 
		{
			get{return objectValue.CreationDate;}
			set{objectValue.CreationDate = value;}
		}			

              /// <summary>
        /// Gets or sets the LicenseID.
        /// </summary>
        /// <value>The LicenseID.</value>   
		[XmlAttribute]
  		public System.Int32 LicenseID 
		{
			get{return objectValue.LicenseID;}
			set{objectValue.LicenseID = value;}
		}			

              /// <summary>
        /// Gets or sets the ProductID.
        /// </summary>
        /// <value>The ProductID.</value>   
		[XmlAttribute]
  		public System.Int32 ProductID 
		{
			get{return objectValue.ProductID;}
			set{objectValue.ProductID = value;}
		}			

              /// <summary>
        /// Gets or sets the PublicKey.
        /// </summary>
        /// <value>The PublicKey.</value>   
		[XmlAttribute]
  		public System.Guid PublicKey 
		{
			get{return objectValue.PublicKey;}
			set{objectValue.PublicKey = value;}
		}			

              /// <summary>
        /// Gets or sets the UserID.
        /// </summary>
        /// <value>The UserID.</value>   
		[XmlAttribute]
  		public System.Int32 UserID 
		{
			get{return objectValue.UserID;}
			set{objectValue.UserID = value;}
		}			

              /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        /// <value>The Version.</value>   
		[XmlAttribute]
        [RequiredFieldValidation()]
  		public System.String Version 
		{
			get{return objectValue.Version;}
			set{objectValue.Version = value;}
		}			

              /// <summary>
        /// Gets or sets the WmTransferID.
        /// </summary>
        /// <value>The WmTransferID.</value>   
		[XmlAttribute]
  		public System.Int32 WmTransferID 
		{
			get{return objectValue.WmTransferID;}
			set{objectValue.WmTransferID = value;}
		}			

        
        #endregion
        
        /// <summary>
        /// Initializes a new instance of the <see cref="License"/> class.
        /// </summary>
        public License() : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="License"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal License(LicenseValue value) : base(value)
        {
        }       
          
        
        /// <summary>
        /// Copies the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
//        protected override void Copy(object destination)
//        {
//            LicenseBase<TPool> dest = destination as LicenseBase<TPool>;
            
 //           dest._Count = _Count;
 //           dest._CreationDate = _CreationDate;
 //           dest._LicenseID = _LicenseID;
 //           dest._ProductID = _ProductID;
 //           dest._PublicKey = _PublicKey;
 //           dest._UserID = _UserID;
 //           dest._Version = _Version;
 //           dest._WmTransferID = _WmTransferID;
            
 //            base.Copy(destination);
 //       }	
        
        

        
        
 
 
	}
}
