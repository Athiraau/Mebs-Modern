using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{

    public class DataContext : DbContext

    {
        private IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _password;
        private readonly string _securityKey;
        private readonly IWebHostEnvironment _env;

        public DataContext(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
            _password = _configuration.GetConnectionString("Password");
            _securityKey = _configuration.GetConnectionString("Key");

            if (!_env.IsDevelopment())
            {
                _password = Decrypt(_password);
            }
            _connectionString = _configuration.GetConnectionString("OracleConnection") + _password;

        }

        public IDbConnection CreateConnection()
         => new OracleConnection(_connectionString);

        public string Decrypt(string EncryptedText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(EncryptedText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));

            //De-allocatinng the memory after doing the Job.
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;

            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();

            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //Releasing the Memory Occupied by TripleDES Service Provider for Decryption.          
            objTripleDESCryptoService.Clear();

            //Convert and return the decrypted data/byte into string format.
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
