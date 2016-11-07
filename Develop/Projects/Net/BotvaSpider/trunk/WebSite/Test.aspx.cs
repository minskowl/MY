using System;
using System.Security.Cryptography;
using Savchin.Utils;
using Site.Bl;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        var privateKey = RSA.ExportParameters(true);
        var publicKey = RSA.ExportParameters(false);

        var manager = new ProductManager();
        var product = new Product();
        product.Name = "Бот Ботвы";
        product.CreationDate = DateTime.Now;
        product.PrivateKey = TypeSerializer<RSAParameters>.ToBinary(privateKey);
        product.PublicKey = TypeSerializer<RSAParameters>.ToBinary(publicKey);


        manager.Save(product);
    }
}
