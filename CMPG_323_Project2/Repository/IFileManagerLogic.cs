using CMPG_323_Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Logic
{
   public interface IFileManagerLogic
    {
        public Task Upload(FileModel model,int Id);
        public string read(string filename);
        public Task<byte[]> GetData(string filename);
        public Task Delete(string filename);
    }
}
