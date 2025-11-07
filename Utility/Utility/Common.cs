using Amazon;
using Amazon.Runtime.Internal.Transform;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using BE;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VM;

namespace Utility
{
    public class Common
    {
        private readonly IConfiguration iConfiguration;
        private string awsAccessKeyId;
        private string awsSecretAccessKey;

        public Common(IConfiguration IConfiguration)
        {
            iConfiguration = IConfiguration;
            awsAccessKeyId = iConfiguration["S3:awsAccessKeyId"];
            awsSecretAccessKey = iConfiguration["S3:awsSecretAccessKey"];
        }
        public static string ConnectionString()
        {
            string c = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appSettings.json").Build();
            string connectionString = configuration.GetConnectionString("Factura");
            return connectionString;
        }

        public static void LogError(string Login_ID, string errmoduleName, string errprocedureName, string errorObject)
        {
            DataHelper.LogErrorinDB(Login_ID, errmoduleName, errprocedureName, errorObject);
        }

        public static string DateFormat(string Date)
        {
            string res = string.Empty;
            if (!string.IsNullOrEmpty(Date))
            {
                if (Date.IndexOf("/") != -1)
                {
                    string[] dateArr = Date.Split('/');
                    res = dateArr[1] + "/" + dateArr[0] + "/" + dateArr[2];
                }
            }
            else
            {
                res = Date;
            }
            return res;
        }

        public static string DateFormatForDB(string Date)
        {
            string res = string.Empty;
            if (!string.IsNullOrEmpty(Date))
            {
                if (Date.IndexOf("/") != -1)
                {
                    string[] dateArr = Date.Split('/');
                    res = dateArr[2] + "-" + dateArr[1] + "-" + dateArr[0];
                }
            }
            else
            {
                res = Date;
            }
            return res;
        }

        public async Task<bool> UploadFileAsync(DocumentDetailsVM vm, string SavePath)
        {
            try
            {
                Dictionary<string, IFormFile> filesDic = new Dictionary<string, IFormFile>();
                filesDic.Add("Aadhaar.jpg", vm.Aadhaar);
                filesDic.Add("BankPassBook.jpg", vm.BankPassBook);
                filesDic.Add("Singnature.jpg", vm.Singnature);

                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }


                foreach (var formFile in filesDic)
                {
                    long fileSize = formFile.Value.Length;
                    string fileType = formFile.Value.ContentType;
                    if (fileSize > 0)
                    {

                        var filePath = Path.Combine(SavePath, formFile.Key);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UploadFileS3Async(DocumentDetailsVM vm, string BucketName, string SavePath)
        {
            try
            {
                Dictionary<string, IFormFile> filesDic = new Dictionary<string, IFormFile>
                {
                    {"Aadhaar.jpg",vm.Aadhaar},
                    {"BankPassBook.jpg",vm.BankPassBook},
                    {"Singnature.jpg",vm.Singnature},
                };

                using (var s3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.EUNorth1))
                {
                    using (var fileTransferUtility = new TransferUtility(s3Client))
                    {
                        foreach (var file in filesDic)
                        {
                            using (var stream = file.Value.OpenReadStream())
                            {
                                string key = $"{SavePath}/{file.Key}";

                                var request = new TransferUtilityUploadRequest
                                {
                                    InputStream = stream,
                                    Key = key,
                                    BucketName = BucketName,
                                    ContentType = file.Value.ContentType
                                };

                                await fileTransferUtility.UploadAsync(request);
                            }
                        }
                    }
                }
            }
            catch (AmazonS3Exception s3Exception)
            {
                return false;
            }
            catch (Exception Ex)
            {
                return false;
            }
            return true;
        }

        public async Task<FileStatus> DownloadFileS3Async(string BucketName, string SavePath, string fileName)
        {
            FileStatus fileResult = new FileStatus();
            try
            {
                using (var s3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.EUNorth1))
                {
                    string key = $"{SavePath}/{fileName}";
                    var request = new GetObjectRequest
                    {
                        BucketName = BucketName,
                        Key = key
                    };

                    using (var res = await s3Client.GetObjectAsync(request))
                    {
                        var memoryStream = new MemoryStream();

                        await res.ResponseStream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        fileResult.contentType = res.Headers.ContentType ?? "application/octet-stream";
                        fileResult.memoryStream = memoryStream;
                        fileResult.fileName = fileName;
                    }
                }
            }
            catch (AmazonS3Exception s3Exception)
            {
                fileResult.status = false;
            }
            catch (Exception Ex)
            {
                fileResult.status = false;
            }
            return fileResult;
        }
    }
}
