
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using System.Data;
using ETicaretAPI.Infrastructure.Operations;
using System.IO;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService 
    {
        readonly IWebHostEnvironment _webHostEnviroment;
      

        public FileService(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<bool> CopyToAsync(string fullPath, IFormFile file)
        {
            try
            {
                 using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,
              1024 * 1024, useAsync: true);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception e)
            {
                //todo Hata Fırlatma Customize

                throw e;
            }
        }

        public async Task<string> fileRenameAsync(string path , string fileName )
        {

       
                return await Task.Run<string>(() =>
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    string extension = Path.GetExtension(fileName);
                    string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                    bool fileIsExists = false;
                    int fileIndex = 0;
                    do
                    {
                        if (File.Exists($"{path}\\{newFileName}"))
                        {
                            fileIsExists = true;
                            fileIndex++;
                            newFileName = $"{NameOperation.CharacterRegulatory(oldName + "-" + fileIndex)}{extension}";
                        }
                        else
                        {
                            fileIsExists = false;
                        }
                    } while (fileIsExists);

                    return newFileName;
                });


            }

            public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
           string uploadPath = Path.Combine(_webHostEnviroment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<bool> results = new();
            List<(string fileName, string path)> datas = new(); 

            foreach (IFormFile file in files)
            {
                string fileNewName =await fileRenameAsync(uploadPath,file.FileName);

                bool result = await CopyToAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName , $"{path}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
                return datas;
            return null;
        }
    }
}

//#region copy
//string newFileName = await Task.Run(async () =>
//{
//    string extension = Path.GetExtension(fileName);
//    string newFileName = string.Empty;

//    if (first)
//    {
//        string oldName = Path.GetFileNameWithoutExtension(fileName);
//        newFileName = $"{Operations.NameOperation.CharacterRegulatory(oldName)}{extension}";
//    }
//    else
//    {
//        newFileName = fileName;
//        int indexNo1 = newFileName.IndexOf('-');
//        if (indexNo1 == -1)
//        {
//            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
//        }
//        else
//        {
//            int lastIndex = 0;

//            while (true)
//            {
//                lastIndex = indexNo1;
//                indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
//                if (indexNo1 == -1)
//                {
//                    indexNo1 = lastIndex;
//                    break;
//                }
//            }

//            int indexNo2 = newFileName.IndexOf('.');
//            string fileNo = newFileName.Substring(indexNo1, indexNo2 - indexNo1 - 1);


//            if (int.TryParse(fileNo, out int _fileNo))
//            {

//                _fileNo++;

//                newFileName = newFileName.Remove(indexNo1, indexNo2 - indexNo1 - 1)
//                .Insert(indexNo1, _fileNo.ToString());
//            }
//            else
//            {
//                newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
//            }
//        }
//    }
//    if (File.Exists($"{path}\\{newFileName}"))
//        return await fileRenameAsync(path, newFileName, false);
//    else
//        return newFileName;
//});

//return newFileName;
//#endregion
