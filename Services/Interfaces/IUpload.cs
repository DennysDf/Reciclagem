using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Services.Interfaces
{
    public  interface IUpload
    {
        void Save(string directory, IFormFile file, int id);
        void Delete(string directory, string filename);
    }
}
