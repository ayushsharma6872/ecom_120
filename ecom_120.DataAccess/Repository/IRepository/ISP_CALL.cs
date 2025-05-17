﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.DataAccess.Repository.IRepository
{
    public interface ISP_CALL:IDisposable
    {
        void Execute(string procedureName,DynamicParameters param=null);
        T Single<T>(String procedureName,DynamicParameters param=null);
        T OneRecord<T>(string procedureName,DynamicParameters param=null);
        IEnumerable<T> List<T>(string procedureName,DynamicParameters param=null);
        Tuple<IEnumerable<T1>,IEnumerable<T2>>List<T1,T2>
            (string procedureName,DynamicParameters param=null);
       
    }
}
