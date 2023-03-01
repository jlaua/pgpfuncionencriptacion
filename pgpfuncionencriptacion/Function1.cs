using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using DidiSoft.Pgp;
using Microsoft.Azure.KeyVault.Core;

namespace pgpfuncionencriptacion
{
    /// <summary>
    /// Es una function que cada vez que se sube un archivo al storage1 
    /// la data es encriptada en PGP en el storage2 con una extension adicional ".pgp" en el nombre de archivo
    /// </summary>

    public class fxEncriptarMensajeBlobStoragePGP
    {
        [FunctionName("fxEncriptarMensajeBlobStoragePGP")]
        public void Run([BlobTrigger("storage1/{name}", Connection = "AzureWebJobsStorage")]Stream inputBlob,
                          [Blob("storage2/{name}.pgp", FileAccess.Write ,Connection = "AzureWebJobsStorage")] Stream outputBlob,
                            string name,
                            ILogger log)
        {
            PGPLib pgp = new PGPLib();

            bool asciiArmor = true;
            //string publicKey = "an inline public key, in a real world application it shell be obtained from a storage explorer"

            string publicKey = "-----BEGIN PGP PUBLIC KEY BLOCK-----" + Environment.NewLine +
                "" + Environment.NewLine +
                "mDMEY/xkRBYJKwYBBAHaRw8BAQdANzJwUzaAwrFuv3JLHKLelsuwwNSeO4AQ3gar" + Environment.NewLine +
                "/4n+lpS0HEJsYWNrV2luIDxqbGF1YUBob3RtYWlsLmNvbT6ImQQTFgoAQRYhBN+w" + Environment.NewLine +
                "5zMt0dawtTmaWePQ8rNjexLtBQJj/GREAhsDBQkDxDXMBQsJCAcCAiICBhUKCQgL" + Environment.NewLine +
                "AgQWAgMBAh4HAheAAAoJEOPQ8rNjexLtOioBAKG0ESqu7XzEgeiYT2wC9VkzAsfj" + Environment.NewLine +
                "A/hUAr7xgTdgIYmVAQDLhYSZir1NgTNWw7Bb+pYpBrDkmPtQCoyLIv5zusKECbg4" + Environment.NewLine +
                "BGP8ZEQSCisGAQQBl1UBBQEBB0CaetTZUwY95titKKk7toHovuHuqW0unkcVFdN4" + Environment.NewLine +
                "tug4ZwMBCAeIfgQYFgoAJhYhBN+w5zMt0dawtTmaWePQ8rNjexLtBQJj/GREAhsM" + Environment.NewLine +
                "BQkDxDXMAAoJEOPQ8rNjexLtueQBANZriKL6SppoRlYhIc/fZNdorvV3NV6aqDEU" + Environment.NewLine +
                "n5lwygplAP9vjQBPjtEuXLriRdI7/g5woA61TO1bRZe45l44ySnAAg==" + Environment.NewLine +
                "=dmMI" + Environment.NewLine +
                "-----END PGP PUBLIC KEY BLOCK-----" + Environment.NewLine;



            pgp.EncryptStream(inputBlob, name, publicKey, outputBlob, asciiArmor);

        }
    }
}
