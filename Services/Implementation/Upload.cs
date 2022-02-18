using Microsoft.AspNetCore.Http;
using Reciclagem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Reciclagem.Services.Implementation
{
    public class Upload : IUpload
    {
        public void Save(string diretorio, IFormFile file, int id)
        {
            if (file != null)
            {
                if (!Directory.Exists(diretorio)) Directory.CreateDirectory(diretorio);

                using (var stream = new FileStream(Path.Combine(diretorio, id.ToString() + "_" + Path.GetFileName(file.FileName)), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
        }

        public void Delete(string diretorio, string nome)
        {
            if (!String.IsNullOrEmpty(nome))
            {
                if (!Directory.Exists(diretorio)) Directory.CreateDirectory(diretorio);

                File.Delete(diretorio + "\\" + nome);
            }
        }
    }
}
